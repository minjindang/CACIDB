using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using com.kangdainfo.online.WebBase.DB;
using com.kangdainfo.online.WebBase.TO;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;

/// <summary>
/// BaseFun 的摘要描述
/// </summary>
public class BaseFun
{
    public BaseFun()
    { }
    //
    // TODO: 在此加入建構函式的程式碼
    //

    /// <summary>
    /// 比較程式編號大小
    /// </summary>
    /// <param name="ProgNum1"></param>
    /// <param name="ProgNum2"></param>
    /// <returns></returns>
    public static bool ProgNumIsBigger(string ProgNum1, string ProgNum2)
    {
        string[] aryProgNum1 = ProgNum1.Split(char.Parse("."));
        string[] aryProgNum2 = ProgNum2.Split(char.Parse("."));

        //if( aryProgNum1.Length < aryProgNum2.Length )
        //    return true;
        //else if( aryProgNum1.Length > aryProgNum2.Length )
        //    return false;
        //else
        {
            for (int i = 0; i < aryProgNum1.Length && i < aryProgNum2.Length; i++)
            {
                if (int.Parse(aryProgNum1[i]) > int.Parse(aryProgNum2[i]))
                    return true;
            }
        }

        return false;
    }

    public string getSysCodeValue(string sys_CdKind, string sys_CdType, string sys_CdCode)
    {
        return getSysCodeValue(sys_CdKind, sys_CdType, sys_CdCode, "Y");
    }

    public string getSysCodeValue(string sys_CdKind, string sys_CdType, string sys_CdCode, string sys_CdState)
    {
        string sys_CdText = null;

        string sqlstr = "SELECT Sys_CdText " +
                        "FROM CACIDB..SysCode " +
                        "WHERE Sys_CdKind=@Sys_CdKind AND Sys_CdType=@Sys_CdType AND Sys_CdCode=@Sys_CdCode AND Sys_CdState=@Sys_CdState ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Sys_CdKind", sys_CdKind);
        cmd.Parameters.AddWithValue("@Sys_CdType", sys_CdType);
        cmd.Parameters.AddWithValue("@Sys_CdCode", sys_CdCode);
        cmd.Parameters.AddWithValue("@Sys_CdState", sys_CdState);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(cmd);

        if (sr.Read())
            sys_CdText = sr["sys_CdText"].ToString();

        sr.Close();

        return sys_CdText;
    }

    public DataTable getSysCodeByKind(string sys_CdKind, string sys_CdType)
    {
        DataTable dt = new DataTable();

        DataTO to = new DataTO();

        to.setValue("Sys_CdKind", sys_CdKind);
        to.setValue("Sys_CdType", sys_CdType);

        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("SysCode", to), dt);

        return dt;
    }

    public DataTable getDistinctSysCodeKind()
    {
        string sqlstr = "SELECT DISTINCT Sys_CdKind,Sys_CdType,Sys_CdNote " +
                        "FROM CACIDB..SysCode ";

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(new SqlCommand(sqlstr), dt);

        return dt;
    }

    public DataTable getUserDep()
    {
        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..UserDep", new DataTO()), dt);

        return dt;
    }

    public DataSet generHouMinute()
    {
        DataSet ds = new DataSet();
        DataTable hour = new DataTable("hoursTable");
        hour.Columns.Add("houhourr");
        for (int i = 8; i < 19; i++)
            hour.Rows.Add(i.ToString().PadLeft(2,'0'));
        ds.Tables.Add(hour);
        DataTable minute = new DataTable("minutesTable");
        minute.Columns.Add("minute");
        for (int i = 0; i < 6; i++)
            minute.Rows.Add(i.ToString().PadRight(2, '0'));
        ds.Tables.Add(minute);
        return ds;
    }

    public DataTable getDepUser(string UsDp_Code)
    {
        string sqlstr = "SELECT * FROM CACIDB..UserAcc Where UsDp_Code=@UsDp_Code";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@UsDp_Code", UsDp_Code);

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    public string getDepName(string depCode)
    {
        string depName = "";
        
        string sqlstr = "SELECT UsDp_Name " +
                        "FROM CACIDB..UserDep " +
                        "WHERE UsDp_Code=@UsDp_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@UsDp_Code", depCode);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(cmd);

        if (sr.Read())
            depName = sr["UsDp_Name"].ToString();

        sr.Close();

        return depName;
    }

    public string getUserDep(string User_Code)
    {
        string sqlstr = "SELECT * FROM CACIDB..UserAcc Where User_Code=@User_Code";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@User_Code", User_Code);

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt.Rows[0]["UsDp_Code"].ToString();
    }

    public DataTable getMeetingType(string Pj_Kind, bool can_Cust_Add)
    {
        DataTO to = new DataTO();

        to.setValue("Pj_Kind", Pj_Kind);
        to.setValue("Mety_CanAdd", can_Cust_Add ? "Y" : "N");

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("MeetingType", to), dt);

        return dt;
    }

    public string getMeetingTypeName(string mety_Code)
    {
        string meetingTypeName = "";
        
        DataTO to = new DataTO();

        to.setValue("Mety_Code", mety_Code);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..MeetingType", to));

        if (sr.Read())
            meetingTypeName = sr["Mety_Name"].ToString();

        sr.Close();

        return meetingTypeName;
    }

    public DataTable getSkillSample()
    {
        DataTable dt = new DataTable();
        DataTO to = new DataTO();
        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("SkillSample", to), dt);
        return dt;
    }

    public DataTable getSkillSample(string Ski_Kind)
    {
        DataTable dt = new DataTable();
        DataTO to = new DataTO();
        to.setValue("Ski_Kind", Ski_Kind);
        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..SkillSample", to), dt);
        return dt;
    }

    public DataTable getCompany(String Com_Tonum)
    {
        DataTable dt = new DataTable();
        DataTO to = new DataTO();
        to.setValue("Com_Tonum", Com_Tonum);
        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Company", to), dt);
        return dt;
    }

    public DataTable getBankAcc(String Com_Code)
    {
        DataTable dt = new DataTable();
        DataTO to = new DataTO();
        to.setValue("Com_Code", Com_Code);
        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("BankAcc", to), dt);
        return dt;
    }
    public String getComAtt_Code(String Com_Code, String ComAtt_Type)
    {
        string ComAtt_Code = "N";

        string sqlstr = "SELECT Top 1 ComAtt_Code FROM CACIDB..ComAtt WHERE Com_Code=@Com_Code AND ComAtt_Type = @ComAtt_Type";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Com_Code", Com_Code);
        cmd.Parameters.AddWithValue("@ComAtt_Type", ComAtt_Type);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(cmd);

        if (sr.Read())
        {
            ComAtt_Code = sr["ComAtt_Code"].ToString();
        }

        sr.Close();

        return ComAtt_Code;
    }
    public String getComAtt_Path(String Com_Code, String ComAtt_Type)
    {
        string ComAtt_Path = "N";

        string sqlstr = "SELECT Top 1 ComAtt_File FROM CACIDB..ComAtt WHERE Com_Code=@Com_Code AND ComAtt_Type = @ComAtt_Type";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Com_Code", Com_Code);
        cmd.Parameters.AddWithValue("@ComAtt_Type", ComAtt_Type);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(cmd);

        if (sr.Read())
        {
            ComAtt_Path = sr["ComAtt_File"].ToString();
        }

        sr.Close();

        return ComAtt_Path;
    }

    public DataTable getTableData(string tableName)
    {
        DataTable dt = new DataTable();
        DataTO to = new DataTO();
        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand(tableName, to), dt);
        return dt;
    }

    public DataTable getTableData(string tableName, DataTO dto)
    {
        DataTable dt = new DataTable();
        string sqlstr = string.Format("SELECT * FROM CACIDB..{0} WHERE 1=1 ", tableName);
        SqlCommand cmd = new SqlCommand(sqlstr);
        for (int i = 0; i < dto.getAllColumnName().Length; i++)
        {
            cmd.CommandText += " AND " + dto.getAllColumnName()[i] + "=@" + dto.getAllColumnName()[i];
            cmd.Parameters.AddWithValue("@" + dto.getAllColumnName()[i], dto.getValue(dto.getAllColumnName()[i]));
        }
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }


    public string getBankName(string bankNum)
    {
        string bankName = "";

        string sqlstr = "SELECT Top 1 Bank_Name FROM CACIDB..Bank WHERE Bank_Num=@Bank_Num";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Bank_Num", bankNum);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(cmd);

        if (sr.Read())
        {
            bankName = sr["Bank_Name"].ToString();
        }

        sr.Close();

        return bankName;
    }
    public SqlParameter getStrParamTableParam(string filter)
    {
        DataTable dtTblType = new DataTable();
        dtTblType.Columns.Add("Items", typeof(string));

        char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

        String[] arrSqlFilter = filter.Split(delimiterChars);

        for (int i = 1; i < arrSqlFilter.Length; i++)
            dtTblType.Rows.Add(arrSqlFilter[i]);


        SqlParameter dparam = new SqlParameter("@StrParamTable", SqlDbType.Structured);
        dparam.Direction = ParameterDirection.Input;
        dparam.TypeName = "dbo.StrTableType";

        dparam.Value = dtTblType;

        return dparam;
    }
    public String getStrParamTableSql()
    {
        return " IN ( SELECT Items FROM @StrParamTable ) ";
    }
    public SqlParameter getIntParamTableParam(string filter)
    {
        DataTable dtTblType = new DataTable();
        dtTblType.Columns.Add("Items", typeof(int));

        char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

        String[] arrSqlFilter = filter.Split(delimiterChars);

        for (int i = 1; i < arrSqlFilter.Length; i++)
            dtTblType.Rows.Add(arrSqlFilter[i]);


        SqlParameter dparam = new SqlParameter("@IntParamTable", SqlDbType.Structured);
        dparam.Direction = ParameterDirection.Input;
        dparam.TypeName = "dbo.IntTableType";

        dparam.Value = dtTblType;

        return dparam;
    }
    public String getIntParamTableSql()
    {
        return " IN ( SELECT Items FROM @IntParamTable ) ";
    }

    public string generCommitteeAccount(string Comm_IDNO)
    {
        string result = string.Empty;
        Random ram = new Random();
        char firstChar = (char)ram.Next(65, 90);
        char SecondChar = (char)ram.Next(65, 90);
        result = firstChar.ToString() + SecondChar.ToString() + Comm_IDNO.Substring(Comm_IDNO.Length - 4);
        return result;
    }

    public string generAccount(string Comm_IDNO)
    {
        string result = string.Empty;
        Random ram = new Random();
        char firstChar = (char)ram.Next(65, 90);
        char SecondChar = (char)ram.Next(65, 90);
        switch (Comm_IDNO.Length)
        {
            case 10: //身份證字號
                result = firstChar.ToString() + SecondChar.ToString() + Comm_IDNO.Substring(6);
                break;
            case 8: //統編
                result = firstChar.ToString() + SecondChar.ToString() + Comm_IDNO.Substring(4);
                break;
            default: //立案字號
                result = firstChar.ToString() + SecondChar.ToString() + ram.Next(1000, 9999).ToString();
                break;
        }
        return result;
    }


    public string generPassword()
    {
        string result = string.Empty;
        Random ram = new Random();
        while (result.Length != 6)
        {
            result += ram.Next(0, 9).ToString();
        }
        return result;
    }

    public void SendEmail(MailAddress[] TargetEmaildata, string Subject, string Content)
    {
        Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        MailSettingsSectionGroup netSmtpMailSection = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
        SmtpClient smtp = new SmtpClient(netSmtpMailSection.Smtp.Network.Host);
        MailMessage msg = new MailMessage();

        msg.IsBodyHtml = true;

        msg.From = new MailAddress(netSmtpMailSection.Smtp.From, "文創產業資料庫系統管理員");

        msg.Subject = Subject;

        msg.Body = Content;

        foreach (MailAddress target in TargetEmaildata)
        {
            msg.To.Add(target);
        }

        msg.SubjectEncoding = Encoding.UTF8;
        msg.BodyEncoding = Encoding.UTF8;

        smtp.Send(msg);
    }
    public String getChkdName(String Chkd_Code)
    {
        string ChkdName = "";

        string sqlstr = "SELECT ChKd_Name " +
                        "FROM CACIDB..CoachKind " +
                        "WHERE ChKd_Code=@ChKd_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@ChKd_Code", Chkd_Code);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(cmd);

        if (sr.Read())
            ChkdName = sr["Chkd_Name"].ToString();

        sr.Close();

        return ChkdName;
    }

    public UserDataTO getUserAcc(string user_code)
    {
        UserDataTO uTo = null;
        
        string sqlstr = "SELECT * FROM CACIDB..UserAcc WHERE User_Code=@User_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@User_Code", user_code);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(cmd);

        if (sr.Read())
        {
            uTo = new UserDataTO(user_code,
                                 sr["User_Name"].ToString(),
                                 sr["User_Tel"].ToString(),
                                 sr["User_Cell"].ToString(),
                                 sr["User_Mail"].ToString(),
                                 sr["UsDp_Code"].ToString(),
                                 sr["User_AcStatus"].ToString(),
                                 sr["User_Level"].ToString(),
                                 sr["User_Note"].ToString(),
                                 default(DateTime),
                                 null);
        }

        sr.Close();

        if (uTo != null)
        {
            string perSql = "SELECT b.* " +
                            "FROM CACIDB..UserRights a JOIN CACIDB..Program b ON a.Prog_Num=b.Prog_Num " +
                            "WHERE User_Code=@User_Code";

            SqlCommand pcmd = new SqlCommand(perSql);

            pcmd.Parameters.AddWithValue("@User_Code", user_code);

            SqlDataReader psr = new SQLAgent(DataBase.CACIDB).select(pcmd);

            while (psr.Read())
            {
                uTo.setPermission(new FunctionPermission(psr["Prog_Num"].ToString(),
                                                         psr["Prog_Type"].ToString(),
                                                         psr["Prog_Name"].ToString(),
                                                         psr["Prog_Path"].ToString(),
                                                         psr["Prog_Note"].ToString(),
                                                         DateTime.Now,
                                                         psr["Rec_InfoID"].ToString()));
            }

            psr.Close();
        }

        return uTo;
    }

    public DataTable getCommGroup(string Pj_Code)
    {
        string sqlstr = "SELECT * FROM CACIDB..CommGroup WHERE Pj_Code=@Pj_Code ";

        DataTable dt = new DataTable();

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Pj_Code", Pj_Code);

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }
    public DataTable getBankData()
    {
        string sqlstr = "SELECT DISTINCT Bank_Num,Bank_Name " +
                        "FROM CACIDB..Bank ";

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(new SqlCommand(sqlstr), dt);

        return dt;
    }

    public string getUserNameByCode(string userCode)
    {
        string userName = string.Empty;
        DataTO queryTo = new DataTO();
        queryTo.setValue("User_Code", userCode);
        DataTable dt = getTableData("UserAcc", queryTo);
        if(dt !=null && dt.Rows.Count > 0)
            userName = dt.Rows[0]["User_Name"].ToString();
        return userName;
    }

    //會議性質
    public string getMeetingType(string Mety_Code)
    {
        string metyName = string.Empty;
        if (!string.IsNullOrEmpty(Mety_Code))
        {
            DataTO queryTo = new DataTO();
            queryTo.setValue("Mety_Code", Mety_Code);
            DataTable dt = new BaseFun().getTableData("MeetingType", queryTo);
            metyName = dt.Rows[0]["Mety_Name"].ToString();
        }
        return metyName;
    }

    //提醒人員
    public string getRmEmpl(string rmEmplCode)
    {
        string[] codeMean = { "申請單位", "承辦人", "顧問人員" };
        char[] RmEmpItems = rmEmplCode.ToString().ToCharArray();
        string RmEmpResult = string.Empty;
        for (int i = 0; i < RmEmpItems.Length; i++)
        {
            if (RmEmpItems[i] == '1')
                RmEmpResult += codeMean[i] + "、";
        }
        if (!string.IsNullOrEmpty(RmEmpResult))
            RmEmpResult = RmEmpResult.Substring(0, RmEmpResult.Length - 1);
        else
            RmEmpResult = "無";
        return RmEmpResult;
    }

    //貨幣樣式
    public string getCurrencySymbol(int current)
    {
        string result = string.Empty;
        if (current > 0)
        {
            result = string.Format("{0:c}", current/1000);
            if (result.Contains('.'))
                result = result.Split('.')[0];
            result = result.Substring(3);
        }
        return result;
    }

    //申請表單樣式
    public DataTable getPjClass()
    {
        DataTable dt = new DataTable();
        string sqlStr = "SELECT PjCs_Code,PjCs_Name FROM CACIDB.dbo.PjClass WHERE PjCs_Code <> 6";
        new SQLAgent(DataBase.CACIDB).select(new SqlCommand(sqlStr), dt);
        return dt;
    }

}
