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
/// RPOUT_08BL 的摘要描述
/// </summary>
public class RPOUT_08BL : ICommonBL, IQueryBL
{
    string uni_id = "ApPjContext.Aow_Code+PjStage.Pj_Code+cast(PjStage.Stage_Index as varchar)";

    void IQueryBL.DeleteData(DataTO to)
    {

    }

    private string getDefaultSql()
    {
        string result = " select " +
                            " Allowance.Aow_Code " +
                            " ,Company.Com_Name " +
                            " ,Company.Com_Imple " +
                            " ,ApPjContext.ApPj_BgnDate " +
                            " ,ApPjContext.ApPj_EndDate " +
	                        " ,dbo.chgToChnDate(ApPjContext.ApPj_BgnDate)ApPj_BgnDate_Roc " +
	                        " ,dbo.chgToChnDate(ApPjContext.ApPj_EndDate)ApPj_EndDate_Roc " +
	                        " ,dbo.chgToChnChtDate(ApPjContext.ApPj_BgnDate)ApPj_BgnDate_Roc_Cht " +
                            " ,dbo.chgToChnChtDate(ApPjContext.ApPj_EndDate)ApPj_EndDate_Roc_Cht " +
                            " ,ApPjContext.ApPj_Name " +
                            " ,round(convert(decimal(10,0),ApPjContext.ApPj_TotAmt/1000),0)ApPj_TotAmt " +
                            " ,round(convert(decimal(10,0),ApPjContext.ApPj_AowAmt/1000),0)ApPj_AowAmt " +
                            " ,round(convert(decimal(10,0),ApPjContext.ApPj_FundAmt/1000),0)ApPj_FundAmt " +
                            " ," + uni_id + " as uni_id " +
                            " ,MtgCrew.Comm_Code " +
                            " ,MtgCrew.Meeting_Code " +
                            " ,dbo.chgToChnDate(GETDATE())printDate " +
                        " from Allowance " +
                        " inner join ApPjContext on ApPjContext.Aow_Code = Allowance.Aow_Code " +
                        " inner join Company on Company.Com_Code = Allowance.Com_Code " +
                        " inner join PjStage on Allowance.Pj_Code = PjStage.Pj_Code " +
                        " inner join Project on Project.Pj_Code = Allowance.Pj_Code " +
                        " inner join MtgCrew on PjStage.Metting_Code = MtgCrew.Meeting_Code " +
                            " and MtgCrew.Meeting_Index = 1 " +
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
                case "sel_Comm_Code":
                    cmd.CommandText += " AND MtgCrew.Comm_Code = @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "txt_Com_Name":
                    cmd.CommandText += " AND Company.Com_Name like '%" + to.getValue(to.getAllColumnName()[i]) + "%'";
                    break;
                case "txt_Eval_Count":
                    cmd.CommandText += " AND (select count(*) from Evaluations where MtgCrew.Comm_Code = Comm_Code and Eval_Status = 'Y') = @" + to.getAllColumnName()[i];
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

        cmd.CommandText += " order by uni_id ";
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

        cmd.CommandText += " order by uni_id ";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }

    public DataTable getDataTable_Sub01()
    {
        string sqlstr = " select " +
	                        " ApplyAsis.Aow_Code " +
	                        " ,ApplyAsis.Aas_Year " +
	                        " ,ApplyAsis.Aas_PjName " +
	                        " ,convert(decimal(10,2),ApplyAsis.Aas_Amount/1000 ,0)allFee " +
                        " from ApplyAsis " +
                        " where 1=1 ";
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(sqlstr);
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }

    public DataTable getDataTable_Sub02()
    {
        string sqlstr = " select " +
	                        " Committee.Comm_Code " +
	                        " ,Evaluations.Meeting_Code " +
	                        " ,Committee.Comm_Name " +
	                        " ,Evaluations.Eval_Note " +
	                        " ,(case Evaluations.Eval_Status " +
		                        " when 'Y' then '1' " +
		                        " else '' " +
		                        " end)as recommend " +
	                        " ,(case Evaluations.Eval_Status " +
		                        " when 'N' then '1' " +
		                        " else '' " +
		                        " end)as unrecommend " +
                        " from Committee " +
                        " inner join Evaluations on Evaluations.Comm_Code = Committee.Comm_Code " +
                            " and Evaluations.Meeting_Index = 1 " +
                        " where 1=1 ";
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(sqlstr);
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }
}
