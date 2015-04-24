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
public class UserDep_01BL : ICommonBL, IQueryBL, IMasterUIBL
{
    #region IQueryBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT * " +
                        "FROM CACIDB..UserDep " +
                        "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {

                case "UsDp_Code":
                    cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;

                case "UsDp_Name":
                    cmd.CommandText += " AND UserDep." + to.getAllColumnName()[i] + " like '%' + @" + to.getAllColumnName()[i] + " + '%'";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;

                   
            }
            
            
            
        }

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT * " +
                        "FROM CACIDB..UserDep " +
                        "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {

                case "UsDp_Code":
                    cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;

                case "UsDp_Name":
                    cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
            }
        }

        cmd.CommandText += " ORDER BY " + sortStr;

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
        new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..UserDep", to));
    }

    void IMasterUIBL.UpdateData(DataTO to)
    {
        new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..UserDep", to));
    }

    void IMasterUIBL.LoadData(DataTO to)
    {
        to.removeValue("Rec_Info");
        to.removeValue("Rec_InfoID");
        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..UserDep", to);
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
        return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("CACIDB..UserDep", to);
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