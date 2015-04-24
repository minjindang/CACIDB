using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.DB;
using System.IO;
using System.Data;
using CuteWebUI;

public partial class Coach_Ins_01 : IInsertUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        //單位(公司)基本資料

        //單位(公司)編號
        to.setValue("Com_Code", hid_Com_Code.Value);

        //單位(公司)名稱
        to.setValue("Com_Name", txt_Com_Name.Text);

        //稅編/身份證字號/立案號
        to.setValue("Com_Tonum", txt_Com_Tonum.Text);

        //單位(公司)負責人
        to.setValue("Com_Boss", txt_Com_Boss.Text);

        //負責人性別
        to.setValue("Com_BsGender", rbl_Com_BsGender.SelectedValue);

        //負責人手機
        to.setValue("Com_BsCell", txt_Com_BsCell.Text);

        //成立時間
        to.setValue("Com_SetupTime", Coach_01BL.chgChnDateToEnDate(txt_Com_SetupTime.Text));

        //負責人年齡層
        to.setValue("Com_BsAgeRange", rbl_Com_BsAgeRange.SelectedValue);

        //負責人最高學歷
        to.setValue("Com_BsDegree", rbl_Com_BsDegree.SelectedValue);

        //登記類型
        to.setValue("Com_OrgForm", ddl_Com_OrgForm.SelectedValue);

        //資本額
        to.setValue("Com_Capital", txt_Com_Capital.Text);

        //員工數
        to.setValue("Com_EmpNum", txt_Com_EmpNum.Text);

        //最近一年營業額
        to.setValue("Com_LTover", txt_Com_LTover.Text);

        //主要產品及服務
        to.setValue("Com_MnProduct", txt_Com_MnProduct.Text);

        //---銀行資料
        to.setValue("BAcc_Code", hid_BAcc_Code.Value);
        to.setValue("BAcc_Bankno", this.ddl_BAcc_Bankno.SelectedValue);
        to.setValue("BAcc_Accno", txt_BAcc_Accno.Text);
        to.setValue("BAcc_Mcredit", rbl_BAcc_Mcredit.SelectedValue);
        to.setValue("BAcc_Scredit", rbl_BAcc_Scredit.SelectedValue);
        to.setValue("BAcc_MCheck", rbl_BAcc_MCheck.SelectedValue);
        to.setValue("BAcc_SCheck", rbl_BAcc_SCheck.SelectedValue);


        //聯絡人手機
        to.setValue("Com_CttCell", txt_Com_CttCell.Text);

        //單位(公司)傳真
        to.setValue("Com_Fax", txt_Com_Fax.Text);

        //聯絡人公司電話
        to.setValue("Com_CttTel", txt_Com_CttTel.Text);

        //聯絡人e-mail
        to.setValue("Com_CttMail", txt_Com_CttMail.Text);

        //聯絡人姓名
        to.setValue("Com_CttName", txt_Com_CttName.Text);

        //申請輔導項目
        string selectedChKd_CodeTerms = this.CoachKindRadioButtonList1.ChKd_Code_Items;
        to.setValue("ChKd_Code", selectedChKd_CodeTerms);
        to.setValue("Coach_QstText", this.txt_Coach_QstText.Text);
        to.setValue("Coach_Code", hid_Coach_Code.Value);
        //檢附上傳資料

        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (Directory.Exists(tempPath))
        {
            // TODO:從暫存目錄轉存到正式目錄(UploadFile) 並記錄相關資訊至資料庫 ok
            if (to.getValue("Com_Code") != null && to.getValue("Com_Code").ToString() != "")
            {
                string targetPath = Request.PhysicalApplicationPath + @"UploadFile\Company\" + to.getValue("Com_Code");
                if (!Directory.Exists(targetPath))
                    Directory.CreateDirectory(targetPath); 
                
                //資料庫資料註冊
                foreach (string _path in Directory.GetFiles(tempPath))
                {
                    String fileName = new FileInfo(_path).Name;
                    if (fileName.StartsWith("IT_"))
                    {
                        to.setValue("ComAtt_FileIT", fileName);//IT
                        to.setValue("ComAtt_CodeIT", this.ComAtt_Code1.Value);
                        foreach (string _delPath in Directory.GetFiles(targetPath, "IT_*"))
                            File.Delete(_delPath);
                    }
                    if (fileName.StartsWith("RP_"))
                    {
                        to.setValue("ComAtt_FileRP", fileName);//RP
                        to.setValue("ComAtt_CodeRP", this.ComAtt_Code2.Value);
                        foreach (string _delPath in Directory.GetFiles(targetPath, "RP_*"))
                            File.Delete(_delPath);
                    }
                    if (fileName.StartsWith("FA_"))
                    {
                        to.setValue("ComAtt_FileFA", fileName);//FA
                        to.setValue("ComAtt_CodeFA", this.ComAtt_Code3.Value);
                        foreach (string _delPath in Directory.GetFiles(targetPath, "FA_*"))
                            File.Delete(_delPath);
                    }
                    if (fileName.StartsWith("OT_"))
                    {
                        to.setValue("ComAtt_FileOT", fileName);//OT
                        to.setValue("ComAtt_CodeOT", this.ComAtt_Code4.Value);
                        foreach (string _delPath in Directory.GetFiles(targetPath, "OT_*"))
                            File.Delete(_delPath);
                        
                    }

                    //移動TEMP檔案至TARGET
                    string destFileName = System.IO.Path.GetFileName(_path);
                    string destFile = System.IO.Path.Combine(targetPath, destFileName);

                    System.IO.File.Copy(_path, destFile,true);
                }

                Directory.Delete(tempPath, true);
                

                
            }
        }

        //專案代碼
        to.setValue("Pj_Code", this.lbl_Pj_Code.Text);
            
        return to;
    }

    protected override bool BeforeHandleInsert(DataTO to)
    {
        //return base.BeforeHandleInsert(to);
        if (to.getValue("ChKd_Code").ToString() == "")
        {
            customMSG_FAIL = "請選擇輔導項目(總計請勾選一項)";
            return false;
        }
        else
            return true;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "輔導資料─資料新增";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "5.1.3";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//訊息列
        #endregion

        #region 宣告資訊

        BL = new Coach_01BL();//邏輯層

        #endregion

        #region 按鈕定義

        InsertButton = btn_Insert;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊

        BackPage = "Coach_Qry_01.aspx";

        #endregion 導向頁面資訊

        #region 頁面檢查設定

        //checkLoginType = checkLoginType.Need;
        checkLoginType = checkLoginType.Need;

        #endregion
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        hid_Com_Code.Value = "N";
        hid_Coach_Code.Value = "N";
        hid_BAcc_Code.Value = "N";

        this.ComAtt_Code1.Value = "N";
        this.ComAtt_Code2.Value = "N";
        this.ComAtt_Code3.Value = "N";
        this.ComAtt_Code4.Value = "N";
        BaseFun bf = new BaseFun();
        //負責人性別
        rbl_Com_BsGender.DataSource = bf.getSysCodeByKind("S", "E");
        rbl_Com_BsGender.DataTextField = "Sys_CdText";
        rbl_Com_BsGender.DataValueField = "Sys_CdCode";
        rbl_Com_BsGender.RepeatDirection = RepeatDirection.Horizontal;
        rbl_Com_BsGender.DataBind();
        //負責人年齡層
        rbl_Com_BsAgeRange.DataSource = bf.getSysCodeByKind("Y", "E");
        rbl_Com_BsAgeRange.DataTextField = "Sys_CdText";
        rbl_Com_BsAgeRange.DataValueField = "Sys_CdCode";
        rbl_Com_BsAgeRange.RepeatDirection = RepeatDirection.Horizontal;
        rbl_Com_BsAgeRange.DataBind();
        //負責人最高學歷
        rbl_Com_BsDegree.DataSource = bf.getSysCodeByKind("T", "S");
        rbl_Com_BsDegree.DataTextField = "Sys_CdText";
        rbl_Com_BsDegree.DataValueField = "Sys_CdCode";
        rbl_Com_BsDegree.RepeatDirection = RepeatDirection.Horizontal;
        rbl_Com_BsDegree.DataBind();
        //債信(企業)
        rbl_BAcc_Mcredit.DataSource = bf.getSysCodeByKind("C", "D");
        rbl_BAcc_Mcredit.DataTextField = "Sys_CdText";
        rbl_BAcc_Mcredit.DataValueField = "Sys_CdCode";
        rbl_BAcc_Mcredit.DataBind();
        //債信(負責人/配偶)
        rbl_BAcc_Scredit.DataSource = bf.getSysCodeByKind("C", "D");
        rbl_BAcc_Scredit.DataTextField = "Sys_CdText";
        rbl_BAcc_Scredit.DataValueField = "Sys_CdCode";
        rbl_BAcc_Scredit.DataBind();
        //票信(企業)
        rbl_BAcc_MCheck.DataSource = bf.getSysCodeByKind("T", "I");
        rbl_BAcc_MCheck.DataTextField = "Sys_CdText";
        rbl_BAcc_MCheck.DataValueField = "Sys_CdCode";
        rbl_BAcc_MCheck.DataBind();
        //票信(負責人/配偶)
        rbl_BAcc_SCheck.DataSource = bf.getSysCodeByKind("T", "I");
        rbl_BAcc_SCheck.DataTextField = "Sys_CdText";
        rbl_BAcc_SCheck.DataValueField = "Sys_CdCode";
        rbl_BAcc_SCheck.DataBind();

        ddl_Com_OrgForm.DataSource = bf.getSysCodeByKind("R", "T");
        ddl_Com_OrgForm.DataTextField = "Sys_CdText";
        ddl_Com_OrgForm.DataValueField = "Sys_CdCode";
        ddl_Com_OrgForm.DataBind();

        ddl_BAcc_Bankno.DataSource = bf.getBankData();
        ddl_BAcc_Bankno.DataTextField = "Bank_Name";
        ddl_BAcc_Bankno.DataValueField = "Bank_Num";
        ddl_BAcc_Bankno.DataBind();

        this.CoachKindRadioButtonList1.ChKd_Code_Items = "E1";

        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);
        else
        {
            Directory.Delete(tempPath, true);
            Directory.CreateDirectory(tempPath);
        }

        DataTO to = (DataTO)Session[ICommonUI.Web_ID + Session.SessionID + "Coach_Ins_01"];
        if (to != null && to.getValue("Pj_Code").ToString() != "")
        {
            lbl_Pj_Code.Text = to.getValue("Pj_Code").ToString();
        }

    }

    protected void txt_Com_Tonum_TextChanged(object sender, EventArgs e)
    {
        BaseFun bf = new BaseFun();
        //公司資料
        DataTable compData = bf.getCompany(txt_Com_Tonum.Text);

        if (compData.Rows.Count > 0)
        {
            //System.Diagnostics.Debug.WriteLine(compData.Rows[0]["Com_Name"].ToString());
            hid_Com_Code.Value = compData.Rows[0]["Com_Code"].ToString();
            txt_Com_Name.Text = compData.Rows[0]["Com_Name"].ToString();
            txt_Com_Boss.Text = compData.Rows[0]["Com_Boss"].ToString();
            txt_Com_SetupTime.Text = !String.IsNullOrEmpty(compData.Rows[0]["Com_SetupTime"].ToString()) ? Coach_01BL.chgEnDateToChnDate(compData.Rows[0]["Com_SetupTime"].ToString()) : "101/01/01";
            txt_Com_Capital.Text = compData.Rows[0]["Com_Capital"].ToString();
            txt_Com_EmpNum.Text = compData.Rows[0]["Com_EmpNum"].ToString();
            txt_Com_LTover.Text = compData.Rows[0]["Com_LTover"].ToString();
            txt_Com_MnProduct.Text = compData.Rows[0]["Com_MnProduct"].ToString();
            txt_Com_Fax.Text = compData.Rows[0]["Com_Fax"].ToString();
            txt_Com_CttName.Text = compData.Rows[0]["Com_CttName"].ToString();
            txt_Com_CttCell.Text = compData.Rows[0]["Com_CttCell"].ToString();
            txt_Com_CttTel.Text = compData.Rows[0]["Com_CttTel"].ToString();
            txt_Com_CttMail.Text = compData.Rows[0]["Com_CttMail"].ToString();
            rbl_Com_BsGender.SelectedValue = !String.IsNullOrEmpty(compData.Rows[0]["Com_BsGender"].ToString()) ? compData.Rows[0]["Com_BsGender"].ToString() : "F";
            txt_Com_BsCell.Text = compData.Rows[0]["Com_BsCell"].ToString();
            rbl_Com_BsAgeRange.SelectedValue = !String.IsNullOrEmpty(compData.Rows[0]["Com_BsAgeRange"].ToString()) ? compData.Rows[0]["Com_BsAgeRange"].ToString() : "1";
            rbl_Com_BsDegree.SelectedValue = !String.IsNullOrEmpty(compData.Rows[0]["Com_BsDegree"].ToString()) ? compData.Rows[0]["Com_BsDegree"].ToString() : "UI";
            txt_Com_BsIDNO.Text = compData.Rows[0]["Com_BsIDNO"].ToString();
            ddl_Com_OrgForm.SelectedValue = compData.Rows[0]["Com_OrgForm"].ToString();

            //抓取附件的ID值
            this.ComAtt_Code1.Value = bf.getComAtt_Code(compData.Rows[0]["Com_Code"].ToString(), "IT");
            this.ComAtt_Code2.Value = bf.getComAtt_Code(compData.Rows[0]["Com_Code"].ToString(), "RP");
            this.ComAtt_Code3.Value = bf.getComAtt_Code(compData.Rows[0]["Com_Code"].ToString(), "FA");
            this.ComAtt_Code4.Value = bf.getComAtt_Code(compData.Rows[0]["Com_Code"].ToString(), "OT");

            //抓取銀行帳戶資料
            DataTable bankAccData = bf.getBankAcc(compData.Rows[0]["Com_Code"].ToString());
            if (bankAccData.Rows.Count > 0)
            {
                hid_BAcc_Code.Value = bankAccData.Rows[0]["BAcc_Code"].ToString();
                ddl_BAcc_Bankno.SelectedValue = bankAccData.Rows[0]["BAcc_Bankno"].ToString();
                txt_BAcc_Accno.Text = bankAccData.Rows[0]["BAcc_Accno"].ToString();
                if (!string.IsNullOrEmpty(bankAccData.Rows[0]["BAcc_Mcredit"].ToString()))
                    rbl_BAcc_Mcredit.SelectedValue = bankAccData.Rows[0]["BAcc_Mcredit"].ToString();
                if (!string.IsNullOrEmpty(bankAccData.Rows[0]["BAcc_Scredit"].ToString()))
                    rbl_BAcc_Scredit.SelectedValue = bankAccData.Rows[0]["BAcc_Scredit"].ToString();
                if (!string.IsNullOrEmpty(bankAccData.Rows[0]["BAcc_MCheck"].ToString()))
                    rbl_BAcc_MCheck.SelectedValue = bankAccData.Rows[0]["BAcc_MCheck"].ToString();
                if (!string.IsNullOrEmpty(bankAccData.Rows[0]["BAcc_SCheck"].ToString()))
                    rbl_BAcc_SCheck.SelectedValue = bankAccData.Rows[0]["BAcc_SCheck"].ToString();
            }
            else 
            {
                hid_BAcc_Code.Value = "N";
            }
        }
        else
        {
            hid_Com_Code.Value = "N";
        }
    }
    protected override void AfterHandleInsert(DataTO to)
    {
        base.AfterHandleInsert(to);

        //if (to.getValue("Com_Code") != null && to.getValue("Com_Code").ToString() != "")
        //{
        //    string targetPath = Request.PhysicalApplicationPath + @"UploadFile\Company\" + to.getValue("Com_Code");
        //    if (!Directory.Exists(targetPath))
        //        Directory.CreateDirectory(targetPath);
        //    string sourcePath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;
        //    string fileName = string.Empty;
        //    string destFile = string.Empty;
        //    if (System.IO.Directory.Exists(sourcePath))
        //    {
        //        string[] files = System.IO.Directory.GetFiles(sourcePath);
        //        foreach (string s in files)
        //        {
        //            fileName = System.IO.Path.GetFileName(s);
        //            destFile = System.IO.Path.Combine(targetPath, fileName);
        //            System.IO.File.Move(s, destFile);
        //        }
        //    }
        //    Directory.Delete(sourcePath, true);
        //}

        //hideImgBtn(btn_Insert);
        hideImgBtn(btn_Insert);
    }
    protected void Uploader_IT_FileUploaded(object sender, UploaderEventArgs e)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("IT_"))
                File.Delete(_path);
        }

        string filePath = tempPath + @"\IT_" + e.FileName;

        e.CopyTo(filePath);

        ((UploadAttachments)sender).InsertButton.Enabled = false;
    }

    protected void Uploader_IT_AttachmentRemoveClicked(object sender, AttachmentItemEventArgs args)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("IT_"))
                File.Delete(_path);
        }

        ((UploadAttachments)sender).InsertButton.Enabled = true;
    }

    protected void Uploader_RP_FileUploaded(object sender, UploaderEventArgs e)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("RP_"))
                File.Delete(_path);
        }

        string filePath = tempPath + @"\RP_" + e.FileName;

        e.CopyTo(filePath);

        ((UploadAttachments)sender).InsertButton.Enabled = false;

    }

    protected void Uploader_RP_AttachmentRemoveClicked(object sender, AttachmentItemEventArgs args)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("RP_"))
                File.Delete(_path);
        }

        ((UploadAttachments)sender).InsertButton.Enabled = true;
    }

    protected void Uploader_FA_FileUploaded(object sender, UploaderEventArgs e)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("FA_"))
                File.Delete(_path);
        }

        string filePath = tempPath + @"\FA_" + e.FileName;

        e.CopyTo(filePath);

        ((UploadAttachments)sender).InsertButton.Enabled = false;
    }

    protected void Uploader_FA_AttachmentRemoveClicked(object sender, AttachmentItemEventArgs args)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("FA_"))
                File.Delete(_path);
        }

        ((UploadAttachments)sender).InsertButton.Enabled = true;
    }

    protected void Uploader_OT_FileUploaded(object sender, UploaderEventArgs e)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("OT_"))
                File.Delete(_path);
        }

        string filePath = tempPath + @"\OT_" + e.FileName;

        e.CopyTo(filePath);

        ((UploadAttachments)sender).InsertButton.Enabled = false;
    }

    protected void Uploader_OT_AttachmentRemoveClicked(object sender, AttachmentItemEventArgs args)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("OT_"))
                File.Delete(_path);
        }

        ((UploadAttachments)sender).InsertButton.Enabled = true;
    }

}