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
/// RPOUT_02BL 的摘要描述
/// </summary>
public class RPOUT_02BL : ICommonBL, IQueryBL
{
    string uni_id = "ApPjContext.Aow_Code+rtrim(convert(char(10), isnull(MtgTimes.Times_Bgn,'9999/12/31'), 112))+Evaluations.Comm_Code+cast(PjStage.Stage_Index as varchar)";

    void IQueryBL.DeleteData(DataTO to)
    {

    }

    private string getDefaultSql()
    {
        string result = " select " +
                            " ApPjContext.ApPj_ApGroup " +
                            " ,(select Sys_CdText from SysCode where Sys_CdKind = 'P' and Sys_CdType = 'G' and Sys_CdCode = ApPjContext.ApPj_ApGroup)ApPj_ApGroup_Name " +
                            " ,Evaluations.Comm_Code " +
                            " ,(select Comm_Name from Committee where Comm_Code = Evaluations.Comm_Code)Comm_Name " +
                            " ,MtgTimes.Times_Bgn " +
                            " ,MtgTimes.Times_End " +
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
                            " ,Project.Pj_Code " +
                            " ,dbo.chgToChnDate(MtgTimes.Times_Bgn)Times_Bgn_Roc " +
                            " ,dbo.chgToChnDate(MtgTimes.Times_End)Times_End_Roc " +
                            " ,dbo.chgToChnChtDate(MtgTimes.Times_Bgn)Times_Bgn_Roc_Cht " +
                            " ,dbo.chgToChnChtDate(MtgTimes.Times_End)Times_End_Roc_Cht " +
                            " ,dbo.chgToChnDate(GETDATE())printDate " +
                            " ," + uni_id + " as uni_id " +
                        " from Project " +
                            " inner join PjStage on PjStage.Pj_Code = Project.Pj_Code " +
                            " inner join Allowance on Allowance.Pj_Code = Project.Pj_Code " +
                            " inner join ApPjContext on ApPjContext.Aow_Code = Allowance.Aow_Code " +
                            " inner join Evaluations on Evaluations.Aow_Code = ApPjContext.Aow_Code " +
                            " inner join MtgTimes on MtgTimes.Meeting_Code = Evaluations.Meeting_Code " +
                                " and MtgTimes.Meeting_Index = Evaluations.Meeting_Index " +
                        " where 1=1 ";
        return result;
    }

    private SqlCommand getFilter(string sqlstr, DataTO to)
    {
        SqlCommand cmd = new SqlCommand(sqlstr);
        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {
                case "txt_Times_Bgn":
                    cmd.CommandText += " AND dbo.chgToChnDate(MtgTimes.Times_Bgn) >= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "txt_Times_End":
                    cmd.CommandText += " AND dbo.chgToChnDate(MtgTimes.Times_End) <= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "txt_Pj_StartDate":
                    cmd.CommandText += " AND right('0'+substring(convert(char(10), isnull(Project.Pj_StartDate,'9999/12/31'), 112),1,4)-1911,3) = @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "sel_Pj_Name":
                    cmd.CommandText += " AND Project.Pj_Code = @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "sel_Pj_Stage":
                    cmd.CommandText += " AND PjStage.Stage_Index = @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
            }
        }
        return cmd;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();

        string sqlstr = getDefaultSql();

        SqlCommand cmd = getFilter(sqlstr, to);

        cmd.CommandText += " order by " + sortStr;
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();

        string sqlstr = getDefaultSql();

        SqlCommand cmd = getFilter(sqlstr, to);

        cmd.CommandText += " order by ApPj_ApGroup,Comm_Name ";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }

    public DataTable getPrintDatas(DataTO conds, String SelectData)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();

        string sqlstr = getDefaultSql();

        SqlCommand cmd = getFilter(sqlstr, conds);

        if (!SelectData.Equals(""))
        {
            cmd.CommandText += " AND " + uni_id + " in (" + SelectData + ")";
        }

        cmd.CommandText += " order by ApPj_ApGroup,Comm_Name ";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }
}
