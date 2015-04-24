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
public class RPOUT_Statics_Lis_03BL : ICommonBL, IQueryBL
{
    
    #region IQueryMarkBL 成員
    private string getDefaultSql()
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();
        string result = "select c.Sys_CdText, count(distinct b.Com_Code)AS yearCount, " +
                        "(select COUNT(distinct Company.Com_Code) " +
                        "from Coach inner join Company  on Coach.Com_Code = Company.Com_Code " +
                        "right join SysCode  on Company.Com_MnSectors = SysCode.Sys_CdCode  " +
                        "and year(Coach.Coach_Date) <= @a " +
                        "where SysCode.Sys_CdKind = ('I') and SysCode.Sys_CdType = ('D') " +
                        "and SysCode.Sys_CdCode = c.Sys_CdCode   " +
                        "group by SysCode.Sys_CdText, SysCode.Sys_CdCode " +
                        ")as accuCount , '' as yearCountPer, '' as accuCountPer " +
                        "from Coach a inner join Company b on a.Com_Code = b.Com_Code right join SysCode c on b.Com_MnSectors = c.Sys_CdCode  " +
                        "and year(a.Coach_Date) = @a "+
                        "where c.Sys_CdKind = ('I') and c.Sys_CdType = ('D') " +
                        "group by c.Sys_CdText,c.Sys_CdCode";

        return result;
    }

    private SqlCommand getCondSql(string sqlstr, DataTO to)
    {

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {
                case "Coach_Date":
                    cmd.Parameters.AddWithValue("@a", (Convert.ToInt16(to.getValue(to.getAllColumnName()[i]))) + 1911);
                    break;             
            }
        }
        return cmd;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();
        string sqlstr = getDefaultSql();
        SqlCommand cmd = getCondSql(sqlstr, to);

        cmd.CommandText += "";  

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }


    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();

        string sqlstr = getDefaultSql();

        SqlCommand cmd = getCondSql(sqlstr, to);

        cmd.CommandText += " order by " + sortStr;
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }


    #endregion



    void IQueryBL.DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }


}