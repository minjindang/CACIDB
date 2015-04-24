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
public class Announcement_04BL : ICommonBL, IMDUIBL
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
        string sqlstr = "SELECT a.Pj_Code,b.Stage_Index,a.Pj_Name + '_' + b.Stage_Name + '_階段通知' Subject,DateAdd(d,-1 * b.Stage_RmDays ,b.Stage_Date) BgnTime,Stage_RmText,a.Pj_Kind " +
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
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        throw new NotImplementedException();
    }

    #endregion
}