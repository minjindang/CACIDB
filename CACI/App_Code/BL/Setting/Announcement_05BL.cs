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
public class Announcement_05BL : ICommonBL, IMDUIBL
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
        string sqlstr = "SELECT DISTINCT a.Meeting_Code,a.Meeting_Name,a.Meeting_BgnTime,count(b.Meeting_Index) mtgTimes " +
                            "FROM CACIDB..Meeting a LEFT OUTER JOIN CACIDB..MtgTimes b ON a.Meeting_Code=b.Meeting_Code " +
                            "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        if (to.getValue("User_Code").ToString() != "admin")
        {
            cmd.CommandText += " AND Meeting_User_Code=@Meeting_User_Code";
            cmd.Parameters.AddWithValue("@Meeting_User_Code", to.getValue("User_Code"));
        }

        cmd.CommandText += "GROUP BY a.Meeting_Code,a.Meeting_Name,a.Meeting_BgnTime " +
                           "Having count(b.Meeting_Index) = 0 ";

        cmd.CommandText += "Order By Meeting_BgnTime DESC ";

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        throw new NotImplementedException();
    }

    #endregion
}