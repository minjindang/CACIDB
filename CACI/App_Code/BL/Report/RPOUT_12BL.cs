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
public class RPOUT_12BL : ICommonBL, IQueryBL
{
    #region IQueryMarkBL 成員

    private string getDefaultSql()
    {
        string sqlstr = "Select CONVERT(varchar(12), a.Cnst_CntDate, 111 ) as Cnst_CntDate, " +																										
                        "(select SysCode.Sys_CdText from SysCode where SysCode.Sys_CdKind=('C') and SysCode.Sys_CdType=('I') " +
	                    "and Sys_CdCode = a.Cnst_CntWay)as CntWay, b.Com_Name, b.Com_CttName, b.Com_CttTel,	b.Com_CttMail, a.Cnst_CntText, " +																									
	                    "(select SysCode.Sys_CdText from SysCode where SysCode.Sys_CdKind=('C') and SysCode.Sys_CdType=('Y') " +
		                "and Sys_CdCode = a.CntClass_Code)as qType,	" +
	                    "(select top 1 SysCode.Sys_CdText from SysCode inner join CntReply on SysCode.Sys_CdCode = CntReply.CtRepl_RpWay " +
		                "where SysCode.Sys_CdKind=('C') and SysCode.Sys_CdType=('R') and CntReply.Cnst_Code = a.Cnst_Code " +
		                "order by CntReply.CtRepl_Date desc)as proWsy, " +
                        "(select SysCode.Sys_CdText from SysCode where SysCode.Sys_CdKind=('N') and SysCode.Sys_CdType=('S') " +
	 	                "and Sys_CdCode = a.Cnst_Status)as CntWay, " +																		
                        "(select top 1 User_Name from CntReply inner join UserAcc on CntReply.Rec_InfoID = UserAcc.User_Code " +
                        "and CntReply.Cnst_Code = a.Cnst_Code order by CntReply.CtRepl_Date desc) as CntUser " +									
	                    "from Consulting a inner join Company b on a.Com_Code = b.Com_Code where 1=1";
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
                case "Com_Name":
                    cmd.CommandText += " AND b." + to.getAllColumnName()[i] + " like '%' + @" + to.getAllColumnName()[i] + " + '%'";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "CntClass_Code":
                    cmd.CommandText += " AND a." + to.getAllColumnName()[i] + " =@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
            }
        }
        return cmd;
    }
   
    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = getFilter(getDefaultSql(), to);
        cmd.CommandText += " order by a.Cnst_CntDate desc";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    public DataTable getPrintInfo(DataTO conds, String SelectData) 
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = getFilter(getDefaultSql(), conds);
        if (!SelectData.Equals(""))
            {
                cmd.CommandText += " AND CONVERT(char(10), a.Cnst_CntDate, 111) in (" + SelectData + ")";
            }
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    #endregion

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        throw new NotImplementedException();
    }

    void IQueryBL.DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }
}