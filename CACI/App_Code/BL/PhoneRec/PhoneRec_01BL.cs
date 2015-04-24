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
public class PhoneRec_01BL : ICommonBL, IQueryBL, IMasterUIBL, IMMDUIBL
{
    #region IQueryUI 成員

    void IQueryBL.DeleteData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("PhoneRec", to));
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("PhRecRep", to));
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("PhRecCase", to));
        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    System.Data.DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();
        string sqlstr = "SELECT a.*,ISNULL(dbo.getSysCodeText('C','Y',a.CntClass_Code ) ,'無類別') as  CntClass_CodeName,b.Com_Tonum " +
                       "FROM CACIDB..PhoneRec a JOIN Company b ON a.PhRec_ComCode = b.Com_Code " +
                       "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0 ; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {
                case "PhRec_ComName":
                case "PhRec_CtName":
                case "PhRec_Tonum":
                case "PhRec_CtTel":
                    cmd.CommandText += " AND a." + to.getAllColumnName()[i] + " like '%' + @" + to.getAllColumnName()[i] + " + '%'";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "CntClass_Code":
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

        string sqlstr = "SELECT a.*,ISNULL(dbo.getSysCodeText('C','Y',a.CntClass_Code ) ,'無類別') as  CntClass_CodeName,b.Com_Tonum " +
                       "FROM CACIDB..PhoneRec a JOIN Company b ON a.PhRec_ComCode = b.Com_Code " +
                       "WHERE 1=1 ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {
                case "PhRec_ComName":
                case "PhRec_CtName":
                case "PhRec_Tonum":
                case "PhRec_CtTel":
                    cmd.CommandText += " AND a." + to.getAllColumnName()[i] + " like '%' + @" + to.getAllColumnName()[i] + " + '%'";
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "CntClass_Code":
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
                

        System.Diagnostics.Debug.WriteLine(cmd.CommandText);
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }

    #endregion

    #region IMasterUIBL 成員
    void IMasterUIBL.InsertData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();
        //建立一專屬PhoneRec的DataTo 並從TO塞入值
        DataTO PhoneRecTo = new DataTO();
        //建立一專屬PhRecRep的DataTo 並從TO塞入值
        DataTO PhRecRepTo = new DataTO();
        

        Char splStr = '_';
        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i].Split(splStr)[0])
            {
                case "PhRec":
                    PhoneRecTo.setValue(to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "PRcRp":
                    PhRecRepTo.setValue(to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                default:
                    break;

            }
        }
        PhoneRecTo.setValue("CntClass_Code", to.getValue("CntClass_Code"));
        PhoneRecTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        PhoneRecTo.setValue("Rec_Info", "\\getDate()");
        PhoneRecTo.setValue("PhRec_QDate", "\\getDate()");
        PhRecRepTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));

        PhRecRepTo.setValue("Rec_Info", "\\getDate()");

        PhoneRecTo.setValue("PhRec_Code", getNewSerialNo(DataBase.CACIDB, "PR"));
        PhRecRepTo.setValue("PRcRp_Code", getNewSerialNo(DataBase.CACIDB, "PP"));
        PhRecRepTo.setValue("PhRec_Code", PhoneRecTo.getValue("PhRec_Code"));

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("PhoneRec", PhoneRecTo));
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("PhRecRep", PhRecRepTo));

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    bool IMasterUIBL.IsDataExist(DataTO to)
    {
        if (!to.isColumnExist("PhRec_Code"))
            return false;
        else
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("PhoneRec", to);
    }

    void IMasterUIBL.LoadData(DataTO to)
    {

        string sqlstr = "SELECT a.*,b.*,c.Com_Tonum as PhRec_Tonum " +
                        "FROM CACIDB..PhoneRec a LEFT JOIN CACIDB..PhRecRep b ON a.PhRec_Code=b.PhRec_Code " +
                                                "LEFT JOIN CACIDB..Company c ON a.PhRec_ComCode=c.Com_Code " +
                        "WHERE a.PhRec_Code=@PhRec_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@PhRec_Code", to.getValue("PhRec_Code"));


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
        //建立一專屬PhoneRec的DataTo 並從TO塞入值
        DataTO PhoneRecTo = new DataTO();
        //建立一專屬PhRecRep的DataTo 並從TO塞入值
        DataTO PhRecRepTo = new DataTO();


        Char splStr = '_';
        for (int i = 0 ; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i].Split(splStr)[0])
            {
                case "PhRec":
                    PhoneRecTo.setValue(to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "PRcRp":
                    PhRecRepTo.setValue(to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                default:
                    break;

            }
        }
        PhoneRecTo.setValue("CntClass_Code", to.getValue("CntClass_Code"));
        PhoneRecTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        PhoneRecTo.setValue("Rec_Info", "\\getDate()");
        PhoneRecTo.setValue("PhRec_QDate", "\\getDate()");
        PhRecRepTo.setValue("PhRec_Code", to.getValue("PhRec_Code"));
        PhRecRepTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
        PhRecRepTo.setValue("Rec_Info", "\\getDate()");
            
        //PhoneRecTo.setValue("PhRec_Code", getNewSerialNo(DataBase.CACIDB, "PR"));
        //PhRecRepTo.setValue("PRcRp_Code", getNewSerialNo(DataBase.CACIDB, "PP"));
        
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("PhoneRec", PhoneRecTo));
        if (PhRecRepTo.getValue("PRcRp_Code").ToString().Equals("N"))
        {
            PhRecRepTo.updateValue("PRcRp_Code", getNewSerialNo(DataBase.CACIDB, "PP"));
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("PhRecRep", PhRecRepTo));
        }
        else
        {
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("PhRecRep", PhRecRepTo));
        }

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
        return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("PhoneRec", to);
    }

    void IMMDUIBL.LoadData(DataTO to, DataSet ds)
    {
        string sqlstr = "SELECT * " +
                "FROM CACIDB..Company " +
                "WHERE Com_Code=@Com_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);

   
        cmd.Parameters.AddWithValue("@Com_Code", to.getValue("PhRec_ComCode"));


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
        //輔導
        DataTable dtCoach = new DataTable("grv_Coach");
        string coachSql = "SELECT * " +
                "FROM CACIDB..Coach a LEFT JOIN CACIDB..CoachKind b ON a.ChKd_Code = b.ChKd_Code " +
                "WHERE Com_Code=@Com_Code ";

        SqlCommand CoachCmd = new SqlCommand(coachSql);
        CoachCmd.Parameters.AddWithValue("@Com_Code", to.getValue("PhRec_ComCode"));
        new SQLAgent(DataBase.CACIDB).select(CoachCmd, dtCoach);
        ds.Tables.Add(dtCoach);

        //諮詢
        DataTable dtConsulting = new DataTable("grv_Consulting");
        string cnstSql = "SELECT Cnst_Code,ISNULL(dbo.chgToChnChtDate(Cnst_CntDate),'無日期') as Cnst_twCntDate,Cnst_CntDate,ISNULL(dbo.getSysCodeText('C','Y',CntClass_Code ) ,'無類別') as  CntClass_CodeName,ISNULL(dbo.getSysCodeText('C','Y',Cnst_CntWay ) ,'未選取') as  Cnst_CntWayName,Cnst_CntDate,ISNULL(dbo.getSysCodeText('C','Y',Cnst_Status ) ,'無類別') as  Cnst_StatusName " +
                "FROM CACIDB..Consulting " +
                "WHERE Com_Code=@Com_Code ";

        SqlCommand cnstCmd = new SqlCommand(cnstSql);
        cnstCmd.Parameters.AddWithValue("@Com_Code", to.getValue("PhRec_ComCode"));
        new SQLAgent(DataBase.CACIDB).select(cnstCmd, dtConsulting);
        ds.Tables.Add(dtConsulting);

        //獎補助
        DataTable dtAow = new DataTable("grv_Aow");
        string aowSql = "SELECT *  " +
                "FROM CACIDB..ApplyAsis " +
                "WHERE Com_Code=@Com_Code ";

        SqlCommand aowCmd = new SqlCommand(aowSql);
        aowCmd.Parameters.AddWithValue("@Com_Code", to.getValue("PhRec_ComCode"));
        new SQLAgent(DataBase.CACIDB).select(aowCmd, dtAow);
        ds.Tables.Add(dtAow);
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

    public DataTable getCoachMeetingData(string Coach_Code)
    {
        DataTable dt = new DataTable();


        String sqlStr = " SELECT * FROM  CACIDB..CoachMeeting a JOIN CACIDB..Meeting b ON a.Meeting_Code = b.Meeting_Code" +
                                                            "   JOIN CACIDB..MtgRecord c ON b.Meeting_Code = c.Meeting_Code  "+
                                                            "   WHERE a.Coach_Code = @Coach_Code ";

        SqlCommand CoachCmd = new SqlCommand(sqlStr);
        CoachCmd.Parameters.AddWithValue("@Coach_Code", Coach_Code);
        new SQLAgent(DataBase.CACIDB).select(CoachCmd, dt);

        return dt;
    }
    public DataTable getCoachStageData(string Coach_Code)
    {
        DataTable dt = new DataTable();


        String sqlStr = " SELECT * FROM  CACIDB..CoachStage a  WHERE a.Coach_Code = @Coach_Code ";

        SqlCommand CoachCmd = new SqlCommand(sqlStr);
        CoachCmd.Parameters.AddWithValue("@Coach_Code", Coach_Code);
        new SQLAgent(DataBase.CACIDB).select(CoachCmd, dt);

        return dt;
    }

    public DataTable getCnstReplyData(string Cnst_Code)
    {
        DataTable dt = new DataTable();


        String sqlStr = " SELECT * FROM  CACIDB..CntReply a  WHERE a.Cnst_Code = @Cnst_Code ";

        SqlCommand ConsultingCmd = new SqlCommand(sqlStr);
        ConsultingCmd.Parameters.AddWithValue("@Cnst_Code", Cnst_Code);
        new SQLAgent(DataBase.CACIDB).select(ConsultingCmd, dt);

        return dt;
    }

    public void doArchive2PhoneRec(String[] selectData,String PhRec_Code)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();
        for (int i = 0; i < selectData.Length; i++)
        {
            DataTO to = new DataTO();
            to.setValue("PhRec_Code", PhRec_Code);
            to.setValue("Case_Code", selectData[i]);
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..PhRecCase", to));
        }
        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }
    #endregion

    
}

