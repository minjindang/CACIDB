using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;

/// <summary>
/// Consulting_01BL 的摘要描述
/// </summary>
public class Consulting_01BL : ICommonBL, IQueryBL, IMasterUIBL, IMMDUIBL
{
    #region IQueryBL 成員

    void IQueryBL.DeleteData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("Consulting", to));

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    System.Data.DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();

        string sqlstr = "SELECT a.*,b.Com_Name,dbo.udfTaiwanDateFormat(a.Rec_Info,'yyy/mm/dd') as twRec_Info,ISNULL(dbo.getSysCodeText('C','N',a.Cnst_Status ) ,'未處理') as  Cnst_StatusName  " +
                   "FROM CACIDB..Consulting a JOIN CACIDB..Company b ON a.Com_Code=b.Com_Code " +
                   "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {
                case "Cnst_Code":
                    cmd.CommandText += " AND a." + to.getAllColumnName()[i] + " =@" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Com_Code":
                    cmd.CommandText += " AND a." + to.getAllColumnName()[i] + " IN( SELECT Com_Code FROM CACIDB..Company WHERE Com_Name like '%' + @" + to.getAllColumnName()[i] + " + '%' )";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Cnst_CntDateS":
                    cmd.CommandText += " AND a.Cnst_CntDate >= dbo.chgToEnDate( @" + to.getAllColumnName()[i] + " ) ";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Cnst_CntDateE":
                    cmd.CommandText += " AND a.Cnst_CntDate <= dbo.chgToEnDate( @" + to.getAllColumnName()[i] + " ) ";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Cnst_Status":
                case "CntClass_Code":
                    if (to.getValue(to.getAllColumnName()[i]).ToString() != "-1")
                    {
                        cmd.CommandText += " AND " + to.getAllColumnName()[i] + bf.getStrParamTableSql();
                        cmd.Parameters.Add(bf.getStrParamTableParam(to.getValue(to.getAllColumnName()[i]).ToString()));
                    }
                    break;
            }
        }

        cmd.CommandText += " Order By " + sortStr;

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }

    System.Data.DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();

        string sqlstr = "SELECT a.*,b.Com_Name,dbo.udfTaiwanDateFormat(a.Rec_Info,'yyy/mm/dd') as twRec_Info,ISNULL(dbo.getSysCodeText('N','S',a.Cnst_Status ) ,'未處理') as  Cnst_StatusName " +
                   "FROM CACIDB..Consulting a JOIN CACIDB..Company b ON a.Com_Code=b.Com_Code " +
                   "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {
                case "Cnst_Code":
                    cmd.CommandText += " AND a." + to.getAllColumnName()[i] + " =@" + to.getAllColumnName()[i] ;
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Com_Code" :
                    cmd.CommandText += " AND a." + to.getAllColumnName()[i] + " IN( SELECT Com_Code FROM CACIDB..Company WHERE Com_Name like '%' + @" + to.getAllColumnName()[i] + " + '%' )";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Cnst_CntDateS" :
                    cmd.CommandText += " AND a.Cnst_CntDate >= dbo.chgToEnDate( @" + to.getAllColumnName()[i] + " ) ";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Cnst_CntDateE" :
                    cmd.CommandText += " AND a.Cnst_CntDate <= dbo.chgToEnDate( @" + to.getAllColumnName()[i] + " ) ";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Cnst_Status" :
                case "CntClass_Code":
                    if (to.getValue(to.getAllColumnName()[i]).ToString() != "-1")
                    {
                        cmd.CommandText += " AND " + to.getAllColumnName()[i] + bf.getStrParamTableSql();
                        cmd.Parameters.Add(bf.getStrParamTableParam(to.getValue(to.getAllColumnName()[i]).ToString()));
                    }
                    break;
            }
        }
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }

    #endregion

    #region IMasterUIBL 成員

    void IMasterUIBL.InsertData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();
        //建立一專屬Company的DataTo 並從TO塞入值
        DataTO CompanyTo = new DataTO();
        CompanyTo.setValue("Com_Name", to.getValue("Com_Name"));
        CompanyTo.setValue("Com_Boss", to.getValue("Com_Boss"));
        CompanyTo.setValue("Com_Tonum", to.getValue("Com_Tonum"));
        CompanyTo.setValue("Com_SetupTime", ICommonBL.chgChnDateToEnDate(to.getValue("Com_SetupTime").ToString()));
        CompanyTo.setValue("Com_Capital", to.getValue("Com_Capital"));
        CompanyTo.setValue("Com_EmpNum", to.getValue("Com_EmpNum"));

        CompanyTo.setValue("Com_LTover", to.getValue("Com_LTover"));
        CompanyTo.setValue("Com_MnSectors", to.getValue("Com_MnSectors"));
        CompanyTo.setValue("Com_OPMode", to.getValue("Com_OPMode"));
        CompanyTo.setValue("Com_OPStatus", to.getValue("Com_OPStatus"));
        CompanyTo.setValue("Com_MnProduct", to.getValue("Com_MnProduct"));

        CompanyTo.setValue("Com_Tel", to.getValue("Com_Tel"));
        CompanyTo.setValue("Com_Fax", to.getValue("Com_Fax"));
        CompanyTo.setValue("Com_OPAddr", to.getValue("Com_OPAddr"));
        CompanyTo.setValue("Com_Url", to.getValue("Com_Url"));
        CompanyTo.setValue("Com_Email", to.getValue("Com_Email"));

        CompanyTo.setValue("Com_CttName", to.getValue("Com_CttName"));
        CompanyTo.setValue("Com_CttTel", to.getValue("Com_CttTel"));
        CompanyTo.setValue("Com_CttMail", to.getValue("Com_CttMail"));
        CompanyTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        CompanyTo.setValue("Rec_Info", "\\getDate()");


        //建立一專屬Consulting的DataTo 並從TO塞入值
        DataTO ConsultTo = new DataTO();
        ConsultTo.setValue("Cnst_CntWay", to.getValue("Cnst_CntWay"));
        ConsultTo.setValue("CntClass_Code", to.getValue("CntClass_Code"));
        ConsultTo.setValue("Cnst_CntText", to.getValue("Cnst_CntText"));
        ConsultTo.setValue("Cnst_Status", to.getValue("Cnst_Status"));
        ConsultTo.setValue("Cnst_CntDate", to.getValue("Cnst_CntDate"));
        ConsultTo.setValue("User_Code", to.getValue("User_Code"));
        ConsultTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        ConsultTo.setValue("Rec_Info", "\\getDate()");

        // TODO:若回覆方式選擇 顧問駐診諮詢(F) ，則需新增一筆會議，並將會議代號存入諮詢回覆中 ok
        if (to.getValue("CtRepl_RpResult").ToString().Equals("F"))
        {
            ConsultTo.setValue("Meeting_Code", getNewSerialNo(DataBase.CACIDB, "ME"));
            DataTO meetingTo = new DataTO();
            meetingTo.setValue("Meeting_Code", ConsultTo.getValue("Meeting_Code").ToString());
            meetingTo.setValue("Meeting_Class", "CC");
            //TODO:建立會議時間、名稱
            DateTime meetingDate = DateTime.Now;
            meetingDate = meetingDate.Date.AddDays(3);
            meetingTo.setValue("Meeting_Name", meetingDate.Date.ToString().Split(' ')[0] + ":" + to.getValue("Com_Name").ToString() + "-顧問駐診諮詢");
            meetingTo.setValue("Meeting_BgnTime", meetingDate);
            meetingTo.setValue("Meeting_EndTime", meetingDate);
            meetingTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
            meetingTo.setValue("Rec_Info", "\\getDate()");
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Meeting", meetingTo));
        }


        //判斷Company是否為新; 若為空則為新增 有值為更新

        if ("N".Equals(to.getValue("Com_Code")))
        {

            string newComCode = getNewSerialNo(DataBase.CACIDB, "CA");
            string newConsultCode = getNewSerialNo(DataBase.CACIDB, "CC");
            CompanyTo.setValue("Com_Code", newComCode);
            ConsultTo.setValue("Com_Code", newComCode);
            ConsultTo.setValue("Cnst_Code", newConsultCode);
            System.Diagnostics.Debug.WriteLine(cmds);
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Company", CompanyTo));
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Consulting", ConsultTo));
            System.Diagnostics.Debug.WriteLine(cmds);
        }
        else
        {
            CompanyTo.setValue("Com_Code", to.getValue("Com_Code"));
            ConsultTo.setValue("Com_Code", to.getValue("Com_Code"));

            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Company", CompanyTo));
            //System.Diagnostics.Debug.WriteLine(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Company", CompanyTo));
            if ("N".Equals(to.getValue("Cnst_Code")))
            {
                string newConsultCode = getNewSerialNo(DataBase.CACIDB, "CC");
                ConsultTo.setValue("Cnst_Code", newConsultCode);
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Consulting", ConsultTo));
            }
            else
            {
                ConsultTo.setValue("Cnst_Code", to.getValue("Cnst_Code"));
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Consulting", ConsultTo));
            }
            System.Diagnostics.Debug.WriteLine(cmds);
        }

        DataTO CntReplyTo = new DataTO();

        CntReplyTo.setValue("CtRepl_Code", getNewSerialNo(DataBase.CACIDB, "CR"));
        CntReplyTo.setValue("Cnst_Code", ConsultTo.getValue("Cnst_Code"));
        CntReplyTo.setValue("CtRepl_Date", ICommonBL.chgChnDateToEnDate(to.getValue("CtRepl_Date").ToString()));
        if(!string.IsNullOrEmpty(to.getValue("CtRepl_RpWay").ToString()))
            CntReplyTo.setValue("CtRepl_RpWay", to.getValue("CtRepl_RpWay").ToString());
        CntReplyTo.setValue("CtRepl_RpText", to.getValue("CtRepl_RpText").ToString());
        CntReplyTo.setValue("CtRepl_RpResult", to.getValue("CtRepl_RpResult").ToString());
        CntReplyTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        CntReplyTo.setValue("Rec_Info", "\\getDate()");

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CntReply", CntReplyTo));

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    bool IMasterUIBL.IsDataExist(DataTO to)
    {
        if (!to.isColumnExist("Cnst_Code"))
            return false;
        else
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Consulting", to);
    }

    void IMasterUIBL.LoadData(DataTO to)
    {
        string sqlstr = "SELECT a.*, b.* ,dbo.chgToChnDate(a.Rec_Info) as twRec_Info " +
                        "FROM CACIDB..Consulting a JOIN CACIDB..Company b ON a.Com_Code = b.Com_Code " +
                        "WHERE Cnst_Code=@Cnst_Code";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Cnst_Code", to.getValue("Cnst_Code"));


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

    void IMasterUIBL.MarkData(DataTO to)
    {
        throw new NotImplementedException();
    }

    void IMasterUIBL.UpdateData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        //建立一專屬Consulting的DataTo 並從TO塞入值
        DataTO ConsultTo = new DataTO();
        ConsultTo.setValue("Cnst_Code", to.getValue("Cnst_Code"));
        ConsultTo.setValue("CntClass_Code", to.getValue("CntClass_Code"));
        ConsultTo.setValue("Cnst_CntText", to.getValue("Cnst_CntText"));

        // TODO:若回覆方式選擇 顧問駐診諮詢(F) ，則需新增一筆會議，並將會議代號存入諮詢回覆中 ok
        if (to.getValue("CtRepl_RpResult").ToString().Equals("F"))
        {
            ConsultTo.setValue("Meeting_Code", getNewSerialNo(DataBase.CACIDB, "ME"));
            DataTO meetingTo = new DataTO();
            meetingTo.setValue("Meeting_Code", ConsultTo.getValue("Meeting_Code").ToString());
            meetingTo.setValue("Meeting_Class", "CC");
            //TODO:建立會議時間、名稱
            DateTime meetingDate = DateTime.Now;
            meetingDate = meetingDate.Date.AddDays(3);
            meetingTo.setValue("Meeting_Name", meetingDate.Date.ToString().Split(' ')[0] + ":" + to.getValue("Com_Name").ToString() + "-顧問駐診諮詢");
            meetingTo.setValue("Meeting_BgnTime", meetingDate);
            meetingTo.setValue("Meeting_EndTime", meetingDate);

            meetingTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
            meetingTo.setValue("Rec_Info", "\\getDate()");
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Meeting", meetingTo));
        }

        ConsultTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        ConsultTo.setValue("Rec_Info", "\\getDate()");

        //回覆-案件處裡結果連同變更案件狀態
        switch (to.getValue("CtRepl_RpResult").ToString())
        {
            case "A":
            case "F":
                ConsultTo.setValue("Cnst_Status", "F");
                break;
            case "B":
            case "C":
            case "D":
                ConsultTo.setValue("Cnst_Status", "T");
                break;
            case "G":
                ConsultTo.setValue("Cnst_Status", "R");
                break;
            default:
                break;
        }

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Consulting", ConsultTo));

        DataTO CntReplyTo = new DataTO();

        CntReplyTo.setValue("CtRepl_Code", getNewSerialNo(DataBase.CACIDB, "CR"));
        CntReplyTo.setValue("Cnst_Code", to.getValue("Cnst_Code"));
        CntReplyTo.setValue("CtRepl_Date", ICommonBL.chgChnDateToEnDate(to.getValue("CtRepl_Date").ToString()));
        CntReplyTo.setValue("CtRepl_RpWay", to.getValue("CtRepl_RpWay").ToString());
        CntReplyTo.setValue("CtRepl_RpText", to.getValue("CtRepl_RpText").ToString());
        CntReplyTo.setValue("CtRepl_RpResult", to.getValue("CtRepl_RpResult").ToString());
        CntReplyTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        CntReplyTo.setValue("Rec_Info", "\\getDate()");
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CntReply", CntReplyTo));
        
        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    #endregion

    #region IMMDUIBL 成員

    void IMMDUIBL.InsertData(DataTO to, DataSet ds)
    {
        throw new NotImplementedException();
    }

    bool IMMDUIBL.IsDataExist(DataTO to)
    {
        if (!to.isColumnExist("Cnst_Code"))
            return false;
        else
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Consulting", to);
    }

    void IMMDUIBL.LoadData(DataTO to, DataSet ds)
    {
        string sqlstr = "SELECT a.*, b.* ,dbo.chgToChnDate(a.Rec_Info) as twRec_Info " +
                        "FROM CACIDB..Consulting a JOIN CACIDB..Company b ON a.Com_Code = b.Com_Code " +
                        "WHERE Cnst_Code=@Cnst_Code";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Cnst_Code", to.getValue("Cnst_Code"));


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

        //取得案件底下之回覆紀錄

        //DataTO CntReplyTO = new DataTO();

        //CntReplyTO.setValue("Cnst_Code", to.getValue("Cnst_Code"));

        //DataTable CntReplyDt = new DataTable("grv_CntReply");

        //new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..CntReply", CntReplyTO), CntReplyDt);

        DataTable CntReplyDt = getReplyList(to.getValue("Cnst_Code").ToString());

        ds.Tables.Add(CntReplyDt);

        //取得案件底下之電話紀錄

        //DataTable PhoneDt = new DataTable("grv_Phone");

        //string phone_Sqlstr = "SELECT b.Case_Code,b.PhRec_Code,dbo.chgToChnDate(c.PRcRp_Date) PRcRp_Date,a.CntClass_Code,b.PRcRp_Handle " +
        //                      "FROM CACIDB..Consulting a JOIN CACIDB..PhRecCase b ON a.Cnst_Code=b.Case_Code " +
        //                                                "JOIN CACIDB..PhRecRep c ON b.PhRec_Code=c.PhRec_Code " +
        //                      "WHERE a.Cnst_Code=@Cnst_Code ";

        //SqlCommand phone_Cmd = new SqlCommand(phone_Sqlstr);

        //phone_Cmd.Parameters.AddWithValue("@Cnst_Code", to.getValue("Cnst_Code"));

        //new SQLAgent(DataBase.CACIDB).select(phone_Cmd, PhoneDt);

        DataTable PhoneDt = getPhoneRecList(to.getValue("Cnst_Code").ToString());

        ds.Tables.Add(PhoneDt);
    }

    DataTable IMMDUIBL.QueryDetailData(string QueryGridViewID, DataTO to)
    {
        throw new NotImplementedException();
    }

    void IMMDUIBL.UpdateData(DataTO to, DataSet ds)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        //建立一專屬Consulting的DataTo 並從TO塞入值
        DataTO ConsultTo = new DataTO();
        ConsultTo.setValue("Cnst_Code", to.getValue("Cnst_Code"));
        ConsultTo.setValue("CntClass_Code", to.getValue("CntClass_Code"));
        ConsultTo.setValue("Cnst_CntText", to.getValue("Cnst_CntText"));

        // TODO:若回覆方式選擇 顧問駐診諮詢(F) ，則需新增一筆會議，並將會議代號存入諮詢回覆中 ok
        if (to.getValue("CtRepl_RpResult").ToString().Equals("F"))
        {
            ConsultTo.setValue("Meeting_Code", getNewSerialNo(DataBase.CACIDB, "ME"));
            DataTO meetingTo = new DataTO();
            meetingTo.setValue("Meeting_Code", ConsultTo.getValue("Meeting_Code").ToString());
            meetingTo.setValue("Meeting_Class", "CC");
            //TODO:建立會議時間、名稱
            DateTime meetingDate = DateTime.Now;
            meetingDate = meetingDate.Date.AddDays(3);
            meetingTo.setValue("Meeting_Name", meetingDate.Date.ToString().Split(' ')[0] + ":" + to.getValue("Com_Name").ToString() + "-顧問駐診諮詢");
            meetingTo.setValue("Meeting_BgnTime", meetingDate);
            meetingTo.setValue("Meeting_EndTime", meetingDate);

            meetingTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
            meetingTo.setValue("Rec_Info", "\\getDate()");
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Meeting", meetingTo));
        }

        ConsultTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        ConsultTo.setValue("Rec_Info", "\\getDate()");

        //回覆-案件處裡結果連同變更案件狀態
        switch (to.getValue("CtRepl_RpResult").ToString())
        {
            case "A":
            case "F":
                ConsultTo.setValue("Cnst_Status", "F");
                break;
            case "B":
            case "C":
            case "D":
                ConsultTo.setValue("Cnst_Status", "T");
                break;
            case "G":
                ConsultTo.setValue("Cnst_Status", "R");
                break;
            default:
                break;
        }

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Consulting", ConsultTo));

        DataTO CntReplyTo = new DataTO();

        CntReplyTo.setValue("CtRepl_Code", getNewSerialNo(DataBase.CACIDB, "CR"));
        CntReplyTo.setValue("Cnst_Code", to.getValue("Cnst_Code"));
        CntReplyTo.setValue("CtRepl_Date", ICommonBL.chgChnDateToEnDate(to.getValue("CtRepl_Date").ToString()));
        CntReplyTo.setValue("CtRepl_RpWay", to.getValue("CtRepl_RpWay").ToString());
        CntReplyTo.setValue("CtRepl_RpText", to.getValue("CtRepl_RpText").ToString());
        CntReplyTo.setValue("CtRepl_RpResult", to.getValue("CtRepl_RpResult").ToString());
        CntReplyTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        CntReplyTo.setValue("Rec_Info", "\\getDate()");
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CntReply", CntReplyTo));

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    #endregion

    #region  自訂程序


    public bool checkProject(DataTO to)
    {
        bool result = false;
        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("PjJudge", to);
        //cmd.Parameters.AddWithValue("@Comm_Code", to.getValue("Comm_Code"));
        SqlDataReader dr = new SQLAgent(DataBase.CACIDB).select(cmd);
        if (dr.Read())
            result = true;
        return result;
    }

    public bool checkMeeting(DataTO to)
    {
        bool result = false;
        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("MtgCrew", to);
        //cmd.Parameters.AddWithValue("@Comm_Code", to.getValue("Comm_Code"));
        SqlDataReader dr = new SQLAgent(DataBase.CACIDB).select(cmd);
        if (dr.Read())
            result = true;
        return result;
    }

    public DataTable getReplyList(string Cnst_Code)
    {
        DataTable ReplyDt = new DataTable("grv_CntReply");

        string ReplySqlStr = "SELECT b.CtRepl_Code,dbo.udfTaiwanDateFormat(b.CtRepl_Date,'yyy/mm/dd') CtRepl_Date,c.Sys_CdText CtRepl_RpWay " +
                             "FROM CACIDB..Consulting a JOIN CACIDB..CntReply b ON a.Cnst_Code=b.Cnst_Code  " +
                                                       "JOIN CACIDB..SysCode c ON b.CtRepl_RpWay=c.Sys_CdCode AND c.Sys_CdKind='C' AND c.Sys_CdType='R' " +
                             "WHERE a.Cnst_Code=@Cnst_Code";

        SqlCommand ReplyCmd = new SqlCommand(ReplySqlStr);

        ReplyCmd.Parameters.AddWithValue("Cnst_Code", Cnst_Code);


        new SQLAgent(DataBase.CACIDB).select(ReplyCmd, ReplyDt);

        return ReplyDt;
    }

    public DataTable getPhoneRecList(string Cnst_Code)
    {
        DataTable PhoneDt = new DataTable("grv_Phone");

        string phone_Sqlstr = "SELECT b.Case_Code,b.PhRec_Code,dbo.chgToChnDate(c.PRcRp_Date) PRcRp_Date,a.CntClass_Code,c.PRcRp_Handle " +
                              "FROM CACIDB..Consulting a JOIN CACIDB..PhRecCase b ON a.Cnst_Code=b.Case_Code " +
                                                        "JOIN CACIDB..PhRecRep c ON b.PhRec_Code=c.PhRec_Code " +
                              "WHERE a.Cnst_Code=@Cnst_Code ";

        SqlCommand phone_Cmd = new SqlCommand(phone_Sqlstr);

        phone_Cmd.Parameters.AddWithValue("@Cnst_Code", Cnst_Code);

        new SQLAgent(DataBase.CACIDB).select(phone_Cmd, PhoneDt);

        return PhoneDt;
    }

    public DataTO getCntReplyData(string CtRepl_Code)
    {
        DataTO cntReplyTo = new DataTO();

        cntReplyTo.setValue("CtRepl_Code", CtRepl_Code);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..CntReply", cntReplyTo));

        if (sr.Read())
        {
            for (int i = 0; i < sr.FieldCount; i++)
            {
                if (!cntReplyTo.isColumnExist(sr.GetName(i)))
                {
                    cntReplyTo.setValue(sr.GetName(i), sr[sr.GetName(i)].ToString());
                }
            }
        }

        sr.Close();

        return cntReplyTo;
    }
    public DataTO getPhRecData(string Case_Code,string PhRec_Code)
    {
        DataTO phRecTo = new DataTO();

        phRecTo.setValue("PhRec_Code", PhRec_Code);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..PhRecRep", phRecTo));

        if (sr.Read())
        {
            for (int i = 0; i < sr.FieldCount; i++)
            {
                if (!phRecTo.isColumnExist(sr.GetName(i)))
                {
                    phRecTo.setValue(sr.GetName(i), sr[sr.GetName(i)].ToString());
                }
            }
        }

        sr.Close();

        return phRecTo;
    }

    #endregion
}