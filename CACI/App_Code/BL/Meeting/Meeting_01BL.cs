using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Project_01BL 的摘要描述
/// </summary>
public class Meeting_01BL : ICommonBL, IQueryBL, IMMDUIBL
{
    public Meeting_01BL()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    void IQueryBL.DeleteData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..Meeting", to));
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..MtgTimes", to));
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..MtgCrew", to));
        string deleteSql = string.Format("DELETE FROM CACIDB..MtgCom WHERE Meeting_Code = '{0}'", to.getValue("Meeting_Code").ToString());
        cmds.Add(new SqlCommand(deleteSql));
        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    private SqlCommand getSqlCommand(DataTO to)
    {
        string sqlstr = "SELECT Rank() OVER(ORDER BY a.Meeting_Code) as SerialNo, d.Pj_Name " +
                        ",a.Meeting_Name, c.Mety_Name,a.Meeting_Code,dbo.chgToChnDate(a.Meeting_BgnTime) as Meeting_BgnTime ,a.Meeting_Code ,c.Mety_Code " +
                        ",dbo.chgToChnDate(a.Meeting_EndTime) as Meeting_EndTime , d.Pj_Kind FROM CACIDB..Meeting a " +
                        "LEFT JOIN CACIDB..UserAcc b  " +
                        "on a.Meeting_User_Code  = b.User_Code " +
                        "LEFT JOIN MeetingType c  " +
                        "on a.Meeting_Class = c.Mety_Code " +
                        "LEFT JOIN Project d " +
                        "on a.Pj_Code = d.Pj_Code " +
                        "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);
        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            //if (to.getAllColumnName()[i].IndexOf("Meeting_") != 0)
                //continue;
            if ("Meeting_Name".Equals(to.getAllColumnName()[i]) || "User_Name".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + " like '%" + to.getValue(to.getAllColumnName()[i]) + "%'";
            else if ("Meeting_BgnTime".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND Meeting_BgnTime >= '" + Meeting_01BL.chgChnDateToEnDate(to.getValue(to.getAllColumnName()[i]).ToString()) + " " + to.getValue("BgnHour").ToString() + ":" + to.getValue("BgnMin") + "'";
            else if ("Meeting_EndTime".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND Meeting_EndTime <= '" + Meeting_01BL.chgChnDateToEnDate(to.getValue(to.getAllColumnName()[i]).ToString()) + " " + to.getValue("EndHour").ToString() + ":" + to.getValue("EndMin") + "'";
            else if ("Pj_Name".Equals(to.getAllColumnName()[i]))
            {
                cmd.CommandText += " AND a.Pj_Code IN( SELECT Pj_Code FROM CACIDB..Project WHERE Pj_Name like '%' + @" + to.getAllColumnName()[i] + " + '%' )";
                cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }
            else
            {
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
                cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }
            System.Diagnostics.Debug.WriteLine("@" + to.getAllColumnName()[i] + ":" + to.getValue(to.getAllColumnName()[i]));
        }
        return cmd;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = getSqlCommand(to);
        cmd.CommandText += " Order By " + sortStr;
        try
        {
            new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = getSqlCommand(to);
        try
        {
            new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
        return dt;
    }

    void IMMDUIBL.InsertData(DataTO to, DataSet ds)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();
        string newMeetingCode = getNewSerialNo(DataBase.CACIDB, "ME");
        DateTime meetingBgnMin = DateTime.MinValue;
        DateTime meetingEngMax = DateTime.MaxValue;
        for (int i = 0; i < ds.Tables.Count; i++)
        {
            if (ds.Tables[i].TableName == "grv_MtgTimes")
            {
                DataView v = ds.Tables[i].DefaultView;
                v.Sort = "Times_End DESC";
                DataTable dt = v.ToTable();
                if (dt != null && dt.Rows.Count > 0)
                {
                    meetingEngMax = Convert.ToDateTime(Meeting_01BL.chgChnDateToEnDate(dt.Rows[0]["Times_End"].ToString().Split(' ')[0]) + " " + dt.Rows[0]["Times_End"].ToString().Split(' ')[1]);
                    v.Sort = "Times_Bgn ASC";
                    DataTable dt2 = v.ToTable();
                    meetingBgnMin = Convert.ToDateTime(Meeting_01BL.chgChnDateToEnDate(dt2.Rows[0]["Times_Bgn"].ToString().Split(' ')[0]) + " " + dt2.Rows[0]["Times_Bgn"].ToString().Split(' ')[1]);
                }
            }
            foreach (DataRow row in ds.Tables[i].Rows)
            {
                DataTO dto = new DataTO();
                foreach (DataColumn column in ds.Tables[i].Columns)
                {
                    if (column.ColumnName == "Times_End" || column.ColumnName == "Times_Bgn") // 場次
                    {
                        dto.setValue(column.ColumnName, Convert.ToDateTime(Meeting_01BL.chgChnDateToEnDate(row[column.ColumnName].ToString().Split(' ')[0]) + " " + row[column.ColumnName].ToString().Split(' ')[1]));
                    }
                    else if (column.ColumnName == "Comm_Name" || column.ColumnName == "Comm_ComName" || column.ColumnName == "Ski_Name") //顧問委員
                        continue;
                    else if (column.ColumnName == "Com_Name" || column.ColumnName == "Comm_Name" || column.ColumnName == "ChKd_Name")// 申請單位
                        continue;
                    else
                        dto.setValue(column.ColumnName, row[column.ColumnName].ToString());
                }
                dto.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
                switch (ds.Tables[i].TableName)
                {
                    case "grv_MtgTimes": // 場次
                        dto.setValue("Meeting_Code", newMeetingCode);
                        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..MtgTimes", dto));
                        break;
                    case "grv_Committee": // 顧問委員
                        dto.setValue("Meeting_Code", newMeetingCode);
                        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..MtgCrew", dto));
                        break;
                    case "grv_Company": // 申請單位
                        dto.setValue("Meeting_Code", newMeetingCode);
                        dto.setValue("MtgCom", getNewSerialNo(DataBase.CACIDB, "MC"));
                        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..MtgCom", dto));
                        break;
                    default:
                        break;
                }
            }
        }
        to.setValue("Meeting_Code", newMeetingCode);
        to.setValue("Meeting_BgnTime", meetingBgnMin);
        to.setValue("Meeting_EndTime", meetingEngMax);
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Meeting", to));

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    bool IMMDUIBL.IsDataExist(DataTO to)
    {
        if (to.isColumnExist("Meeting_Code"))
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Meeting", to);
        else
            return false;
    }

    void IMMDUIBL.LoadData(DataTO to, DataSet ds)
    {
        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Meeting", to);
        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(cmd);
        if (sr.Read())
        {
            for (int i = 0; i < sr.FieldCount; i++)
            {
                if (!to.isColumnExist(sr.GetName(i)))
                    to.setValue(sr.GetName(i), sr[sr.GetName(i)].ToString());
            }
        }
        //場次
        DataTable dtMtgTimes = new DataTable("grv_MtgTimes");
        string dsqlstr = "SELECT Meeting_Index,Times_Place " +
                         ",dbo.chgToChnDate(Times_Bgn) + ' ' + SUBSTRING(CONVERT(VARCHAR(10),Times_Bgn,108),0,6) AS Times_Bgn " +
                         ",dbo.chgToChnDate(Times_End) + ' ' + SUBSTRING(CONVERT(VARCHAR(10),Times_End,108),0,6) AS Times_End " +
                         " FROM CACIDB..MtgTimes " +
                         "WHERE Meeting_Code = @Meeting_Code ";
        cmd = new SqlCommand(dsqlstr);
        cmd.Parameters.AddWithValue("@Meeting_Code", to.getValue("Meeting_Code"));
        new SQLAgent(DataBase.CACIDB).select(cmd, dtMtgTimes);
        ds.Tables.Add(dtMtgTimes);
        //顧問委員
        DataTable dtCommittee = new DataTable("grv_Committee");
        string dsqlstr2 = "SELECT Meeting_Index,a.Comm_Code,b.Comm_Name, " +
                          "b.Comm_Title,b.Comm_ComName,STUFF ( (  " +
                          "SELECT N'、 ' + Skill_Note FROM CACIDB..CommSKill b WHERE b.Comm_Code = a.Comm_Code  " +
                          "FOR XML PATH ('') ),1,1,'' ) AS Ski_Name  " +
                          "FROM CACIDB..MtgCrew a " +
                          "LEFT JOIN CACIDB.dbo.Committee b " +
                          "ON a.Comm_Code = b.Comm_Code " +
                          "WHERE Meeting_Code = @Meeting_Code ";
        cmd = new SqlCommand(dsqlstr2);
        cmd.Parameters.AddWithValue("@Meeting_Code", to.getValue("Meeting_Code"));
        new SQLAgent(DataBase.CACIDB).select(cmd, dtCommittee);
        ds.Tables.Add(dtCommittee);
        //公司單位
        //透過Meeting_Code找出Pj_Code,並查詢Project是獎補助還是輔導
        DataTO queryTo = new DataTO();
        queryTo.setValue("Pj_Code", to.getValue("Pj_Code").ToString());
        DataTable pjTable = new BaseFun().getTableData("Project", queryTo);
        DataTable dtCompany = new DataTable("grv_Company");
        string dsqlstr3 = string.Empty;
        if (pjTable.Rows.Count > 0 && pjTable.Rows[0]["Pj_Kind"].ToString() == "B")
        {
            dsqlstr3 = "SELECT DISTINCT a.Meeting_Index,a.Com_Code ,b.Com_Name,d.ChKd_Name  ,e.Comm_Code,e.Comm_Name " +
                              "FROM CACIDB..MtgCom a " +
                              "LEFT JOIN CACIDB.dbo.Company b " +
                              "ON a.Com_Code = b.Com_Code " +
                              "LEFT JOIN CACIDB..Coach c " +
                              "ON b.Com_Code = c.Com_Code " +
                              "LEFT JOIN CACIDB..CoachKind d " +
                              "ON c.ChKd_Code = d.ChKd_Code " +
                              "LEFT JOIN CACIDB..Committee e " +
                              "ON a.Comm_Code = e.Comm_Code " +
                              "WHERE a.Meeting_Code = @Meeting_Code ";
            cmd = new SqlCommand(dsqlstr3);
            cmd.Parameters.AddWithValue("@Meeting_Code", to.getValue("Meeting_Code"));
            new SQLAgent(DataBase.CACIDB).select(cmd, dtCompany);
        }
        else
        {
            dsqlstr3 = "SELECT a.Meeting_Index,a.Com_Code ,b.Com_Name " +
                              ",STUFF ( ( SELECT N'、 ' + dbo.getSysCodeText('P','G',f.CmGp_Num) " +
                              "FROM CACIDB..CommGroup f WHERE f.Pj_Code = c.Pj_Code " +
                              "FOR XML PATH ('') ),1,1,'' ) AS ChKd_Name " +
                              "FROM CACIDB..MtgCom a   " +
                              "LEFT JOIN CACIDB.dbo.Meeting e " +
                              "ON a.Meeting_Code = e.Meeting_Code " +
                              "LEFT JOIN CACIDB.dbo.Company b " +
                              "ON a.Com_Code = b.Com_Code " +
                              "LEFT JOIN CACIDB..Allowance c " +
                              "ON b.Com_Code = c.Com_Code  AND e.Pj_Code = c.Pj_Code " +
                              "WHERE Comm_Code IS NULL AND a.Meeting_Code = @Meeting_Code ";
            cmd = new SqlCommand(dsqlstr3);
            cmd.Parameters.AddWithValue("@Meeting_Code", to.getValue("Meeting_Code"));
            new SQLAgent(DataBase.CACIDB).select(cmd, dtCompany);
        }
        ds.Tables.Add(dtCompany);
    }

    DataTable IMMDUIBL.QueryDetailData(string QueryGridViewID, DataTO to)
    {
        DataTable dt = new DataTable();

        switch (QueryGridViewID)
        {
            case "grv_SearchCommittee":
                Committee_01BL cmBl = new Committee_01BL();
                dt = ((IQueryBL)cmBl).QueryDataForList(to);
                break;
            case "grv_SearchCompany":
                if(to.getValue("Pj_Kind").ToString() == "A")
                    dt = SearchAllowanceCompany(to);
                else if (to.getValue("Pj_Kind").ToString() == "B")
                    dt = SearchCoachCompany(to);
                break;
            default:
                break;
        }

        return dt;
    }

    void IMMDUIBL.UpdateData(DataTO to, DataSet ds)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..MtgTimes", to));
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..MtgCrew", to));
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..MtgCom", to));
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..Meeting", to));
        DateTime meetingBgnMin = DateTime.MinValue;
        DateTime meetingEngMax = DateTime.MaxValue;
        string newMeetingCode = to.getValue("Meeting_Code").ToString();
        for (int i = 0; i < ds.Tables.Count; i++)
        {
            if (ds.Tables[i].TableName == "grv_MtgTimes")
            {
                DataView v = ds.Tables[i].DefaultView;
                v.Sort = "Times_End DESC";
                DataTable dt = v.ToTable();
                if(dt != null && dt.Rows.Count > 0)
                {
                    meetingEngMax = Convert.ToDateTime(Meeting_01BL.chgChnDateToEnDate(dt.Rows[0]["Times_End"].ToString().Split(' ')[0]) + " " + dt.Rows[0]["Times_End"].ToString().Split(' ')[1]);
                    v.Sort = "Times_Bgn ASC";
                    DataTable dt2 = v.ToTable();
                    meetingBgnMin = Convert.ToDateTime(Meeting_01BL.chgChnDateToEnDate(dt2.Rows[0]["Times_Bgn"].ToString().Split(' ')[0]) + " " + dt2.Rows[0]["Times_Bgn"].ToString().Split(' ')[1]);
                }
            }
            foreach (DataRow row in ds.Tables[i].Rows)
            {
                DataTO dto = new DataTO();
                foreach (DataColumn column in ds.Tables[i].Columns)
                {
                    if (column.ColumnName == "Times_End" || column.ColumnName == "Times_Bgn") // 場次
                    {
                        dto.setValue(column.ColumnName, Convert.ToDateTime(Meeting_01BL.chgChnDateToEnDate(row[column.ColumnName].ToString().Split(' ')[0]) + " " + row[column.ColumnName].ToString().Split(' ')[1]));
                    }
                    else if (column.ColumnName == "Comm_Name" || column.ColumnName == "Comm_ComName" || column.ColumnName == "Ski_Name") //顧問委員
                        continue;
                    else if (column.ColumnName == "Com_Name" || column.ColumnName == "ChKd_Name" || column.ColumnName == "Comm_Name")// 申請單位
                        continue;
                    else
                        dto.setValue(column.ColumnName, row[column.ColumnName].ToString());
                }
                dto.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
                switch (ds.Tables[i].TableName)
                {
                    case "grv_MtgTimes": // 場次
                        dto.setValue("Meeting_Code", newMeetingCode);
                        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..MtgTimes", dto));
                        break;
                    case "grv_Committee": // 顧問委員
                        dto.setValue("Meeting_Code", newMeetingCode);
                        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..MtgCrew", dto));
                        break;
                    case "grv_Company": // 申請單位
                        dto.setValue("Meeting_Code", newMeetingCode);
                        dto.setValue("MtgCom", getNewSerialNo(DataBase.CACIDB, "MC"));
                        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..MtgCom", dto));
                        break;
                    default:
                        break;
                }
            }
        }
        try
        {
            new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }

    private DataTable SearchCoachCompany(DataTO to)
    {
        DataTable dt = new DataTable();
        string sqlStr = "SELECT DISTINCT a.Com_Code , c.Com_Name,b.ChKd_Name " +
                     "FROM CACIDB..Coach a " +
                     "LEFT JOIN CACIDB..CoachKind b " +
                     "ON a.ChKd_Code = b.ChKd_Code " +
                     "LEFT JOIN CACIDB..Company c " +
                     "ON a.Com_Code = c.Com_Code " +
                     "WHERE 1=1 ";
        SqlCommand cmd = new SqlCommand(sqlStr);
        foreach (string item in to.getAllColumnName())
        {
            if(item != "Pj_Kind")
                cmd.CommandText += " AND " + item + " like '%" + to.getValue(item) + "%'";
        }
        System.Diagnostics.Debug.WriteLine(cmd.CommandText);
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }

    private DataTable SearchAllowanceCompany(DataTO to)
    {
        DataTable dt = new DataTable();
        //string sqlStr = "SELECT  DISTINCT a.Com_Code , b.Com_Name , dbo.getSysCodeText('P','G',c.CmGp_Num) as Item " +
        //             "FROM CACIDB..Allowance a " +
        //             "LEFT JOIN CACIDB..Company b " +
        //             "ON a.Com_Code = b.Com_Code " +
        //             "LEFT JOIN CACIDB.dbo.CommGroup c " +
        //             "ON a.Pj_Code = c.Pj_Code " +
        //             "WHERE 1=1 ";
        string sqlStr = "SELECT DISTINCT a.Com_Code , b.Com_Name " +
                     ",STUFF ( ( SELECT N'、 ' + dbo.getSysCodeText('P','G',c.CmGp_Num) " +
                     "FROM CACIDB..CommGroup c WHERE c.Pj_Code = a.Pj_Code " +
                     "FOR XML PATH ('') ),1,1,'' ) AS ChKd_Name " +
                     "FROM CACIDB..Allowance a  " +
                     "LEFT JOIN CACIDB..Company b " +
                     "ON a.Com_Code = b.Com_Code " +
                     "WHERE 1=1 ";
        SqlCommand cmd = new SqlCommand(sqlStr);
        foreach (string item in to.getAllColumnName())
        {
            if (item != "Pj_Kind")
            {
                if(item == "Pj_Code")
                    cmd.CommandText += " AND a.Pj_Code = '" + to.getValue(item) + "'";
                else
                    cmd.CommandText += " AND " + item + " like '%" + to.getValue(item) + "%'";
            }
        }
        System.Diagnostics.Debug.WriteLine(cmd.CommandText);
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }
}