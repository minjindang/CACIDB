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
public class RPOUT_Qry_13BL : ICommonBL, IQueryBL
{
    #region IQueryMarkBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        string sqlstr = "select a.Cnst_CntDate, " +
                         "(select SysCode.Sys_CdText  " +
                         "from SysCode " +
                         "where a.Cnst_CntWay = SysCode.Sys_CdCode " +
                         "and SysCode.Sys_CdKind=('C') and SysCode. Sys_CdType=('I')) as caseSource, " +
                         "b.Com_Name,  " +
                         "b.Com_CttName, b.Com_CttTel, b.Com_CttCell, b.Com_CttMail,  " +
                         "(select SysCode.Sys_CdText " +
                         "from SysCode  " +
                         "where a.CntClass_Code = SysCode.Sys_CdCode " +
                         "and SysCode.Sys_CdKind=('C') and SysCode.Sys_CdType=('Y')) as qType, " +
                         "d.Meeting_BgnTime,  " +
                         "f.Comm_Name, "+
                         "a.Cnst_CntText, " +
                         "(select SysCode.Sys_CdText " +
                         "from SysCode " +
                         "where a.Cnst_Status = SysCode.Sys_CdCode " +
                         "and SysCode.Sys_CdKind=('N') and SysCode.Sys_CdType=('S')) as result " +
                         "from Consulting a inner join Company b on a.Com_Code = b.Com_Code  " +
                         "inner join Meeting d on a.Meeting_Code = d.Meeting_Code  " +
                         "inner join MtgCrew e on d.Meeting_Code = e.Meeting_Code  " +
                         "inner join Committee f on f.Comm_Code = e.Comm_Code " +
                         "order by a.Cnst_CntDate, b.Com_Name ";
                                     

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(sqlstr);
       

 
        //SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Company", to);

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    #endregion


    void IQueryBL.DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }

    public string sortStr { get; set; }


    public DataTable QueryDataForList(DataTO to)
    {
        throw new NotImplementedException();
    }


    public DataTable QueryDataForList(DataTO to, string sortStr)
    {
        throw new NotImplementedException();
    }
}