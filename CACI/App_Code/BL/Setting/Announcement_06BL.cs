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
public class Announcement_06BL : ICommonBL, IMDUIBL
{
    #region IMDUIBL 成員

    void IMDUIBL.InsertData(DataTO to, DataTable dt)
    {
        throw new NotImplementedException();
    }

    bool IMDUIBL.IsDataExist(DataTO to)
    {
        return to.isColumnExist("User_Code");
    }

    void IMDUIBL.LoadData(DataTO to, DataTable dt)
    {
        string sqlstr = "SELECT DISTINCT a.Pj_Code,a.Pj_Name,a.Pj_StartDate,count(b.Comm_Code) pj_Judge " +
                            "FROM CACIDB..Project a LEFT OUTER JOIN CACIDB..PjJudge b ON a.Pj_Code=b.Pj_Code " +
                            "WHERE a.Pj_Kind = 'A' ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        if (to.getValue("User_Code").ToString() != "admin")
        {
            cmd.CommandText += " AND a.Pj_User_Code=@Pj_User_Code";
            cmd.Parameters.AddWithValue("@Pj_User_Code", to.getValue("User_Code"));
        }

        cmd.CommandText += "GROUP BY a.Pj_Code,a.Pj_Name,a.Pj_StartDate " +
                           "Having count(b.Comm_Code) = 0 ";

        cmd.CommandText += "Order By Pj_StartDate ASC ";

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        throw new NotImplementedException();
    }

    #endregion
}