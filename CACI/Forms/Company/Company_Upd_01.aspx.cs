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

public partial class Company_Upd_01 : IUpdateUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();
        if (!to.isColumnExist("Com_Code"))
            to.setValue("Com_Code", lblComm_Code.Text);

        if (!to.isColumnExist("Com_Name"))
            to.setValue("Com_Name", txt_Com_Name.Text);

        if (!to.isColumnExist("Com_Tonum"))
            to.setValue("Com_Tonum", txt_Com_Tonum.Text);

        if (!to.isColumnExist("Com_Boss"))
            to.setValue("Com_Boss", txt_Com_Boss.Text);

        if (!to.isColumnExist("Com_BsIDNO"))
            to.setValue("Com_BsIDNO", txt_Com_BsIDNO.Text);

        if (!to.isColumnExist("Com_OrgForm"))
            to.setValue("Com_OrgForm", this.ddl_Com_OrgForm.SelectedValue.ToString());

        if (!to.isColumnExist("Com_Capital"))
            to.setValue("Com_Capital", txt_Com_Capital.Text);

        if (!to.isColumnExist("Com_EmpNum"))
            to.setValue("Com_EmpNum", txt_Com_EmpNum.Text);

        if (!to.isColumnExist("Com_MnSectors"))
            to.setValue("Com_MnSectors", this.ddl_Com_MnSectors.SelectedValue.ToString());

        if (!to.isColumnExist("Com_MnOther"))
            to.setValue("Com_MnOther", txt_Com_MnOther.Text);

        if (!to.isColumnExist("Com_SbSectors"))
            to.setValue("Com_SbSectors", this.ddl_Com_SbSectors.SelectedValue.ToString());

        if (!to.isColumnExist("Com_SbOther"))
            to.setValue("Com_SbOther", txt_Com_SbOther.Text);

        if (!to.isColumnExist("Com_MnProduct"))
            to.setValue("Com_MnProduct", txt_Com_MnProduct.Text);

        if (!to.isColumnExist("Com_Tel"))
            to.setValue("Com_Tel", txt_Com_Tel.Text);

        if (!to.isColumnExist("Com_Fax"))
            to.setValue("Com_Fax", txt_Com_Fax.Text);

        if (!to.isColumnExist("Com_OPAddr"))
            to.setValue("Com_OPAddr", txt_Com_OPAddr.Text);

        if (!to.isColumnExist("Com_Url"))
            to.setValue("Com_Url", txt_Com_Url.Text);

        if (!to.isColumnExist("Com_Email"))
            to.setValue("Com_Email", txt_Com_Email.Text);

        if (!to.isColumnExist("Com_CttName"))
            to.setValue("Com_CttName", txt_Com_CttName.Text);

        if (!to.isColumnExist("Com_CttCell"))
            to.setValue("Com_CttCell", txt_Com_CttCell.Text);

        if (!to.isColumnExist("Com_CttTel"))
            to.setValue("Com_CttTel", txt_Com_CttTel.Text);

        if (!to.isColumnExist("Com_CttMail"))
            to.setValue("Com_CttMail", txt_Com_CttMail.Text);

        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (Directory.Exists(tempPath))
        {

            foreach (string _path in Directory.GetFiles(tempPath))
            {
                if (new FileInfo(_path).Name.StartsWith("PR_"))
                {
                    to.setValue("Com_MnPdFile1", _path);
                }
                else if (new FileInfo(_path).Name.StartsWith("RP_"))
                {
                    to.setValue("ComAtt_File2", _path);
                }
                else if (new FileInfo(_path).Name.StartsWith("FA_"))
                {
                    to.setValue("ComAtt_File3", _path);
                }
                else if (new FileInfo(_path).Name.StartsWith("OT_"))
                {
                    to.setValue("ComAtt_File4", _path);
                }
            }

        }
        //if (RadAsyncUpload1.UploadedFiles.Count > 0)
        //{
        //    to.setValue("Com_MnPdFile1", RadAsyncUpload1.UploadedFiles[0].FileName);
        //    to.setValue("Com_MnPdFile1_PATH", Request.PhysicalApplicationPath + @"UploadFile\Company\");
        //}

        //if (RadAsyncUpload2.UploadedFiles.Count > 0)
        //{
        //    to.setValue("ComAtt_Code2", lbl_ComAtt_Code2.Text);
        //    to.setValue("ComAtt_File2", RadAsyncUpload2.UploadedFiles[0].FileName);
        //}

        //if (RadAsyncUpload3.UploadedFiles.Count > 0)
        //{
        //    to.setValue("ComAtt_Code3", lbl_ComAtt_Code3.Text);
        //    to.setValue("ComAtt_File3", RadAsyncUpload3.UploadedFiles[0].FileName);
        //}

        //if (RadAsyncUpload4.UploadedFiles.Count > 0)
        //{
        //    to.setValue("ComAtt_Code4", lbl_ComAtt_Code4.Text);
        //    to.setValue("ComAtt_File4", RadAsyncUpload4.UploadedFiles[0].FileName);
        //}

        return to;
    }
    public override void RenderData(DataTO to)
    {
        BaseFun bf = new BaseFun();

        //基本資料
        this.lblComm_Code.Text = to.getValue("Com_Code").ToString();
        this.txt_Com_Name.Text = to.getValue("Com_Name").ToString();
        this.txt_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
        this.txt_Com_Boss.Text = to.getValue("Com_Boss").ToString();
        this.txt_Com_BsIDNO.Text = to.getValue("Com_BsIDNO").ToString();

        //組織形式(登記類型)
        this.ddl_Com_OrgForm.DataSource = bf.getSysCodeByKind("R", "T");
        this.ddl_Com_OrgForm.DataBind();
        this.ddl_Com_OrgForm.SelectedValue = to.getValue("Com_OrgForm").ToString();

        this.txt_Com_Capital.Text = to.getValue("Com_Capital").ToString();
        this.txt_Com_EmpNum.Text = to.getValue("Com_EmpNum").ToString();

        //主要產業別
        this.ddl_Com_MnSectors.DataSource = bf.getSysCodeByKind("I", "D");
        this.ddl_Com_MnSectors.DataBind();
        this.ddl_Com_MnSectors.SelectedValue = to.getValue("Com_MnSectors").ToString();
        this.txt_Com_MnOther.Text = to.getValue("Com_MnOther").ToString();

        //次要產業別
        this.ddl_Com_SbSectors.DataSource = bf.getSysCodeByKind("I", "D");
        this.ddl_Com_SbSectors.DataBind();
        this.ddl_Com_SbSectors.SelectedValue = to.getValue("Com_SbSectors").ToString();
        this.txt_Com_SbOther.Text = to.getValue("Com_SbOther").ToString();

        this.txt_Com_MnProduct.Text = to.getValue("Com_MnProduct").ToString();

        //單位聯絡資訊
        this.txt_Com_Tel.Text = to.getValue("Com_Tel").ToString();
        this.txt_Com_Fax.Text = to.getValue("Com_Fax").ToString();
        this.txt_Com_OPAddr.Text = to.getValue("Com_OPAddr").ToString();
        this.txt_Com_Url.Text = to.getValue("Com_Url").ToString();
        this.txt_Com_Email.Text = to.getValue("Com_Email").ToString();

        //聯絡人資訊
        this.txt_Com_CttName.Text = to.getValue("Com_CttName").ToString();
        this.txt_Com_CttCell.Text = to.getValue("Com_CttCell").ToString();
        this.txt_Com_CttTel.Text = to.getValue("Com_CttTel").ToString();
        this.txt_Com_CttMail.Text = to.getValue("Com_CttMail").ToString();

        //應檢附資料上傳

        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);
        else
        {
            Directory.Delete(tempPath, true);
            Directory.CreateDirectory(tempPath);
        }

        //檔案下載
        this.hpl_ComMnPd_file1.Text = "瀏覽";
        this.hpl_ComMnPd_file1.NavigateUrl = to.getValue("Com_MnPdFile1").ToString();
        hpl_ComMnPd_file1.Visible = true;
        this.lbl_ComAtt_Code2.Text = bf.getComAtt_Code(to.getValue("Com_Code").ToString(), "RP");

        this.hpl_ComAtt_file2.Text = "瀏覽";
        this.hpl_ComAtt_file2.NavigateUrl = bf.getComAtt_Path(to.getValue("Com_Code").ToString(), "RP");
        hpl_ComAtt_file2.Visible = true;

        this.lbl_ComAtt_Code3.Text = bf.getComAtt_Code(to.getValue("Com_Code").ToString(), "FA");
        this.hpl_ComAtt_file3.Text = "瀏覽";
        this.hpl_ComAtt_file3.NavigateUrl = bf.getComAtt_Path(to.getValue("Com_Code").ToString(), "FA");
        this.hpl_ComAtt_file3.Visible = true;

        this.lbl_ComAtt_Code4.Text = bf.getComAtt_Code(to.getValue("Com_Code").ToString(), "OT");
        this.hpl_ComAtt_file4.Text = "瀏覽";
        this.hpl_ComAtt_file4.NavigateUrl = bf.getComAtt_Path(to.getValue("Com_Code").ToString(), "OT");
        this.hpl_ComAtt_file4.Visible = true;

    }
    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "產業履歷管理作業─內網資料修改畫面";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "7.1.5";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//訊息列
        #endregion

        #region 宣告資訊

        BL = new Company_01BL();//邏輯層

        #endregion

        #region 按鈕定義


        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊

        BackPage = "Company_Qry_01.aspx";

        #endregion 導向頁面資訊

        #region 頁面檢查設定

        checkLoginType = checkLoginType.Need;

        #endregion
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        BaseFun bf = new BaseFun();

        //基本資料
        this.txt_Com_Name.Text = "";
        this.txt_Com_Tonum.Text = "";
        this.txt_Com_Boss.Text = "";
        this.txt_Com_BsIDNO.Text = "";

        //組織形式(登記類型)
        this.ddl_Com_OrgForm.DataSource = bf.getSysCodeByKind("R", "T");
        this.ddl_Com_OrgForm.DataBind();

        this.txt_Com_Capital.Text = "";
        this.txt_Com_EmpNum.Text = "";

        //主要產業別
        this.ddl_Com_MnSectors.DataSource = bf.getSysCodeByKind("I", "D");
        this.ddl_Com_MnSectors.DataBind();
        this.txt_Com_MnOther.Text = "";

        //次要產業別
        this.ddl_Com_SbSectors.DataSource = bf.getSysCodeByKind("I", "D");
        this.ddl_Com_SbSectors.DataBind();
        this.txt_Com_SbOther.Text = "";

        this.txt_Com_MnProduct.Text = "";

        //單位聯絡資訊
        this.txt_Com_Tel.Text = "";
        this.txt_Com_Fax.Text = "";
        this.txt_Com_OPAddr.Text = "";
        this.txt_Com_Url.Text = "";
        this.txt_Com_Email.Text = "";

        //聯絡人資訊
        this.txt_Com_CttName.Text = "";
        this.txt_Com_CttCell.Text = "";
        this.txt_Com_CttTel.Text = "";
        this.txt_Com_CttMail.Text = "";

        //應檢附資料上傳
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);
        else
        {
            Directory.Delete(tempPath, true);
            Directory.CreateDirectory(tempPath);
        }

        this.lbl_ComAtt_Code2.Text = "";
        this.lbl_ComAtt_Code3.Text = "";
        this.lbl_ComAtt_Code4.Text = "";
    }

    protected void txt_Com_Tonum_TextChanged(object sender, EventArgs e)
    {
        //BaseFun bf = new BaseFun();
        ////公司資料
        //DataTable compData = bf.getCompany(txt_Com_Tonum.Text);

        //if (compData.Rows.Count > 0)
        //{
        //    //System.Diagnostics.Debug.WriteLine(compData.Rows[0]["Com_Name"].ToString());
        //    hid_Com_Code.Value = compData.Rows[0]["Com_Code"].ToString();
        //    txt_Com_Name.Text = compData.Rows[0]["Com_Name"].ToString();
        //    txt_Com_Boss.Text = compData.Rows[0]["Com_Boss"].ToString();
        //    txt_Com_SetupTime.Text = Coach_01BL.chgEnDateToChnDate(compData.Rows[0]["Com_SetupTime"].ToString());
        //    //抓取附件的ID值
        //    this.ComAtt_Code1.Value = bf.getComAtt_Code(compData.Rows[0]["Com_Code"].ToString(), "IT");
        //    this.ComAtt_Code2.Value = bf.getComAtt_Code(compData.Rows[0]["Com_Code"].ToString(), "RP");
        //    this.ComAtt_Code3.Value = bf.getComAtt_Code(compData.Rows[0]["Com_Code"].ToString(), "FA");
        //    this.ComAtt_Code4.Value = bf.getComAtt_Code(compData.Rows[0]["Com_Code"].ToString(), "OT");

        //    //抓取銀行帳戶資料
        //    DataTable bankAccData = bf.getBankAcc(compData.Rows[0]["Com_Code"].ToString());
        //    if (bankAccData.Rows.Count > 0)
        //    {
        //        hid_BAcc_Code.Value = bankAccData.Rows[0]["BAcc_Code"].ToString();
        //        txt_BAcc_Accno.Text = bankAccData.Rows[0]["BAcc_Accno"].ToString();
        //    }
        //    else
        //    {
        //        hid_BAcc_Code.Value = "N";
        //    }
        //}
        //else
        //{
        //    hid_Com_Code.Value = "N";
        //}
    }
    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Com_Code");
    }

    protected override void AfterHandleUpdate(DataTO to)
    {
        //base.AfterHandleUpdate();
        
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
        //hideImgBtn(UpdateButton);
       
    }

    //----------------------------------------檔案上傳-------------------------------------------------//

    protected void Uploader_Product_FileUploaded(object sender, UploaderEventArgs e)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("PR_"))
                File.Delete(_path);
        }

        string filePath = tempPath + @"\PR_" + e.FileName;

        e.CopyTo(filePath);

        ((UploadAttachments)sender).InsertButton.Enabled = false;

        hpl_ComMnPd_file1.Visible = false;
    }

    protected void Uploader_Product_AttachmentRemoveClicked(object sender, AttachmentItemEventArgs args)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("PR_"))
                File.Delete(_path);
        }

        ((UploadAttachments)sender).InsertButton.Enabled = true;
    }

    //protected void RadAsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    //{
    //    string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

    //    string filePath = tempPath + @"\" + "1_" + e.File.FileName;

    //    e.File.SaveAs(filePath);
    //}

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

    //protected void RadAsyncUpload2_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    //{
    //    string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

    //    string filePath = tempPath + @"\" + "2_" + e.File.FileName;

    //    e.File.SaveAs(filePath);
    //}

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

    //protected void RadAsyncUpload3_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    //{
    //    string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

    //    string filePath = tempPath + @"\" + "3_" + e.File.FileName;

    //    e.File.SaveAs(filePath);
    //}

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

    //protected void RadAsyncUpload4_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    //{
    //    string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

    //    string filePath = tempPath + @"\" + "4_" + e.File.FileName;

    //    e.File.SaveAs(filePath);
    //}

    protected void ddl_Com_MnSectors_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_Com_MnSectors.SelectedValue == "P")
        {
            this.txt_Com_MnOther.Visible = true;
            this.txt_Com_MnOther.Text = "";
        }
        else
        {
            this.txt_Com_MnOther.Visible = false;
            this.txt_Com_MnOther.Text = "";
        }
    }

    protected void ddl_Com_SbSectors_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_Com_SbSectors.SelectedValue == "P")
        {
            this.txt_Com_SbOther.Visible = true;
            this.txt_Com_SbOther.Text = "";
        }
        else
        {
            this.txt_Com_SbOther.Visible = false;
            this.txt_Com_SbOther.Text = "";
        }
    }

}