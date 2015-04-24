using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;
using com.kangdainfo.online.WebBase.BL;

public partial class PjClass_Upd_01 : IMDUpdateUI 
{

    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        return to;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "專案計畫類別─資料維護";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "10.7.1";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grvQuery;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        ValidationDetailGroupID = "grvQuery";

        #endregion

        #region 宣告資訊

        BL = new PjClass_01BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        InsertDetailButton = btnDTL_INSERT;
        UpdateDetailButton = btnDTL_UPDATE;
        UpdateDetailButton.Visible = false;
        ClearDetailButton = btnDTL_CLEAR;

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;
        BackButton.Visible = false;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

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
        txt_PjCs_Code.Text = "";
        txt_PjCs_Name.Text = "";
        txt_PjCs_Path.Text = "";


    }

    public override ROW_CMD_TYPE GetRowCommand(string strCmd)
    {
        ROW_CMD_TYPE type = ROW_CMD_TYPE.ROW_CMD_UNKNOWN;

        switch (strCmd)
        {
            case "select":
                type = ROW_CMD_TYPE.ROW_CMD_MODIFY;
                break;
            case "del":
                type = ROW_CMD_TYPE.ROW_CMD_DELETE;
                break;
            default:
                break;
        }

        return type;
    }

    public override DataTO PopulateDetailData()
    {
        DataTO to = new DataTO();
        to.setValue("PjCs_Code", txt_PjCs_Code.Text);
        to.setValue("PjCs_Name", txt_PjCs_Name.Text);
        to.setValue("PjCs_Path", txt_PjCs_Path.Text);
        return to;
    }

    public override void RenderDetailData(DataTO to)
    {
        txt_PjCs_Code.Text = to.getValue("PjCs_Code").ToString();
        txt_PjCs_Name.Text = to.getValue("PjCs_Name").ToString();
        txt_PjCs_Path.Text = to.getValue("PjCs_Path").ToString();
    }

    public override void InitialDetail()
    {
        txt_PjCs_Code.Text = "";
        txt_PjCs_Name.Text = "";
        txt_PjCs_Path.Text = "";
    }

    public override void RenderData(DataTO to)
    {

    }

    public override bool CheckPK(DataTO to)
    {
        return true;
    }

    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            ((ImageButton)e.Row.Cells[0].Controls[0]).OnClientClick = "if(!confirm('確定要刪除?')){return false;}";
        //e.Row.Cells[3].Visible = false;
    }
}