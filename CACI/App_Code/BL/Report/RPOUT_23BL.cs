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
public class RPOUT_23BL : ICommonBL, IQueryBL
{
    #region IQueryMarkBL 成員

        private string getDefaultSql()
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();
        //string result =  "select ApPj_Name, Com_Name, Com_Tel, Com_Fax, Com_OPAddr,ApPj_BgnDate, "+
        //                 "       ApPj_EndDate,ApPj_TotAmt, ApPj_AowAmt, ApPj_Agenda, Com_CttName,"+
        //                 "       Com_CttTitle,  Com_CttTel,  Com_CttMail "+
        //                 " from Allowance a inner join Company b on a.Com_Code = b.Com_Code "+
        //                 " inner join ApPjContext d on a.Aow_Code = d.Aow_Code ";                 
        string result = "select ApPj_Name, Com_Name, Com_Tel, Com_Fax, Com_OPAddr, Convert(varchar(4),YEAR(Aow_Date)-1911) as Aow_cDate," +
                         " '民國'+Convert(varchar(4),YEAR(ApPj_BgnDate)-1911)+'年'+Convert(varchar(2),MONTH(ApPj_BgnDate))+'月'+Convert(varchar(2),DAY(ApPj_BgnDate))+'日  至'+Convert(varchar(4),YEAR(ApPj_EndDate)-1911)+'年'+Convert(varchar(2),MONTH(ApPj_EndDate))+'月'+Convert(varchar(2),DAY(ApPj_EndDate))+'日' as ApPj_Date," +
                         "ApPj_BgnDate, ApPj_EndDate,ApPj_TotAmt, ApPj_AowAmt, Com_CttName," +
                         "       Com_CttTitle,  Com_CttTel,  Com_CttMail,ApPj_Goal,"+
                         "       ApPj_Policies, ApPj_Profit,ApPj_Solution " +
                         " from Allowance a inner join Company b on a.Com_Code = b.Com_Code " +
                         " inner join ApPjContext d on a.Aow_Code = d.Aow_Code ";                            
                         
        return result;
    }
    private SqlCommand getFilter(string sqlstr, DataTO to)
    {

        SqlCommand cmd = new SqlCommand(sqlstr);
        
        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {

                case "Aow_Code":
                    cmd.CommandText += " AND a.Aow_Code = @" + to.getAllColumnName()[i];
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

        cmd.CommandText += " ";
        
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
          cmd.CommandText += " AND Company.Com_Code in (" + SelectData + ")";
          
        }

      cmd.CommandText += " order by Com_Name";

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    
      }
    public DataTable getPrintInfo_Sub01(DataTO to)
    {
        DataTable dt = new DataTable();
        string sqlstr = "select a.Aow_Code,b.Aas_Year, b.Aas_PjName,  b.Aas_PjUnit, b.Aas_Amount ,year(getdate()) from Allowance a " +
                        "inner join ApplyAsis b on a.Aow_Code = b.Aow_Code " +
                        "where b.Aas_Type = 'A' " +
                        "and a.Aow_Code = @Aow_Code ";
        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Aow_Code", to.getValue("Aow_Code"));
        //new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        //string sqlstr = "select a.Aow_Code,b.Aas_Year, b.Aas_PjName,  b.Aas_PjUnit, b.Aas_Amount ,year(getdate()) from Allowance a " +
        //                "inner join ApplyAsis b on a.Aow_Code = b.Aow_Code " +
        //                "where b.Aas_Type = 'A' ";
        //SqlCommand cmd = new SqlCommand(sqlstr);
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