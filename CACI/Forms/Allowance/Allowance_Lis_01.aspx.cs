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

public partial class Allowance_Lis_01 : IMMDListUI
{

    /*
     * grv_Score
     * grv_SmpStage
     */

    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "獎補助作業─內網資料查看畫面創業圓夢計畫";
        ProgNum = "2.1.2";
        ProgType = PAGE_ACTION.PAGE_LIST;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grvQuery, pnlGridView, lblPage));
        addDetailMember(new DetailDataGroup(grv_AowStage, pnlGridView2, lblPage2));

        #endregion

        #region 宣告資訊
        BL = new Allowance_01BL();
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
        //負責人性別
        rbl_Com_BsGender.DataSource = bf.getSysCodeByKind("S", "E");
        rbl_Com_BsGender.DataTextField = "Sys_CdText";
        rbl_Com_BsGender.DataValueField = "Sys_CdCode";
        rbl_Com_BsGender.DataBind();
        //產業別:
        rbl_ApPj_Msectors.DataSource = bf.getSysCodeByKind("I", "D");
        rbl_ApPj_Msectors.DataTextField = "Sys_CdText";
        rbl_ApPj_Msectors.DataValueField = "Sys_CdCode";
        rbl_ApPj_Msectors.DataBind();
        //申請組別
        ddl_ApPj_ApGroup.DataSource = bf.getSysCodeByKind("P", "G");
        ddl_ApPj_ApGroup.DataTextField = "Sys_CdText";
        ddl_ApPj_ApGroup.DataValueField = "Sys_CdCode";
        ddl_ApPj_ApGroup.DataBind();
        //公司登記所在地
        ddl_Com_PostCode.DataSource = bf.getTableData("PostCode");
        ddl_Com_PostCode.DataTextField = "AreaName";
        ddl_Com_PostCode.DataValueField = "Post_Code";
        ddl_Com_PostCode.DataBind();
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
        BaseFun bf = new BaseFun();
        //團體
        lbl_Aow_GPName.Text = to.getValue("Aow_GPName").ToString();
        lbl_Aow_RegNum.Text = to.getValue("Aow_RegNum").ToString();
        lbl_Aow_FMan.Text = to.getValue("Aow_FMan").ToString();
        lbl_Aow_FMIDNO.Text = to.getValue("Aow_FMIDNO").ToString();
        //單位
        lbl_Com_Name.Text = to.getValue("Com_Name").ToString();
        lbl_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
        lbl_Com_CttCell.Text = to.getValue("Com_CttCell").ToString();
        if (!string.IsNullOrEmpty(to.getValue("Com_BsGender").ToString()))
            rbl_Com_BsGender.SelectedValue = to.getValue("Com_BsGender").ToString();
        lbl_Com_BsTel.Text = to.getValue("Com_BsTel").ToString();
        lbl_Com_BsNightTel.Text = to.getValue("Com_BsNightTel").ToString();
        lbl_Com_CttName.Text = to.getValue("Com_CttName").ToString();
        lbl_Com_CttTel.Text = to.getValue("Com_CttTel").ToString();
        ddl_Com_PostCode.SelectedValue = to.getValue("Com_PostCode").ToString();
        lbl_Com_BsTel.Text = to.getValue("Com_BsTel").ToString();
        lbl_Com_BsNightTel.Text = to.getValue("Com_BsNightTel").ToString();
        lbl_Com_Fax.Text = to.getValue("Com_Fax").ToString();
        lbl_Com_BsTel.Text = to.getValue("Com_BsTel").ToString();
        lbl_Com_Email.Text = to.getValue("Com_Email").ToString();
        lbl_Com_OPAddr.Text = to.getValue("Com_OPAddr").ToString();
        lbl_Com_Url.Text = to.getValue("Com_Url").ToString();
        lbl_Com_CttName.Text = to.getValue("Com_CttName").ToString();
        lbl_Com_CttTel.Text = to.getValue("Com_CttTel").ToString();
        lbl_Com_CttCell.Text = to.getValue("Com_CttCell").ToString();
        lbl_Com_Account.Text = to.getValue("Com_Account").ToString();
        lbl_Com_Pass.Text = to.getValue("Com_Pass").ToString();
        //計劃資料
        lbl_ApPj_Name.Text = to.getValue("ApPj_Name").ToString();
        lbl_ApPj_BgnDate.Text = to.getValue("ApPj_BgnDate").ToString();
        lbl_ApPj_EndDate.Text = to.getValue("ApPj_EndDate").ToString();
        rbl_ApPj_Msectors.SelectedValue = to.getValue("ApPj_Msectors").ToString();
        ddl_ApPj_ApGroup.SelectedValue = to.getValue("ApPj_ApGroup").ToString();
        lbl_ApPj_Goal.Text = to.getValue("ApPj_Goal").ToString();
        lbl_ApPj_Policies.Text = to.getValue("ApPj_Policies").ToString();
        lbl_ApPj_Profit.Text = to.getValue("ApPj_Profit").ToString();
        lbl_ApPj_Solution.Text = to.getValue("ApPj_Solution").ToString();
        //總經費&申請補助(三位數)
        lbl_ApPj_TotAmt.Text = bf.getCurrencySymbol(Convert.ToInt32(to.getValue("ApPj_TotAmt")));
        lbl_ApPj_AowAmt.Text = bf.getCurrencySymbol(Convert.ToInt32(to.getValue("ApPj_AowAmt")));
        lbl_ApPj_OthAmt.Text = bf.getCurrencySymbol(Convert.ToInt32(to.getValue("ApPj_OthAmt")));
    }


}