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

public partial class Allowance_Lis_02 : IMMDListUI
{

    /*
     * grv_Score
     * grv_SmpStage
     */

    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "獎補助作業─內網資料查看畫面一般徵件計畫";
        ProgNum = "2.1.19";
        ProgType = PAGE_ACTION.PAGE_LIST;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grvQuery, pnlGridView, lblPage));
        addDetailMember(new DetailDataGroup(grv_AowStage, pnlGridView2, lblPage2));

        #endregion

        #region 宣告資訊
        BL = new Allowance_02BL();
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
        //組織形式
        ddl_Com_OrgForm.DataSource = bf.getSysCodeByKind("R", "T");
        ddl_Com_OrgForm.DataTextField = "Sys_CdText";
        ddl_Com_OrgForm.DataValueField = "Sys_CdCode";
        ddl_Com_OrgForm.DataBind();
        //產業別:
        rbl_Com_MnSectors.DataSource = bf.getSysCodeByKind("I", "D");
        rbl_Com_MnSectors.DataTextField = "Sys_CdText";
        rbl_Com_MnSectors.DataValueField = "Sys_CdCode";
        rbl_Com_MnSectors.DataBind();
        //申請組別
        ddl_ApPj_ApGroup.DataSource = bf.getSysCodeByKind("P", "G");
        ddl_ApPj_ApGroup.DataTextField = "Sys_CdText";
        ddl_ApPj_ApGroup.DataValueField = "Sys_CdCode";
        ddl_ApPj_ApGroup.DataBind();
        //商品類別
        ddl_ApPj_ProdCls.DataSource = bf.getSysCodeByKind("A", "P");
        ddl_ApPj_ProdCls.DataTextField = "Sys_CdText";
        ddl_ApPj_ProdCls.DataValueField = "Sys_CdCode";
        ddl_ApPj_ProdCls.DataBind();
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
        //單位(公司)基本資料
        lbl_Com_Name.Text = to.getValue("Com_Name").ToString();
        lbl_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
        BaseFun bf = new BaseFun();
        lbl_Com_Capital.Text = bf.getCurrencySymbol((Convert.ToInt32(to.getValue("Com_Capital").ToString()))) + "元";   //金額欄位每3位數(千)加逗號
        lbl_Com_LTover.Text = bf.getCurrencySymbol((Convert.ToInt32(to.getValue("Com_LTover").ToString()))) + "元";     //金額欄位每3位數(千)加逗號
        ddl_Com_OrgForm.SelectedValue = to.getValue("Com_OrgForm").ToString();
        lbl_Com_Fax.Text = to.getValue("Com_Fax").ToString();
        lbl_Com_Boss.Text = to.getValue("Com_Boss").ToString();
        lbl_Com_BsTel.Text = to.getValue("Com_BsTel").ToString();
        lbl_Com_CttName.Text = to.getValue("Com_CttName").ToString();
        lbl_Com_CttTel.Text = to.getValue("Com_CttTel").ToString();
        lbl_Com_Email.Text = to.getValue("Com_Email").ToString();
        lbl_Com_OPAddr.Text = to.getValue("Com_OPAddr").ToString();
        lbl_Com_Url.Text = to.getValue("Com_Url").ToString();
        lbl_Com_MnProduct.Text = to.getValue("Com_MnProduct").ToString();
        rbl_Com_MnSectors.SelectedValue = to.getValue("Com_MnSectors").ToString();
        lbl_Com_Account.Text = to.getValue("Com_Account").ToString();
        txt_Com_Pass.Text = to.getValue("Com_Pass").ToString();
        ddl_Com_PostCode.SelectedValue = to.getValue("Com_PostCode").ToString();
        //計劃資料
        lbl_ApPj_Name.Text = to.getValue("ApPj_Name").ToString();
        lbl_ApPj_BgnDate.Text = to.getValue("ApPj_BgnDate").ToString();
        lbl_ApPj_EndDate.Text = to.getValue("ApPj_EndDate").ToString();
        ddl_ApPj_ProdCls.SelectedValue = to.getValue("ApPj_ProdCls").ToString();
        ddl_ApPj_ApGroup.SelectedValue = to.getValue("ApPj_ApGroup").ToString();
        lbl_ApPj_TotAmt.Text = bf.getCurrencySymbol(Convert.ToInt32(to.getValue("ApPj_TotAmt")));   //金額欄位每3位數(千)加逗號
        lbl_ApPj_AowAmt.Text = bf.getCurrencySymbol(Convert.ToInt32(to.getValue("ApPj_AowAmt")));   //金額欄位每3位數(千)加逗號
        lbl_ApPj_OthAmt.Text = bf.getCurrencySymbol(Convert.ToInt32(to.getValue("ApPj_OthAmt")));          //金額欄位每3位數(千)加逗號
        lbl_ApPj_Goal.Text = to.getValue("ApPj_Goal").ToString();
        lbl_ApPj_Policies.Text = to.getValue("ApPj_Policies").ToString();
        lbl_ApPj_Profit.Text = to.getValue("ApPj_Profit").ToString();
        lbl_ApPj_Solution.Text = to.getValue("ApPj_Solution").ToString();
    }

    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e) 
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[3].Text = new BaseFun().getCurrencySymbol(Convert.ToInt32(e.Row.Cells[3].Text)) + "元";
        //}
    }

}