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
public class CoachKind_01BL : ICommonBL, IQueryBL, IMDUIBL
{
    #region IQueryBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT DISTINCT ChKd_Code,ChKd_Order,ChKd_Name,ChKd_Note " +
                        "FROM CACIDB..CoachKind " +
                        "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);


        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT DISTINCT ChKd_Code,ChKd_Order,(SELECT Sys_CdText FROM SysCode WHERE Sys_CdKind+Sys_CdType = a.ChKd_Order ) as ChKd_OrderName,ChKd_Name,ChKd_Note " +
                        "FROM CACIDB..CoachKind a" +
                        "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.CommandText += " Order By " + sortStr;

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

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
        string masterSqlstr = "SELECT ChKd_Code,ChKd_Order,(SELECT Sys_CdText FROM SysCode WHERE Sys_CdCode = a.ChKd_Order AND Sys_CdKind+Sys_CdType = 'CK' ) as ChKd_OrderName,ChKd_Name,ChKd_Note,'N' IsNew " +
                              "FROM CACIDB..CoachKind a ";
                             

        SqlCommand masCmd = new SqlCommand(masterSqlstr);

        new SQLAgent(DataBase.CACIDB).select(masCmd, dt);
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        string delStr = "DELETE FROM CACIDB..CoachKind WHERE 1=1 ";

        SqlCommand delCmd = new SqlCommand(delStr);

        cmds.Add(delCmd);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTO dto = new DataTO();

            dto.setValue("ChKd_Code", dt.Rows[i]["ChKd_Code"].ToString());
            dto.setValue("ChKd_Order", dt.Rows[i]["ChKd_Order"].ToString());
            dto.setValue("ChKd_Name", dt.Rows[i]["ChKd_Name"].ToString());
            dto.setValue("ChKd_Note", dt.Rows[i]["ChKd_Note"].ToString());

            dto.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
            dto.setValue("Rec_Info", "\\getDate()");

            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CoachKind", dto));
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    #endregion

    #region 自訂功能

    
    #endregion
}