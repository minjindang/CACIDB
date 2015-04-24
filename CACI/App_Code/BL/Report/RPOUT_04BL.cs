using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;

/// <summary>
/// RPOUT_04BL 的摘要描述
/// </summary>
public class RPOUT_04BL : ICommonBL, IQueryBL
{
    private string getQuerySql() 
    {
        string querystr = "select d.Aow_Code, " +
			              "(case when h.Com_Imple <> ' ' then h.Com_Name + '/' + h.Com_Imple else h.Com_Name end) as applyCom, " +
			              "(select Sys_CdText from SysCode where Sys_CdKind = 'I' and Sys_CdType = 'D' and Sys_CdCode = e.ApPj_Msectors) as ApPj_Msectors, " +
                          "e.ApPj_Name, cast(cast(dbo.chgToChnDate(e.ApPj_BgnDate) as varchar(9) ) + '~' + cast(dbo.chgToChnDate(e.ApPj_EndDate) as varchar(9)) as varchar(19)) as ApPj_BgnDateToEndDate, " +
                          "cast(round(e.ApPj_TotAmt/1000, 2) as decimal(20 ,2)) as ApPj_TotAmt, " +
                          "cast(round(e.ApPj_AowAmt/1000, 2) as decimal(20 ,2)) as ApPj_AowAmt, e.Aow_Code as uni_id " +
                          "from Project b left join PjStage c on b.Pj_Code = c.Pj_Code " +
						                 "left join Allowance d on b.Pj_Code = d.Pj_Code " +
						                 "left join ApPjContext e on d.Aow_Code = e.Aow_Code " +
						                 "left join Company h on h.Com_Code = d.Com_Code " +
                          "where YEAR(b.Pj_StartDate) - 1911 = @Pj_StartDate and b.Pj_Code = @Pj_Code and c.Stage_Index = @Stage_Index and 1=1";

        return querystr;
    }

    private SqlCommand getFilter(string sqlstr, DataTO to)
    {
        SqlCommand cmd = new SqlCommand(sqlstr);
        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {
                case "txt_Aow_DateS":
                    cmd.CommandText += " AND dbo.chgToChnDate(Allowance.Aow_Date) >= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "txt_Aow_DateE":
                    cmd.CommandText += " AND dbo.chgToChnDate(Allowance.Aow_Date) <= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
            }
        }
        return cmd;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = getFilter(getQuerySql(), to);
        cmd.Parameters.AddWithValue("@Pj_StartDate", to.getValue("txt_Pj_StartDate").ToString());
        cmd.Parameters.AddWithValue("@Pj_Code", to.getValue("sel_Pj_Name").ToString());
        cmd.Parameters.AddWithValue("@Stage_Index", to.getValue("sel_Pj_Stage").ToString());
        cmd.CommandText += " order by ApPj_ApGroup, d.Com_Code ";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        string strName = "Jeff,Jack,Jay,Jason";

        string[] strQry = strName.Split(",".ToCharArray());


        return dt;
    }

    public string getReportSql() 
    {
        string reportstr = "SELECT DISTINCT ApPjContext.ApPj_ApGroup, " +
                           "(SELECT (CASE WHEN Com_Imple <> '' THEN Com_Name + '/' + Com_Imple ELSE Com_Imple END) AS Expr1 FROM Company WHERE (Com_Code = Evaluations_1.Com_Code)) AS Com_Name, " +
                           "(SELECT Sys_CdText FROM SysCode WHERE (Sys_CdKind = 'P') AND (Sys_CdType = 'G') AND (Sys_CdCode = ApPjContext.ApPj_ApGroup)) AS ApPj_ApGroup_Name, ApPjContext.ApPj_Msectors, " +
                           "(SELECT Sys_CdText FROM SysCode AS SysCode_1 WHERE (Sys_CdKind = 'I') AND (Sys_CdType = 'D') AND (Sys_CdCode = ApPjContext.ApPj_Msectors)) AS ApPj_Msectors_Name, " +
                           "ApPjContext.Aow_Code, ApPjContext.ApPj_Name, ApPjContext.ApPj_Goal, ApPjContext.ApPj_Policies, ApPjContext.ApPj_Profit, ApPjContext.ApPj_Solution, ApPjContext.ApPj_BgnDate, " +
                           "ApPjContext.ApPj_EndDate, dbo.chgToChnDate(ApPjContext.ApPj_BgnDate) AS ApPj_BgnDate_Roc, " +
                           "dbo.chgToChnDate(ApPjContext.ApPj_EndDate) AS ApPj_EndDate_Roc, " +
                           "dbo.chgToChnChtDate(ApPjContext.ApPj_BgnDate) AS ApPj_BgnDate_Roc_Cht, " +
                           "dbo.chgToChnChtDate(ApPjContext.ApPj_EndDate) AS ApPj_EndDate_Roc_Cht, CONVERT(decimal(10, 2), " +
                           "ApPjContext.ApPj_TotAmt / 1000, 0) AS ApPj_TotAmt, CONVERT(decimal(10, 2), ApPjContext.ApPj_AowAmt / 1000, 0) AS ApPj_AowAmt, " +
                           "dbo.chgToChnDate(GETDATE()) AS printDate, " +
                           "ApPjContext.Aow_Code AS uni_id, " +
                           "(SELECT CONVERT(decimal(10, 2), AVG(Evaluations.Eval_TotScore), 0) AS Expr1 FROM Evaluations LEFT OUTER JOIN MtgTimes ON MtgTimes.Meeting_Code = Evaluations.Meeting_Code AND MtgTimes.Meeting_Index = Evaluations.Meeting_Index WHERE (Evaluations.Comm_Code = PjJudge.Comm_Code) AND (Evaluations.Aow_Code = Allowance.Aow_Code)) AS Eval_TotScore_avg " +
                           "FROM PjJudge LEFT OUTER JOIN Project ON Project.Pj_Code = PjJudge.Pj_Code " +
                                        "LEFT OUTER JOIN PjStage ON PjStage.Pj_Code = PjJudge.Pj_Code " +
                                        "LEFT OUTER JOIN Allowance ON Allowance.Pj_Code = PjJudge.Pj_Code " + 
                                        "LEFT OUTER JOIN ApPjContext ON ApPjContext.Aow_Code = Allowance.Aow_Code " +
                                        "INNER JOIN Evaluations AS Evaluations_1 ON Evaluations_1.Aow_Code = ApPjContext.Aow_Code " +
                           "WHERE (ApPjContext.Aow_Code IS NOT NULL) AND (1 = 1)";

        return reportstr;
    }

    public DataTable getPrintDatas(DataTO conds, String SelectData)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = getFilter(getReportSql(), conds);
        if (!SelectData.Equals(""))
        {
            cmd.CommandText += " AND ApPjContext.Aow_Code in (" + SelectData + ")";
        }
        cmd.CommandText += " GROUP BY ApPj_ApGroup, Evaluations_1.Com_Code, ApPjContext.ApPj_Msectors, ApPjContext.Aow_Code, ApPjContext.ApPj_Name, ApPjContext.ApPj_Goal, ApPjContext.ApPj_Policies, ApPjContext.ApPj_Profit, ApPjContext.ApPj_Solution, ApPjContext.ApPj_BgnDate, ApPjContext.ApPj_EndDate, ApPjContext.ApPj_TotAmt, ApPjContext.ApPj_AowAmt, PjJudge.Comm_Code, Allowance.Aow_Code";
        //cmd.CommandText += " order by ApPj_ApGroup ";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    public DataTable getPrintInfo_Sub01(DataTO conds, String SelectData)
    {
        DataTable dt = new DataTable();
        string sqlstr = "SELECT ApplyAsis.Aow_Code, ApplyAsis.Aas_PjName, CAST(ROUND(CAST(ApplyAsis.Aas_Amount / 1000 AS FLOAT), 2) AS VARCHAR) + '萬' AS Aas_Amount " +
                        "FROM ApplyAsis WHERE Aas_Type = 'A' AND 1=1";
        SqlCommand cmd = new SqlCommand(sqlstr);
        if (!SelectData.Equals(""))
        {
            cmd.CommandText += " AND ApplyAsis.Aow_Code in (" + SelectData + ")";
        }
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    public DataTable getDataTable_Sub02(DataTO conds, String SelectData)
    {
        DataTable dt = new DataTable();
        string sqlstr = "SELECT distinct d.Aow_Code, " +
                        "(SELECT Comm_Name FROM Committee WHERE (Comm_Code = i.Comm_Code)) AS CommName, " +
                        "j.Eval_TotScore, j.Eval_Note FROM Project AS b INNER JOIN PjStage AS c ON b.Pj_Code = c.Pj_Code " +
                                                                       "INNER JOIN Allowance AS d ON b.Pj_Code = d.Pj_Code " +
                                                                       "INNER JOIN PjJudge AS i ON b.Pj_Code = i.Pj_Code " +
                                                                       "LEFT OUTER JOIN Evaluations AS j ON d.Aow_Code = j.Aow_Code AND i.Comm_Code = j.Comm_Code " +
                                                                       "WHERE 1 = 1";
        SqlCommand cmd = new SqlCommand(sqlstr);
        if (!SelectData.Equals(""))
        {
            cmd.CommandText += " AND d.Aow_Code in (" + SelectData + ")";
        }
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        throw new NotImplementedException();
    }

    void IQueryBL.DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }
}
