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
public class Announcement_01BL : ICommonBL, IQueryBL, IMasterUIBL
{
    #region IQueryBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT RANK() OVER(Order By Ann_Code) AS Serial,*,ISNULL(dbo.getSysCodeText('A','K',Ann_Type ) ,'無類別') as  Ann_TypeName, dbo.udfTaiwanDateFormat(Ann_BgnTime,'yyy/mm/dd') as twAnn_BgnTime " +
                        "FROM CACIDB..Announcement " +
                        "WHERE 1=1" ;

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            if (to.getAllColumnName()[i] == "UsDp_Code")
                continue;
            else if (to.getAllColumnName()[i] == "Ann_BgnTime")
            {
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + " >= @" + to.getAllColumnName()[i];
                cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }
            else if (to.getAllColumnName()[i] == "Ann_EndTime")
            {
                cmd.CommandText += " AND Ann_BgnTime <= @" + to.getAllColumnName()[i];
                cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }
            else
            {
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
                cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }
        }

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT RANK() OVER(Order By Ann_Code) AS Serial,*,ISNULL(dbo.getSysCodeText('A','K',Ann_Type ) ,'無類別') as  Ann_TypeName, dbo.udfTaiwanDateFormat(Ann_BgnTime,'yyy/mm/dd') as twAnn_BgnTime " +
                        "FROM CACIDB..Announcement " +
                        "WHERE 1=1";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            if (to.getAllColumnName()[i] == "UsDp_Code")
                continue;
            else if (to.getAllColumnName()[i] == "Ann_BgnTime")
            {
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + " >= @" + to.getAllColumnName()[i];
                cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }
            else if (to.getAllColumnName()[i] == "Ann_EndTime")
            {
                cmd.CommandText += " AND Ann_BgnTime <= @" + to.getAllColumnName()[i];
                cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }
            else
            {
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
                cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }
        }

        cmd.CommandText += " Order By " + sortStr;

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    void IQueryBL.DeleteData(DataTO to)
    {
        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..Announcement", to);

        new SQLAgent(DataBase.CACIDB).execute(cmd);
    }

    #endregion

    #region IMasterUIBL 成員

    void IMasterUIBL.InsertData(DataTO to)
    {
        to.setValue("Ann_Code", ICommonBL.getNewSerialNo(DataBase.CACIDB, "AC"));

        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Announcement", to);

        new SQLAgent(DataBase.CACIDB).execute(cmd);
    }

    void IMasterUIBL.UpdateData(DataTO to)
    {
        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..Announcement", to);

        new SQLAgent(DataBase.CACIDB).execute(cmd);
    }

    void IMasterUIBL.LoadData(DataTO to)
    {
        //SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Announcement", to);

        string sqlstr = "SELECT a.*,b.Sys_CdText Ann_Type_val " +
                        "FROM CACIDB..Announcement a JOIN CACIDB..SysCode b ON a.Ann_Type=b.Sys_CdCode AND b.Sys_CdKind='A' AND b.Sys_CdType='K' " +
                        "WHERE 1=1";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            cmd.CommandText += " AND a." + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
            cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));

        }

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
        if (!to.isColumnExist("Ann_Code"))
            return false;
        return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("CACIDB..Announcement", to);
    }

    void IMasterUIBL.MarkData(DataTO to)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    #region 自訂動作

    public DataTable getActiveProject()
    {
        string sqlstr = "SELECT a.Pj_Code,a.Pj_Name " +
                        ",(SELECT TOP 1 b.Stage_Date FROM CACIDB..PjStage b WHERE a.Pj_Code=b.Pj_Code ) AS Last_Stage_Date " +
                        "FROM CACIDB..Project a " +
                        "WHERE (SELECT TOP 1 b.Stage_Date FROM CACIDB..PjStage b WHERE a.Pj_Code=b.Pj_Code ) > getDate()";

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(new SqlCommand(sqlstr), dt);

        return dt;
    }
    
    #endregion
}