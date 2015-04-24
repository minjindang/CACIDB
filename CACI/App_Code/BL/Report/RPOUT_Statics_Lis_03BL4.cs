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
public class RPOUT_Statics_Lis_03BL4 : ICommonBL, IQueryBL
{
    
    #region IQueryMarkBL 成員
    private string getDefaultSql()
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();
        string result = "select c.Sys_CdText, COUNT(a.Coach_Code) as yearCount,(select COUNT(Coach.Coach_Code)from Coach inner join CoachKind on Coach.ChKd_Code = CoachKind.ChKd_Code " +
                        "right join SysCode on CoachKind.ChKd_Order = SysCode.Sys_CdCode and year(Coach.Coach_Date) <= @a where SysCode.Sys_CdKind = ('C') and SysCode.Sys_CdType = ('K') " +
                        "and SysCode.Sys_CdCode = c.Sys_CdCode group by SysCode.Sys_CdText )as accuCount from Coach a inner join CoachKind b on a.ChKd_Code = b.ChKd_Code " +
                        "right join SysCode c on b.ChKd_Order = c.Sys_CdCode and year(a.Coach_Date) = @a where c.Sys_CdKind = ('C') and c.Sys_CdType = ('K') group by c.Sys_CdText,c.Sys_CdCode";
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
                    //cmd.CommandText += " AND Year(a." + to.getAllColumnName()[i] + ")-1911 =@" + to.getAllColumnName()[i];
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