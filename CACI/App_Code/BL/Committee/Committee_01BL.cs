using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;
using System.IO;

/// <summary>
/// Committee_01BL 的摘要描述
/// </summary>
public class Committee_01BL : ICommonBL, IQueryBL, IMDUIBL
{
    #region IQueryUI 成員

    void IQueryBL.DeleteData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("Committee", to));

        DataTO dTo = new DataTO();

        dTo.setValue("Comm_Code", to.getValue("Comm_Code").ToString());

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CommSKill", dTo));

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    System.Data.DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        throw new NotImplementedException();
    }

    System.Data.DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT DISTINCT a.* ,STUFF ( ( " +
                        "SELECT N'、 ' + Skill_Note FROM CACIDB..CommSKill b WHERE b.Comm_Code = a.Comm_Code " +
                        "FOR XML PATH ('') ),1,1,'' ) AS Ski_Name " +
                        "FROM CACIDB..Committee a " +
                        "LEFT JOIN CACIDB..CommSKill b " +
                        "ON a.Comm_Code = b.Comm_Code " +
                        "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            if ("Comm_Name".Equals(to.getAllColumnName()[i]) || "Comm_ComName".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + " like '%" + to.getValue(to.getAllColumnName()[i]) + "%'";
            else
            {
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
                cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }
            System.Diagnostics.Debug.WriteLine("@" + to.getAllColumnName()[i] +":"+ to.getValue(to.getAllColumnName()[i]));
        }
        
        System.Diagnostics.Debug.WriteLine(cmd.CommandText);
        
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        
        return dt;
    }

    #endregion

    #region IMDUIBL 成員

    void IMDUIBL.InsertData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        string newMemberID = getNewSerialNo(DataBase.CACIDB, "CO");

        to.setValue("Comm_Code", newMemberID);

        if (to.isColumnExist("Comm_BkFile"))
        {
            string defaultPath = HttpContext.Current.Request.PhysicalApplicationPath + @"UploadFile\Committee\" + newMemberID;
            
            File.Move(to.getValue("Comm_BkFile").ToString(),Path.Combine(defaultPath,new FileInfo(to.getValue("Comm_BkFile").ToString()).Name));

            to.updateValue("Comm_BkFile", "/CACI/UploadFile/Committee/" + newMemberID + "/" + new FileInfo(to.getValue("Comm_BkFile").ToString()).Name);
        }

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Committee", to));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTO dto = new DataTO();

            dto.setValue("Comm_Code", newMemberID);

            foreach (DataColumn col in dt.Columns)
            {
                if (!"Ski_Name".Equals(col.ColumnName))
                    dto.setValue(col.ColumnName, dt.Rows[i][col.ColumnName].ToString());
            }

            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CommSKill", dto));
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    bool IMDUIBL.IsDataExist(DataTO to)
    {
        if (!to.isColumnExist("Comm_Code"))
            return false;
        else
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Committee", to);
    }

    void IMDUIBL.LoadData(DataTO to, DataTable dt)
    {
        string sqlstr = "SELECT *, (SELECT TOP 1 Bank_Name FROM CACIDB..Bank WHERE Bank_Num=Comm_Bank_Num) Bank_Name " +
                        "FROM CACIDB..Committee " +
                        "WHERE Comm_Code=@Comm_Code";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Comm_Code", to.getValue("Comm_Code"));

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(cmd);
        
        if (sr.Read())
        {
            for (int i = 0; i < sr.FieldCount; i++)
            {
                if (!to.isColumnExist(sr.GetName(i)))
                {
                    to.setValue(sr.GetName(i), sr[sr.GetName(i)].ToString());
                }
            }
        }

        string dsqlstr = "SELECT * " +
                        "FROM CACIDB..CommSKill a LEFT JOIN CACIDB..SkillSample b " +
                        "ON a.Ski_Num = b.Ski_Num " +
                        "WHERE 1=1 AND Comm_Code = @Comm_Code";
        
        cmd = new SqlCommand(dsqlstr);
        
        cmd.Parameters.AddWithValue("@Comm_Code", to.getValue("Comm_Code"));
        
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        // 刪除不在清單內的資料
        string delStr = "DELETE FROM CommSKill " +
                        "WHERE Comm_Code=@Comm_Code ";

        SqlCommand delCmd = new SqlCommand(delStr);

        delCmd.Parameters.AddWithValue("@Comm_Code", to.getValue("Comm_Code"));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            delCmd.CommandText += "AND Ski_Num != @Ski_Num" + i.ToString() + " ";
            delCmd.Parameters.AddWithValue("@Ski_Num" + i.ToString(), dt.Rows[i]["Ski_Num"].ToString());
            System.Diagnostics.Debug.WriteLine("i:"+i);
            System.Diagnostics.Debug.WriteLine("Ski_Num:" + dt.Rows[i]["Ski_Num"].ToString());
        }

        cmds.Add(delCmd);

        if (to.isColumnExist("Comm_BkFile"))
        {
            string defaultPath = HttpContext.Current.Request.PhysicalApplicationPath + @"UploadFile\Committee\" + to.getValue("Com_Code").ToString();

            if (Directory.Exists(defaultPath))
            {
                foreach (string file in Directory.GetFiles(defaultPath))
                {
                    if (file.StartsWith("Bk_"))
                        File.Delete(Path.Combine(defaultPath, file));
                }
            }

            File.Move(to.getValue("Comm_BkFile").ToString(), Path.Combine(defaultPath, new FileInfo(to.getValue("Comm_BkFile").ToString()).Name));

            to.updateValue("Comm_BkFile", "/CACI/UploadFile/Committee/" + to.getValue("Com_Code").ToString() + "/" + new FileInfo(to.getValue("Comm_BkFile").ToString()).Name);
        }

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Committee", to));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTO ddto = new DataTO();

            ddto.setValue("Comm_Code", to.getValue("Comm_Code").ToString());

            for (int t = 0; t < dt.Columns.Count; t++)
            {
                if (!"Ski_Name".Equals(dt.Columns[t].ColumnName))
                {
                    System.Diagnostics.Debug.WriteLine(dt.Columns[t].ColumnName + ":" + dt.Rows[i][dt.Columns[t].ColumnName].ToString());
                    ddto.setValue(dt.Columns[t].ColumnName, dt.Rows[i][dt.Columns[t].ColumnName].ToString());
                }
            }

            if (new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("CommSKill", ddto))
            {
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CommSKill", ddto));
            }
            else
            {
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CommSKill", ddto));
            }
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    #endregion

    #region 自訂程序

    public bool checkProject(DataTO to)
    {
        bool result = false;
        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("PjJudge", to);
        //cmd.Parameters.AddWithValue("@Comm_Code", to.getValue("Comm_Code"));
        SqlDataReader dr = new SQLAgent(DataBase.CACIDB).select(cmd);
        if (dr.Read())
            result = true;
        return result;
    }

    public bool checkMeeting(DataTO to)
    {
        bool result = false;
        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("MtgCrew", to);
        //cmd.Parameters.AddWithValue("@Comm_Code", to.getValue("Comm_Code"));
        SqlDataReader dr = new SQLAgent(DataBase.CACIDB).select(cmd);
        if (dr.Read())
            result = true;
        return result;
    }

    public string getCommitteeSkill(string Comm_Code)
    {
        string sqlStr = "SELECT Ski_Name FROM CACIDB.dbo.CommSKill a " +
                     "LEFT JOIN CACIDB.dbo.SkillSample b " +
                     "ON a.Ski_Num = b.Ski_Num " +
                     "WHERE Comm_Code = @Comm_Code ";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@Comm_Code", Comm_Code);
        DataTable dtSkill = new DataTable();
        new SQLAgent(DataBase.CACIDB).select(cmd, dtSkill);
        string result = string.Empty;
        foreach (DataRow item in dtSkill.Rows)
        {
            result += item["Ski_Name"] + "、";
        }
        if (!string.IsNullOrEmpty(result))
            result = result.Substring(0, result.Length - 1);
        else
            result = "N/A";
        return result;
    }

    #endregion
}