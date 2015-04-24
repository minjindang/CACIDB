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
public class Program_01BL : ICommonBL, IQueryBL, IMasterUIBL
{
    #region IQueryBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..Program", to), dt);

        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..Program", to);

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
        // 新增程式時，同時加入admin權限
        
        //new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Program", to));

        List<SqlCommand> cmds = new List<SqlCommand>();

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Program", to));

        DataTO userRightTo = new DataTO();

        userRightTo.setValue("User_Code", "admin");
        userRightTo.setValue("Prog_Num", to.getValue("Prog_Num").ToString());
        userRightTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..UserRights", userRightTo));

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    void IMasterUIBL.UpdateData(DataTO to)
    {
        new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..Program", to));
    }

    void IMasterUIBL.LoadData(DataTO to)
    {
        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..Program", to);

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
    }

    bool IMasterUIBL.IsDataExist(DataTO to)
    {
        return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("CACIDB..Program", to);
    }

    void IMasterUIBL.MarkData(DataTO to)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region 自訂功能

    public DataTable getProgramClass()
    {
        string sqlstr = "SELECT * FROM CACIDB..SysCode Where Sys_CdKind='R' AND Sys_CdType='K' ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    #endregion
}