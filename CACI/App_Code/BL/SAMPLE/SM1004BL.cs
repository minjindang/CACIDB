using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;

/// <summary>
/// SAMPLEBL 的摘要描述
/// </summary>
public class SM1004BL : ICommonBL,IQueryBL,IMQDUIBL
{
    #region IQueryBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();

        SqlCommand cmd = new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("Master", to);

        new SQLAgent(DataBase.TBQGDB).select(cmd, dt);

        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        SqlCommand cmd = new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("Master", to);

        cmd.CommandText += " Order By " + sortStr;

        new SQLAgent(DataBase.TBQGDB).select(cmd, dt);

        return dt;
    }

    void IQueryBL.DeleteData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getDeleteCommand("Master", to));
        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getDeleteCommand("MDDRela", to));

        new SQLAgent(DataBase.TBQGDB).execute(cmds.ToArray());
    }

    #endregion

    #region IMDQueryUIBL 成員

    void IMQDUIBL.InsertData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        string newMcol_1 = ICommonBL.getNewSerialNo(DataBase.TBQGDB, "NE");

        to.setValue("Mcol_1", newMcol_1);

        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("Master", to));

        foreach(DataRow row in dt.Rows)
        {
            DataTO dto = new DataTO();

            dto.setValue("Mcol_1", newMcol_1);

            dto.setValue("DDcol_1", row["DDcol_1"].ToString());

            cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("MDDRela", dto));
        }

        new SQLAgent(DataBase.TBQGDB).execute(cmds.ToArray());
    }

    void IMQDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        // 刪除不在清單內的資料
        string delStr = "DELETE FROM MDDRela " +
                        "WHERE Mcol_1=@Mcol_1 ";

        SqlCommand delCmd = new SqlCommand(delStr);

        delCmd.Parameters.AddWithValue("@Mcol_1", to.getValue("Mcol_1"));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            delCmd.CommandText += "AND DDcol_1 != @DDcol_1" + i.ToString() + " ";
            delCmd.Parameters.AddWithValue("@DDcol_1" + i.ToString(), dt.Rows[i]["DDcol_1"].ToString());
        }

        cmds.Add(delCmd);

        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getUpdateCommand("Master", to));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTO dto = new DataTO();
            
            dto.setValue("Mcol_1", to.getValue("Mcol_1").ToString());

            dto.setValue("DDcol_1", dt.Rows[i]["DDcol_1"].ToString());

            if (!new SQLCommandBuilder(DataBase.TBQGDB).isDataExistByPrimayKey("MDDRela", dto))
            {
                cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("MDDRela", dto));
            }
        }

        new SQLAgent(DataBase.TBQGDB).execute(cmds.ToArray());
    }

    DataTable IMQDUIBL.QueryDetailData(DataTO to)
    {
        SqlCommand cmd = new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("DDetail_1", to);

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.TBQGDB).select(cmd, dt);

        return dt;
    }

    void IMQDUIBL.LoadData(DataTO to, DataTable dt)
    {
        SqlDataReader sr = new SQLAgent(DataBase.TBQGDB).select(new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("Master", to));

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

        sr.Close();

        string sqlstr = "SELECT b.* " +
                        "FROM MDDRela a JOIN DDetail_1 b ON a.DDcol_1=b.DDcol_1 " +
                        "WHERE a.Mcol_1=@Mcol_1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Mcol_1", to.getValue("Mcol_1").ToString());

        new SQLAgent(DataBase.TBQGDB).select(cmd, dt);
    }

    bool IMQDUIBL.IsDataExist(DataTO to)
    {
        if (!to.isColumnExist("Mcol_1"))
            return false;
        else
            return new SQLCommandBuilder(DataBase.TBQGDB).isDataExistByPrimayKey("Master", to);
    }

    #endregion

    
}