using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;

/// <summary>
/// SAMPLEBL 的摘要描述
/// </summary>
public class UserRights_01BL : ICommonBL, IQueryBL, IMasterUIBL
{
    #region IQueryBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT a.*,b.*,c.Sys_CdText " +
                        "FROM CACIDB..UserAcc a JOIN CACIDB..UserDep b ON a.UsDp_Code=b.UsDp_Code " +
                                               "JOIN CACIDB..SysCode c ON a.User_Level=c.Sys_CdCode " +
                        "WHERE 1=1 AND c.Sys_CdKind='U' AND c.Sys_CdType='L' ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            cmd.CommandText += " AND a." + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
            cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
        }

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT a.*,b.*,c.Sys_CdText " +
                        "FROM CACIDB..UserAcc a JOIN CACIDB..UserDep b ON a.UsDp_Code=b.UsDp_Code " +
                                               "JOIN CACIDB..SysCode c ON a.User_Level=c.Sys_CdCode " +
                        "WHERE 1=1 AND c.Sys_CdKind='U' AND c.Sys_CdType='L' ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            cmd.CommandText += " AND a." + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
            cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
        }

        cmd.CommandText += " Order By " + sortStr;

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    void IQueryBL.DeleteData(DataTO to)
    {
    }

    #endregion

    #region IMDUIBL 成員

    void IMasterUIBL.InsertData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        List<DataTO> permissions = (List<DataTO>)to.getValue("permissions");

        to.removeValue("permissions");

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..UserAcc", to));

        for (int i = 0; i < permissions.Count; i++)
        {
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..UserRights", permissions[i]));
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    void IMasterUIBL.UpdateData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        string delsqlstr = "DELETE FROM CACIDB..UserRights WHERE User_Code=@User_Code";

        SqlCommand delCmd = new SqlCommand(delsqlstr);

        delCmd.Parameters.AddWithValue("@User_Code", to.getValue("User_Code").ToString());

        cmds.Add(delCmd);

        List<DataTO> permissions = (List<DataTO>)to.getValue("permissions");

        to.removeValue("permissions");

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..UserAcc", to));

        for (int i = 0; i < permissions.Count; i++)
        {
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..UserRights", permissions[i]));
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    void IMasterUIBL.LoadData(DataTO to)
    {
        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..UserAcc", to);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(cmd);

        if (sr.Read())
        {
            for (int i = 0; i < sr.FieldCount; i++)
            {
                if (!to.isColumnExist(sr.GetName(i)))
                {
                    to.setValue(sr.GetName(i), sr[sr.GetName(i)].ToString());
                }
            }
        }

        if (!to.isColumnExist("permissions"))
        {
            DataTable dt = getUserPermissionPrograms(to.getValue("User_Code").ToString());

            to.setValue("permissions", dt);
        }
    }

    bool IMasterUIBL.IsDataExist(DataTO to)
    {
        if (!to.isColumnExist("User_Code"))
            return false;
        else
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("CACIDB..UserAcc", to);
    }

    void IMasterUIBL.MarkData(DataTO to)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region 自訂功能

    public DataTable getAllProgramData()
    {
        DataTable dt = new DataTable();

        //new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..Program", new DataTO()), dt);

        string sqlstr = "SELECT Prog_Num + ' ' + Prog_Name Prog_Name,Prog_Num,Prog_Path," +
                        "CAST(SUBSTRING(Prog_Num, 1, CHARINDEX('.', Prog_Num) - 1) AS int) AS LV1, " +
                        "CAST(SUBSTRING(Prog_Num, CHARINDEX('.', Prog_Num) + 1, CHARINDEX('.', Prog_Num, CHARINDEX('.', Prog_Num) + 1) - CHARINDEX('.', Prog_Num) - 1) AS int) AS LV2, " +
                        "CAST(SUBSTRING(Prog_Num, CHARINDEX('.', Prog_Num, CHARINDEX('.', Prog_Num) + 1) + 1, { fn LENGTH(Prog_Num) } - CHARINDEX('.', Prog_Num) + 1) AS int) AS LV3 " +
                        "FROM CACIDB..Program " +
                        "ORDER BY LV1,LV2,LV3 ";

        new SQLAgent(DataBase.CACIDB).select(new SqlCommand(sqlstr), dt);

        return dt;
    }

    public DataTable getUserPermissionPrograms(string User_Code)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT a.Prog_Num + ' ' + a.Prog_Name Prog_Name,a.Prog_Num,a.Prog_Path,a.Prog_Type," +
                        "CAST(SUBSTRING(a.Prog_Num, 1, CHARINDEX('.', a.Prog_Num) - 1) AS int) AS LV1, " +
                        "CAST(SUBSTRING(a.Prog_Num, CHARINDEX('.', a.Prog_Num) + 1, CHARINDEX('.', a.Prog_Num, CHARINDEX('.', a.Prog_Num) + 1) - CHARINDEX('.', a.Prog_Num) - 1) AS int) AS LV2, " +
                        "CAST(SUBSTRING(a.Prog_Num, CHARINDEX('.', a.Prog_Num, CHARINDEX('.', a.Prog_Num) + 1) + 1, { fn LENGTH(a.Prog_Num) } - CHARINDEX('.', a.Prog_Num) + 1) AS int) AS LV3 " +
                        "FROM CACIDB..Program a JOIN CACIDB..UserRights b ON a.Prog_Num=b.Prog_Num " +
                        "WHERE b.User_Code=@User_Code " +
                        "ORDER BY LV1,LV2,LV3 ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@User_Code", User_Code);

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    public DataTable getUserNotPermissionPrograms(string User_Code)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT Prog_Num + ' ' + Prog_Name Prog_Name,Prog_Num " +
                        "FROM CACIDB..Program " +
                        "WHERE Prog_Num not in (SELECT Prog_Num FROM CACIDB..UserRights WHERE User_Code=@User_Code) ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@User_Code", User_Code);

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }


    #endregion

    

}