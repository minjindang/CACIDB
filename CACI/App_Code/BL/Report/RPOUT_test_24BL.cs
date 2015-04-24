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
/// RPOUT_test_24BL 的摘要描述
/// </summary>
public class RPOUT_test_24BL : ICommonBL, IQueryBL
{
    #region IQueryBL 成員

    private string getDefaultSql() {

        string sqlstr = "select a.Aow_Code, c.ApPj_Name, b.Com_Name, b.Com_Boss, b.Com_BsTel, b.Com_CttName, " +
                        "b.Com_CttTel, b.Com_Fax, b.Com_CttMail, b.Com_OPAddr, b.Com_Url, " +
                        "c.ApPj_BgnDate, c.ApPj_EndDate, c.ApPj_TotAmt, c.ApPj_AowAmt, " +
                        "c.ApPj_OthAmt, c.ApPj_Agenda, d.Aas_PjName, d.Aas_PjUnit, d.Aas_Amount " +                         
                        "from Allowance a inner join Company b on a.Com_Code = b.Com_Code " +
				        "inner join ApPjContext c on a.Aow_Code = c.Aow_Code " +
                        "inner join ApplyAsis d on a.Aow_Code = d.Aow_Code";   
        return sqlstr;
    }
    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        throw new NotImplementedException();
    }
    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        throw new NotImplementedException();
    }
    public DataTable getPrintInfo(DataTO to)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(getDefaultSql());
        if (to.getValue("Aow_Code") != "")
        {
            //cmd.CommandText += " WHERE d.Aas_Type = 'A' AND a.Aow_Code in (" + to.getValue("Aow_Code") + ")";
            cmd.CommandText += " WHERE d.Aas_Type = 'A' AND a.Aow_Code in " + "('" + to.getValue("Aow_Code") + "')";
            new SQLAgent(DataBase.CACIDB).select(cmd, dt);
            return dt;
        }
        else
        {
            new SQLAgent(DataBase.CACIDB).select(cmd, dt);
            return dt;
        }
       
        
    }
    
    #endregion

    void IQueryBL.DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }
}