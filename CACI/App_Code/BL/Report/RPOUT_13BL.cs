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
public class RPOUT_13BL : ICommonBL, IQueryBL
{
    //string uni_id = "Company.Com_Tonum";
    #region IQueryMarkBL 成員
    private string getDefaultSql()
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();
        string result = "select dbo.chgToChnDate(convert(nvarchar(10),getdate(),111)) as getdate, Consulting.Com_Code,dbo.chgToChnDate(Consulting.Cnst_CntDate) as Cnst_CntDate, " +
                         "(select SysCode.Sys_CdText " +
                         "from SysCode " +
                         "where Consulting.Cnst_CntWay = SysCode.Sys_CdCode " +
                         "and SysCode.Sys_CdKind=('C') and SysCode. Sys_CdType=('I')) as caseSource, " +
                         "Company.Com_Name, " +
                         "Company.Com_CttName, Company.Com_CttTel, Company.Com_CttCell, Company.Com_CttMail, " +
                         "(select SysCode.Sys_CdText " +
                         "from SysCode " +
                         "where Consulting.CntClass_Code = SysCode.Sys_CdCode " +
                         "and SysCode.Sys_CdKind=('C') and SysCode.Sys_CdType=('Y')) as qType, " +
                         "dbo.chgToChnDate(Meeting.Meeting_BgnTime) as Meeting_BgnTime, " +
                         "Committee.Comm_Name, " +
                         "Consulting.Cnst_CntText, " +
                         "(select SysCode.Sys_CdText " +
                         "from SysCode " +
                         "where Consulting.Cnst_Status = SysCode.Sys_CdCode " +
                         "and SysCode.Sys_CdKind=('N') and SysCode.Sys_CdType=('S')) as result " +
                         "from Consulting inner join Company on Consulting.Com_Code = Company.Com_Code " +
                         "inner join Meeting on Consulting.Meeting_Code = Meeting.Meeting_Code " +
                         "inner join MtgCrew on Meeting.Meeting_Code = MtgCrew.Meeting_Code " +
                         "inner join Committee on Committee.Comm_Code = MtgCrew.Comm_Code ";
                         
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

                case "Cnst_CntDateS":
                    cmd.CommandText += " AND dbo.chgToChnDate(Consulting.Cnst_CntDate) >= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Cnst_CntDateE":
                    cmd.CommandText += " AND dbo.chgToChnDate(Consulting.Cnst_CntDate) <= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Meeting_BgnTimeS":
                    cmd.CommandText += " AND dbo.chgToChnDate(Meeting.Meeting_BgnTime) >= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Meeting_BgnTimeE":
                    cmd.CommandText += " AND dbo.chgToChnDate(Meeting.Meeting_BgnTime) <= @" + to.getAllColumnName()[i];
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

        cmd.CommandText += " order by Consulting.Cnst_CntDate,Company.Com_Name ";
        
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
       
        cmd.CommandText += " order by Consulting.Cnst_CntDate,Company.Com_Name ";
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