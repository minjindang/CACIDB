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
/// RPOUT_06BL 的摘要描述
/// </summary>
public class RPOUT_06BL : ICommonBL, IQueryBL
{
    void IQueryBL.DeleteData(DataTO to)
    {
 
    }

    private string getDefaultSql()
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();
        string result = "select RANK() OVER(ORDER BY Allowance.Aow_Code,Comm_Name,ApPj_Name) as Inde "
                               + ",Allowance.Aow_Code "
                               + ",MtgTimes.Times_Bgn "
                               + ",Committee.Comm_Name "
                               + ",Company.Com_Name "
                               + ",ApPjContext.ApPj_Name "
                               + ",(select Sys_CdText from SysCode where Sys_CdKind='C' and Sys_CdType='K' and Sys_CdCode =AowCoach.AwCh_ChNeed) as AwCh_Need "
                               + ",AowCoach.AwCh_Agenda "
                               + ",AowCoach.AwCh_SgPoint "
                               + " from Project inner join Allowance on Project.Pj_Code = Allowance.Pj_Code "
                                     + " inner join ApPjContext on ApPjContext.Aow_Code = Allowance.Aow_Code "
                                     + " inner join AowCoach on AowCoach.Aow_Code = Allowance.Aow_Code "
                                     + " inner join MtgTimes on MtgTimes.Meeting_Code = AowCoach.Meeting_Code "
                                              + " and MtgTimes.Meeting_Index = AowCoach.Meeting_Index "
                                     + " inner join Committee on Committee.Comm_Code = AowCoach.Comm_Code "
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
                case "Pj_StartDate":
                    cmd.CommandText += " AND Year(Project." + to.getAllColumnName()[i] + ")-1911 =@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Pj_Name":
                    cmd.CommandText += " AND Project.Pj_Code = @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "ApPj_Name":
                    cmd.CommandText += " AND ApPjContext." + to.getAllColumnName()[i] + " like '%' + @" + to.getAllColumnName()[i] + " + '%'";
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

        cmd.CommandText += " order by Committee.Comm_Name,ApPjContext.ApPj_Name,Allowance.Aow_Code ";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }

    public DataTable getPrintInfo(DataTO to,String SelectData)
    {
        DataTable dt = new DataTable();

        string sqlstr = "select RANK() OVER(ORDER BY Allowance.Aow_Code,Comm_Name,ApPj_Name) as Inde "
                                + ",Allowance.Aow_Code "
                                + ",RIGHT(CONVERT(varchar(10),MtgTimes.Times_Bgn),4) as Times_YYYY "
                                + ", RIGHT(dbo.chgToChnChtDate(MtgTimes.Times_Bgn),7) as Times_MMDD "
                                + ",Committee.Comm_Name "
                                + ",Company.Com_Name "
                                + ",ApPjContext.ApPj_Name "
                                + ",(select Sys_CdText from SysCode where Sys_CdKind='C' and Sys_CdType='K' and Sys_CdCode =AowCoach.AwCh_ChNeed) as AwCh_Need "
                                + ",case AowCoach.AwCh_ChNeed when 'FA' then '■' else '□' end + '財務' as AC1 "
                                + ",case AowCoach.AwCh_ChNeed when 'LW' then '■' else '□' end + '法律' as AC2 "
                                + ",case AowCoach.AwCh_ChNeed when 'MG' then '■' else '□' end + '經營管理' as AC3 "
                                + ",case AowCoach.AwCh_ChNeed when 'MK' then '■' else '□' end + '行銷' as AC4 "
                                + ",case AowCoach.AwCh_ChNeed when 'RD' then '■' else '□' end + '創新研發' as AC5 "
                                + ",AowCoach.AwCh_Agenda "
                                + ",AowCoach.AwCh_SgPoint "
                                + ",MtgTimes.Meeting_Code"
                                + ",MtgTimes.Meeting_Index"
                          + " from Project inner join Allowance on Project.Pj_Code = Allowance.Pj_Code "
                          + " inner join ApPjContext on ApPjContext.Aow_Code = Allowance.Aow_Code "
                          + " inner join AowCoach on AowCoach.Aow_Code = Allowance.Aow_Code "
                          + " inner join MtgTimes on MtgTimes.Meeting_Code = AowCoach.Meeting_Code "
                                + " and MtgTimes.Meeting_Index = AowCoach.Meeting_Index "
                          + " inner join Committee on Committee.Comm_Code = AowCoach.Comm_Code "
                          + " inner join Company on Company.Com_Code = Allowance.Com_Code "
                          + " where 1=1 ";

        SqlCommand cmd = getCondSql(sqlstr, to);

        if (SelectData != "")
        {
            cmd.CommandText = "Select * From (" + cmd.CommandText + ") A";
            cmd.CommandText += " Where A.Inde in (" + SelectData + ")";
        }
        cmd.CommandText += " order by A.Comm_Name,A.ApPj_Name,A.Aow_Code ";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }

    public DataTable getPrintInfo_Sub01(DataTO to, String SelectData)
    {
        DataTable dt = new DataTable();
        string sqlstr = "SELECT cast(RANK() OVER(ORDER BY Evaluations.Aow_Code,Evaluations.Meeting_Code,Evaluations.Meeting_Index,Evaluations.Comm_Code,Evaluations.Com_Code) as varchar(2)) as Inde "
                            + ",* "
                        + " FROM Evaluations inner join Allowance on Evaluations.Aow_Code = Allowance.Aow_Code "
                                         //+ " inner join MtgTimes on Evaluations.Meeting_Code = MtgTimes.Meeting_Code "
                                         //                   + " and Evaluations.Meeting_Index = MtgTimes.Meeting_Index "
                        + " where 1=1 AND Evaluations.Meeting_Code = @Meeting_Code";
        SqlCommand cmd = new SqlCommand(sqlstr);
        cmd.Parameters.AddWithValue("@Meeting_Code", to.getValue("Meeting_Code"));
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }


}