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

public partial class Company_Lis_01 : IMMDListUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override void RenderData(DataTO to)
    {
        BaseFun bf = new BaseFun();

        //基本資料
        this.lbl_Com_Name.Text = to.getValue("Com_Name").ToString();
        this.lbl_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
        this.lbl_Com_Boss.Text = to.getValue("Com_Boss").ToString();
        this.lbl_Com_BsIDNO.Text = to.getValue("Com_BsIDNO").ToString();

        //組織形式(登記類型)
        this.ddl_Com_OrgForm.DataSource = bf.getSysCodeByKind("R", "T");
        this.ddl_Com_OrgForm.DataBind();
        this.ddl_Com_OrgForm.SelectedValue = to.getValue("Com_OrgForm").ToString();

        this.lbl_Com_Capital.Text = to.getValue("Com_Capital").ToString();
        this.lbl_Com_EmpNum.Text = to.getValue("Com_EmpNum").ToString();

        //主要產業別
        this.ddl_Com_MnSectors.DataSource = bf.getSysCodeByKind("I", "D");
        this.ddl_Com_MnSectors.DataBind();
        this.ddl_Com_MnSectors.SelectedValue = to.getValue("Com_MnSectors").ToString();
        this.lbl_Com_MnOther.Text = to.getValue("Com_MnOther").ToString();

        //次要產業別
        this.ddl_Com_SbSectors.DataSource = bf.getSysCodeByKind("I", "D");
        this.ddl_Com_SbSectors.DataBind();
        this.ddl_Com_SbSectors.SelectedValue = to.getValue("Com_SbSectors").ToString();
        this.lbl_Com_SbOther.Text = to.getValue("Com_SbOther").ToString();

        this.lbl_Com_MnProduct.Text = to.getValue("Com_MnProduct").ToString();

        //單位聯絡資訊
        this.lbl_Com_Tel.Text = to.getValue("Com_Tel").ToString();
        this.lbl_Com_Fax.Text = to.getValue("Com_Fax").ToString();
        this.lbl_Com_OPAddr.Text = to.getValue("Com_OPAddr").ToString();
        this.lbl_Com_Url.Text = to.getValue("Com_Url").ToString();
        this.lbl_Com_Email.Text = to.getValue("Com_Email").ToString();

        //聯絡人資訊
        this.lbl_Com_CttName.Text = to.getValue("Com_CttName").ToString();
        this.lbl_Com_CttCell.Text = to.getValue("Com_CttCell").ToString();
        this.lbl_Com_CttTel.Text = to.getValue("Com_CttTel").ToString();
        this.lbl_Com_CttMail.Text = to.getValue("Com_CttMail").ToString();

        //應檢附資料上傳

        //string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        //if (!Directory.Exists(tempPath))
        //    Directory.CreateDirectory(tempPath);
        //else
        //{
        //    Directory.Delete(tempPath, true);
        //    Directory.CreateDirectory(tempPath);
        //}

        //檔案下載
        this.hpl_ComMnPd_file1.Text = "瀏覽";
        this.hpl_ComMnPd_file1.NavigateUrl = to.getValue("Com_MnPdFile1").ToString();
        this.hpl_ComAtt_file2.Text = "瀏覽";
        this.hpl_ComAtt_file2.NavigateUrl = bf.getComAtt_Path(to.getValue("Com_Code").ToString(), "RP");
        this.hpl_ComAtt_file3.Text = "瀏覽";
        this.hpl_ComAtt_file3.NavigateUrl = bf.getComAtt_Path(to.getValue("Com_Code").ToString(), "FA");
        this.hpl_ComAtt_file4.Text = "瀏覽";
        this.hpl_ComAtt_file4.NavigateUrl = bf.getComAtt_Path(to.getValue("Com_Code").ToString(), "OT");
    }
    
    public override bool CheckPK(DataTO to)
    {
        if (to.isColumnExist("Com_Code"))
            return true;
        else
            return false;
    }
    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "產業履歷管理作業─內網資料查看畫面";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "7.1.3";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//訊息列

        addDetailMember(new DetailDataGroup(grv_Aow, pnl_Aow, lblPage2));//申請補助案歷程
        addDetailMember(new DetailDataGroup(grv_Coach1, pnl_Coach1, lblPage3));//諮詢歷程
        addDetailMember(new DetailDataGroup(grv_Coach2, pnl_Coach2, lblPage4));//輔導歷程
        //addDetailMember(new DetailDataGroup(grv_Money, pnl_Money, lblPage5));//申請融資歷程
        addDetailMember(new DetailDataGroup(grv_Phone, pnl_Phone, lblPage6));//電話記錄
        addDetailMember(new DetailDataGroup(grv_Evaluations, pnl_Evaluations, lblPage7));//管考記錄

        #endregion

        #region 宣告資訊

        BL = new Company_01BL();//邏輯層

        #endregion

        #region 按鈕定義

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
        this.lbl_Com_Name.Text = "";
        this.lbl_Com_Tonum.Text = "";
        this.lbl_Com_Boss.Text = "";
        this.lbl_Com_BsIDNO.Text = "";

        //組織形式(登記類型)
        this.ddl_Com_OrgForm.DataSource = bf.getSysCodeByKind("R", "T");
        this.ddl_Com_OrgForm.DataBind();

        this.lbl_Com_Capital.Text = "";
        this.lbl_Com_EmpNum.Text = "";

        //主要產業別
        this.ddl_Com_MnSectors.DataSource = bf.getSysCodeByKind("I", "D");
        this.ddl_Com_MnSectors.DataBind();
        this.lbl_Com_MnOther.Text = "";

        //次要產業別
        this.ddl_Com_SbSectors.DataSource = bf.getSysCodeByKind("I", "D");
        this.ddl_Com_SbSectors.DataBind();
        this.lbl_Com_SbOther.Text = "";

        this.lbl_Com_MnProduct.Text = "";

        //單位聯絡資訊
        this.lbl_Com_Tel.Text = "";
        this.lbl_Com_Fax.Text = "";
        this.lbl_Com_OPAddr.Text = "";
        this.lbl_Com_Url.Text = "";
        this.lbl_Com_Email.Text = "";

        //聯絡人資訊
        this.lbl_Com_CttName.Text = "";
        this.lbl_Com_CttCell.Text = "";
        this.lbl_Com_CttTel.Text = "";
        this.lbl_Com_CttMail.Text = "";

        //應檢附資料上傳
        //string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        //if (!Directory.Exists(tempPath))
        //    Directory.CreateDirectory(tempPath);
        //else
        //{
        //    Directory.Delete(tempPath, true);
        //    Directory.CreateDirectory(tempPath);
        //}
    }

    protected void btn_ComAtt_file1_Click(object sender, EventArgs e)
    {
        //String Url = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host +(Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + this.ComAtt_Path1.Value);
        //this.GoURL(Url);
    }

    protected override void SetHandler()
    {
        base.SetHandler();

        grv_Aow.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Aow_TemplateSelection);
        grv_Aow.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Aow_TemplateDataModeSelection);

        grv_Coach1.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Coach1_TemplateSelection);
        grv_Coach1.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Coach1_TemplateDataModeSelection);

        grv_Coach2.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Coach2_TemplateSelection);
        grv_Coach2.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Coach2_TemplateDataModeSelection);

        grv_Phone.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Phone_TemplateSelection);
        grv_Phone.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Phone_TemplateDataModeSelection);

        grv_Evaluations.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Evaluations_TemplateSelection);
        grv_Evaluations.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Evaluations_TemplateDataModeSelection);
    }

    void grv_Aow_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\Allowance_Qry_02.ascx";
    }

    void grv_Aow_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_Coach1_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\Coach_Qry_02.ascx";
    }

    void grv_Coach1_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_Coach2_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\Coach_Qry_03.ascx";
    }

    void grv_Coach2_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_Phone_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\PhoneRec_Qry_02.ascx";
    }

    void grv_Phone_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_Evaluations_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\Evaluations_Qry_02.ascx";
    }

    void grv_Evaluations_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }
}