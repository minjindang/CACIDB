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
public class RPOUT_Qry_14BL : ICommonBL, IQueryBL
{
    #region IQueryMarkBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        string sqlstr = "select distinct b.Com_Name, b.Com_Code, b.Com_Tel, b.Com_CttName, " +
                         "b.Com_CttCell , b.Com_CttMail, b.Com_CttName2, b.Com_CttCell2, b.Com_CttMail2, " +
                         "b.Com_OPAddr, c.Pj_Name " +
                         "from Coach a inner join Company b on a.Com_Code = b.Com_Code " +
                         "inner join Project c on a.Pj_Code = c.Pj_Code " +
                         "order by b.Com_Code ";

        DataTable dt = new DataTable();

        SqlCommand cmd = new SqlCommand(sqlstr);

        //SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Company", to);

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

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

 

    #endregion


    void IQueryBL.DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }
}