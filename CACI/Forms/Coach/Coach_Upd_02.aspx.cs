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

public partial class Coach_Upd_02 : IMDListUI
{


    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "輔導作業─內網申請流程維護畫面";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "5.1.5";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grv_CoachStage;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        #endregion

        #region 宣告資訊

        BL = new CoachStage_01BL();//邏輯層

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
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {

    }

    protected override void SetHandler()
    {
        base.SetHandler();
        grv_CoachStage.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_CoachStage_TemplateDataModeSelection);
        grv_CoachStage.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_CoachStage_TemplateSelection);

    }

    void grv_CoachStage_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_CoachStage_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\CoachStage_Upd_01.ascx";
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Coach_Code");
    }

    public override void RenderData(DataTO to)
    {

        BaseFun bf = new BaseFun();
        this.lbl_Com_Name.Text = to.getValue("Com_Name").ToString();
        this.lbl_Com_Code.Text = to.getValue("Com_Code").ToString();
        this.lbl_Pj_Code.Text  = to.getValue("Pj_Code").ToString();
        this.lbl_Coach_Code.Text = to.getValue("Coach_Code").ToString();
        this.lbl_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
        this.lbl_Com_Boss.Text = to.getValue("Com_Boss").ToString();
        this.lbl_ChKd_Name.Text = bf.getChkdName(to.getValue("ChKd_Code").ToString());
        this.lbl_Pj_Name.Text = to.getValue("Pj_Name").ToString();

    }
}