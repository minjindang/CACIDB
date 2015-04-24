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
using System.IO;

/// <summary>
/// Company_01BL 的摘要描述
/// </summary>
public class Company_01BL : ICommonBL, IQueryBL, IMasterUIBL, IMMDUIBL
{
    #region IQueryBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT DISTINCT a.Com_Code,RANK() OVER(ORDER BY a.Com_Code) AS Serial,a.Com_Name,a.Com_Tonum,a.Com_Boss,CASE a.Com_MnSectors WHEN 'P' THEN a.Com_MnOther ELSE b.Sys_CdText END Com_MnSectors " +
                        "FROM CACIDB..Company a          JOIN CACIDB..SysCode b ON a.Com_MnSectors=b.Sys_CdCode AND b.Sys_CdKind='I' AND b.Sys_CdType='D' " +
                                              "FULL JOIN CACIDB..Allowance c ON a.Com_Code=a.Com_Code " +
                                              "FULL JOIN CACIDB..Project d ON c.Pj_Code=d.Pj_Code " +
                                              "FULL JOIN CACIDB..Coach e ON a.Com_Code=e.Com_Code " +
                                              "FULL JOIN CACIDB..Project f ON e.Pj_Code=f.Pj_Code " +
                        "WHERE a.Com_Code is not null";

        SqlCommand cmd = new SqlCommand(sqlstr);

        if (to.isColumnExist("Pj_StartDate_Year"))
        {
            cmd.CommandText += " AND (Year(d.Pj_StartDate) -1911 =@Pj_StartDate_Year OR Year(f.Pj_StartDate) - 1911 =@Pj_StartDate_Year)";
            cmd.Parameters.AddWithValue("@Pj_StartDate_Year", to.getValue("Pj_StartDate_Year").ToString());
        }

        if (to.isColumnExist("Pj_Kind"))
        {
            cmd.CommandText += " AND (d.Pj_Kind=@Pj_Kind OR f.Pj_Kind=@Pj_Kind) ";
            cmd.Parameters.AddWithValue("@Pj_Kind", to.getValue("Pj_Kind").ToString());
        }

        if (to.isColumnExist("Aow_Class"))
        {
            cmd.CommandText += " AND (a.Aow_Class=@Aow_Class) ";
            cmd.Parameters.AddWithValue("@Aow_Class", to.getValue("Aow_Class").ToString());
        }

        if (to.isColumnExist("Com_Name"))
        {
            cmd.CommandText += " AND a.Com_Name Like '%' + @Com_Name + '%' ";
            cmd.Parameters.AddWithValue("@Com_Name", to.getValue("Com_Name").ToString());
        }

        if (to.isColumnExist("Com_Tonum"))
        {
            cmd.CommandText += " AND a.txt_Com_Tonum=@txt_Com_Tonum ";
            cmd.Parameters.AddWithValue("@txt_Com_Tonum", to.getValue("txt_Com_Tonum").ToString());
        }

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT DISTINCT a.Com_Code,RANK() OVER(ORDER BY a.Com_Code) AS Serial,a.Com_Name,a.Com_Tonum,a.Com_Boss,CASE a.Com_MnSectors WHEN 'P' THEN a.Com_MnOther ELSE b.Sys_CdText END Com_MnSectors " +
                        "FROM CACIDB..Company a          JOIN CACIDB..SysCode b ON a.Com_MnSectors=b.Sys_CdText AND b.Sys_CdKind='I' AND b.Sys_CdType='D' " +
                                              "LEFT FULL JOIN CACIDB..Allowance c ON a.Com_Code=a.Com_Code " +
                                              "LEFT FULL JOIN CACIDB..Project d ON c.Pj_Code=d.Pj_Code " +
                                              "LEFT FULL JOIN CACIDB..Coach e ON a.Com_Code=e.Com_Code " +
                                              "LEFT FULL JOIN CACIDB..Project f ON e.Pj_Code=f.Pj_Code " +
                        "WHERE 1=1";

        SqlCommand cmd = new SqlCommand(sqlstr);

        if (to.isColumnExist("Pj_StartDate_Year"))
        {
            cmd.CommandText += " AND (Year(d.Pj_StartDate) -1911 =@Pj_StartDate_Year OR Year(f.Pj_StartDate) - 1911 =@Pj_StartDate_Year)";
            cmd.Parameters.AddWithValue("@Pj_StartDate_Year", to.getValue("Pj_StartDate_Year").ToString());
        }

        if (to.isColumnExist("Pj_Kind"))
        {
            cmd.CommandText += " AND (d.Pj_Kind=@Pj_Kind OR f.Pj_Kind=@Pj_Kind) ";
            cmd.Parameters.AddWithValue("@Pj_Kind", to.getValue("Pj_Kind").ToString());
        }

        if (to.isColumnExist("Aow_Class"))
        {
            cmd.CommandText += " AND (a.Aow_Class=@Aow_Class) ";
            cmd.Parameters.AddWithValue("@Aow_Class", to.getValue("Aow_Class").ToString());
        }

        if (to.isColumnExist("Com_Name"))
        {
            cmd.CommandText += " AND a.Com_Name Like '%' + @Com_Name + '%' ";
            cmd.Parameters.AddWithValue("@Com_Name", to.getValue("Com_Name").ToString());
        }

        if (to.isColumnExist("Com_Tonum"))
        {
            cmd.CommandText += " AND a.txt_Com_Tonum=@txt_Com_Tonum ";
            cmd.Parameters.AddWithValue("@txt_Com_Tonum", to.getValue("txt_Com_Tonum").ToString());
        }

        cmd.CommandText += " Order By " + sortStr;

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    void IQueryBL.DeleteData(DataTO to)
    {
        SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("Company", to);

        /* 
         * TODO:確認 是否 刪除其他獎補助、輔導、諮詢、電話記錄、會議等 相關資料
         *                或是 將其狀態(Com_AcStatus)設定為鎖定(L) 即可
         */

        new SQLAgent(DataBase.CACIDB).execute(cmd);
    }

    #endregion

    #region IMasterUIBL 成員

    void IMasterUIBL.InsertData(DataTO to)
    {
        string newCom_Code = getNewSerialNo(DataBase.CACIDB, "CA");
        to.setValue("Com_Code", newCom_Code);

        List<SqlCommand> cmds = new List<SqlCommand>();
        //建立一專屬Company的DataTo 並從TO塞入值
        DataTO CompanyTo = new DataTO();

        CompanyTo.setValue("Com_Code", newCom_Code);
        CompanyTo.setValue("Com_Name", to.getValue("Com_Name"));
        CompanyTo.setValue("Com_Tonum", to.getValue("Com_Tonum"));
        CompanyTo.setValue("Com_Boss", to.getValue("Com_Boss"));
        CompanyTo.setValue("Com_BsIDNO", to.getValue("Com_BsIDNO"));
        CompanyTo.setValue("Com_AcStatus", "");
        CompanyTo.setValue("Com_OrgForm", to.getValue("Com_OrgForm"));
        
        if (to.getValue("Com_Capital") != null && to.getValue("Com_Capital").ToString() != "")
        {
            CompanyTo.setValue("Com_Capital", to.getValue("Com_Capital"));
        }

        if (to.getValue("Com_EmpNum") != null && to.getValue("Com_EmpNum").ToString() != "")
        {
            CompanyTo.setValue("Com_EmpNum", to.getValue("Com_EmpNum"));
        }

        CompanyTo.setValue("Com_MnSectors", to.getValue("Com_MnSectors"));
        CompanyTo.setValue("Com_MnOther", to.getValue("Com_MnOther"));
        CompanyTo.setValue("Com_SbSectors", to.getValue("Com_SbSectors"));
        CompanyTo.setValue("Com_SbOther", to.getValue("Com_SbOther"));
        CompanyTo.setValue("Com_MnProduct", to.getValue("Com_MnProduct"));
        CompanyTo.setValue("Com_Tel", to.getValue("Com_Tel"));
        CompanyTo.setValue("Com_Fax", to.getValue("Com_Fax"));
        CompanyTo.setValue("Com_OPAddr", to.getValue("Com_OPAddr"));
        CompanyTo.setValue("Com_Url", to.getValue("Com_Url"));
        CompanyTo.setValue("Com_Email", to.getValue("Com_Email"));
        CompanyTo.setValue("Com_CttCell", to.getValue("Com_CttCell"));
        CompanyTo.setValue("Com_CttTel", to.getValue("Com_CttTel"));
        CompanyTo.setValue("Com_CttMail", to.getValue("Com_CttMail"));
        CompanyTo.setValue("Com_RecDate", "\\getDate()");
        CompanyTo.setValue("Com_RecMan", to.getValue("Rec_InfoID"));
        CompanyTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID"));
        CompanyTo.setValue("Rec_Info", "\\getDate()");

        string defaultPath = HttpContext.Current.Request.PhysicalApplicationPath + "/UploadFile/Company/" + newCom_Code + "/";

        //上傳檔案(主要產品及服務附件上傳)
        if (to.getValue("Com_MnPdFile1") != null && to.getValue("Com_MnPdFile1").ToString() != "")
        {
            //CompanyTo.setValue("Com_MnPdFile1", "/CACI/UploadFile/Company/" + newCom_Code + "/1_" + to.getValue("Com_MnPdFile1"));
            File.Move(to.getValue("Com_MnPdFile1").ToString(), Path.Combine(defaultPath, new FileInfo(to.getValue("Com_MnPdFile1").ToString()).Name));

            CompanyTo.setValue("Com_MnPdFile1", "/CACI/UploadFile/Company/" + newCom_Code + "/" + new FileInfo(to.getValue("Com_MnPdFile").ToString()).Name);
        }
        
        //new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Company", CompanyTo));

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Company", CompanyTo));

        //上傳檔案(單位設立登記影本、完稅資料影本、檢附其他文件)
        //建立一專屬ComAtt的DataTo 並從TO塞入值
        for (int i = 2; i <= 4; i++)
        {
            DataTO ComAttTo = new DataTO();
            if (to.getValue("ComAtt_File" + i.ToString()) != null && to.getValue("ComAtt_File" + i.ToString()).ToString() != "")
            {
                string newComAtt_Code = getNewSerialNo(DataBase.CACIDB, "CM");
                ComAttTo.setValue("Com_Code", newCom_Code);
                ComAttTo.setValue("ComAtt_Code", newComAtt_Code);

                //switch (i)
                //{
                //    case 2://單位設立登記影本
                //        ComAttTo.setValue("ComAtt_Type", "RP");
                //        break;
                //    case 3://完稅資料影本
                //        ComAttTo.setValue("ComAtt_Type", "FA");
                //        break;
                //    case 4://檢附其他文件l
                //        ComAttTo.setValue("ComAtt_Type", "OT");
                //        break;
                //    default:
                //        ComAttTo.setValue("ComAtt_Type", "Error");
                //        break;
                //}

                ComAttTo.setValue("ComAtt_Type", to.getValue("ComAtt_File" + i.ToString()).ToString().Split('_')[0]);

                File.Move(to.getValue("ComAtt_File" + i.ToString()).ToString(), Path.Combine(defaultPath, new FileInfo(to.getValue("ComAtt_File" + i.ToString()).ToString()).Name));

                ComAttTo.setValue("ComAtt_File", "/CACI/UploadFile/Company/" + newCom_Code + "/" + new FileInfo(to.getValue("ComAtt_File" + i.ToString()).ToString()).Name);
                ComAttTo.setValue("ComAtt_Date", "\\getDate()");
                ComAttTo.setValue("ComAtt_User_Code", "\\" + to.getValue("Rec_InfoID"));
                ComAttTo.setValue("ComAtt_Note", to.getValue("ComAtt_Note"));
                ComAttTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
                ComAttTo.setValue("Rec_Info", "\\getDate()");
                //new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..ComAtt", ComAttTo));

                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..ComAtt", ComAttTo));
            }
        }
        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    void IMasterUIBL.LoadData(DataTO to)
    {
        //SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..Company", to);
        string sqlstr = "SELECT * " +
                        "FROM CACIDB..Company " +
                        "WHERE Com_Code=@Com_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Com_Code", to.getValue("Com_Code"));


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

    void IMasterUIBL.UpdateData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();
        //建立一專屬Company的DataTo 並從TO塞入值
        DataTO CompanyTo = new DataTO();

        CompanyTo.setValue("Com_Code", to.getValue("Com_Code"));
        CompanyTo.setValue("Com_Name", to.getValue("Com_Name"));
        CompanyTo.setValue("Com_Tonum", to.getValue("Com_Tonum"));
        CompanyTo.setValue("Com_Boss", to.getValue("Com_Boss"));
        CompanyTo.setValue("Com_BsIDNO", to.getValue("Com_BsIDNO"));
        CompanyTo.setValue("Com_AcStatus", "");
        CompanyTo.setValue("Com_OrgForm", to.getValue("Com_OrgForm"));

        if (to.getValue("Com_Capital") != null && to.getValue("Com_Capital").ToString() != "")
        {
            CompanyTo.setValue("Com_Capital", to.getValue("Com_Capital"));
        }

        if (to.getValue("Com_EmpNum") != null && to.getValue("Com_EmpNum").ToString() != "")
        {
            CompanyTo.setValue("Com_EmpNum", to.getValue("Com_EmpNum"));
        }

        CompanyTo.setValue("Com_MnSectors", to.getValue("Com_MnSectors"));
        CompanyTo.setValue("Com_MnOther", to.getValue("Com_MnOther"));
        CompanyTo.setValue("Com_SbSectors", to.getValue("Com_SbSectors"));
        CompanyTo.setValue("Com_SbOther", to.getValue("Com_SbOther"));
        CompanyTo.setValue("Com_MnProduct", to.getValue("Com_MnProduct"));
        CompanyTo.setValue("Com_Tel", to.getValue("Com_Tel"));
        CompanyTo.setValue("Com_Fax", to.getValue("Com_Fax"));
        CompanyTo.setValue("Com_OPAddr", to.getValue("Com_OPAddr"));
        CompanyTo.setValue("Com_Url", to.getValue("Com_Url"));
        CompanyTo.setValue("Com_Email", to.getValue("Com_Email"));
        CompanyTo.setValue("Com_CttCell", to.getValue("Com_CttCell"));
        CompanyTo.setValue("Com_CttTel", to.getValue("Com_CttTel"));
        CompanyTo.setValue("Com_CttMail", to.getValue("Com_CttMail"));
        CompanyTo.setValue("Com_RecDate", "\\getDate()");
        CompanyTo.setValue("Com_RecMan", to.getValue("Rec_InfoID"));
        CompanyTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID"));
        CompanyTo.setValue("Rec_Info", "\\getDate()");

        string defaultPath = HttpContext.Current.Request.PhysicalApplicationPath + "/UploadFile/Company/" + to.getValue("Com_Code").ToString() + "/";

        //上傳檔案(主要產品及服務附件上傳)
        if (to.getValue("Com_MnPdFile1").ToString() != "")
        {
            foreach (string path in Directory.GetFiles(defaultPath))
            {
                if (path.StartsWith("PR_"))
                {
                    File.Delete(Path.Combine(defaultPath, path));
                }
            }

            File.Move(to.getValue("Com_MnPdFile1").ToString(), Path.Combine(defaultPath, new FileInfo(to.getValue("Com_MnPdFile1").ToString()).Name));

            CompanyTo.setValue("Com_MnPdFile1", "/CACI/UploadFile/Company/" + to.getValue("Com_Code").ToString() + "/" + new FileInfo(to.getValue("Com_MnPdFile1").ToString()).Name);

            //CompanyTo.setValue("Com_MnPdFile1", "/CACI/UploadFile/Company/" + to.getValue("Com_Code").ToString() + "/1_" + to.getValue("Com_MnPdFile1"));
        }

        //new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..Company", CompanyTo));
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..Company", CompanyTo));

        //上傳檔案(單位設立登記影本、完稅資料影本、檢附其他文件)
        //建立一專屬ComAtt的DataTo 並從TO塞入值
        for (int i = 2; i <= 4; i++)
        {
            DataTO ComAttTo = new DataTO();
            if (to.getValue("ComAtt_File" + i.ToString()).ToString() != "")
            {
                ComAttTo.setValue("Com_Code", to.getValue("Com_Code"));
                ComAttTo.setValue("ComAtt_Code", to.getValue("ComAtt_Code" + i.ToString()));

                foreach (string path in Directory.GetFiles(defaultPath))
                {
                    if (path.StartsWith(to.getValue("ComAtt_File" + i.ToString()).ToString().Split('_')[0] + "_"))
                    {
                        File.Delete(Path.Combine(defaultPath, path));
                    }
                }

                ComAttTo.setValue("ComAtt_Type", to.getValue("ComAtt_File" + i.ToString()).ToString().Split('_')[0]);

                ComAttTo.setValue("ComAtt_File", "/CACI/UploadFile/Company/" + to.getValue("Com_Code").ToString() + "/" + new FileInfo(to.getValue("ComAtt_File" + i.ToString()).ToString()).Name);

                ComAttTo.setValue("ComAtt_Date", "\\getDate()");
                ComAttTo.setValue("ComAtt_User_Code", "\\" + to.getValue("Rec_InfoID"));
                ComAttTo.setValue("ComAtt_Note", to.getValue("ComAtt_Note"));
                ComAttTo.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID"));
                ComAttTo.setValue("Rec_Info", "\\getDate()");
                //new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..ComAtt", ComAttTo));

                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..ComAtt", ComAttTo));

                //刪除舊檔資料

                string delStr = "DELETE FROM CACIDB..ComAtt Where Com_Code=@Com_Code AND ComAtt_Type=@ComAtt_Type ";

                SqlCommand delCmd = new SqlCommand(delStr);

                delCmd.Parameters.Add("@Com_Code", to.getValue("Com_Code").ToString());
                delCmd.Parameters.Add("@ComAtt_Type", to.getValue("ComAtt_File" + i.ToString()).ToString().Split('_')[0]);

                cmds.Add(delCmd);
            }
        }
        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    bool IMasterUIBL.IsDataExist(DataTO to)
    {
        if (!to.isColumnExist("Com_Code"))
            return false;
        else
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("CACIDB..Company", to);
    }

    void IMasterUIBL.MarkData(DataTO to)
    {
        throw new NotImplementedException();
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
        //SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..Company", to);
        string sqlstr = "SELECT * " +
                        "FROM CACIDB..Company " +
                        "WHERE Com_Code=@Com_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Com_Code", to.getValue("Com_Code"));


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

        //申請補助案歷程
        DataTable dtAow = new DataTable("grv_Aow");
        string strAowSql = "SELECT ROW_NUMBER() OVER(ORDER BY Aow_Date) AS List, " +
                            "dbo.udfTaiwanDateFormat(B.Aow_Date,'yyy/mm/dd') Aow_Date, " +
                            "ISNULL(dbo.getSysCodeText('P','K',A.Pj_Kind),'') AS Pj_Class_Text, " +
                            "ISNULL(dbo.getSysCodeText('C','S',Aow_Status),'') AS Aow_Status_Text, " +
                            " * " +
                            "FROM CACIDB..Project A " +
                            "JOIN CACIDB..Allowance B " +
                            "ON A.Pj_Code = B.Pj_Code " +
                            "AND B.Com_Code=@Com_Code " +
                            "ORDER BY B.Aow_Code ";
        
        SqlCommand AowCmd = new SqlCommand(strAowSql);
        AowCmd.Parameters.AddWithValue("@Com_Code", to.getValue("Com_Code"));
        new SQLAgent(DataBase.CACIDB).select(AowCmd, dtAow);
        ds.Tables.Add(dtAow);

        //諮詢歷程
        DataTable dtCoach1 = new DataTable("grv_Coach1");
        string strCoachSql1 = "SELECT ISNULL(dbo.getSysCodeText('C','I',Cnst_CntWay),'') AS Cnst_CntWay_Text, " +
                              "ISNULL(dbo.getSysCodeText('C','Y',CntClass_Code),'') AS CntClass_Code_Text, " +
                              "ISNULL(dbo.getSysCodeText('N','S',Cnst_Status),'') AS Cnst_Status_Text, " +
                              "* " +
                              "FROM CACIDB..Consulting " +
                              "WHERE Com_Code=@Com_Code " +
                              "ORDER BY Cnst_CntDate";

        SqlCommand CoachCmd1 = new SqlCommand(strCoachSql1);
        CoachCmd1.Parameters.AddWithValue("@Com_Code", to.getValue("Com_Code"));
        new SQLAgent(DataBase.CACIDB).select(CoachCmd1, dtCoach1);
        ds.Tables.Add(dtCoach1);

        //輔導歷程
        DataTable dtCoach2 = new DataTable("grv_Coach2");
        string strCoachSql2 = "SELECT ROW_NUMBER() OVER(ORDER BY A.Coach_Code) AS List, " +
                              "dbo.udfTaiwanDateFormat( A.Coach_Date,'yyy/mm/dd') AS Coach_Date, " +
                              "ISNULL(dbo.getSysCodeText('O','S',A.Coach_Status),'') AS Coach_Status_Text, " +
                              "* " + 
                              "FROM CACIDB..Coach A " +
                              "JOIN CACIDB..CoachKind B " +
                              "ON A.ChKd_Code=B.ChKd_Code " +
                              "AND A.Com_Code=@Com_Code " +
                              "ORDER BY A.Coach_Code ";

        SqlCommand CoachCmd2 = new SqlCommand(strCoachSql2);
        CoachCmd2.Parameters.AddWithValue("@Com_Code", to.getValue("Com_Code"));
        new SQLAgent(DataBase.CACIDB).select(CoachCmd2, dtCoach2);
        ds.Tables.Add(dtCoach2);


        //申請投融資歷程
        //DataTable dtMoney = new DataTable("grv_Money");
        //string strMoneySql = "SELECT ROW_NUMBER() OVER(ORDER BY CaseID) AS List, " +
        //                     "dbo.udfTaiwanDateFormat( ApplyDate,'yyy/mm/dd') AS Date, " +
        //                     "CASE Status WHEN '1' THEN '案件申請中' " +
        //                     "WHEN '2' THEN '初步審查中' " +
        //                     "WHEN '3' THEN '財務審查中' " +
        //                     "WHEN '4' THEN '技術審查中' " +
        //                     "WHEN '5' THEN '信保保證資料審查中' " +
        //                     "WHEN '6' THEN '貸款審議會審查中' " +
        //                     "WHEN '7' THEN '重新審查中' " +
        //                     "WHEN '8' THEN '審查通過' " +
        //                     "WHEN '9' THEN '審查不通過' " +
        //                     "END AS Status_Text, " +
        //                     " * " +
        //                     "FROM CACIDB..LnApplyCase " +
        //                     "WHERE Com_Code=@Com_Code " +
        //                     "ORDER BY ApplyDate ";

        //SqlCommand MoneyCmd = new SqlCommand(strMoneySql);
        //MoneyCmd.Parameters.AddWithValue("@Com_Code", to.getValue("Com_Code"));
        //new SQLAgent(DataBase.CACIDB).select(MoneyCmd, dtMoney);
        //ds.Tables.Add(dtMoney);

        //電話記錄
        DataTable dtPhone = new DataTable("grv_Phone");
        string strPhoneSql = "SELECT ROW_NUMBER() OVER(ORDER BY A.PhRec_Code) AS List, " +
                             "ISNULL(dbo.getSysCodeText('C','Y',A.CntClass_Code),'') AS CntClass_Code_Text, " +
                             "ISNULL(dbo.getSysCodeText('C','R',B.PRcRp_Handle),'') AS PRcRp_Handle_Text, " +
                             "* " +
                             "FROM CACIDB..PhoneRec A " +
                             "JOIN CACIDB..PhRecRep B " +
                             "ON A.PhRec_Code=B.PhRec_Code " +
                             "AND A.PhRec_ComCode=@Com_Code " +
                             "ORDER BY PhRec_QDate ";

        SqlCommand PhoneCmd = new SqlCommand(strPhoneSql);
        PhoneCmd.Parameters.AddWithValue("@Com_Code", to.getValue("Com_Code"));
        new SQLAgent(DataBase.CACIDB).select(PhoneCmd, dtPhone);
        ds.Tables.Add(dtPhone);

        //管考記錄
        DataTable dtEval = new DataTable("grv_Evaluations");
        string strEvalSql = "SELECT ROW_NUMBER() OVER(ORDER BY A.Pj_Code) AS List, " +
                            " * FROM CACIDB..Allowance A " +
                            "JOIN CACIDB..Project B ON A.Pj_Code=B.Pj_Code " +
                            "JOIN CACIDB..ApPjContext C ON A.Aow_Code=C.Aow_Code " +
                            "JOIN CACIDB..MtgRecordByUnit D ON A.Com_Code=D.Com_Code " +
                            "JOIN CACIDB..MtgTimes E ON D.Meeting_Code=E.Meeting_Code " +
                            "AND D.Meeting_Index=E.Meeting_Index " +
                            "JOIN CACIDB..Meeting F ON D.Meeting_Code=F.Meeting_Code " +
                            "JOIN CACIDB..MeetingType G ON F.Meeting_Class=G.Mety_Code " +
                            "WHERE A.Com_Code=@Com_Code AND D.Com_Code=@Com_Code " +
                            "ORDER BY A.Pj_Code, A.Aow_Code "; 

        SqlCommand EvalCmd = new SqlCommand(strEvalSql);
        EvalCmd.Parameters.AddWithValue("@Com_Code", to.getValue("Com_Code"));
        new SQLAgent(DataBase.CACIDB).select(EvalCmd, dtEval);
        ds.Tables.Add(dtEval);
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

    #region 自訂功能
    
    //補助案歷程
    public DataTable getAowData(string Aow_Code, string Pj_Code)
    {
        DataTable dt = new DataTable();

        string sqlStr = "SELECT ROW_NUMBER() OVER(ORDER BY A.Aow_Code) AS list,dbo.udfTaiwanDateFormat( A.AwSg_Date,'yyy/mm/dd') AwSg_Date, " +
                        "ISNULL(dbo.getSysCodeText('S','S',AwSg_Verify),'') AS AwSg_Verify_Text, * " +
                        "FROM CACIDB..AowStage A " +
                        "JOIN CACIDB..PjStage B " +
                        "ON A.Stage_Index=B.Stage_Index " +
                        "AND A.Aow_Code=@Aow_Code AND A.Pj_Code=B.Pj_Code AND B.Pj_Code=@Pj_Code " +
                        "ORDER BY A.Aow_Code,A.AwSg_Date ";

        SqlCommand AowCmd = new SqlCommand(sqlStr);
        AowCmd.Parameters.AddWithValue("@Aow_Code", Aow_Code);
        AowCmd.Parameters.AddWithValue("@Pj_Code", Pj_Code);
        new SQLAgent(DataBase.CACIDB).select(AowCmd, dt);

        return dt;
    }

    //諮詢歷程
    public DataTable getCnstData(string Com_Code, string Cnst_Code)
    {
        DataTable dt = new DataTable();

        string sqlStr = "SELECT '諮詢回復' AS Type, dbo.udfTaiwanDateFormat( B.CtRepl_Date,'yyy/mm/dd') AS Date, B.CtRepl_RpText AS Text FROM CACIDB..Consulting A " +
                        "JOIN CACIDB..CntReply B ON A.Cnst_Code=B.Cnst_Code AND A.Cnst_Code=@Cnst_Code " +
                        "AND A.Com_Code=@Com_Code " +
                        "UNION " +
                        "SELECT '電話紀錄' AS Type, dbo.udfTaiwanDateFormat( C.PRcRp_Date,'yyy/mm/dd') AS Date, C.PRcRp_Text AS Text FROM CACIDB..Consulting A " +
                        "JOIN CACIDB..PhRecCase B ON A.Cnst_Code=B.Case_Code AND A.Com_Code=@Com_Code AND A.Cnst_Code=@Cnst_Code " +
                        "JOIN CACIDB..PhRecRep C ON B.PhRec_Code=C.PhRec_Code " +
                        "ORDER BY Date ";

        SqlCommand AowCmd = new SqlCommand(sqlStr);
        AowCmd.Parameters.AddWithValue("@Com_Code", Com_Code);
        AowCmd.Parameters.AddWithValue("@Cnst_Code", Cnst_Code);

        new SQLAgent(DataBase.CACIDB).select(AowCmd, dt);

        return dt;
    }

    //輔導歷程
    public DataTable getCoachData(string Com_Code, string Coach_Code)
    {
        DataTable dt = new DataTable();

        string sqlStr = "SELECT ROW_NUMBER() OVER(ORDER BY A.Meeting_Code) AS List, " +
                        "* FROM CACIDB..CoachMeeting A " +
                        "JOIN CACIDB..Meeting B ON A.Meeting_Code=B.Meeting_Code " +
                        "JOIN CACIDB..MtgTimes C ON A.Meeting_Code=C.Meeting_Code " +
                        "JOIN CACIDB..Evaluations D ON A.Meeting_Code=D.Meeting_Code AND C.Meeting_Index=D.Meeting_Index " +
                        "WHERE A.Coach_Code=@Coach_Code AND D.Com_Code=@Com_Code " +
                        "ORDER BY A.Coach_Code, A.Meeting_Code, C.Meeting_Index";

        SqlCommand AowCmd = new SqlCommand(sqlStr);
        AowCmd.Parameters.AddWithValue("@Com_Code", Com_Code);
        AowCmd.Parameters.AddWithValue("@Coach_Code", Coach_Code);

        new SQLAgent(DataBase.CACIDB).select(AowCmd, dt);

        return dt;
    }

    //輔導歷程(電話)
    public DataTable getCoachPhoneData(string Com_Code, string Coach_Code)
    {
        DataTable dt = new DataTable();

        string sqlStr = "SELECT * FROM CACIDB..Coach " +
                        "SELECT * FROM CACIDB..PhoneRec	" +
                        "SELECT * FROM CACIDB..PhRecRep	" +
                        "SELECT ROW_NUMBER() OVER(ORDER BY A.Coach_Code) AS List, " +
                        "* FROM CACIDB..Coach A " +
                        "JOIN CACIDB..PhoneRec B ON A.Com_Code=B.PhRec_Code " +
                        "JOIN CACIDB..PhRecRep C ON B.PhRec_Code=C.PhRec_Code " +
                        "WHERE A.Coach_Code=@Coach_Code AND A.Com_Code=@Com_Code " +
                        "ORDER BY A.Coach_Code, B.PhRec_Code ";

        SqlCommand AowCmd = new SqlCommand(sqlStr);
        AowCmd.Parameters.AddWithValue("@Com_Code", Com_Code);
        AowCmd.Parameters.AddWithValue("@Coach_Code", Coach_Code);

        new SQLAgent(DataBase.CACIDB).select(AowCmd, dt);

        return dt;
    }
    
    //電話記錄
    public DataTable getPhoneData(string Com_Code, string PhRec_ComCode)
    {
        DataTable dt = new DataTable();

        string sqlStr = "SELECT ROW_NUMBER() OVER(ORDER BY PRcRp_Handle) AS List, " +
                        "ISNULL(dbo.getSysCodeText('C','R',PRcRp_Handle),'') AS PRcRp_Handle_Text, " +
                        "* FROM CACIDB..PhRecRep WHERE PhRec_Code=@PhRec_ComCode " +
                        "ORDER BY PRcRp_Date ";

        SqlCommand AowCmd = new SqlCommand(sqlStr);
        AowCmd.Parameters.AddWithValue("@Com_Code", Com_Code);
        AowCmd.Parameters.AddWithValue("@PhRec_ComCode", PhRec_ComCode);

        new SQLAgent(DataBase.CACIDB).select(AowCmd, dt);

        return dt;
    }

    //管考記錄
    public DataTable get_EvaluationsData(string Com_Code, string Aow_Code, string Meeting_Code, string Meeting_Index)
    {
        DataTable dt = new DataTable();

        string sqlStr = "SELECT ROW_NUMBER() OVER(ORDER BY A.Pj_Code) AS List, " +
                        "* FROM CACIDB..Evaluations A " +
                        "JOIN CACIDB..MtgTimes B " +
                        "ON A.Meeting_Code=B.Meeting_Code AND A.Meeting_Index=B.Meeting_Index " +
                        "JOIN CACIDB..Committee C " +
                        "ON A.Comm_Code=C.Comm_Code " +
                        "WHERE A.Com_Code=@Com_Code AND A.Aow_Code=@Aow_Code AND A.Meeting_Code=@Meeting_Code AND A.Meeting_Index=@Meeting_Index " +
                        "ORDER BY A.Aow_Code ";

        SqlCommand AowCmd = new SqlCommand(sqlStr);
        AowCmd.Parameters.AddWithValue("@Com_Code", Com_Code);
        AowCmd.Parameters.AddWithValue("@Aow_Code", Aow_Code);
        AowCmd.Parameters.AddWithValue("@Meeting_Code", Meeting_Code);
        AowCmd.Parameters.AddWithValue("@Meeting_Index", Meeting_Index);

        new SQLAgent(DataBase.CACIDB).select(AowCmd, dt);

        return dt;
    }  

    #endregion
}