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
/// RPOUT_03BL 的摘要描述
/// </summary>
public class RPOUT_03BL : ICommonBL, IQueryBL
{
    #region IQueryBL 成員

    string uni_id = "ApPjContext.Aow_Code+rtrim(convert(char(10), isnull(MtgTimes.Times_Bgn,'99991231'), 112))+Evaluations.Comm_Code+cast(PjStage.Stage_Index as varchar)";
    private string getDefaultSql()
    {
        string result = " select " +
                            " ApPjContext.ApPj_ApGroup " +
                            " ,(select Sys_CdText from SysCode where Sys_CdKind = 'P' and Sys_CdType = 'G' and Sys_CdCode = ApPjContext.ApPj_ApGroup)ApPj_ApGroup_Name " +
                            " ,Evaluations.Comm_Code " +
                            " ,(select Comm_Name from Committee where Comm_Code = Evaluations.Comm_Code)Comm_Name " +
                            " ,ApPjContext.Aow_Code " +
                            " ,Evaluations.Com_Code " +
                            " ,(select " +
                                " (case when Com_Imple <> '' " +
                                " then Com_Name + '/' + Com_Imple " +
                                " else Com_Imple end) " +
                            " from Company where Com_Code = Evaluations.Com_Code)Com_Name " +
                            " ,ApPjContext.ApPj_Msectors " +
                            " ,(select Sys_CdText from SysCode where Sys_CdKind = 'I' and Sys_CdType = 'D' and Sys_CdCode = ApPjContext.ApPj_Msectors)ApPj_Msectors_Name " +
                            " ,ApPjContext.ApPj_Name " +
                            " ,Evaluations.Eval_TotScore " +
                            " ,Evaluations.Eval_Note " +
                            " ,(select Score_Items from Score where Score_PjCode = Project.Pj_Code and EvalTail.Score_Code = Score_Code) + ' : ' + cast(EvalTail.Tail_Score as varchar) as Project_Tail_Score " +
                            " ,(select Score_Items from Score where Score_PjCode = Project.Pj_Code and EvalTail.Score_Code = Score_Code) + ' : ' + EvalTail.Tail_Text as Project_Tail_Text " +
                            " ,dbo.chgToChnDate(GETDATE())printDate " +
                            " ," + uni_id + " as uni_id " +
                        " from Project " +
                            " inner join PjStage on PjStage.Pj_Code = Project.Pj_Code " +
                            " inner join Allowance on Allowance.Pj_Code = Project.Pj_Code " +
                            " inner join ApPjContext on ApPjContext.Aow_Code = Allowance.Aow_Code " +
                            " inner join Evaluations on Evaluations.Aow_Code = ApPjContext.Aow_Code " +
                            " inner join MtgTimes on MtgTimes.Meeting_Code = Evaluations.Meeting_Code " +
                                " and MtgTimes.Meeting_Index = Evaluations.Meeting_Index " +
                            " inner join EvalTail on EvalTail.Aow_Code = Evaluations.Aow_Code " +
                                " and EvalTail.Meeting_Code = Evaluations.Meeting_Code " +
                                " and EvalTail.Meeting_Index = Evaluations.Meeting_Index " +
                        " where 1=1 ";
        return result;
    }

    private string getQuerySql()
    {
        string querystr = "select d.Aow_Code, (select (case when Com_Imple <> '' then Com_Name  + '/' +  Com_Imple  else Com_Name end) as Com_Name  from Company where Com_Code = e.Com_Code) as Com_Name,  " +
                         "(select Sys_CdText from SysCode where Sys_CdKind='I' and Sys_CdType='D' and Sys_CdCode =c.ApPj_Msectors) as ApPj_Msectors, " +
                         "c.ApPj_Name, AVG(e.Eval_TotScore) as Eval_TotScore " +
                         "from Project a, PjStage b, ApPjContext c, Allowance d, Evaluations e, MtgTimes g, Company h " +
                         "where a.Pj_Code = @Pj_Code and YEAR(a.Pj_StartDate)-1911 = @Pj_StartDate and b.Stage_Index = @Stage_Index " +
                         "and a.Pj_Code = b.Pj_Code and a.Pj_Code = d.Pj_Code and c.Aow_Code = d.Aow_Code and c.Aow_Code = e.Aow_Code " +
                         "and e.Meeting_Code = g.Meeting_Code and e.Meeting_Index = g.Meeting_Index and d.Com_Code = h.Com_Code and 1=1";

        return querystr;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = getFilter(getQuerySql(), to);
        cmd.Parameters.AddWithValue("@Pj_Code", to.getValue("sel_Pj_Name").ToString());
        cmd.Parameters.AddWithValue("@Pj_StartDate", to.getValue("txt_Pj_StartDate").ToString());
        cmd.Parameters.AddWithValue("@Stage_Index", to.getValue("sel_Pj_Stage").ToString());
        cmd.CommandText += " group by ApPj_ApGroup, d.Aow_Code, h.Com_Name, c.ApPj_Msectors, c.ApPj_Name,e.Com_Code";
        cmd.CommandText += " order by ApPj_ApGroup, d.Aow_Code, h.Com_Name, c.ApPj_Msectors, c.ApPj_Name";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        throw new NotImplementedException();
    }

    private SqlCommand getFilter(string sqlstr, DataTO to)
    {
        SqlCommand cmd = new SqlCommand(sqlstr);
        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {
                case "txt_Times_Bgn":
                    cmd.CommandText += " AND dbo.chgToChnDate(g.Times_Bgn) >= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "txt_Times_End":
                    cmd.CommandText += " AND dbo.chgToChnDate(g.Times_End) <= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                    
            }
        }
        return cmd;
    }

    public DataTable getPrintDatas(DataTO conds, String SelectData)
    {
        DataTable dt = new DataTable();
        string sqlstr = getDefaultSql();
        SqlCommand cmd = getFilter(sqlstr, conds);
        if (!SelectData.Equals(""))
        {
            cmd.CommandText += " AND " + uni_id + " in (" + SelectData + ")";
        }
        //cmd.CommandText += " group by ApPj_ApGroup, Allowance.Aow_Code, ApPjContext.ApPj_Msectors, ApPjContext.ApPj_Name";
        //cmd.CommandText += " order by ApPj_ApGroup, Allowance.Aow_Code, ApPjContext.ApPj_Msectors, ApPjContext.ApPj_Name";      
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    #endregion

    void IQueryBL.DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }
}
