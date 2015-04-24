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
/// RPOUT_05BL 的摘要描述
/// </summary>
public class RPOUT_01BL : ICommonBL, IQueryBL
{

    void IQueryBL.DeleteData(DataTO to)
    {

    }

    private string getDefaultSql(DataTO to)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();
        string result = "select distinct "
                          + " RANK() OVER(ORDER BY Allowance.Aow_Code,Com_Name,Com_Imple,ApPj_Name,ApPj_BgnDate,ApPj_EndDate,Com_Boss,Com_Boss,Com_CttTel,ApPj_ProdCls,ApPj_ApGroup,ApPj_Msectors,Com_Fax,Com_Email,Com_Capital,Com_OPAddr,ApPj_ProdCls,ApPj_TotAmt,ApPj_AowAmt) as Inde "
                          + ",Company.Com_Code "
                          + ",Allowance.Aow_Code "
                          + ",Company.Com_Name "
                          + ",Company.Com_Imple "
                          + ",ApPjContext.ApPj_Name "
                          + ",(Select Sys_CdText from SysCode where Sys_CdKind='I' and Sys_CdType='D' and Sys_CdCode = ApPjContext.ApPj_Msectors) as ApPj_Msectors "
                          + ",ApPjContext.ApPj_BgnDate "
                          + ",ApPjContext.ApPj_EndDate "
                          + ",dbo.chgToChnDate(ApPjContext.ApPj_BgnDate)+'~'+dbo.chgToChnDate(ApPjContext.ApPj_EndDate) as ApPj_BEgnDate "
                          + ",Allowance.Aow_RegNum "
                          + ",Company.Com_Boss "
                          + ",Company.Com_CttName "
                          + ",Company.Com_CttTel "
                          + ",Company.Com_CttCell "
                          + ",(select Sys_CdText from SysCode where Sys_CdKind='A' and Sys_CdType='P' and Sys_CdCode =ApPjContext.ApPj_ProdCls) as ApPj_ProdCls "
                          + ",(select Sys_CdText from SysCode where Sys_CdKind='P' and Sys_CdType='G' and Sys_CdCode =ApPjContext.ApPj_ApGroup) as ApPj_ApGroup "
                          + ",(select Sys_CdText from SysCode where Sys_CdKind='I' and Sys_CdType='D' and Sys_CdCode =ApPjContext.ApPj_Msectors) as ApPj_MsectorsApPj_Msectors "
                          + ",Company.Com_Fax "
                          + ",Company.Com_Email "
                          + ",Company.Com_PostCode "
                          + ",round(cast(Company.Com_Capital as float)/1000,2) as Com_Capital "
                          + ",Company.Com_OPAddr "
                          + ",dbo.GetAllPjName(Allowance.Aow_Code) as Aas_PjName "
                          + ",convert(decimal(10,2),(Select count(Aas_Amount) from ApplyAsis where Aow_Code=Allowance.Aow_Code)) as Aas_Amount "
                          + ",case when ApPjContext.ApPj_ProdCls='E' then Company.Com_MnProduct else '' end as MnProduct "
                          + ",case when ApPjContext.ApPj_ProdCls='N' then Company.Com_MnProduct else '' end as SubProduct "
                          + ",convert(decimal(10,2),ApPjContext.ApPj_TotAmt/10000) as ApPj_TotAmt "
                          + ",convert(decimal(10,2),ApPjContext.ApPj_AowAmt/10000) as ApPj_AowAmt "
                          + ",convert(decimal(10,2),ApPjContext.ApPj_FundAmt/10000)as ApPj_FundAmt "
                          + ",cast((round((cast(ApPjContext.ApPj_AowAmt as float)/cast(ApPjContext.ApPj_TotAmt as float)),2)*100)as varchar(10))+'%' as Aow_Tot "
                          + ",(Select case when AwSg_Verify='Y' then '通過' "
                                       + " when AwSg_Verify='N' then '不通過' "
                                       + " else '' end "
                            + " from AowStage where Pj_Code=Project.Pj_Code "
                            + " and Stage_Index=PjStage.Stage_Index "
                            + " and Aow_Code=Allowance.Aow_Code "
                            + " and PjStage.Stage_Name='" + to.getValue("AwSg_Verify_1").ToString() + "') as AwSg_Verify_1 "
                          + ",(Select case when AwSg_Verify='Y' then '通過' "
                                       + " when AwSg_Verify='N' then '不通過' "
                                       + " else '' end "
                            + " from AowStage where Pj_Code=Project.Pj_Code "
                            + " and Stage_Index=PjStage.Stage_Index "
                            + " and Aow_Code=Allowance.Aow_Code "
                            + " and PjStage.Stage_Name='" + to.getValue("AwSg_Verify_2").ToString() + "') as AwSg_Verify_2 "
                     + " from Project inner join PjStage on Project.Pj_Code = PjStage.Pj_Code "
                                 + " inner join Allowance on Allowance.Pj_Code = Project.Pj_Code "
                                 + " inner join ApPjContext on ApPjContext.Aow_Code = Allowance.Aow_Code "
                                 + " inner join Company on Company.Com_Code = Allowance.Com_Code "
                    + " where 1=1 ";
        return result;
    }

    private SqlCommand getCondSql(string sqlstr, DataTO to)
    {

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {
                case "Pj_Name": //專案名稱
                    cmd.CommandText += " AND Project.Pj_Code =@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
               
                case "Aow_SDate": //申請日期起
                    cmd.CommandText += " AND dbo.chgToChnDate(Allowance.Aow_Date) >=@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Aow_EDate": //申請日期迄
                    cmd.CommandText += " AND dbo.chgToChnDate(Allowance.Aow_Date) <=@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Com_CapitalS": //資本額起
                    cmd.CommandText += " AND Company.Com_Capital / 1000 >=@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Com_CapitalE": //資本額迄
                    cmd.CommandText += " AND Company.Com_Capital / 1000 <=@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Com_Name": //申請單位
                    cmd.CommandText += " AND Company." + to.getAllColumnName()[i] + " like @" + to.getAllColumnName()[i] + " + '%'";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "ApPj_Name": //計畫名稱
                    cmd.CommandText += " AND ApPjContext." + to.getAllColumnName()[i] + " like @" + to.getAllColumnName()[i] + " + '%'";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "ApPj_ProdCls": //商名類別
                    cmd.CommandText += " AND ApPjContext." + to.getAllColumnName()[i] + " =@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "ApPj_ApGroup": //申請組別
                    cmd.CommandText += " AND ApPjContext." + to.getAllColumnName()[i] + " =@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "ApPj_Msectors":  //產業別
                    cmd.CommandText += " AND ApPjContext." + to.getAllColumnName()[i] + " =@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Com_PostCode":  //公司登記所在地
                    cmd.CommandText += " AND Company." + to.getAllColumnName()[i] + " =@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
/*
                case "Com_OPAddr":
                    cmd.CommandText += " AND Company." + to.getAllColumnName()[i] + " like '%' + @" + to.getAllColumnName()[i] + " + '%'";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
*/ 
            }
        }

        return cmd;
    }



    System.Data.DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();

        string sqlstr = getDefaultSql(to);

        SqlCommand cmd = getCondSql(sqlstr, to);

        cmd.CommandText += " order by " + sortStr;
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }

    System.Data.DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();

        string sqlstr = getDefaultSql(to);

        SqlCommand cmd = getCondSql(sqlstr, to);

        cmd.CommandText += "  ";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }

    public DataTable getPrintInfo(DataTO to, String SelectData)
    {
        DataTable dt = new DataTable();
  
        string sqlstr = getDefaultSql(to);

        SqlCommand cmd = getCondSql(sqlstr, to);
        if (SelectData != "")
        {
            cmd.CommandText = "Select * From (" + cmd.CommandText + ") A";
            cmd.CommandText += " Where A.Aow_Code in (" + SelectData + ")";
        }
        cmd.CommandText += "  ";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }
}