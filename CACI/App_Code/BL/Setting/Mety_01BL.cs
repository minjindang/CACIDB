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
public class Mety_01BL : ICommonBL, IQueryBL, IMasterUIBL
{
    #region IQueryBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();

        //SqlCommand cmd = new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("CACIDB..MeetingType", to);

        string sqlstr = "SELECT a.Mety_Code,b.Sys_CdText Pj_Kind,Mety_Name,CASE Mety_CanAdd WHEN 'Y' THEN '是' ELSE '否' END Mety_CanAdd " +
                        "FROM CACIDB..MeetingType a JOIN CACIDB..SysCode b ON a.Pj_Kind=b.Sys_CdCode AND b.Sys_CdKind='P' AND b.Sys_CdType='K' " + 
                        "WHERE 1=1";
        
        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
            cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
        }

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT a.Mety_Code,b.Sys_CdText Pj_Kind,Mety_Name,CASE Mety_CanAdd WHEN 'Y' THEN '是' ELSE '否' END Mety_CanAdd " +
                        "FROM CACIDB..MeetingType a JOIN CACIDB..SysCode b ON a.Pj_Kind=b.Sys_CdCode AND b.Sys_CdKind='P' AND b.Sys_CdType='K' " +
                        "WHERE 1=1";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
            cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
        }

        cmd.CommandText += " Order By " + sortStr;

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    void IQueryBL.DeleteData(DataTO to)
    {
        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..MeetingType", to);

        new SQLAgent(DataBase.CACIDB).execute(cmd);
    }

    #endregion

    #region IMasterUIBL 成員

    void IMasterUIBL.InsertData(DataTO to)
    {
        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..MeetingType", to);

        new SQLAgent(DataBase.CACIDB).execute(cmd);
    }

    void IMasterUIBL.UpdateData(DataTO to)
    {
        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..MeetingType", to);

        new SQLAgent(DataBase.CACIDB).execute(cmd);
    }

    void IMasterUIBL.LoadData(DataTO to)
    {
        DataTO sto = new DataTO();

        sto.setValue("Mety_Code", to.getValue("Mety_Code"));
        
        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..MeetingType", sto);

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

        sr.Close();
    }

    bool IMasterUIBL.IsDataExist(DataTO to)
    {
        return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("CACIDB..MeetingType", to);
    }

    void IMasterUIBL.MarkData(DataTO to)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    #region 自訂動作

    public DataTable getDetailData(DataTO to)
    {
        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Detail_1", to), dt);

        return dt;
    }

    public DataTable getPj_Kind_List()
    {
        string sqlstr = "SELECT * FROM CACIDB..SysCode WHERE Sys_CdKind='P' AND Sys_CdType='K' ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }
    #endregion
}