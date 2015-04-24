using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;

/// <summary>
/// RPOUT_Statics_02BL 的摘要描述
/// </summary>
public class RPOUT_Statics_02BL : ICommonBL, IQueryBL
{
    #region IQueryBL 成員

    private string getDefaultSql()
    {
        string sqlstr = "SELECT CAST(YEAR(GETDATE()) - 1911 AS nvarchar) + '/' + CAST(MONTH(GETDATE()) AS nvarchar) + '/' + CAST(DAY(GETDATE()) AS nvarchar) AS today, " +
                        "b.Sys_CdText, COUNT(a.Cnst_Code) AS dataNumber, " +
                        "cast(Round(((convert(float(10),count(a.Cnst_Code))/convert(float(10),(select count(Consulting.Cnst_Code) from Consulting where year(Consulting.Cnst_CntDate)= @Cnst_CntDate)))*100),0)as varchar(10))+'%' as sta, " +
                        "cast(Round(((convert(float(10),count(a.Cnst_Code))/convert(float(10),(select count(Consulting.Cnst_Code) from Consulting where year(Consulting.Cnst_CntDate)= @Cnst_CntDate)))*100),0)as float(10)) as staNumber " +
                        "FROM SysCode AS b LEFT OUTER JOIN Consulting AS a ON a.CntClass_Code = b.Sys_CdCode AND YEAR(a.Cnst_CntDate) = @Cnst_CntDate " +
                        "WHERE (b.Sys_CdKind = 'C') AND (b.Sys_CdType = 'Y') AND 1=1";
        return sqlstr;
    }
    private SqlCommand getFilter(string sqlstr, DataTO to) 
    {
        SqlCommand cmd = new SqlCommand(sqlstr);
        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {
                case "Cnst_CntDate_Bgn":
                    cmd.CommandText += " AND dbo.chgToChnDate(a.Cnst_CntDate) >=@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Cnst_CntDate_End":
                    cmd.CommandText += " AND dbo.chgToChnDate(a.Cnst_CntDate) <=@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
            }
        }
        return cmd;
    }
    public DataTable getYear() 
    {
        DataTable dt = new DataTable();
        string sqlstr = "select distinct YEAR(Cnst_CntDate) - 1911 as Year, YEAR(Cnst_CntDate) as AD " +
                        "from Consulting where Cnst_CntDate is not null order by YEAR(Cnst_CntDate)  - 1911  desc";
        SqlCommand cmd = new SqlCommand(sqlstr);
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }
    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        throw new NotImplementedException();
    }
    DataTable IQueryBL.QueryDataForList(DataTO to) 
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = getFilter(getDefaultSql(), to);
        cmd.Parameters.AddWithValue("@Cnst_CntDate", to.getValue("Cnst_CntDate").ToString());
        cmd.CommandText += " GROUP BY a.CntClass_Code,b.Sys_CdText";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }
    public DataTable getPrintInfo(DataTO to, string SelectData)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(getDefaultSql());
        cmd.Parameters.AddWithValue("@Cnst_CntDate", to.getValue("Cnst_CntDate").ToString());
        cmd.CommandText += " GROUP BY a.CntClass_Code,b.Sys_CdText";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }
    void IQueryBL.DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }
    #endregion
}