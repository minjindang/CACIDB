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

public partial class PjSamples_Lis_01 : IMMDListUI
{
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "獎補助範本資料查看畫面";
        ProgNum = "1.1.2";
        ProgType = PAGE_ACTION.PAGE_LIST;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grv_Score, pnlGridView, lblPage));
        addDetailMember(new DetailDataGroup(grv_SmpStage, pnlGridView2, lblPage2));

        #endregion

        #region 宣告資訊
        BL = new PjSamples_01BL();
        #endregion

        #region 按鈕定義 start

        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊
        BackPage = "PjSamples_Qry_01.aspx";
        #endregion

        #region 頁面檢查設定
        checkLoginType = checkLoginType.Need;
        #endregion
    }

    protected override void SetHandler()
    {
        base.SetHandler();

        grv_Score.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Score_TemplateSelection);
        grv_Score.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Score_TemplateDataModeSelection);

        grv_SmpStage.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_SmpStage_TemplateSelection);
        grv_SmpStage.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_SmpStage_TemplateDataModeSelection);
        
    }

    public override void SetDefault()
    {
        //ddl_PjSp_Kind.DataSource = new BaseFun().getSysCodeByKind("P", "K");
        //ddl_PjSp_Kind.DataBind();

        //申請表單樣式
        ddl_Pj_PjFill.DataSource = new BaseFun().getPjClass();
        ddl_Pj_PjFill.DataTextField = "PjCs_Name";
        ddl_Pj_PjFill.DataValueField = "PjCs_Code";
        ddl_Pj_PjFill.DataBind();
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("PjSp_Code");
    }

    void grv_Score_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_Score_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\Score_Lis_01.ascx";
    }

    void grv_SmpStage_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_SmpStage_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\SmpStage_Lis_01.ascx";
    }

    public override void RenderData(DataTO to)
    {
        lbl_PjSp_Code.Text = to.getValue("PjSp_Code").ToString();

        lbl_PjSp_Name.Text = to.getValue("PjSp_Name").ToString();
        lbl_PjSp_Trans.Text = to.getValue("PjSp_Trans").ToString();
        lbl_UserAcc.Text = new BaseFun().getUserNameByCode(to.getValue("PjSp_User_Code").ToString());
        lbl_PjSp_WebExp.Text = to.getValue("PjSp_WebExp").ToString();
        lbl_PjSp_PjIntro.Text = to.getValue("PjSp_PjIntro").ToString();
        lbl_PjSp_PjNote.Text = to.getValue("PjSp_PjNote").ToString();

        if (to.getValue("PjSp_PjFile").ToString() != "")
        {
            //hyp_PjSp_PjFile.Text = new FileInfo(to.getValue("PjSp_PjFile").ToString()).Name;
            //hyp_PjSp_PjFile.NavigateUrl = to.getValue("PjSp_PjFile").ToString();
        }

        ddl_Pj_PjFill.SelectedValue = to.getValue("PjSp_PjFill").ToString();
    }
}