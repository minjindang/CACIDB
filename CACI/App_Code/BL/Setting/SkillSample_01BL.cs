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
public class SkillSample_01BL : ICommonBL, IQueryBL, IMDUIBL
{
    #region IQueryBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();
        return dt;
    }

    void IQueryBL.DeleteData(DataTO to)
    {
    }

    #endregion


    #region IMDUIBL 成員

    void IMDUIBL.InsertData(DataTO to, DataTable dt)
    {
        throw new NotImplementedException();
    }

    bool IMDUIBL.IsDataExist(DataTO to)
    {
        return true;
    }

    void IMDUIBL.LoadData(DataTO to, DataTable dt)
    {
        string masterSqlstr = "SELECT Ski_Num,Ski_Kind,Sys_CdText,Ski_Name FROM CACIDB..SkillSample AS a " +
                              "LEFT JOIn dbo.SysCode AS b ON a.Ski_Kind = b.Sys_CdCode   " +
                              "WHERE b.Sys_CdKind = 'I' and b.Sys_CdType = 'D' ";
        SqlCommand masCmd = new SqlCommand(masterSqlstr);
        new SQLAgent(DataBase.CACIDB).select(masCmd, dt);
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();
        string delStr = "DELETE FROM SkillSample WHERE 1=1 ";
        SqlCommand delCmd = new SqlCommand(delStr);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTO dto = new DataTO();
            dto.setValue("Rec_InfoID",to.getValue("Rec_InfoID"));
            dto.setValue("Rec_Info", "\\getDate()");
            dto.setValue("Ski_Kind", dt.Rows[i]["Ski_Kind"].ToString());
            dto.setValue("Ski_Name", dt.Rows[i]["Ski_Name"].ToString());
            dto.setValue("Ski_Num", dt.Rows[i]["Ski_Num"].ToString());
            if (new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("SkillSample", dto))
            {
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("SkillSample", dto));
            }
            else
            {
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("SkillSample", dto));
            }
            delCmd.CommandText += "AND Ski_Num != @Ski_Num" + i.ToString() + " ";
            delCmd.Parameters.AddWithValue("@Ski_Num" + i.ToString(), dt.Rows[i]["Ski_Num"].ToString());
        }
        cmds.Insert(0, delCmd);
        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    #endregion

    #region 自訂功能

    #endregion
}