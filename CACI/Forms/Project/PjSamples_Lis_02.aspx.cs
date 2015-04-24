using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class PjSamples_Lis_02 : IMDListUI
{
    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "輔導範本資料查看畫面";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "1.1.5";
        ProgType = PAGE_ACTION.PAGE_LIST;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grv_SmpStage;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        BackButton = btn_Back;

        #endregion

        #region 宣告資訊

        BL = new PjSamples_02BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        BackButton = btn_Back;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        BackPage = "PjSamples_Qry_01.aspx";

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
        grv_SmpStage.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_SmpStage_TemplateSelection);
        grv_SmpStage.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_SmpStage_TemplateDataModeSelection);
    }

    public override void RenderData(DataTO to)
    {
        lbl_PjSp_Code.Text = to.getValue("PjSp_Code").ToString();

        lbl_PjSp_Name.Text = to.getValue("PjSp_Name").ToString();
        lbl_PjSp_Trans.Text = to.getValue("PjSp_Trans").ToString();
        lbl_UserAcc.Text = to.getValue("PjSp_User_Code").ToString();
        lbl_PjSp_WebExp.Text = to.getValue("PjSp_WebExp").ToString();
        lbl_PjSp_PjIntro.Text = to.getValue("PjSp_PjIntro").ToString();
        lbl_PjSp_PjNote.Text = to.getValue("PjSp_PjNote").ToString();

        if (to.getValue("PjSp_PjFile").ToString() != "")
        {
            //hyp_PjSp_PjFile.Text = new FileInfo(to.getValue("PjSp_PjFile").ToString()).Name;
            //hyp_PjSp_PjFile.NavigateUrl = to.getValue("PjSp_PjFile").ToString();
        }

        
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("PjSp_Code");
    }

    void grv_SmpStage_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_SmpStage_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\SmpStage_Lis_02.ascx";
    }

    protected void grv_SmpStage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //階段類型
            e.Row.Cells[3].Text = new BaseFun().getSysCodeValue("S", "K", e.Row.Cells[3].Text);
        }
    }
}