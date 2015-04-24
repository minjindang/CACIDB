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
/// RPOUT_Prt11L1 的摘要描述
/// </summary>
public class RPOUT_11BL : ICommonBL, IQueryBL
{
    #region IQueryBL 成員

    private string getDefaultSql() {

        string sqlstr = "select Cast(Year(GETDATE()) - 1911 as nvarchar) + '/' + Cast(Month(GETDATE()) as nvarchar) + '/' + Cast(DAY(GETDATE()) as nvarchar) as today, " +
                        "Cast(Year((Cnst_CntDate)) as nvarchar) + '年' + Cast(Month((Cnst_CntDate)) as nvarchar) + '月' + Cast(DAY((Cnst_CntDate)) as nvarchar) + '日' as Cnst_CntDate, b.Cnst_Code, a.Com_Name, a.Com_Boss, a.Com_Tonum, a.Com_CttName, " +
                        "a.Com_SetupTime, a.Com_Tel, a.Com_EmpNum, a.Com_Fax, a.Com_Email, a.Com_OPAddr, a.Com_Url, a.Com_Capital, a.Com_LTover, " +
                        "(select case a.Com_MnSectors when 'A' then '■' else '□' end) as CMS_A, " +
                        "(select case a.Com_MnSectors when 'B' then '■' else '□' end) as CMS_B, " +
                        "(select case a.Com_MnSectors when 'C' then '■' else '□' end) as CMS_C, " +
                        "(select case a.Com_MnSectors when 'D' then '■' else '□' end) as CMS_D, " +
                        "(select case a.Com_MnSectors when 'E' then '■' else '□' end) as CMS_E, " +
                        "(select case a.Com_MnSectors when 'F' then '■' else '□' end) as CMS_F, " +
                        "(select case a.Com_MnSectors when 'G' then '■' else '□' end) as CMS_G, " +
                        "(select case a.Com_MnSectors when 'H' then '■' else '□' end) as CMS_H, " +
                        "(select case a.Com_MnSectors when 'I' then '■' else '□' end) as CMS_I, " +
                        "(select case a.Com_MnSectors when 'J' then '■' else '□' end) as CMS_J, " +
                        "(select case a.Com_MnSectors when 'K' then '■' else '□' end) as CMS_K, " +
                        "(select case a.Com_MnSectors when 'L' then '■' else '□' end) as CMS_L, " +
                        "(select case a.Com_MnSectors when 'M' then '■' else '□' end) as CMS_M, " +
                        "(select case a.Com_MnSectors when 'N' then '■' else '□' end) as CMS_N, " +
                        "(select case a.Com_MnSectors when 'O' then '■' else '□' end) as CMS_O, " +
                        "(select case a.Com_SbSectors when 'A' then '■' else '□' end) as CSS_A, " +
                        "(select case a.Com_SbSectors when 'B' then '■' else '□' end) as CSS_B, " +
                        "(select case a.Com_SbSectors when 'C' then '■' else '□' end) as CSS_C, " +
                        "(select case a.Com_SbSectors when 'D' then '■' else '□' end) as CSS_D, " +
                        "(select case a.Com_SbSectors when 'E' then '■' else '□' end) as CSS_E, " +
                        "(select case a.Com_SbSectors when 'F' then '■' else '□' end) as CSS_F, " +
                        "(select case a.Com_SbSectors when 'G' then '■' else '□' end) as CSS_G, " +
                        "(select case a.Com_SbSectors when 'H' then '■' else '□' end) as CSS_H, " +
                        "(select case a.Com_SbSectors when 'I' then '■' else '□' end) as CSS_I, " +
                        "(select case a.Com_SbSectors when 'J' then '■' else '□' end) as CSS_J, " +
                        "(select case a.Com_SbSectors when 'K' then '■' else '□' end) as CSS_K, " +
                        "(select case a.Com_SbSectors when 'L' then '■' else '□' end) as CSS_L, " +
                        "(select case a.Com_SbSectors when 'M' then '■' else '□' end) as CSS_M, " +
                        "(select case a.Com_SbSectors when 'N' then '■' else '□' end) as CSS_N, " +
                        "(select case a.Com_SbSectors when 'O' then '■' else '□' end) as CSS_O, " +
                        "(select case a.Com_MnOther when ISNULL(a.Com_MnOther, ' ') then '■' else '□' end) as Com_MnOther, " +
                        "a.Com_MnOther as MnOther,a.Com_MnProduct, a.Com_OPMode, a.Com_OPStatus, " +
                        "(select case b.CntClass_Code when 'AP' then '■' else '□' end) as CCC_AP, " +
                        "(select case b.CntClass_Code when 'CM' then '■' else '□' end) as CCC_CM, " +
                        "(select case b.CntClass_Code when 'CP' then '■' else '□' end) as CCC_CP, " +
                        "(select case b.CntClass_Code when 'CS' then '■' else '□' end) as CCC_CS, " +
                        "(select case b.CntClass_Code when 'DP' then '■' else '□' end) as CCC_DP, " +
                        "(select case b.CntClass_Code when 'IF' then '■' else '□' end) as CCC_IF, " +
                        "(select case b.CntClass_Code when 'IG' then '■' else '□' end) as CCC_IG, " +
                        "(select case b.CntClass_Code when 'IM' then '■' else '□' end) as CCC_IM, " +
                        "(select case b.CntClass_Code when 'MC' then '■' else '□' end) as CCC_MC, " +
                        "(select case b.CntClass_Code when 'MP' then '■' else '□' end) as CCC_MP, " +
                        "(select case b.CntClass_Code when 'OT' then '■' else '□' end) as CCC_OT, " +
                        "(select case b.CntClass_Code when 'PT' then '■' else '□' end) as CCC_PT, " +
                        "(select case b.CntClass_Code when 'SG' then '■' else '□' end) as CCC_SG, " +
                        "b.Cnst_CntText, a.Com_MnSectors, b.CntClass_Code from Company a inner join Consulting b on a.Com_Code = b.Com_Code where 1=1";

        return sqlstr;
    }
    public DataTable getYear()
    {
        DataTable dt = new DataTable();
        string sqlstr = "select distinct YEAR(Cnst_CntDate) - 1911 as Year, YEAR(Cnst_CntDate) as AD " +
                        "from Consulting where Cnst_CntDate is not null order by YEAR(Cnst_CntDate)  - 1911 desc";
        SqlCommand cmd = new SqlCommand(sqlstr);
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }
    private SqlCommand getFilter(string sqlstr, DataTO to)
    {
        SqlCommand cmd = new SqlCommand(sqlstr);
        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {
                case "Cnst_CntDate":
                    cmd.CommandText += " AND Year(b." + to.getAllColumnName()[i] + ") =@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;             
                case "Com_Name":
                    cmd.CommandText += " AND a." + to.getAllColumnName()[i] + " like '%' + @" + to.getAllColumnName()[i] + " + '%'";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Com_MnSectors":
                    cmd.CommandText += " AND a." + to.getAllColumnName()[i] + " =@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "CntClass_Code":
                    cmd.CommandText += " AND b." + to.getAllColumnName()[i] + " =@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
            }
        }
        return cmd;
    }
    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        //DataTable dt = new DataTable();
        //string sqlstr = getDefaultSql();
        //SqlCommand cmd = getFilter(sqlstr, to);
        //cmd.CommandText += " order by " + sortStr;
        //new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        //return dt;
        throw new NotImplementedException();

    }
    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = getFilter(getDefaultSql(), to);
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }
    public DataTable getPrintInfo(DataTO to, String SelectData)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(getDefaultSql());
        if (!SelectData.Equals(""))
        {
            cmd.CommandText += " AND a.Com_Name in (" + SelectData + ")";
        }
        try
        {
            new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
        return dt;
    } 
    
    #endregion

    void IQueryBL.DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }
}