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
public class RPOUT_05BL : ICommonBL, IQueryBL
{

    void IQueryBL.DeleteData(DataTO to)
    {

    }

    private string getDefaultSql()
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();
        string result = "select RANK() OVER(ORDER BY ApPj_Name,Com_Name,ApPj_Msectors,ApPj_BgnDate,ApPj_EndDate,ApPj_TotAmt,ApPj_AowAmt,Eval_Note) as Inde "
                             + ",ApPjContext.ApPj_Name,ApPjContext.ApPj_Goal,ApPjContext.ApPj_Policies,ApPjContext.ApPj_Profit,ApPjContext.ApPj_Solution  "
                             + ",(select Sys_CdText from SysCode where Sys_CdKind='P' and Sys_CdType='G' and Sys_CdCode =ApPjContext.ApPj_ApGroup) as ApPj_ApGroup "
                             + ",Company.Com_Name "
                             + ",(Select Sys_CdText from SysCode where Sys_CdKind='I' and Sys_CdType='D' and Sys_CdCode = ApPjContext.ApPj_Msectors) as ApPj_Msectors "                   
                             + ",dbo.chgToChnDate(ApPjContext.ApPj_BgnDate)+'~'+dbo.chgToChnDate(ApPjContext.ApPj_EndDate) as ApPj_BEgnDate "
                             + ",convert(decimal(10,2),ApPjContext.ApPj_TotAmt/10000) as ApPj_TotAmt "
                             + ",convert(decimal(10,2),ApPjContext.ApPj_AowAmt/10000) as ApPj_AowAmt "
                             + ",Evaluations.Eval_Note "
                             + ",Evaluations.Eval_TotScore "
                             + ",'' as Result "
                             + ",case Evaluations.Eval_Status "
                                    + "when 'Y' then Evaluations.Eval_TotScore + 1 "
                                    + "when 'E' then Evaluations.Eval_TotScore + 0.5 "
                                    + "when 'N' then Evaluations.Eval_TotScore "
                             + " end as VoteScore "
                             + ",dbo.GetAllPjName(Allowance.Aow_Code) as AllPjName "
                             + ",dbo.chgToChnDate(getdate()) as Print_Date "
                             + ",convert(decimal(10,2),MtgRecordByUnit.Recommend_Fund/10000) as Recommend_Fund "
                             + ",MtgRecordByUnit.Staff_Recomm "
                             + ",MtgRecordByUnit.Staff_Note "
                             + " from Project inner join PjStage on Project.Pj_Code = PjStage.Pj_Code "
                             + " inner join Allowance on Allowance.Pj_Code = Project.Pj_Code "
                             + " inner join ApPjContext on ApPjContext.Aow_Code = Allowance.Aow_Code "                           
                             + " inner join Evaluations on Evaluations.Aow_Code = ApPjContext.Aow_Code "
                             + " inner join MtgTimes on MtgTimes.Meeting_Code = Evaluations.Meeting_Code  "
                            
                             + " and  MtgTimes.Meeting_Index = Evaluations.Meeting_Index "
                             + " inner join Company on Company.Com_Code = Allowance.Com_Code "
                             + " and Company.Com_Code=Evaluations.Com_Code  "
                             + " left outer join MtgRecordByUnit on MtgRecordByUnit.Meeting_Code = MtgTimes.Meeting_Code "
                                              + " and MtgRecordByUnit.Meeting_Index = MtgTimes.Meeting_Index "
                                              + " and MtgRecordByUnit.Com_Code = Company.Com_Code "                             
                             + "where 1=1   ";
        return result;
    }

    private SqlCommand getCondSql(string sqlstr, DataTO to)
    {

        SqlCommand cmd = new SqlCommand(sqlstr);
        
        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {
                case "ApPj_Msectors":
                    cmd.CommandText += " AND ApPjContext." + to.getAllColumnName()[i] + " =@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Pj_StartDate":
                    cmd.CommandText += " AND Year(Project." + to.getAllColumnName()[i] + ")-1911 =@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Pj_Name":
                    cmd.CommandText += " AND Project.Pj_Code =@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Stage_Name":
                    cmd.CommandText += " AND PjStage.Stage_Index =@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Aow_SDate":
                    cmd.CommandText += " AND dbo.chgToChnDate(Allowance.Aow_Date) >=@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Aow_EDate":
                    cmd.CommandText += " AND dbo.chgToChnDate(Allowance.Aow_Date) <=@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
            }
        }

        return cmd;
    }



    System.Data.DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();

        string sqlstr = getDefaultSql();

        SqlCommand cmd = getCondSql(sqlstr, to);

        cmd.CommandText += " order by " + sortStr;
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }

    System.Data.DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();

        string sqlstr = getDefaultSql();

        SqlCommand cmd = getCondSql(sqlstr, to);

        if (to.getValue("Sort").ToString() == "1")
        {
            cmd.CommandText += " order by ApPjContext.ApPj_ApGroup,Evaluations.Eval_TotScore desc ";
        }
        else
        {
            cmd.CommandText += " order by ApPjContext.ApPj_ApGroup,VoteScore desc ";
        }

        
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }

    public DataTable getPrintInfo(DataTO to,String SelectData)
    {
        DataTable dt = new DataTable();

        string sqlstr = getDefaultSql();

        SqlCommand cmd = getCondSql(sqlstr, to);
        if (SelectData != "")
        {
            cmd.CommandText = "Select * From (" + cmd.CommandText + ") A";
            cmd.CommandText += " Where A.Inde in (" + SelectData + ")";
        }

        if (to.getValue("Sort").ToString() == "1")
        {
            cmd.CommandText += " order by A.ApPj_ApGroup,A.Eval_TotScore desc ";
        }
        else
        {
            cmd.CommandText += " order by A.ApPj_ApGroup,A.VoteScore desc ";
        }
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }
}