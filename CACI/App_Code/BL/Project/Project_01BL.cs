using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;
using System.Data;
using System.Data.SqlClient;
using System.IO;

/// <summary>
/// Project_01BL 的摘要描述
/// </summary>
public class Project_01BL : ICommonBL, IQueryBL, IMMDUIBL, IMDUIBL
{
    #region IQueryBL 成員

    void IQueryBL.DeleteData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..Project", to));

        DataTO pjStage = new DataTO();
        pjStage.setValue("Pj_Code", to.getValue("Pj_Code").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..PjStage", pjStage));

        DataTO score = new DataTO();
        score.setValue("Score_PjCode", to.getValue("Pj_Code").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..Score", score));

        DataTO commGroup = new DataTO();
        commGroup.setValue("Pj_Code", to.getValue("Pj_Code").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..CommGroup", commGroup));

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT Rank() over(order by Pj_Code) as Serial ,Pj_Code ,Pj_Name , Pj_StartDate ,Pj_Kind ,Pj_BgnDate  " +
                        "FROM CACIDB..Project " +
                        "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            if ("Pj_Name".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + " like '%" + to.getValue(to.getAllColumnName()[i]) + "%'";
            else if ("Pj_BgnDate".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND Pj_BgnDate >= " + to.getValue(Project_01BL.chgEnDateToChnDate(to.getAllColumnName()[i]));
            else if ("Pj_FinishDate".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND Pj_BgnDate <= " + to.getValue(Project_01BL.chgEnDateToChnDate(to.getAllColumnName()[i]));
            else if ("Pj_StartDate".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND Pj_StartDate >= " + to.getValue(Project_01BL.chgEnDateToChnDate(to.getAllColumnName()[i]));
            else if ("Pj_EndDate".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND Pj_StartDate <= " + to.getValue(Project_01BL.chgEnDateToChnDate(to.getAllColumnName()[i]));
            else
            {
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
                cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }
            System.Diagnostics.Debug.WriteLine("@" + to.getAllColumnName()[i] + ":" + to.getValue(to.getAllColumnName()[i]));
        }
        cmd.CommandText += " Order By " + sortStr;
        System.Diagnostics.Debug.WriteLine(cmd.CommandText);
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

        string sqlstr = "SELECT Rank() over(order by Pj_Code) as Serial ,Pj_Code ,Pj_Name , Pj_StartDate ,Pj_Kind ,Pj_BgnDate " +
                        "FROM CACIDB..Project " +
                        "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            if ("Pj_Name".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + " like '%" + to.getValue(to.getAllColumnName()[i]) + "%'";
            else if ("Pj_BgnDate".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND Pj_BgnDate >= " + Project_01BL.chgEnDateToChnDate(to.getValue(to.getAllColumnName()[i]).ToString());
            else if ("Pj_FinishDate".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND Pj_BgnDate <= " + Project_01BL.chgEnDateToChnDate(to.getValue(to.getAllColumnName()[i]).ToString());
            else if ("Pj_StartDate".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND Pj_StartDate >= " + Project_01BL.chgEnDateToChnDate(to.getValue(to.getAllColumnName()[i]).ToString());
            else if ("Pj_EndDate".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND Pj_StartDate <= " + Project_01BL.chgEnDateToChnDate(to.getValue(to.getAllColumnName()[i]).ToString());
            else
            {
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
                cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }
            System.Diagnostics.Debug.WriteLine("@" + to.getAllColumnName()[i] + ":" + to.getValue(to.getAllColumnName()[i]));
        }
        System.Diagnostics.Debug.WriteLine(cmd.CommandText);
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

    #endregion

    #region IMMDUIBL(獎補助專案) 成員

    void IMMDUIBL.InsertData(DataTO to, DataSet ds)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        string newPjCode = getNewSerialNo(DataBase.CACIDB, "JS");
        to.setValue("Pj_Code", newPjCode);

        if (to.isColumnExist("Pj_PjFile"))
        {
            string defaultPath = HttpContext.Current.Request.PhysicalApplicationPath + @"UploadFile\Project\" + to.getValue("Pj_Code").ToString();

            File.Move(to.getValue("Pj_PjFile").ToString(), Path.Combine(defaultPath, new FileInfo(to.getValue("Pj_PjFile").ToString()).Name));

            to.updateValue("Pj_PjFile", "/CACI/UploadFile/Project/" + to.getValue("Pj_Code").ToString() + "/" + new FileInfo(to.getValue("Pj_PjFile").ToString()).Name);
        }

        to.setValue("Pj_IsJgroup", "N");

        for (int i = 0; i < ds.Tables.Count; i++)
        {
            foreach (DataRow row in ds.Tables[i].Rows)
            {
                DataTO dto = new DataTO();

                foreach (DataColumn column in ds.Tables[i].Columns)
                {
                    if ((column.ColumnName == "Stage_Days" || column.ColumnName == "Stage_RmDays") && row[column.ColumnName].ToString() == "")
                        continue;
                    else if (column.ColumnName == "CmGp_NumName")
                        continue;
                    else if ("Stage_Date" == column.ColumnName)
                    {
                        if (row["Stage_Date"].ToString() == "N/A")
                            continue;
                        else
                            dto.setValue("Stage_Date", Project_01BL.chgChnDateToEnDate(row["Stage_Date"].ToString()));
                    }
                    else
                        dto.setValue(column.ColumnName, row[column.ColumnName].ToString());
                }

                dto.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());

                switch(ds.Tables[i].TableName)
                {
                    case "grv_Score": // 評分項目
                        dto.setValue("Score_PjCode", to.getValue("Pj_Code").ToString());
                        if (dto.isColumnExist("Score_Code"))
                            dto.updateValue("Score_Code", getNewSerialNo(DataBase.CACIDB,"JC"));
                        else
                            dto.setValue("Score_Code", getNewSerialNo(DataBase.CACIDB, "JC"));
                        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Score", dto));
                        break;
                    case "grv_PjStage" : // 階段
                        dto.setValue("Pj_Code", to.getValue("Pj_Code").ToString());
                        
                        /*
                         * 若勾選提醒，則自動增加公告(Announcement) Ann_Type='A'
                         * 若需開會，則自動增加會議(Meeting) Meeting_Class='AR' (審查)  or  'AT'(輔導)
                         * 
                         */

                        if (dto.getValue("Stage_IsMeeting").ToString() == "Y")
                        {
                            /* 增加會議 */

                            DataTO meetTo = new DataTO();

                            meetTo.setValue("Meeting_Code", getNewSerialNo(DataBase.CACIDB, "ME"));
                            meetTo.setValue("Pj_Code", newPjCode);
                            meetTo.setValue("Meeting_Class", dto.getValue("Stage_MtKind"));
                            meetTo.setValue("Meeting_Name", to.getValue("Pj_Name").ToString() + "_" + new BaseFun().getMeetingTypeName(to.getValue("Stage_MtKind").ToString()));
                            meetTo.setValue("Meeting_BgnTime", dto.getValue("Stage_Date").ToString() + " 09:00:00 ");
                            meetTo.setValue("Meeting_EndTime", dto.getValue("Stage_Date").ToString() + " 18:00:00 ");
                            meetTo.setValue("Meeting_User_Code", to.getValue("Rec_InfoID").ToString());
                            meetTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID").ToString());
                            meetTo.setValue("Rec_Info", "\\getDate()");

                            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Meeting", meetTo));

                            /* 增加公告 */
                        }

                        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..PjStage", dto));

                        break;
                    case "grv_CommGroup": // 分組
                        dto.setValue("Pj_Code", to.getValue("Pj_Code").ToString());
                        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..CommGroup", dto));
                        to.updateValue("Pj_IsJgroup", "Y");
                        break;
                    default:
                        break;
                }
            }
        }

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Project", to));

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());

        //System.Diagnostics.Debug.WriteLine(ex.Message);
    }

    bool IMMDUIBL.IsDataExist(DataTO to)
    {
        if (!to.isColumnExist("Pj_Code"))
            return false;
        else
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Project", to);
    }

    void IMMDUIBL.LoadData(DataTO to, DataSet ds)
    {
        //string Stage_Index = "";
        //if (to.isColumnExist("Stage_Index"))
        //{
        //    Stage_Index = to.getValue("Stage_Index").ToString();
        //    to.removeValue("Stage_Index");
        //}

        DataTO ProjectTO = new DataTO();

        ProjectTO.setValue("Pj_Code", to.getValue("Pj_Code"));

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Project", ProjectTO));

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

        //if (Stage_Index != "")
        //{
        //    to.setValue("Stage_Index", Stage_Index);
        //}

        if (!ds.Tables.Contains("grv_Score"))
        {
            DataTO ScoreTO = new DataTO();

            ScoreTO.setValue("Score_PjCode", to.getValue("Pj_Code"));

            DataTable ScoreDt = new DataTable("grv_Score");

            new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..Score", ScoreTO), ScoreDt);

            ds.Tables.Add(ScoreDt);
        }

        if (!ds.Tables.Contains("grv_PjStage"))
        {
            DataTable StageDt = new DataTable("grv_PjStage");
            string pjStageSql = "SELECT Pj_Code,Stage_Index,Stage_Kind,Stage_Name,Stage_Days,dbo.chgToChnDate(Stage_Date) as Stage_Date "
                              + ",Stage_Text,Stage_RmFlag,Stage_IsMeeting,Stage_MtKind,Metting_Code "
                              + ",Stage_RmEmpl,Stage_RmDays,Stage_RmText,Stage_Note,Rec_Info,Rec_InfoID "
                              + "FROM CACIDB..PjStage "
                              + "WHERE Pj_Code = @Pj_Code";
            SqlCommand cmd = new SqlCommand(pjStageSql);
            cmd.Parameters.AddWithValue("@Pj_Code", to.getValue("Pj_Code"));
            new SQLAgent(DataBase.CACIDB).select(cmd, StageDt);

            ds.Tables.Add(StageDt);
        }

        if (!ds.Tables.Contains("grv_CommGroup"))
        {
            //DataTO CommGroupTo = new DataTO();
            string sqlstr = "SELECT a.Pj_Code, a.CmGp_Num,a.CmGp_Name, " +
                       "(SELECT Sys_CdText FROM dbo.SysCode b WHERE b.Sys_CdKind = 'P' AND b.Sys_CdType = 'G' " +
                       "AND b.Sys_CdCode = a.CmGp_Num) as CmGp_NumName " +
                       "FROM CACIDB..CommGroup as a WHERE a.Pj_Code = @Pj_Code ";
            SqlCommand cmd = new SqlCommand(sqlstr);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("Pj_Code", to.getValue("Pj_Code"));

            DataTable CommGroupDT = new DataTable("grv_CommGroup");

            new SQLAgent(DataBase.CACIDB).select(cmd, CommGroupDT);

            ds.Tables.Add(CommGroupDT);
        }

    }

    DataTable IMMDUIBL.QueryDetailData(string QueryGridViewID, DataTO to)
    {
        throw new NotImplementedException();
    }

    void IMMDUIBL.UpdateData(DataTO to, DataSet ds)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..PjStage", to));

        DataTO dScoreTO = new DataTO();

        dScoreTO.setValue("Score_PjCode", to.getValue("Pj_Code").ToString());

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..Score", to));

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..CommGroup", to));

        if (to.isColumnExist("Pj_PjFile"))
        {
            string defaultPath = HttpContext.Current.Request.PhysicalApplicationPath + @"UploadFile\Project\" + to.getValue("Pj_Code").ToString();

            foreach (string _path in Directory.GetFiles(defaultPath))
            {
                if (new FileInfo(_path).Name.StartsWith("PjSp_"))
                    File.Delete(_path);
            }

            File.Move(to.getValue("Pj_PjFile").ToString(), Path.Combine(defaultPath, new FileInfo(to.getValue("Pj_PjFile").ToString()).Name));

            to.updateValue("Pj_PjFile", "/CACI/UploadFile/Project/" + to.getValue("Pj_Code").ToString() + "/" + new FileInfo(to.getValue("Pj_PjFile").ToString()).Name);
        }

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..Project", to));

        for (int i = 0; i < ds.Tables.Count; i++)
        {
            foreach (DataRow row in ds.Tables[i].Rows)
            {
                DataTO dto = new DataTO();
                
                foreach (DataColumn column in ds.Tables[i].Columns)
                {
                    if ((column.ColumnName == "Stage_Days" || column.ColumnName == "Stage_RmDays") && row[column.ColumnName].ToString() == "")
                        continue;
                    else if (column.ColumnName == "CmGp_NumName")
                        continue;
                    else if ("Stage_Date" == column.ColumnName)
                    {
                        if (row["Stage_Date"].ToString() == "N/A")
                            continue;
                        else
                            dto.setValue("Stage_Date", Project_01BL.chgChnDateToEnDate(row["Stage_Date"].ToString()));
                    }
                    else 
                        dto.setValue(column.ColumnName, row[column.ColumnName].ToString());
                }

                dto.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());

                if (ds.Tables[i].TableName == "grv_Score")
                {
                    dto.setValue("Score_PjCode", to.getValue("Pj_Code").ToString());

                    cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Score", dto));
                }
                else if (ds.Tables[i].TableName == "grv_PjStage")
                {
                    dto.setValue("Pj_Code", to.getValue("Pj_Code").ToString());

                    if (dto.getValue("Stage_IsMeeting").ToString() == "Y" && dto.getValue("Metting_Code").ToString() == "")
                    {
                        /* 增加會議 */

                        DataTO meetTo = new DataTO();

                        meetTo.setValue("Meeting_Code", getNewSerialNo(DataBase.CACIDB, "ME"));
                        meetTo.setValue("Pj_Code", to.getValue("Pj_Code").ToString());
                        meetTo.setValue("Meeting_Class", dto.getValue("Stage_MtKind"));
                        meetTo.setValue("Meeting_Name", to.getValue("Pj_Name").ToString() + "_" + new BaseFun().getMeetingTypeName(to.getValue("Stage_MtKind").ToString()));
                        meetTo.setValue("Meeting_BgnTime", dto.getValue("Stage_Date").ToString() + " 09:00:00 ");
                        meetTo.setValue("Meeting_EndTime", dto.getValue("Stage_Date").ToString() + " 18:00:00 ");
                        meetTo.setValue("Meeting_User_Code", to.getValue("Rec_InfoID").ToString());
                        meetTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID").ToString());
                        meetTo.setValue("Rec_Info", "\\getDate()");

                        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Meeting", meetTo));
                    }
                    else if (dto.getValue("Stage_IsMeeting").ToString() == "N" && dto.getValue("Metting_Code").ToString() != "")
                    {
                        DataTO meetTo = new DataTO();

                        meetTo.setValue("Meeting_Code", dto.getValue("Metting_Code").ToString());

                        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..Meeting", meetTo));
                    }

                    cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..PjStage", dto));
                }
                else if (ds.Tables[i].TableName == "grv_CommGroup")
                {
                    dto.setValue("Pj_Code", to.getValue("Pj_Code").ToString());
                    cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..CommGroup", dto));
                }
            }
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    #endregion

    #region IMDUIBL(輔導專案) 成員

    void IMDUIBL.InsertData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        string newMemberID = getNewSerialNo(DataBase.CACIDB, "JS");

        to.setValue("Pj_Code", newMemberID);
        to.setValue("Pj_IsJgroup", "N");

        if (to.isColumnExist("Pj_PjFile"))
        {
            string defaultPath = HttpContext.Current.Request.PhysicalApplicationPath + @"UploadFile\Project\" + to.getValue("Pj_Code").ToString();

            File.Move(to.getValue("Pj_PjFile").ToString(), Path.Combine(defaultPath, new FileInfo(to.getValue("Pj_PjFile").ToString()).Name));

            to.updateValue("Pj_PjFile", "/CACI/UploadFile/Project/" + to.getValue("Pj_Code").ToString() + "/" + new FileInfo(to.getValue("Pj_PjFile").ToString()).Name);
        }
        
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Project", to));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTO dto = new DataTO();

            dto.setValue("Pj_Code", newMemberID);
            dto.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
            foreach (DataColumn col in dt.Columns)
            {
                if(col.ColumnName == "Stage_Days")
                    continue;
                else if (col.ColumnName == "Stage_RmDays" && dt.Rows[i][col.ColumnName].ToString() == "")
                    continue;
                else if (col.ColumnName == "Stage_Date")
                    dto.setValue("Stage_Date", Project_01BL.chgChnDateToEnDate(dt.Rows[i][col.ColumnName].ToString()));
                else 
                    dto.setValue(col.ColumnName, dt.Rows[i][col.ColumnName].ToString());
            }
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..PjStage", dto));
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    bool IMDUIBL.IsDataExist(DataTO to)
    {
        if (!to.isColumnExist("Pj_Code"))
            return false;
        else
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Project", to);
    }

    void IMDUIBL.LoadData(DataTO to, DataTable dt)
    {
        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Project", to));
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
        //DataTO pjStageTO = new DataTO();
        //pjStageTO.setValue("Pj_Code", to.getValue("Pj_Code"));
        string sqlStr = "SELECT [Pj_Code],[Stage_Index],[Stage_Kind],[Stage_Name] " +
                        ",[Stage_Days],dbo.chgToChnDate([Stage_Date]) as Stage_Date,[Stage_Text] " +
                        ",[Stage_RmFlag],[Stage_IsMeeting],[Stage_MtKind],[Metting_Code] " +
                        ",[Stage_RmEmpl],[Stage_RmDays],[Stage_RmText],[Stage_Note] " +
                        "FROM [CACIDB].[dbo].[PjStage] " +
                        "WHERE Pj_Code = @Pj_Code ";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@Pj_Code", to.getValue("Pj_Code"));
        //new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..PjStage", pjStageTO), dt);
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..PjStage", to));

        if (to.isColumnExist("Pj_PjFile"))
        {
            string defaultPath = HttpContext.Current.Request.PhysicalApplicationPath + @"UploadFile\Project\" + to.getValue("Pj_Code").ToString();

            foreach (string _path in Directory.GetFiles(defaultPath))
            {
                if (new FileInfo(_path).Name.StartsWith("PjSp_"))
                    File.Delete(_path);
            }

            File.Move(to.getValue("Pj_PjFile").ToString(), Path.Combine(defaultPath, new FileInfo(to.getValue("Pj_PjFile").ToString()).Name));

            to.updateValue("Pj_PjFile", "/CACI/UploadFile/Project/" + to.getValue("Pj_Code").ToString() + "/" + new FileInfo(to.getValue("Pj_PjFile").ToString()).Name);
        }

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..Project", to));

        foreach (DataRow row in dt.Rows)
        {
            DataTO dto = new DataTO();
            dto.setValue("Pj_Code", to.getValue("Pj_Code").ToString());
            dto.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());

            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName == "Stage_Date")
                    dto.setValue("Stage_Date", Project_01BL.chgChnDateToEnDate(row[col.ColumnName].ToString()));
                else
                    dto.setValue(col.ColumnName, row[col.ColumnName].ToString());
            }

            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..PjStage", dto));
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    #endregion

    #region 自訂程序

    public DataTO getPjStageData(string PjSp_Code, int SpStage_Index)
    {
        DataTO pjStageTo = new DataTO();

        pjStageTo.setValue("Pj_Code", PjSp_Code);
        pjStageTo.setValue("Stage_Index", SpStage_Index);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..PjStage", pjStageTo));

        if (sr.Read())
        {
            for (int i = 0; i < sr.FieldCount; i++)
            {
                if (!pjStageTo.isColumnExist(sr.GetName(i)))
                {
                    pjStageTo.setValue(sr.GetName(i), sr[sr.GetName(i)].ToString());
                }
            }
        }

        sr.Close();

        return pjStageTo;
    }

    public DataTO getCommGroup(string Pj_Code)
    {
        string sqlstr = "SELECT a.Pj_Code, a.CmGp_Num,a.CmGp_Name, " +
                  "(SELECT Sys_CdText FROM dbo.SysCode b WHERE b.Sys_CdKind = 'P' AND b.Sys_CdType = 'G' " +
                  "AND b.Sys_CdCode = a.CmGp_Num) as CmGp_NumName " +
                  "FROM CACIDB..CommGroup as a WHERE a.Pj_Code = @Pj_Code ";
        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("Pj_Code", Pj_Code);

        //DataTable commGroupDT = new DataTable("grv_CommGroup");

        //new SQLAgent(DataBase.CACIDB).select(cmd, commGroupDT);

        DataTO commGroupTo = new DataTO();

        //DataTO pjStageTo = new DataTO();

        //pjStageTo.setValue("Pj_Code", Pj_Code);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(cmd);

        if (sr.Read())
        {
            for (int i = 0; i < sr.FieldCount; i++)
            {
                if (!commGroupTo.isColumnExist(sr.GetName(i)))
                {
                    commGroupTo.setValue(sr.GetName(i), sr[sr.GetName(i)].ToString());
                }
            }
        }

        sr.Close();

        return commGroupTo;
    }

    public bool checkAllowance(DataTO to)
    {
        bool result = false;
        string sqlStr = "SELECT * FROM CACIDB..Allowance WHERE Pj_Code = @Pj_Code AND Aow_Status <> 'F' " +
                        " UNION " +
                        "SELECT Pj_Code FROM CACIDB..Coach WHERE Pj_Code = @Pj_Code AND Coach_Status <> '5' ";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@Pj_Code", to.getValue("Pj_Code"));
        //cmd.Parameters.AddWithValue("@Comm_Code", to.getValue("Comm_Code"));
        SqlDataReader dr = new SQLAgent(DataBase.CACIDB).select(cmd);
        if (dr.Read())
            result = true;
        return result;
    }

    #endregion


}