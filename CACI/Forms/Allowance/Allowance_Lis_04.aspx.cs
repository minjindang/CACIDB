using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.DB;

public partial class Allowance_Lis_04 : IMMDListUI
{

    /*
     * grv_Score
     * grv_SmpStage
     */

    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "獎補助作業─內網資料查看畫面縣市政府補助";
        ProgNum = "2.1.21";
        ProgType = PAGE_ACTION.PAGE_LIST;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grvQuery, pnlGridView, lblPage));
        addDetailMember(new DetailDataGroup(grv_AowStage, pnlGridView2, lblPage2));

        #endregion

        #region 宣告資訊
        BL = new Allowance_04BL();
        #endregion

        #region 按鈕定義 start

        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊
        BackPage = "Allowance_Qry_01.aspx";
        #endregion

        #region 頁面檢查設定
        checkLoginType = checkLoginType.Need;
        #endregion
    }

    public override void SetDefault()
    {
        BaseFun bf = new BaseFun();
        //產業別:
        rbl_Com_MnSectors.DataSource = bf.getSysCodeByKind("I", "D");
        rbl_Com_MnSectors.DataTextField = "Sys_CdText";
        rbl_Com_MnSectors.DataValueField = "Sys_CdCode";
        rbl_Com_MnSectors.DataBind();
    }

    protected override void SetHandler()
    {
        base.SetHandler();
        grv_AowStage.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_AowStage_TemplateDataModeSelection);
        grv_AowStage.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_AowStage_TemplateSelection);
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Aow_Code");
    }

    void grv_AowStage_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_AowStage_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\AowStage_Lis_01.ascx";
    }


    public override void RenderData(DataTO to)
    {
        //獎補助
        lbl_Aow_GPName.Text = to.getValue("Aow_GPName").ToString();
        lbl_Aow_RegNum.Text = to.getValue("Aow_RegNum").ToString();
        lbl_Aow_FMan.Text = to.getValue("Aow_FMan").ToString();
        lbl_Aow_FMIDNO.Text = to.getValue("Aow_FMIDNO").ToString();
        lbl_Aow_PJPM.Text = to.getValue("Aow_PJPM").ToString();
        lbl_Aow_PMTel.Text = to.getValue("Aow_PMTel").ToString();
        //單位(公司)基本資料
        lbl_Com_Name.Text = to.getValue("Com_Name").ToString();
        lbl_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();

        lbl_Com_Fax.Text = to.getValue("Com_Fax").ToString();
        lbl_Com_OPAddr.Text = to.getValue("Com_OPAddr").ToString();
        rbl_Com_MnSectors.SelectedValue = to.getValue("Com_MnSectors").ToString();
        lbl_Com_CttName.Text = to.getValue("Com_CttName").ToString();
        lbl_Com_CttTel.Text = to.getValue("Com_CttTel").ToString();
        lbl_Com_Fax.Text = to.getValue("Com_Fax").ToString();
        lbl_Com_CttMail.Text = to.getValue("Com_CttMail").ToString();
        lbl_Com_Url.Text = to.getValue("Com_Url").ToString();
        lbl_Com_Account.Text = to.getValue("Com_Account").ToString();
        txt_Com_Pass.Text = to.getValue("Com_Pass").ToString();
        //計劃資料
        lbl_ApPj_Name.Text = to.getValue("ApPj_Name").ToString();
        lbl_ApPj_Goal.Text = to.getValue("ApPj_Goal").ToString();
        lbl_ApPj_Policies.Text = to.getValue("ApPj_Policies").ToString();
        lbl_ApPj_Profit.Text = to.getValue("ApPj_Profit").ToString();
        lbl_ApPj_Solution.Text = to.getValue("ApPj_Solution").ToString();
        BaseFun bf = new BaseFun();
        lbl_ApPj_TotAmt.Text = bf.getCurrencySymbol(Convert.ToInt32(to.getValue("ApPj_TotAmt")));  //金額欄位每3位數(千)加逗號
        lbl_ApPj_AowAmt.Text = bf.getCurrencySymbol(Convert.ToInt32(to.getValue("ApPj_AowAmt")));  //金額欄位每3位數(千)加逗號
        lbl_ApPj_OthAmt.Text = bf.getCurrencySymbol(Convert.ToInt32(to.getValue("ApPj_OthAmt")));           //金額欄位每3位數(千)加逗號
    }

}