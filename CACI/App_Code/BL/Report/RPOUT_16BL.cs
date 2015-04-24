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
public class RPOUT_16BL : ICommonBL, IQueryBL
{
    #region IQueryMarkBL 成員

        private string getDefaultSql()
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();
        string result = "select a.Aow_Code,c.Com_Name,c.Com_BsGender,c.Com_BsIDNO,c.Com_OPAddr, " +
                         "c.Com_Tel,c.Com_BsTel,c.Com_BsCell,c.Com_Fax,c.Com_Email,b.ApPj_Name, " +
                         "(select '■'+Sys_CdText from SysCode  " +
                         "where Sys_CdKind='I' and Sys_CdType='D' and Sys_CdCode =b.ApPj_Msectors)+ " +
                         "(select '■'+Sys_CdText from SysCode  " +
                         "where Sys_CdKind='I' and Sys_CdType='D' and Sys_CdCode =b.ApPj_Ssectors),  " +
                         " b.ApPj_Agenda,b.ApPj_TotAmt,b.ApPj_AowAmt,b.ApPj_OthAmt " +
                         "from Allowance a,ApPjContext b, Company c " +
                         "where a.Aow_Code=b.Aow_Code and a.Com_Code=c.Com_Code  ";
                                                          
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

     

      cmd.CommandText += "";
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