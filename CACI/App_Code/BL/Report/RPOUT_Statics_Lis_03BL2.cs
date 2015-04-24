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
public class RPOUT_Statics_Lis_03BL2 : ICommonBL, IQueryBL
{
    
    #region IQueryMarkBL 成員
    private string getDefaultSql()
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();
        string result = "select (case when c.Sys_CdText = '商業登記' then '商業登記' when c.Sys_CdText = '有限公司' " +
                        "then '有限公司' when c.Sys_CdText = '股份有限公司' then '股份有限公司' else '其他' " +
                        "end) as name, COUNT(distinct b.Com_Code) as yearCount, " +
                        "(select COUNT(distinct Company.Com_Code)  " +
                        "from Coach inner join Company on Coach.Com_Code = Company.Com_Code " +
                        "right join SysCode on Company.Com_OrgForm = SysCode.Sys_CdCode  " +
                        "and YEAR(Coach.Coach_Date) <= @a " +
                        "where SysCode.Sys_CdKind = ('R') and SysCode.Sys_CdType = ('T') " +
                        "and (case when SysCode.Sys_CdText = '商業登記' then '商業登記' when SysCode.Sys_CdText = '有限公司' " +
                        "then '有限公司' when SysCode.Sys_CdText = '股份有限公司' then '股份有限公司' else '其他' " +
                        "end) = (case when c.Sys_CdText = '商業登記' then '商業登記' when c.Sys_CdText = '有限公司' " +
                        "then '有限公司' when c.Sys_CdText = '股份有限公司' then '股份有限公司' else '其他' end)	 " +
                        "group by (case when SysCode.Sys_CdText = '商業登記' then '商業登記' when SysCode.Sys_CdText = '有限公司' " +
                        "then '有限公司' when SysCode.Sys_CdText = '股份有限公司' then '股份有限公司' else '其他' end) ) as accuCount " +
                        "from Coach a inner join Company b on a.Com_Code = b.Com_Code " +
                        "right join SysCode c on b.Com_OrgForm = c.Sys_CdCode " +
                        "and YEAR(a.Coach_Date) = @a " +
                        "where c.Sys_CdKind = ('R') and c.Sys_CdType = ('T') " +
                        "group by (case when c.Sys_CdText = '商業登記' " +
                        "then '商業登記' when c.Sys_CdText = '有限公司'then '有限公司'when c.Sys_CdText = '股份有限公司' " +
                        "then '股份有限公司'else '其他'end) ";
                                                                                                                            
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
        String sqlstr = getDefaultSql();
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

    public DataTable QueryDataForList(DataTO to, string sortStr)
    {
        throw new NotImplementedException();
    }

    public DataTable QueryDataForList(DataTO to)
    {
        throw new NotImplementedException();
    }

}