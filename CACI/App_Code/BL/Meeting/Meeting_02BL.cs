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
public class Meeting_02BL : ICommonBL, IMMDUIBL
{
    public Meeting_02BL()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
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
        string sqlStr = "SELECT Meeting_Code,Meeting_Name ,Meeting_User_Code ,Pj_Code " +
                        ",dbo.chgToChnDate(Meeting_BgnTime) + ' ' + SUBSTRING(CONVERT(VARCHAR(10),Meeting_BgnTime,108),0,6) AS Meeting_BgnTime " +
                        ",dbo.chgToChnDate(Meeting_EndTime) + ' ' + SUBSTRING(CONVERT(VARCHAR(10),Meeting_EndTime,108),0,6) AS Meeting_EndTime " +
                        "FROM CACIDB..Meeting WHERE Meeting_Code = @Meeting_Code ";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@Meeting_Code", to.getValue("Meeting_Code").ToString());
        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(cmd);
        if (sr.Read())
        {
            for (int i = 0; i < sr.FieldCount; i++)
            {
                if (!to.isColumnExist(sr.GetName(i)))
                    to.setValue(sr.GetName(i), sr[sr.GetName(i)].ToString());
            }
        }
        DataTO queryProject = new DataTO();
        queryProject.setValue("Pj_Code", to.getValue("Pj_Code").ToString());
        DataTable prjectDT = new DataTable();
        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Project", queryProject), prjectDT);
        if (prjectDT != null && prjectDT.Rows.Count > 0)
        {
            foreach (DataColumn item in prjectDT.Columns)
            {
                if (!to.isColumnExist(item.ColumnName))
                    to.setValue(item.ColumnName, prjectDT.Rows[0][item.ColumnName].ToString());
            }
        }
        //會議記錄內容
        DataTable dtMtgTimes1 = new DataTable("grv_MtgTimes1");
        string dsqlstr = "SELECT Meeting_Index,Times_Place,Meeting_Code " +
                         ",dbo.chgToChnDate(Times_Bgn) + ' ' + SUBSTRING(CONVERT(VARCHAR(10),Times_Bgn,108),0,6) AS Times_Bgn " +
                         ",dbo.chgToChnDate(Times_End) + ' ' + SUBSTRING(CONVERT(VARCHAR(10),Times_End,108),0,6) AS Times_End " +
                         " FROM CACIDB..MtgTimes " +
                         "WHERE Meeting_Code = @Meeting_Code ";
        cmd = new SqlCommand(dsqlstr);
        cmd.Parameters.AddWithValue("@Meeting_Code", to.getValue("Meeting_Code"));
        new SQLAgent(DataBase.CACIDB).select(cmd, dtMtgTimes1);
        ds.Tables.Add(dtMtgTimes1);
        //委員紀錄
        DataTable dtMtgTimes2 = new DataTable("grv_MtgTimes2");
        new SQLAgent(DataBase.CACIDB).select(cmd, dtMtgTimes2);
        ds.Tables.Add(dtMtgTimes2);
    }

    DataTable IMMDUIBL.QueryDetailData(string QueryGridViewID, DataTO to)
    {
        DataTable dt = new DataTable();
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

    public void insertToMtgRecord(DataTO to)
    {
        new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("MtgRecord", to));
    }

    public void updateToMtgRecord(DataTO to)
    {
        new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("MtgRecord", to));
    }

    public void insertToEvaluations(DataTO to)
    {
        new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Evaluations", to));
    }

    public void updateToEvaluations(DataTO to)
    {
        new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Evaluations", to));
    }
}