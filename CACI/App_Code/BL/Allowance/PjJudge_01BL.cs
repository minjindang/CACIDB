using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Project_01BL 的摘要描述
/// </summary>
public class PjJudge_01BL : ICommonBL, IQueryBL, IMQDUIBL
{
    #region IQueryBL 成員

    void IQueryBL.DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT a.Pj_Code,a.Pj_Name,dbo.chgToChnChtDate(a.Pj_StartDate) Pj_StartDate,count(b.Comm_Code) Pj_Judge_Count " +
                        "FROM CACIDB..Project a LEFT JOIN CACIDB..PjJudge b ON a.Pj_Code=b.Pj_Code " +
                        "WHERE a.Pj_Kind='A' AND a.Pj_StartDate > geDate() ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            if ("Pj_Name".Equals(to.getAllColumnName()[i]))
            {
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + " like '%' + @" + to.getAllColumnName()[i] + " + '%'";
                cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }
        }
        cmd.CommandText += " GROUP By a.Pj_Code,a.Pj_Name,a.Pj_StartDate ";
        cmd.CommandText += " Order By " + sortStr;
        
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        
        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT a.Pj_Code,a.Pj_Name,dbo.chgToChnChtDate(a.Pj_StartDate) Pj_StartDate,count(b.Comm_Code) Pj_Judge_Count " +
                        "FROM CACIDB..Project a LEFT JOIN CACIDB..PjJudge b ON a.Pj_Code=b.Pj_Code " +
                        "WHERE a.Pj_Kind='A' AND a.Pj_StartDate > getDate() ";
                       

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            if ("Pj_Name".Equals(to.getAllColumnName()[i]))
            {
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + " like '%' + @" + to.getAllColumnName()[i] + " + '%'";
                cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }
        }
        cmd.CommandText += " GROUP By a.Pj_Code,a.Pj_Name,a.Pj_StartDate ";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    #endregion

    #region IMQDUIBL 成員

    void IMQDUIBL.InsertData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        foreach (DataRow row in dt.Rows)
        {
            DataTO dto = new DataTO();

            for (int i = 0; i < to.getAllColumnName().Count(); i++)
            {
                dto.setValue(to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }

            dto.setValue("Comm_Code", row["Comm_Code"].ToString());
            dto.setValue("Pj_Identity", row["Pj_Identity"].ToString());
            dto.setValue("PjJg_Flag", "Y");
            dto.setValue("CmGp_Code", row["CmGp_Code"].ToString());

            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..PjJudge", dto));
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    bool IMQDUIBL.IsDataExist(DataTO to)
    {
        if (to.isColumnExist("Pj_Code"))
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("PjJudge", to);
        else
            return false;
    }

    void IMQDUIBL.LoadData(DataTO to, DataTable dt)
    {
        string sqlstr = "SELECT Pj_Code,Pj_Name,Pj_StartDate,Pj_User_Code,Pj_Fund,Pj_PjIntro,Pj_PjNote " +
                        "FROM CACIDB..Project " +
                        "WHERE Pj_Code=@Pj_Code";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Pj_Code", to.getValue("Pj_Code"));

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

        sr.Close();

        string pjJudge_Sqlstr = "SELECT DISTINCT a.Comm_Code, a.Comm_Name, a.Comm_ComName,d.CmGp_Code,dbo.getSysCodeText('P','I',d.Pj_Identity) Pj_IdentityText,d.Pj_Identity," +
                                "e.CmGp_Name CmGp_Name," +
                                "ISNULL(STUFF((SELECT N'、' + c.Ski_Name " +
                                             "FROM CommSKill AS b JOIN SkillSample As c ON b.Ski_Num=c.Ski_Num " +
                                             "WHERE b.Comm_Code=a.Comm_Code FOR XML PATH ('')  ),1,1,''),'') CommSkill " +
                                "FROM CACIDB..PjJudge d JOIN CACIDB..Committee a ON a.Comm_Code=d.Comm_Code " +
                                                       "FULL JOIN CACIDB..CommGroup e ON d.CmGp_Code=e.CmGp_Num AND d.Pj_Code=e.Pj_Code " +
                                "WHERE d.Pj_Code=@Pj_Code AND d.PjJg_Flag='Y' ";

        SqlCommand judgeCmd = new SqlCommand(pjJudge_Sqlstr);

        judgeCmd.Parameters.AddWithValue("@Pj_Code", to.getValue("Pj_Code"));

        new SQLAgent(DataBase.CACIDB).select(judgeCmd, dt);

    }

    DataTable IMQDUIBL.QueryDetailData(DataTO to)
    {
        DataTable dt = new DataTable();
        
        string sqlstr = "SELECT DISTINCT a.Comm_Code, a.Comm_Name, a.Comm_ComName," +
                               "ISNULL(STUFF((SELECT N'、' + c.Ski_Name " +
                                             "FROM CommSKill AS b JOIN SkillSample As c ON b.Ski_Num=c.Ski_Num " +
                                             "WHERE b.Comm_Code=a.Comm_Code FOR XML PATH ('')  ),1,1,''),'') CommSkill " +
                        "FROM CACIDB..Committee AS a " +
                        "WHERE 1=1 AND Comm_CoachWay='100' ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Count(); i++)
        {
            if (to.getAllColumnName()[i] == "Ski_Num")
            {
                cmd.CommandText += " GROUP BY a.Comm_Code, a.Comm_Name, a.Comm_ComName";
                cmd.CommandText += " Having charIndex(ISNULL(STUFF((select N'、' + c.Ski_Num FROM CommSKill AS b JOIN SkillSample As c ON b.Ski_Num=c.Ski_Num WHERE b.Comm_Code=a.Comm_Code FOR XML PATH ('')  ),1,1,''),''),'" + to.getValue(to.getAllColumnName()[i]) + "')>0 ";
            }
            else
            {
                cmd.CommandText += " AND a." + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
                cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }
        }

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    void IMQDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();
        
        string delsqlstr = "DELETE FROM CACIDB..PjJudge " +
                           "WHERE Pj_Code=@Pj_Code ";

        SqlCommand delCmd = new SqlCommand(delsqlstr);

        delCmd.Parameters.AddWithValue("@Pj_Code", to.getValue("Pj_Code").ToString());

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i == 0)
            {
                delCmd.CommandText += " AND ( Comm_Code=@CommCode" + i.ToString();
            }
            else if (i == dt.Rows.Count - 1)
            {
                delCmd.CommandText += " OR Comm_Code=@CommCode" + i.ToString() + ")";
            }
            else
            {
                delCmd.CommandText += " OR Comm_Code=@CommCode" + i.ToString();
            }

            delCmd.Parameters.AddWithValue("@CommCode" + i.ToString(), dt.Rows[i]["Comm_Code"].ToString());
        }

        cmds.Add(delCmd);

        string usqlstr = "UPDATE CACIDB..PjJudge " +
                         "SET PjJg_Flag='N' " +
                         "WHERE Pj_Code=@Pj_Code ";

        SqlCommand uCmd = new SqlCommand(usqlstr);

        uCmd.Parameters.AddWithValue("@Pj_Code", to.getValue("Pj_Code").ToString());

        cmds.Add(uCmd);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTO dto = new DataTO();

            for (int t = 0; t < to.getAllColumnName().Count(); t++)
            {
                dto.setValue(to.getAllColumnName()[t], to.getValue(to.getAllColumnName()[t]));
            }

            dto.setValue("Comm_Code", dt.Rows[i]["Comm_Code"].ToString());
            dto.setValue("Pj_Identity", dt.Rows[i]["Pj_Identity"].ToString());
            dto.setValue("PjJg_Flag", "Y");
            dto.setValue("CmGp_Code", dt.Rows[i]["CmGp_Code"].ToString());

            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..PjJudge", dto));
        }

        foreach (SqlCommand ccmd in cmds)
        {
            System.Diagnostics.Debug.WriteLine("CommandText=" + ccmd.CommandText);

            string param = "";

            foreach (SqlParameter pa in ccmd.Parameters)
            {
                param += pa.ParameterName + "=" + pa.Value.ToString() + ";";
            }

            System.Diagnostics.Debug.WriteLine("Parameter=" + param);
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    #endregion
}