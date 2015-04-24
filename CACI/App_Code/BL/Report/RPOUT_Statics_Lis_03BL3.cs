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
public class RPOUT_Statics_Lis_03BL3 : ICommonBL, IQueryBL
{
    
    #region IQueryMarkBL 成員
    private string getDefaultSql()
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();
        string result = "select '100萬以下' as capRangeName, COUNT(distinct b.Com_Code) as capRangeCount,(select COUNT(distinct Company.Com_Code) " +
                        "from Coach inner join Company on Coach.Com_Code = Company.Com_Code where Company.Com_Capital < 1000000 and year(Coach.Coach_Date) <= @a)as accuCapRangeCount " +
                        "from Coach a inner join Company b on a.Com_Code = b.Com_Code where b.Com_Capital < 1000000 and year(a.Coach_Date) = @a " +
                        "union select '100-300萬' as capRangeName, COUNT(distinct b.Com_Code) as capRangeCount,(select COUNT(distinct Company.Com_Code) " +
                        "from Coach inner join Company on Coach.Com_Code = Company.Com_Code where (Company.Com_Capital >= 1000000 or Company.Com_Capital > 3000000) " +
                        "and year(Coach.Coach_Date) <= @a)as accuCapRangeCount from Coach a inner join Company b on a.Com_Code = b.Com_Code " +
                        "where (b.Com_Capital >= 1000000 or b.Com_Capital > 3000000) and year(a.Coach_Date) = @a union " +
                        "select '300-500萬' as capRangeName, COUNT(distinct b.Com_Code) as capRangeCount, (select COUNT(distinct Company.Com_Code) " +
                        "from Coach inner join Company on Coach.Com_Code = Company.Com_Code where (Company.Com_Capital >= 3000000 or Company.Com_Capital > 5000000)  " +
                        "and year(Coach.Coach_Date) <= @a )as accuCapRangeCount from Coach a inner join Company b on a.Com_Code = b.Com_Code " +
                        "where (b.Com_Capital >= 3000000 or b.Com_Capital > 5000000) and year(a.Coach_Date) = @a " +
                        "union select '500-1000萬' as capRangeName, COUNT(distinct b.Com_Code) as capRangeCount,(select COUNT(distinct Company.Com_Code) " +
                        "from Coach inner join Company on Coach.Com_Code = Company.Com_Code where (Company.Com_Capital >= 5000000 or Company.Com_Capital > 10000000) " +
                        "and year(Coach.Coach_Date) <= @a)as accuCapRangeCount from Coach a inner join Company b on a.Com_Code = b.Com_Code " +
                        "where (b.Com_Capital >= 5000000 or b.Com_Capital > 10000000) and year(a.Coach_Date) = @a " +
                        "union select '1000-5000萬' as capRangeName, COUNT(distinct b.Com_Code) as capRangeCount,(select COUNT(distinct Company.Com_Code) " +
                        "from Coach inner join Company on Coach.Com_Code = Company.Com_Code where (Company.Com_Capital >= 10000000 or Company.Com_Capital > 50000000) " +
                        "and year(Coach.Coach_Date) <= @a)as accuCapRangeCount from Coach a inner join Company b on a.Com_Code = b.Com_Code where (b.Com_Capital >= 10000000 or b.Com_Capital > 50000000) and year(a.Coach_Date) = @a " +
                        "union select '5000萬以上' as capRangeName, COUNT(distinct b.Com_Code) as capRangeCount,(select COUNT(distinct Company.Com_Code) " +
                        "from Coach inner join Company on Coach.Com_Code = Company.Com_Code where Company.Com_Capital >= 50000000 and year(Coach.Coach_Date) <= @a)as accuCapRangeCount " +
                        "from Coach a inner join Company b on a.Com_Code = b.Com_Code where b.Com_Capital >= 50000000 and year(a.Coach_Date) = @a ";
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
                    cmd.Parameters.AddWithValue("@a",(Convert.ToInt16(to.getValue(to.getAllColumnName()[i])))+1911);
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