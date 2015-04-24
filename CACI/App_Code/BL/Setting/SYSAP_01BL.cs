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
public class SYSAP_01BL : ICommonBL, IQueryBL, IMDUIBL
{
    #region IQueryBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        
        string sqlstr = "SELECT DISTINCT Sys_CdKind,Sys_CdType,Sys_CdNote " +
                        "FROM CACIDB..SysCode " +
                        "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            cmd.CommandText += " AND " + to.getAllColumnName()[i] + " Like @" + to.getAllColumnName()[i] + " + '%'";

            cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
        }

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT DISTINCT Sys_CdKind,Sys_CdType,Sys_CdNote " +
                        "FROM CACIDB..SysCode " +
                        "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            cmd.CommandText += " AND " + to.getAllColumnName()[i] + " Like @" + to.getAllColumnName()[i] + " + '%'";

            cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
        }

        cmd.CommandText += " Order By " + sortStr;

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt ;
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
        return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("CACIDB..SysCode", to);
    }

    void IMDUIBL.LoadData(DataTO to, DataTable dt)
    {
        string masterSqlstr = "SELECT Sys_CdCode,Sys_CdText,Sys_CdNote,Sys_CdState,'O' IsNew " +
                              "FROM CACIDB..SysCode " +
                              "WHERE Sys_CdKind=@Sys_CdKind AND Sys_CdType=@Sys_CdType ";

        SqlCommand masCmd = new SqlCommand(masterSqlstr);

        masCmd.Parameters.AddWithValue("@Sys_CdKind", to.getValue("Sys_CdKind").ToString());
        masCmd.Parameters.AddWithValue("@Sys_CdType", to.getValue("Sys_CdType").ToString());

        new SQLAgent(DataBase.CACIDB).select(masCmd, dt);

        if (dt.Rows.Count > 0 && !to.isColumnExist("Sys_CdNote"))
            to.setValue("Sys_CdNote", dt.Rows[0]["Sys_CdNote"].ToString());
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        string delStr = "DELETE FROM SysCode " +
                        "WHERE Sys_CdKind=@Sys_CdKind AND Sys_CdType=@Sys_CdType ";

        SqlCommand delCmd = new SqlCommand(delStr);

        delCmd.Parameters.AddWithValue("@Sys_CdKind", to.getValue("Sys_CdKind"));
        delCmd.Parameters.AddWithValue("@Sys_CdType", to.getValue("Sys_CdType"));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTO dto = new DataTO();

            dto.setValue("Sys_CdKind", to.getValue("Sys_CdKind"));
            dto.setValue("Sys_CdType", to.getValue("Sys_CdType"));
            dto.setValue("Sys_CdCode", dt.Rows[i]["Sys_CdCode"].ToString());
            dto.setValue("Sys_CdText", dt.Rows[i]["Sys_CdText"].ToString());
            dto.setValue("Sys_CdState", dt.Rows[i]["Sys_CdState"].ToString());
            dto.setValue("Sys_CdNote", to.getValue("Sys_CdNote"));
            dto.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
            dto.setValue("Rec_Info", "\\getDate()");

            if (dt.Rows[i]["IsNew"].ToString() == "N")
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("SysCode", dto));
            else
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("SysCode", dto));

            delCmd.CommandText += "AND Sys_CdCode != @Sys_CdCode" + i.ToString() + " ";
            delCmd.Parameters.AddWithValue("@Sys_CdCode" + i.ToString(), dt.Rows[i]["Sys_CdCode"].ToString());
        }

        cmds.Insert(0, delCmd);

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    #endregion
}