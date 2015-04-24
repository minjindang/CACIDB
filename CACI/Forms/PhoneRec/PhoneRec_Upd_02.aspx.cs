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

public partial class PhoneRec_Upd_02 : IMMDListUI
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

        this.hid_PhRec_Code.Value = to.getValue("PhRec_Code").ToString();
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
        //to.setValue("PhRec_Code", "11111");

        return to.isColumnExist("PhRec_Code");
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "電話紀錄作業─導入電話紀錄歸戶或轉介功能";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "4.1.6";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//訊息列
        addDetailMember(new DetailDataGroup(grv_Aow, pnl_grv_Aow, lblPage1));
        addDetailMember(new DetailDataGroup(grv_Coach, pnl_grv_Coach, lblPage2));
        addDetailMember(new DetailDataGroup(grv_Consulting, pnl_grv_Consulting, lblPage3));

        #endregion

        #region 宣告資訊

        BL = new PhoneRec_01BL();//邏輯層

        #endregion

        #region 按鈕定義

        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊

        BackPage = "PhoneRec_Upd_01.aspx";

        #endregion 導向頁面資訊

        #region 頁面檢查設定

        checkLoginType = checkLoginType.Need;

        #endregion

        //addDetailMember(new DetailDataGroup(grv_CoachStage, pnl_CoachStage, lbl_CoachStage));
        //addDetailMember(new DetailDataGroup(grv_Meeting, pnl_Meeting, lbl_Meeting));
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


        DataTO to = (DataTO)Session[ICommonUI.Web_ID + Session.SessionID + "PhoneRec_Upd_02"];
        if (to != null && to.getValue("PhRec_Code").ToString() != "")
        {
            this.hid_PhRec_Code.Value = to.getValue("PhRec_Code").ToString();
            this.hid_PhRec_ComCode.Value = to.getValue("PhRec_ComCode").ToString();
            this.hid_PRcRp_Code.Value = to.getValue("PRcRp_Code").ToString();
        }
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

        //grv_Aow.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Aow_TemplateSelection);
        //grv_Aow.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Aow_TemplateDataModeSelection);

        grv_Coach.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Coach_TemplateSelection);
        grv_Coach.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Coach_TemplateDataModeSelection);

        grv_Consulting.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Consulting_TemplateSelection);
        grv_Consulting.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Consulting_TemplateDataModeSelection);
    }
    void grv_Aow_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\ConsultingHistory_Lis_01.ascx";
    }

    void grv_Aow_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }
    void grv_Consulting_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\ConsultingHistory_Lis_01.ascx";
    }

    void grv_Consulting_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_Coach_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\CoachHistory_Lis_01.ascx";
    }

    void grv_Coach_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }
    protected void Coach_SelectAll(object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)(this.grv_Coach.HeaderRow.Cells[0].FindControl("cbHead"))).Checked;
        foreach (GridViewRow gvRow in grv_Coach.Rows)
        {
            ((CheckBox)(gvRow.Cells[0].FindControl("cbItem"))).Checked = isChecked;
        }
    }
    protected void Aow_SelectAll(object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)(this.grv_Aow.HeaderRow.Cells[0].FindControl("cbHead"))).Checked;
        foreach (GridViewRow gvRow in grv_Aow.Rows)
        {
            ((CheckBox)(gvRow.Cells[0].FindControl("cbItem"))).Checked = isChecked;
        }
    }
    protected void Consulting_SelectAll(object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)(this.grv_Consulting.HeaderRow.Cells[0].FindControl("cbHead"))).Checked;
        foreach (GridViewRow gvRow in grv_Consulting.Rows)
        {
            ((CheckBox)(gvRow.Cells[0].FindControl("cbItem"))).Checked = isChecked;
        }
    }
    protected void btn_Archive_Click(object sender, ImageClickEventArgs e)
    {
        String SelectData = "";

        //DataTable dt_Coach = ICommonUI.GridView2DataTable(grv_Coach);

        foreach (GridViewRow gvRow in grv_Aow.Rows)
        {
            if (((CheckBox)(gvRow.Cells[0].FindControl("cbItem"))).Checked)
                SelectData +=  this.grv_Aow.DataKeys[gvRow.RowIndex].Value.ToString() + ",";

        }
        foreach (GridViewRow gvRow in grv_Coach.Rows)
        {
            if (((CheckBox)(gvRow.Cells[0].FindControl("cbItem"))).Checked)
                SelectData +=  this.grv_Coach.DataKeys[gvRow.RowIndex].Value.ToString() + ",";

        }
        foreach (GridViewRow gvRow in grv_Consulting.Rows)
        {
            if (((CheckBox)(gvRow.Cells[0].FindControl("cbItem"))).Checked)
                SelectData += this.grv_Consulting.DataKeys[gvRow.RowIndex].Value.ToString() + ",";

        }
        if (SelectData != "")
        {
            SelectData = SelectData.Substring(0, SelectData.Length - 1);
        }
        new PhoneRec_01BL().doArchive2PhoneRec(SelectData.Split(','), this.hid_PhRec_Code.Value);

    }
}