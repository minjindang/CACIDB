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
public class Announcement_02BL : ICommonBL, IMMDUIBL
{
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

    #region IMMDUIBL 成員

    void IMMDUIBL.InsertData(DataTO to, DataSet ds)
    {
        throw new NotImplementedException();
    }

    bool IMMDUIBL.IsDataExist(DataTO to)
    {
        return to.isColumnExist("User_Code");
    }

    void IMMDUIBL.LoadData(DataTO to, DataSet ds)
    {
        if (!ds.Tables.Contains("grv_Announcement"))
        {
            DataTable dt = new DataTable("grv_Announcement");

            string sqlstr = "SELECT TOP 3 Ann_Code,Ann_Name,Ann_BgnTime " +
                            "FROM CACIDB..Announcement " +
                            "WHERE Ann_Type='S' AND Ann_EndTime > getDate() ";

            SqlCommand cmd = new SqlCommand(sqlstr);

            cmd.CommandText += "Order By Ann_BgnTime DESC ";

            new SQLAgent(DataBase.CACIDB).select(cmd, dt);

            ds.Tables.Add(dt);
        }

        if (!ds.Tables.Contains("grv_Stage"))
        {
            DataTable dt = new DataTable("grv_Stage");

            string sqlstr = "SELECT TOP 4 a.Pj_Code,b.Stage_Index,a.Pj_Name + '_' + b.Stage_Name + '_階段通知' Subject,DateAdd(d,-1 * b.Stage_RmDays ,b.Stage_Date) BgnTime,Stage_RmText,a.Pj_Kind " +
                            "FROM CACIDB..Project a JOIN CACIDB..PjStage b ON a.Pj_Code=b.Pj_Code " +
                            "WHERE b.Stage_RmFlag = 'Y' " +
                                  "AND DateAdd(d,-1 * b.Stage_RmDays ,b.Stage_Date) <= getDate() AND b.Stage_Date >= getDate() " +
                                  "AND DateAdd(d,-1 * b.Stage_RmDays ,b.Stage_Date) is not null";

            // TODO:增加輔導專案資料

            SqlCommand cmd = new SqlCommand(sqlstr);

            if (to.getValue("User_Code").ToString() != "admin")
            {
                cmd.CommandText += " AND SUBSTRING(b.Stage_RmEmpl,2,1) = '1' AND a.Pj_User_Code=@Pj_User_Code ";
                cmd.Parameters.AddWithValue("@Pj_User_Code", to.getValue("User_Code"));
            }
            new SQLAgent(DataBase.CACIDB).select(cmd, dt);

            ds.Tables.Add(dt);
        }

        if (!ds.Tables.Contains("grv_Metting"))
        {
            DataTable dt = new DataTable("grv_Metting");

            string sqlstr = "SELECT DISTINCT TOP 4 a.Meeting_Code,a.Meeting_Name,a.Meeting_BgnTime,count(b.Meeting_Index) mtgTimes " +
                            "FROM CACIDB..Meeting a LEFT OUTER JOIN CACIDB..MtgTimes b ON a.Meeting_Code=b.Meeting_Code " +
                            "WHERE 1=1 ";
            
            SqlCommand cmd = new SqlCommand(sqlstr);

            if (to.getValue("User_Code").ToString() != "admin")
            {
                cmd.CommandText += " AND Meeting_User_Code=@Meeting_User_Code ";
                cmd.Parameters.AddWithValue("@Meeting_User_Code", to.getValue("User_Code"));
            }

            cmd.CommandText += "GROUP BY a.Meeting_Code,a.Meeting_Name,a.Meeting_BgnTime " +
                               "Having count(b.Meeting_Index) = 0 ";

            cmd.CommandText += "Order By Meeting_BgnTime DESC ";

            new SQLAgent(DataBase.CACIDB).select(cmd, dt);

            ds.Tables.Add(dt);
        }

        if (!ds.Tables.Contains("grv_Judge"))
        {
            DataTable dt = new DataTable("grv_Judge");

            string sqlstr = "SELECT DISTINCT TOP 4 a.Pj_Code,a.Pj_Name,a.Pj_StartDate,count(b.Comm_Code) pj_Judge " +
                            "FROM CACIDB..Project a LEFT OUTER JOIN CACIDB..PjJudge b ON a.Pj_Code=b.Pj_Code " +
                            "WHERE a.Pj_Kind = 'A' ";

            SqlCommand cmd = new SqlCommand(sqlstr);

            if (to.getValue("User_Code").ToString() != "admin")
            {
                cmd.CommandText += " AND a.Pj_User_Code=@Pj_User_Code ";
                cmd.Parameters.AddWithValue("@Pj_User_Code", to.getValue("User_Code"));
            }

            cmd.CommandText += "GROUP BY a.Pj_Code,a.Pj_Name,a.Pj_StartDate " +
                               "Having count(b.Comm_Code) = 0 ";

            cmd.CommandText += "Order By Pj_StartDate ASC ";

            new SQLAgent(DataBase.CACIDB).select(cmd, dt);

            ds.Tables.Add(dt);
        }
    }

    DataTable IMMDUIBL.QueryDetailData(string QueryGridViewID, DataTO to)
    {
        throw new NotImplementedException();
    }

    void IMMDUIBL.UpdateData(DataTO to, DataSet ds)
    {
        throw new NotImplementedException();
    }

    #endregion
}