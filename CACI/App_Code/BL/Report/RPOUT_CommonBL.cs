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
/// RPOUT系列共用
/// </summary>
public class RPOUT_CommonBL : ICommonBL
{
    public DataTable getProjectName(string projectYear)
    {
        string sqlstr = "select * from Project where right('0'+substring(convert(char(10), isnull(Pj_StartDate,'9999/12/31'), 112),1,4)-1911,3) = @projectYear";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@projectYear", projectYear);

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    public DataTable getProjectStage(string projectCode)
    {
        string sqlstr = "select * from PjStage where Pj_Code = @projectCode";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@projectCode", projectCode);

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    public DataTable getProjectName()
    {
        string sqlstr = "select * from Project";

        SqlCommand cmd = new SqlCommand(sqlstr);

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    public DataTable getProjectStage()
    {
        string sqlstr = "select * from PjStage";

        SqlCommand cmd = new SqlCommand(sqlstr);

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    public DataTable getCommittee()
    {
        string sqlstr = " select " +
                            " * " +
                        " from MtgCrew " +
                        " inner join Committee on MtgCrew.Comm_Code = Committee.Comm_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    public DataTable getApPj_ApGroup()
    {
        string sqlstr = " select " +
                            " ApPj_ApGroup, " +
                            " (select Sys_CdText from SysCode where Sys_CdKind = 'P' and Sys_CdType = 'G' and Sys_CdCode = ApPj_ApGroup)ApPj_ApGroup_Name " +
                        " from ApPjContext " +
                        " where ApPj_ApGroup is not null ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    public DataTable getCom_MnSectors()
    {
        string sqlstr = " select " +
                            " Com_MnSectors " +
                            " ,(select Sys_CdText from SysCode where Sys_CdKind = 'I' and Sys_CdType = 'D' and Sys_CdCode = Com_MnSectors)Com_MnSectors_Name " +
                        " from Company " +
                        " where Com_MnSectors is not null ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    public DataTable getAwSg_Verify(string Project_Code)
    {
        string sqlstr = "select * from PjStage where Pj_Code = @Pj_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Pj_Code", Project_Code);

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }
}
