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

public partial class Project_Lis_02 : IMMDListUI
{

    /*
     * grv_Score
     * grv_SmpStage
     */

    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "專案設定作業─輔導專案資料查看";
        ProgNum = "1.2.5";
        ProgType = PAGE_ACTION.PAGE_LIST;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grv_PjStage, pnlGridView2, lblPage2));

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
    }

    protected override void SetHandler()
    {
        base.SetHandler();
        grv_PjStage.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_PjStage_TemplateSelection);
        grv_PjStage.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_PjStage_TemplateDataModeSelection);
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Pj_Code");
    }


    void grv_PjStage_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_PjStage_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\PjStage_Lis_03.ascx";
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
        lbl_Pj_Fund.Text = to.getValue("Pj_Fund").ToString();
        lbl_Pj_Fund.Text = bf.getCurrencySymbol(Convert.ToInt32(to.getValue("Pj_Fund")));
        if (to.getValue("Pj_PjFile").ToString() != "")
        {
            //hyp_Pj_PjFile.Text = new FileInfo(to.getValue("Pj_PjFile").ToString()).Name;
            //hyp_Pj_PjFile.NavigateUrl = to.getValue("Pj_PjFile").ToString();
        }
    }

    protected void grv_PjStage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if(!string.IsNullOrEmpty(e.Row.Cells[3].Text) && "&nbsp;" != e.Row.Cells[3].Text)
            //    e.Row.Cells[3].Text = Project_01BL.chgEnDateToChnDate(e.Row.Cells[3].Text.Split(' ')[0]);
            BaseFun bf = new BaseFun();
            e.Row.Cells[3].Text = bf.getSysCodeValue("S", "K", e.Row.Cells[3].Text);
        }
    }
}