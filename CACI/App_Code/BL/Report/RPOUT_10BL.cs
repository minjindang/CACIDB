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
/// RPOUT_Prt10 的摘要描述
/// </summary>
public class RPOUT_10BL : ICommonBL, IQueryBL
{
    #region IQueryBL 成員

    private string getDefaultSql() {

        string sqlstr = "select a.Aow_Code, b.Com_Imple, c.ApPj_Name, c.ApPj_BgnDate, c.ApPj_EndDate, b.Com_CttName, b.Com_CttTel, b.Com_CttCell, b.Com_Fax, b.Com_CttMail, b.Com_OPAddr, " +
                        "(select y.Sys_CdText from AowStage z inner join SysCode y on z.AwSg_Verify = y.Sys_CdCode " +
                        "where Stage_Index = @Stage_Index_S and z.Aow_Code = a.Aow_Code and y.Sys_CdKind = ('S') and y.Sys_CdType = ('S'))as firstResult, " +
                        "(select y.Sys_CdText from AowStage z inner join SysCode y on z.AwSg_Verify = y.Sys_CdCode " +
                        "where Stage_Index = @Stage_Index_E and z.Aow_Code = a.Aow_Code and y.Sys_CdKind = ('S') and y.Sys_CdType = ('S'))as finalResult, " +
                        "cast(Round(convert(float(10),(a.Aow_Approved/1000)),0)as VARCHAR(10))as finalFee " +
                        "from Allowance a inner join Company b on a.Com_Code = b.Com_Code " +
                        "inner join ApPjContext c on a.Aow_Code = c.Aow_Code " +
                        "inner join Project d on a.Pj_Code = d.Pj_Code " +
                        "where d.Pj_Code = @Pj_Code";
        return sqlstr;
    }
    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        throw new NotImplementedException();
    }
    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(getDefaultSql());
        cmd.Parameters.AddWithValue("@Stage_Index_S", to.getValue("Stage_Index_S").ToString());       //Stage_Index_S
        cmd.Parameters.AddWithValue("@Stage_Index_E", to.getValue("Stage_Index_E").ToString());       //Stage_Index_E
        cmd.Parameters.AddWithValue("@Pj_Code", to.getValue("Pj_Code").ToString());                   //JS00000032
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }
    public DataTable getPrintInfo(DataTO conds, String SelectData)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(getDefaultSql());
        cmd.Parameters.AddWithValue("@Stage_Index_S", conds.getValue("Stage_Index_S").ToString());
        cmd.Parameters.AddWithValue("@Stage_Index_E", conds.getValue("Stage_Index_E").ToString());
        cmd.Parameters.AddWithValue("@Pj_Code", conds.getValue("Pj_Code").ToString());
        if (!SelectData.Equals(""))
        {
            cmd.CommandText += " AND a.Aow_Code in (" + SelectData + ") order by a.Aow_Code";
        }
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
       
        return dt;
    }
    public DataTable getYear()
    {
        DataTable dt = new DataTable();
        string sqlstr = "select distinct YEAR(Cnst_CntDate) - 1911 as Year, YEAR(Cnst_CntDate) as AD " +
                        "from Consulting where Cnst_CntDate is not null order by YEAR(Cnst_CntDate) - 1911 desc";
        SqlCommand cmd = new SqlCommand(sqlstr);
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }
    public DataTable getPj_Name(DataTO to, string SelectData)
    {
        DataTable dt = new DataTable();
        string sqlstr = "select Pj_Name, Pj_Code from Project where year(Pj_StartDate) - 1911 = @Pj_StartDate";
        SqlCommand cmd = new SqlCommand(sqlstr);
        cmd.Parameters.AddWithValue("@Pj_StartDate", SelectData);
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }
    public DataTable getStage_Name(DataTO to, string Pj_Code)
    {
        DataTable dt = new DataTable();
        string sqlstr = "select Stage_Name, Stage_Index from PjStage where Pj_Code = @Pj_Code";
        SqlCommand cmd = new SqlCommand(sqlstr);
        cmd.Parameters.AddWithValue("@Pj_Code", Pj_Code);
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }
    
    #endregion

    void IQueryBL.DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }
}