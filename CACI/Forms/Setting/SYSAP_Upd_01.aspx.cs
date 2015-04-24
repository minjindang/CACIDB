using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class SYSAP_Upd_01 : IMDUpdateUI
{

    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        to.setValue("Sys_CdKind", hid_Sys_CdKind.Value);
        to.setValue("Sys_CdType", hid_Sys_CdType.Value);
        to.setValue("Sys_CdNote", lbl_Sys_CdNote.Text);

        return to;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "系統參數設定作業─資料修改";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "9.1.14";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grvQuery;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        InsertDetailButton = btnDTL_INSERT;
        UpdateDetailButton = btnDTL_UPDATE;
        ClearDetailButton = btnDTL_CLEAR;

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        ValidationDetailGroupID = "grvQuery";

        #endregion

        #region 宣告資訊

        BL = new SYSAP_01BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        BackPage = "SYSAP_Qry_01.aspx";

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
        hid_Sys_CdKind.Value = "";
        hid_Sys_CdType.Value = "";
        lbl_Sys_CdNote.Text = "";
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

        to.setValue("Sys_CdCode", txt_Sys_CdCode.Text);
        to.setValue("Sys_CdText", txt_Sys_CdText.Text);
        to.setValue("Sys_CdState", ddl_Sys_CdState.SelectedValue);
        to.setValue("IsNew", hid_IsNew.Value);

        return to;
    }

    public override void RenderDetailData(DataTO to)
    {
        txt_Sys_CdCode.Text = to.getValue("Sys_CdCode").ToString();
        txt_Sys_CdText.Text = to.getValue("Sys_CdText").ToString();
        ddl_Sys_CdState.SelectedValue = to.getValue("Sys_CdState").ToString();
        hid_IsNew.Value = to.getValue("IsNew").ToString();

        txt_Sys_CdCode.Enabled = false;
    }

    public override void InitialDetail()
    {
        base.InitialDetail();

        txt_Sys_CdCode.Text = "";
        txt_Sys_CdText.Text = "";
        ddl_Sys_CdState.SelectedValue = "";
        hid_IsNew.Value = "N";
    }

    public override void RenderData(DataTO to)
    {
        hid_Sys_CdKind.Value = to.getValue("Sys_CdKind").ToString();
        hid_Sys_CdType.Value = to.getValue("Sys_CdType").ToString();
        lbl_Sys_CdNote.Text = to.getValue("Sys_CdNote").ToString();
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Sys_CdKind") && to.isColumnExist("Sys_CdType");
    }

    protected override void AfterHandleDetailInsert()
    {
        base.AfterHandleDetailInsert();

        txt_Sys_CdCode.Enabled = true;

        hid_IsNew.Value = "N";
    }

    protected override void AfterHandleDetailUpdate()
    {
        base.AfterHandleDetailUpdate();

        txt_Sys_CdCode.Enabled = true;

        hid_IsNew.Value = "N";
    }

    protected override void AfterHandleUpdate()
    {
        base.AfterHandleUpdate();

        btn_Update.Enabled = false;
        btn_Update.Visible = false;
    }


    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[5].Style.Add(HtmlTextWriterStyle.Display, "none");
    }
}