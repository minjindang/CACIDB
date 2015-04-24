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

public partial class Project_Lis_01 : IMMDListUI
{

    /*
     * grv_Score
     * grv_SmpStage
     */

    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "專案設定作業─獎補助專案資料查看畫面";
        ProgNum = "1.2.2";
        ProgType = PAGE_ACTION.PAGE_LIST;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grv_Score, pnlGridView, lblPage));
        addDetailMember(new DetailDataGroup(grv_PjStage, pnlGridView2, lblPage2));
        addDetailMember(new DetailDataGroup(grv_CommGroup, pnlGridView3, lblPage3));

        #endregion

        #region 宣告資訊
        BL = new Project_01BL();
        #endregion

        #region 按鈕定義 start

        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊
        BackPage = "Project_Qry_01.aspx";
        #endregion

        #region 頁面檢查設定
        checkLoginType = checkLoginType.Need;
        #endregion
    }

    public override void SetDefault()
    {
        //ddl_Pj_Kind.DataSource = new BaseFun().getSysCodeByKind("P", "K");
        //ddl_Pj_Kind.DataBind();
        //申請表單樣式
        ddl_Pj_PjFill.DataSource = new BaseFun().getPjClass();
        ddl_Pj_PjFill.DataTextField = "PjCs_Name";
        ddl_Pj_PjFill.DataValueField = "PjCs_Code";
        ddl_Pj_PjFill.DataBind();
    }

    protected override void SetHandler()
    {
        base.SetHandler();

        //TemplateSelection="grv_Score_TemplateSelection" TemplateDataModeSelection="grv_Score_TemplateDataModeSelection"

        grv_Score.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Score_TemplateSelection);

        grv_Score.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Score_TemplateDataModeSelection);

        //TemplateSelection="grv_SmpStage_TemplateSelection" TemplateDataModeSelection="grv_SmpStage_TemplateDataModeSelection" 

        grv_PjStage.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_PjStage_TemplateSelection);

        grv_PjStage.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_PjStage_TemplateDataModeSelection);

        this.grv_CommGroup.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_CommGroup_TemplateSelection);

        this.grv_CommGroup.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_CommGroup_TemplateDataModeSelection);


    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Pj_Code");
    }

    void grv_Score_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_Score_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\Score_Lis_02.ascx";
    }

    void grv_PjStage_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_PjStage_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\PjStage_Lis_02.ascx";
    }

    void grv_CommGroup_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_CommGroup_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\CommGroup_Lis_01.ascx";
    }

    public override void RenderData(DataTO to)
    {
        BaseFun bf = new BaseFun();
        lbl_Pj_Code.Text = to.getValue("Pj_Code").ToString();
        lbl_Pj_Name.Text = to.getValue("Pj_Name").ToString();
        lbl_Pj_Trans.Text = to.getValue("Pj_Trans").ToString();
        //承辦人
        lbl_UserAcc.Text = bf.getUserNameByCode(to.getValue("Pj_User_Code").ToString());
        lbl_Pj_WebExp.Text = to.getValue("Pj_WebExp").ToString();
        lbl_Pj_PjIntro.Text = to.getValue("Pj_PjIntro").ToString();
        lbl_Pj_PjNote.Text = to.getValue("Pj_PjNote").ToString();
        lbl_Pj_StartDate.Text = Project_01BL.chgEnDateToChnDate(to.getValue("Pj_StartDate").ToString().Split(' ')[0]);
        lbl_Pj_BgnDate.Text = Project_01BL.chgEnDateToChnDate(to.getValue("Pj_BgnDate").ToString().Split(' ')[0]) + "~" + Project_01BL.chgEnDateToChnDate(to.getValue("Pj_EndDate").ToString().Split(' ')[0]);
        lbl_Pj_Fund.Text = bf.getCurrencySymbol(Convert.ToInt32(to.getValue("Pj_Fund")));
        if (to.getValue("Pj_PjFile").ToString() != "")
        {
            //hyp_Pj_PjFile.Text = new FileInfo(to.getValue("Pj_PjFile").ToString()).Name;
            //hyp_Pj_PjFile.NavigateUrl = to.getValue("Pj_PjFile").ToString();
        }
        ddl_Pj_PjFill.SelectedValue = to.getValue("Pj_PjFill").ToString();
    }

    protected void grv_PjStage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BaseFun bf = new BaseFun();
            e.Row.Cells[3].Text = bf.getSysCodeValue("S", "K", e.Row.Cells[3].Text);
        }
    }
}