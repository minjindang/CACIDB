using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;
using System.Text.RegularExpressions;

/// <summary>
/// Coach_01BL 的摘要描述
/// </summary>
public class Coach_01BL : ICommonBL, IQueryBL, IMasterUIBL, IMMDUIBL
{
    #region IQueryUI 成員

    void IQueryBL.DeleteData(DataTO to)
    {
       
        String delSql = "DELETE FROM CoachStage WHERE Coach_Code = @Coach_Code ";

        SqlCommand cmd = new SqlCommand(delSql);
        cmd.Parameters.AddWithValue("@Coach_Code", to.getValue("Coach_Code"));
        
        List<SqlCommand> cmds = new List<SqlCommand>();
        cmds.Add(cmd);
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("Coach", to));
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CoachFile", to));
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CoachMeeting", to));
        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    System.Data.DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();
        string sqlstr = "SELECT a.Coach_Code,b.Com_Name,c.Pj_Name,c.Pj_Code,dbo.chgToChnDate(a.Coach_Date) as twCoach_Date " +
                       "FROM CACIDB..Coach a  JOIN CACIDB..Company b ON a.Com_Code=b.Com_Code " +
                                            " JOIN CACIDB..Project c ON a.Pj_Code=c.Pj_Code " +
                       "WHERE 1=1 AND c.Pj_Kind = 'B' ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {
                case "Coach_Code":
                    cmd.CommandText += " AND a." + to.getAllColumnName()[i] + " like '%' + @" + to.getAllColumnName()[i] + " + '%'";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Pj_Name":
                    cmd.CommandText += " AND c." + to.getAllColumnName()[i] + " like '%' + @" + to.getAllColumnName()[i] + " + '%'";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Com_Name":
                case "Com_Boss":
                case "Com_Tonum":
                    cmd.CommandText += " AND b." + to.getAllColumnName()[i] + " like '%' + @" + to.getAllColumnName()[i] + " + '%'";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Coach_DateS":
                    cmd.CommandText += " AND a.Coach_Date <= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Coach_DateE":
                    cmd.CommandText += " AND a.Coach_Date >= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "ChKd_Code":
                    if (to.getValue(to.getAllColumnName()[i]).ToString() != "-1")
                    {
                        cmd.CommandText += " AND " + to.getAllColumnName()[i] + bf.getStrParamTableSql();
                        cmd.Parameters.Add(bf.getStrParamTableParam(to.getValue(to.getAllColumnName()[i]).ToString()));
                    }
                    break;
                default:
                    break;
            }
        }
        cmd.CommandText += " Order By " + sortStr;
        System.Diagnostics.Debug.WriteLine(cmd.CommandText);

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    System.Data.DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();

        string sqlstr = "SELECT a.Coach_Code,b.Com_Name,c.Pj_Name,c.Pj_Code,dbo.chgToChnDate(a.Coach_Date) as twCoach_Date " +
                       "FROM CACIDB..Coach a  JOIN CACIDB..Company b ON a.Com_Code=b.Com_Code " +
                                            " JOIN CACIDB..Project c ON a.Pj_Code=c.Pj_Code " +
                       "WHERE 1=1  AND c.Pj_Kind = 'B' ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {
                case "Coach_Code":
                     cmd.CommandText += " AND a." + to.getAllColumnName()[i] + " like '%' + @" + to.getAllColumnName()[i] + " + '%'";
                     cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Pj_Name":
                     cmd.CommandText += " AND c." + to.getAllColumnName()[i] + " like '%' + @" + to.getAllColumnName()[i] + " + '%'";
                     cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Com_Name":
                case "Com_Boss":
                case "Com_Tonum":
                     cmd.CommandText += " AND b." + to.getAllColumnName()[i] + " like '%' + @" + to.getAllColumnName()[i] + " + '%'";
                     cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
               case "Coach_DateS":
                    cmd.CommandText += " AND a.Coach_Date <= dbo.chgToEnDate( @" + to.getAllColumnName()[i]+" ) ";
                     cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
               case "Coach_DateE":
                    cmd.CommandText += " AND a.Coach_Date >= dbo.chgToEnDate( @" + to.getAllColumnName()[i] + " ) ";
                     cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
               case "ChKd_Code":
                    if (to.getValue(to.getAllColumnName()[i]).ToString() != "-1")
                    {
                        cmd.CommandText += " AND " + to.getAllColumnName()[i] + bf.getStrParamTableSql();
                        cmd.Parameters.Add(bf.getStrParamTableParam(to.getValue(to.getAllColumnName()[i]).ToString()));
                        System.Diagnostics.Debug.WriteLine(to.getValue(to.getAllColumnName()[i]).ToString());
                    }
                    break;
                default:
                    break;
            }
        }
                

        System.Diagnostics.Debug.WriteLine(cmd.CommandText);
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
        string newComCode;
        //建立一專屬Bank的DataTo 並從TO塞入值
        DataTO BankTo = new DataTO();
        //建立一專屬ComAtt的DataTo 並從TO塞入值
        DataTO ComAttTo = new DataTO();
        //建立一專屬Coach的DataTo 並從TO塞入值
        DataTO CoachTo = new DataTO();
        Char splStr = '_';
        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i].Split(splStr)[0])
            {
                case "Com":
                    CompanyTo.setValue(to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "BAcc":
                    BankTo.setValue(to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                default:
                    break;

            }
        }
        CompanyTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        CompanyTo.setValue("Rec_Info", "\\getDate()");
        BankTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        BankTo.setValue("Rec_Info", "\\getDate()");

        //判斷Company是否為新; 若為空則為新增 有值為更新
        if ("N".Equals(to.getValue("Com_Code")))
        {
            newComCode = getNewSerialNo(DataBase.CACIDB, "CA");
            string newBAccCode = getNewSerialNo(DataBase.CACIDB, "CB");
            CompanyTo.updateValue("Com_Code", newComCode);
            BankTo.setValue("Com_Code", newComCode);
            BankTo.updateValue("BAcc_Code", newBAccCode);
            System.Diagnostics.Debug.WriteLine(cmds);
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Company", CompanyTo));
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("BankAcc", BankTo));
            System.Diagnostics.Debug.WriteLine(cmds);
        }
        else
        {
            CompanyTo.updateValue("Com_Code", to.getValue("Com_Code"));
            BankTo.setValue("Com_Code", to.getValue("Com_Code"));

            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Company", CompanyTo));
            if ("N".Equals(to.getValue("BAcc_Code")))
            {
                string newBAccCode = getNewSerialNo(DataBase.CACIDB, "CB");
                BankTo.updateValue("BAcc_Code", newBAccCode);
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("BankAcc", BankTo));
            }
            else
            {
                BankTo.updateValue("BAcc_Code", to.getValue("BAcc_Code"));
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("BankAcc", BankTo));
            }
        }

        //輔導資料更新
        CoachTo.setValue("Coach_Code","");
        CoachTo.setValue("ChKd_Code", to.getValue("ChKd_Code"));
        CoachTo.setValue("Pj_Code", to.getValue("Pj_Code"));
        CoachTo.setValue("Com_Code", CompanyTo.getValue("Com_Code"));
        CoachTo.setValue("Coach_QstText", to.getValue("Coach_QstText"));
        CoachTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        CoachTo.setValue("Rec_Info", "\\getDate()");
        if (to.getValue("Coach_Code").ToString() == "N")
        {
            CoachTo.updateValue("Coach_Code", getNewSerialNo(DataBase.CACIDB, "CP"));
            CoachTo.setValue("Coach_Date", "\\getDate()");
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Coach", CoachTo));
        }
        else
        {
            CoachTo.updateValue("Coach_Code", to.getValue("Coach_Code"));
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Coach", CoachTo));
        }
        //上傳檔案
        ComAttTo.setValue("ComAtt_Code", "");
        ComAttTo.setValue("Com_Code", CompanyTo.getValue("Com_Code"));
        ComAttTo.setValue("ComAtt_Type", "");
        ComAttTo.setValue("ComAtt_File", "");
        ComAttTo.setValue("ComAtt_Date", "\\getDate()");
        ComAttTo.setValue("ComAtt_User_Code", "\\" + to.getValue("Rec_InfoID"));
        ComAttTo.setValue("ComAtt_Note", "");
        ComAttTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        ComAttTo.setValue("Rec_Info", "\\getDate()");
        String[] sysCodeArr = { "IT", "RP", "FA", "OT" };
        for (int k = 1; k <= sysCodeArr.Length ; k++)
        {
            if (to.isColumnExist( "ComAtt_File" + sysCodeArr[k-1] ))
            {
                ComAttTo.updateValue("ComAtt_File", "/CACI/UploadFile/Company/" + ComAttTo.getValue("Com_Code").ToString() + "/" + to.getValue("ComAtt_File" + sysCodeArr[k-1] ).ToString());
                ComAttTo.updateValue("ComAtt_Type", sysCodeArr[k-1]);
                if (to.getValue("ComAtt_Code" + sysCodeArr[k-1] ).ToString() == "N")
                {
                    ComAttTo.updateValue("ComAtt_Code", getNewSerialNo(DataBase.CACIDB, "CM"));
                    cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("ComAtt", ComAttTo));
                }
                else
                {
                    ComAttTo.updateValue("ComAtt_Code", to.getValue("ComAtt_Code" + sysCodeArr[k - 1] ));
                    cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("ComAtt", ComAttTo));
                }
            }
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    bool IMasterUIBL.IsDataExist(DataTO to)
    {
        if (!to.isColumnExist("Coach_Code"))
            return false;
        else
            return  new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Coach", to);
    }

    void IMasterUIBL.LoadData(DataTO to)
    {

        string sqlstr = "SELECT * " +
                        "FROM CACIDB..Coach a JOIN CACIDB..Company b ON a.Com_Code=b.Com_Code " +
                                            " JOIN CACIDB..Project c ON a.Pj_Code=c.Pj_Code " +
                        "WHERE Coach_Code=@Coach_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Coach_Code", to.getValue("Coach_Code"));


        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(cmd);

        if (sr.Read())
        {
            for (int i = 0; i < sr.FieldCount; i++)
            {
                if (!to.isColumnExist(sr.GetName(i)))
                {
                    //System.Diagnostics.Debug.WriteLine(string.Format("sr.GetName(?)",i) + sr.GetName(i));
                    //System.Diagnostics.Debug.WriteLine(string.Format("sr[sr.GetName(?)].ToString()",i) + sr[sr.GetName(i)].ToString());
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
        //輔導資料更新
        DataTO CoachTo = new DataTO();
        CoachTo.setValue("Coach_Code", "");
        CoachTo.setValue("ChKd_Code", to.getValue("ChKd_Code"));
        CoachTo.setValue("Com_Code", to.getValue("Com_Code"));
        CoachTo.setValue("Coach_QstText", to.getValue("Coach_QstText"));
        CoachTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        CoachTo.setValue("Rec_Info", "\\getDate()");
        CoachTo.updateValue("Coach_Code", to.getValue("Coach_Code"));
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Coach", CoachTo));
        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());

        /*List<SqlCommand> cmds = new List<SqlCommand>();
        //建立一專屬Company的DataTo 並從TO塞入值
        DataTO CompanyTo = new DataTO();
        string newComCode;
        //建立一專屬Bank的DataTo 並從TO塞入值
        DataTO BankTo = new DataTO();
        //建立一專屬ComAtt的DataTo 並從TO塞入值
        DataTO ComAttTo = new DataTO();
        //建立一專屬Coach的DataTo 並從TO塞入值
        DataTO CoachTo = new DataTO();
        Char splStr = '_';
        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i].Split(splStr)[0])
            {
                case "Com":
                    CompanyTo.setValue(to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "BAcc":
                    BankTo.setValue(to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                default:
                    break;

            }
        }
        CompanyTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        CompanyTo.setValue("Rec_Info", "\\getDate()");
        BankTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        BankTo.setValue("Rec_Info", "\\getDate()");

        //判斷Company是否為新; 若為空則為新增 有值為更新
        if ("N".Equals(to.getValue("Com_Code")))
        {

            newComCode = getNewSerialNo(DataBase.CACIDB, "CA");
            string newBAccCode = getNewSerialNo(DataBase.CACIDB, "CB");
            CompanyTo.updateValue("Com_Code", newComCode);
            BankTo.setValue("Com_Code", newComCode);
            BankTo.updateValue("BAcc_Code", newBAccCode);
            System.Diagnostics.Debug.WriteLine(cmds);
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Company", CompanyTo));
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("BankAcc", BankTo));
            System.Diagnostics.Debug.WriteLine(cmds);
        }
        else
        {
            CompanyTo.updateValue("Com_Code", to.getValue("Com_Code"));
            BankTo.setValue("Com_Code", to.getValue("Com_Code"));

            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Company", CompanyTo));
            if ("N".Equals(to.getValue("BAcc_Code")))
            {
                string newBAccCode = getNewSerialNo(DataBase.CACIDB, "CB");
                BankTo.updateValue("BAcc_Code", newBAccCode);
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("BankAcc", BankTo));
            }
            else
            {
                BankTo.updateValue("BAcc_Code", to.getValue("BAcc_Code"));
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("BankAcc", BankTo));
            }
            //System.Diagnostics.Debug.WriteLine(cmds);

        }
        //輔導資料更新
        CoachTo.setValue("Coach_Code", "");
        CoachTo.setValue("ChKd_Code", to.getValue("ChKd_Code"));
        CoachTo.setValue("Com_Code", CompanyTo.getValue("Com_Code"));
        CoachTo.setValue("Coach_QstText", to.getValue("Coach_QstText"));
        CoachTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        CoachTo.setValue("Rec_Info", "\\getDate()");
        if (to.getValue("Coach_Code").ToString() == "N")
        {
            CoachTo.updateValue("Coach_Code", getNewSerialNo(DataBase.CACIDB, "CP"));
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Coach", CoachTo));
        }
        else
        {
            CoachTo.updateValue("Coach_Code", to.getValue("Coach_Code"));
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Coach", CoachTo));
        }
        //上傳檔案
        ComAttTo.setValue("ComAtt_Code", "");
        ComAttTo.setValue("Com_Code", CompanyTo.getValue("Com_Code"));
        ComAttTo.setValue("ComAtt_Type", "");
        ComAttTo.setValue("ComAtt_File", "");
        ComAttTo.setValue("ComAtt_Date", "\\getDate()");
        ComAttTo.setValue("ComAtt_User_Code", "\\" + to.getValue("Rec_InfoID"));
        ComAttTo.setValue("ComAtt_Note", "");
        ComAttTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        ComAttTo.setValue("Rec_Info", "\\getDate()");
        String[] sysCodeArr = { "IT", "RP", "FA", "OT" };
        for (int k = 1; k <= sysCodeArr.Length; k++)
        {
            if (to.isColumnExist("ComAtt_File" + k))
            {
                ComAttTo.updateValue("ComAtt_File", "/CACI/UploadFile/Company/" + ComAttTo.getValue("Com_Code").ToString() + "/" + k + "_" + to.getValue("ComAtt_File" + k).ToString());
                ComAttTo.updateValue("ComAtt_Type", sysCodeArr[k - 1]);
                if (to.getValue("ComAtt_Code" + k).ToString() == "N")
                {
                    ComAttTo.updateValue("ComAtt_Code", getNewSerialNo(DataBase.CACIDB, "CM"));
                    cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("ComAtt", ComAttTo));
                }
                else
                {
                    ComAttTo.updateValue("ComAtt_Code", to.getValue("ComAtt_Code" + k));
                    cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("ComAtt", ComAttTo));
                }

            }
        }
        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());*/
    }

    #endregion

    #region IMMDUIBL 成員

    void IMMDUIBL.InsertData(DataTO to, DataSet ds)
    {
        throw new NotImplementedException();
    }

    bool IMMDUIBL.IsDataExist(DataTO to)
    {
        return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Coach", to);
    }

    void IMMDUIBL.LoadData(DataTO to, DataSet ds)
    {
        string sqlstr = "SELECT * " +
                        "FROM CACIDB..Coach a JOIN CACIDB..Company b ON a.Com_Code=b.Com_Code " +
                                            " JOIN CACIDB..Project c ON a.Pj_Code=c.Pj_Code " +
                        "WHERE Coach_Code=@Coach_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Coach_Code", to.getValue("Coach_Code"));


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

        //DataTO CoachTO = new DataTO();

        //CoachTO.setValue("Coach_Code", to.getValue("Coach_Code"));

        DataTable CoachDt = new DataTable("grv_CoachStage");

        //new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..CoachStage", CoachTO), CoachDt);

        string CoachSqlStr = "SELECT Coach_Code,Pj_Code,Stage_Index,dbo.chgToChnChtDate(ChSg_Date) ChSg_Date,dbo.getSysCodeText('O','S',ChSg_Verify) ChSg_Verify " +
                             "FROM CACIDB..CoachStage " +
                             "WHERE Coach_Code=@Coach_Code ";

        SqlCommand CoachCmd = new SqlCommand(CoachSqlStr);

        CoachCmd.Parameters.AddWithValue("@Coach_Code", to.getValue("Coach_Code"));

        new SQLAgent(DataBase.CACIDB).select(CoachCmd, CoachDt);

        ds.Tables.Add(CoachDt);

        //取得案件底下之電話紀錄

        DataTO MeetingTo = new DataTO();
        DataTable MeetingDt = new DataTable("grv_Meeting");

        string MeetingSqlStr = "SELECT d.MtgCom,c.Meeting_Name,e.Times_Bgn Meeting_BgnTime  " +
                               "FROM CACIDB..Coach a JOIN CACIDB..CoachMeeting b ON a.Coach_Code=b.Coach_Code " +
                                                    "JOIN CACIDB..Meeting c ON b.Meeting_Code=c.Meeting_Code " +
                                                    "JOIN CACIDB..MtgCom d ON c.Meeting_Code=c.Meeting_Code AND a.Com_Code=d.Com_Code " +
                                                    "JOIN CACIDB..MtgTimes e ON d.Meeting_Code=e.Meeting_Code AND d.Meeting_Index=e.Meeting_Index " +
                               "WHERE a.Coach_Code=@Coach_Code ";

        SqlCommand MeetingCmd = new SqlCommand(MeetingSqlStr);

        MeetingCmd.Parameters.AddWithValue("@Coach_Code", to.getValue("Coach_Code"));

        new SQLAgent(DataBase.CACIDB).select(MeetingCmd, MeetingDt);

        ds.Tables.Add(MeetingDt);
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

    #region 自訂程序

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

    public DataTO getCoachStage(string Coach_Code, string Pj_Code, int Stage_Index)
    {
        DataTO to = new DataTO();

        to.setValue("Coach_Code", Coach_Code);
        to.setValue("Pj_Code", Pj_Code);
        to.setValue("Stage_Index", Stage_Index);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..CoachStage", to));

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

        return to;
    }

    #endregion

    
}
