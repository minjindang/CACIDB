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
public class RPOUT_14BL : ICommonBL, IQueryBL
{
    #region IQueryMarkBL 成員

    private string getDefaultSql()
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();
        string result = "select dbo.chgToChnDate(convert(nvarchar(10),getdate(),111)) as getdate, Company.Com_Name, " +
                         "Company.Com_Tel, " +
                         "Company.Com_CttName, " +
                         "Company.Com_CttCell, " +
                         "Company.Com_CttMail, " +
                         "Company.Com_CttName2, " +
                         "Company.Com_CttCell2, " +
                         "Company.Com_CttMail2, " +
                         "Company.Com_OPAddr, " +
                         "Project.Pj_Name " +
                         "from Coach inner join Company on Coach.Com_Code = Company.Com_Code " +
                         "inner join Project on Coach.Pj_Code = Project.Pj_Code ";
                        
                                                                       
        return result;
    }
    private SqlCommand getFilter(string sqlstr, DataTO to)
    {

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {

                case "Com_Tonum":
                    cmd.CommandText += " AND Company.Com_Tonum = @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;

                case "Com_Name":
                    cmd.CommandText += " AND Company.Com_Name = @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;

                case "Pj_Name":
                    cmd.CommandText += " AND Project.Pj_Name = @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;

                case "Coach_DateS":
                    cmd.CommandText += " AND dbo.chgToChnDate(Coach.Coach_Date) >= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Coach_DateE":
                    cmd.CommandText += " AND dbo.chgToChnDate(Coach.Coach_Date) <= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
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

        SqlCommand cmd = getFilter(sqlstr, to);

        cmd.CommandText += " order by Company.Com_Code ";

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }
    DataTable IQueryBL.QueryDataForList(DataTO to, String sortStr)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();

        string sqlstr = getDefaultSql();

        SqlCommand cmd = getFilter(sqlstr, to);

        cmd.CommandText += " order by " + sortStr;
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }


    public DataTable getPrintInfo(DataTO conds, String SelectData)
    {

        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();

        string sqlstr = getDefaultSql();

        SqlCommand cmd = getFilter(sqlstr, conds);

        if (!SelectData.Equals(""))
        {
            cmd.CommandText += " AND Company.Com_Name in (" + SelectData + ")";

        }  

        cmd.CommandText += " order by Company.Com_Code ";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;

    }
    #endregion

    public void DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }

    public DataTable QueryDataForList(DataTO to)
    {
        throw new NotImplementedException();
    }

    void IQueryBL.DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }

}