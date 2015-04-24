﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using System.Web.UI.HtmlControls;
using com.kangdainfo.online.WebControl;

public partial class SM1001_1Q : IQueryUI
{
     /// <summary>
    /// 將GridView中的控制項Command屬性,轉換為UI中用以判斷動作的列舉值(需實作)
    /// </summary>
    /// <param name="strCmd">控制項Command屬性</param>
    /// <returns>欲執行之動作</returns>
    public override ROW_CMD_TYPE GetRowCommand(string strCmd)
    {
        ROW_CMD_TYPE cmdType = ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
        switch (strCmd)
        {
            case "mod":
                cmdType = ROW_CMD_TYPE.ROW_CMD_MODIFY;
                break;
            case "del":
                cmdType = ROW_CMD_TYPE.ROW_CMD_DELETE;
                break;
            case "detail":
                cmdType = ROW_CMD_TYPE.ROW_CMD_QUERY_DETAIL;
                break;
            default:
                cmdType = ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
                break;
        }

        return cmdType;
    }

    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        if (txt_Mcol_1.Text != "")
            to.setValue("Mcol_1", txt_Mcol_1.Text);

        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        txt_Mcol_1.Text = to.getValue("Mcol_1").ToString();
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "Master查詢作業範本";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "0.1";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new SM1001BL();//邏輯層

        #endregion

        #region GridView資訊
        DataGridView = grvQuery;//GridView設定
        PagePanel = pnlPageInfo;//頁數資訊
        DataPanel = pnlGridView;//GridView區塊設定
        PageLabel = lblPageCount;//總筆數
        PageNumDropDownList = ddlPageNum;//目前頁數
        PageUpButton = lnkPageUP;//上一頁
        PageDownButton = lnkPageDown;//下一頁
        #endregion

        #region 按鈕定義 start

        QueryButton = btn_Query;
        ClearButton = btn_Clear;
        InsertButton = btn_Insert;
        //MarkButton = btn_Mark;

        #endregion 按鈕定義 end

        #region 導向頁面資訊
        InsertPage = "SM1001I.aspx";//新增頁
        ModifyPage = "SM1001M.aspx";//修改頁

        #endregion 導向頁面資訊

        #region 頁面檢查設定
        checkLoginType = checkLoginType.None;
        #endregion
    }

    protected override void SetHandler()
    {
        base.SetHandler();

        grvQuery.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grvQuery_TemplateSelection);
        grvQuery.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grvQuery_TemplateDataModeSelection);
    }

    void grvQuery_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grvQuery_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "UserPart\\WebControlSample.ascx";
    }

    protected override void AfterHandleQuery()
    {
        base.AfterHandleQuery();

        if (grvQuery is MDGridView)
            ((MDGridView)grvQuery).RowExpanded.CollapseAll();
    }

    //protected override after

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        txt_Mcol_1.Text = "";
    }

    //public override Control DoShowDetail(DataTO to)
    //{
    //    return btn_tst;
    //}
}