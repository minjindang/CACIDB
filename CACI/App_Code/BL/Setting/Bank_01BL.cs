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
public class Bank_01BL : ICommonBL, IQueryBL, IMDUIBL
{
    #region IQueryBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT DISTINCT Bank_Name,Bank_Num " +
                        "FROM CACIDB..Bank " +
                        "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);


        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT DISTINCT Bank_Name,Bank_Num " +
                        "FROM CACIDB..Bank " +
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
        string masterSqlstr = "SELECT Bank_Name,Bank_Num " +
                              "FROM CACIDB..Bank ";

        SqlCommand masCmd = new SqlCommand(masterSqlstr);

        new SQLAgent(DataBase.CACIDB).select(masCmd, dt);
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        string delStr = "DELETE FROM Bank WHERE 1=1 " ;

        SqlCommand delCmd = new SqlCommand(delStr);

        cmds.Add(delCmd);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTO dto = new DataTO();

            dto.setValue("Bank_Name", dt.Rows[i]["Bank_Name"].ToString());
            dto.setValue("Bank_Num", dt.Rows[i]["Bank_Num"].ToString());
            dto.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
            dto.setValue("Rec_Info", "\\getDate()");

            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Bank", dto));
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    #endregion

    #region 自訂功能

    public DataTable getBankData()
    {
        string sqlstr = "SELECT Bank_Name,Bank_Num,'N' as IsNew FROM CACIDB..Bank  ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    #endregion
}