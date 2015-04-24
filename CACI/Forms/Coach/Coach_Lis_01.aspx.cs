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

public partial class Coach_Lis_01 : IMMDListUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override void RenderData(DataTO to)
    {
        BaseFun bf = new BaseFun();
        lbl_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
        hid_Com_Code.Value = to.getValue("Com_Code").ToString();
        lbl_Com_Name.Text = to.getValue("Com_Name").ToString();
        lbl_Com_Boss.Text = to.getValue("Com_Boss").ToString();
        lbl_Com_SetupTime.Text = Coach_01BL.chgEnDateToChnDate(to.getValue("Com_SetupTime").ToString());
        lbl_Com_Capital.Text = to.getValue("Com_Capital").ToString();
        lbl_Com_EmpNum.Text = to.getValue("Com_EmpNum").ToString();
        lbl_Com_LTover.Text = to.getValue("Com_LTover").ToString();
        lbl_Com_MnProduct.Text = to.getValue("Com_MnProduct").ToString();
        lbl_Com_Fax.Text = to.getValue("Com_Fax").ToString();
        lbl_Com_CttName.Text = to.getValue("Com_CttName").ToString();
        lbl_Com_CttCell.Text = to.getValue("Com_CttCell").ToString();
        lbl_Com_CttTel.Text = to.getValue("Com_CttTel").ToString();
        lbl_Com_CttMail.Text = to.getValue("Com_CttMail").ToString();
        rbl_Com_BsGender.SelectedValue = to.getValue("Com_BsGender").ToString();
        lbl_Com_BsCell.Text = to.getValue("Com_BsCell").ToString();
        rbl_Com_BsAgeRange.SelectedValue = to.getValue("Com_BsAgeRange").ToString();
        rbl_Com_BsDegree.SelectedValue = to.getValue("Com_BsDegree").ToString();
        lbl_Com_BsIDNO.Text = to.getValue("Com_BsIDNO").ToString();
        ddl_Com_OrgForm.SelectedValue = to.getValue("Com_OrgForm").ToString();

        //抓取附件的Path值
        this.ComAtt_Path1.Value = bf.getComAtt_Path(hid_Com_Code.Value, "IT");
        if (ComAtt_Path1.Value == "N")
            this.btn_ComAtt_file1.Enabled = false;

        this.ComAtt_Path2.Value = bf.getComAtt_Path(hid_Com_Code.Value, "RP");
        if (ComAtt_Path2.Value == "N")
            this.btn_ComAtt_file2.Enabled = false;

        this.ComAtt_Path3.Value = bf.getComAtt_Path(hid_Com_Code.Value, "FA");
        if (ComAtt_Path3.Value == "N")
            this.btn_ComAtt_file3.Enabled = false;

        this.ComAtt_Path4.Value = bf.getComAtt_Path(hid_Com_Code.Value, "OT");
        if (ComAtt_Path4.Value == "N")
            this.btn_ComAtt_file4.Enabled = false;

        //抓取銀行帳戶資料
        DataTable bankAccData = bf.getBankAcc(hid_Com_Code.Value);
        if (bankAccData.Rows.Count > 0)
        {
            hid_BAcc_Code.Value = bankAccData.Rows[0]["BAcc_Code"].ToString();
            ddl_BAcc_Bankno.SelectedValue = bankAccData.Rows[0]["BAcc_Bankno"].ToString();
            lbl_BAcc_Accno.Text = bankAccData.Rows[0]["BAcc_Accno"].ToString();
            rbl_BAcc_Mcredit.SelectedValue = bankAccData.Rows[0]["BAcc_Mcredit"].ToString();
            rbl_BAcc_Scredit.SelectedValue = bankAccData.Rows[0]["BAcc_Scredit"].ToString();
            rbl_BAcc_MCheck.SelectedValue = bankAccData.Rows[0]["BAcc_MCheck"].ToString();
            rbl_BAcc_SCheck.SelectedValue = bankAccData.Rows[0]["BAcc_SCheck"].ToString();
        }

        hid_Coach_Code.Value = to.getValue("Coach_Code").ToString();
        lbl_Coach_QstText.Text = to.getValue("Coach_QstText").ToString();
        lbl_ChKd_Name.Text = bf.getChkdName(to.getValue("ChKd_Code").ToString());

    }
    
    public override bool CheckPK(DataTO to)
    {
        if (to.isColumnExist("Coach_Code"))
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
        ProgNm = "輔導資料查詢維護─內網資料查看";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "5.1.4";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//訊息列
        #endregion

        #region 宣告資訊

        BL = new Coach_01BL();//邏輯層

        #endregion

        #region 按鈕定義

        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊

        BackPage = "Coach_Qry_01.aspx";

        #endregion 導向頁面資訊

        #region 頁面檢查設定

        checkLoginType = checkLoginType.Need;

        #endregion

        addDetailMember(new DetailDataGroup(grv_CoachStage, pnl_CoachStage, lbl_CoachStage));
        //addDetailMember(new DetailDataGroup(grv_Meeting, pnl_Meeting, lbl_Meeting));
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        hid_Com_Code.Value = "N";
        hid_Coach_Code.Value = "N";
        hid_BAcc_Code.Value = "N";

        this.ComAtt_Path1.Value = "N";
        this.ComAtt_Path2.Value = "N";
        this.ComAtt_Path3.Value = "N";
        this.ComAtt_Path4.Value = "N";
        BaseFun bf = new BaseFun();
        //負責人性別
        rbl_Com_BsGender.DataSource = bf.getSysCodeByKind("S", "E");
        rbl_Com_BsGender.DataTextField = "Sys_CdText";
        rbl_Com_BsGender.DataValueField = "Sys_CdCode";
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


    }

    protected void btn_ComAtt_file1_Click(object sender, EventArgs e)
    {
        //String Url = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host +(Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + this.ComAtt_Path1.Value);
        //this.GoURL(Url);
    }

    protected override void SetHandler()
    {
        base.SetHandler();

        grv_CoachStage.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_CoachStage_TemplateSelection);
        grv_CoachStage.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_CoachStage_TemplateDataModeSelection);

        //grv_Meeting.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Meeting_TemplateSelection);
        //grv_Meeting.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Meeting_TemplateDataModeSelection);
    }

    void grv_Meeting_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\Evaluations_Lis_01.ascx";
    }

    void grv_Meeting_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }
    
    void grv_CoachStage_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\CoachStage_Lis_01.ascx";
    }

    void grv_CoachStage_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

}