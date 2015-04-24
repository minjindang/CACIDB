using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class CoachKind_Upd_01 : IMDUpdateUI
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
        ProgNm = "輔導類別資料作業─資料修改";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "1.10.9";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grvQuery;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        InsertDetailButton = btnDTL_INSERT;
        UpdateDetailButton = btnDTL_UPDATE;
        UpdateDetailButton.Visible = false;
        ClearDetailButton = btnDTL_CLEAR;

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;
        BackButton.Visible = false;

        ValidationDetailGroupID = "grvQuery";

        #endregion

        #region 宣告資訊

        BL = new CoachKind_01BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;
        BackButton.Visible = false;
        ClearButton.Visible = false;
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
        ddl_ChKd_Order.DataSource = new BaseFun().getSysCodeByKind("C", "K");
        ddl_ChKd_Order.DataBind();
    }

    public override void HandleDetailClear(object sender, EventArgs e)
    {
        base.HandleDetailClear(sender,e);
        txt_ChKd_Code.Text = "";
        txt_ChKd_Name.Text = "";
        txt_ChKd_Note.Text = "";
        ddl_ChKd_Order.SelectedIndex = 0;
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

        to.setValue("ChKd_Name", txt_ChKd_Name.Text);
        to.setValue("ChKd_Code", txt_ChKd_Code.Text);
        to.setValue("ChKd_Order", ddl_ChKd_Order.SelectedValue);
        to.setValue("ChKd_OrderName", ddl_ChKd_Order.SelectedItem.Text);
        to.setValue("ChKd_Note", txt_ChKd_Note.Text);
        to.setValue("IsNew", hid_IsNew.Value);

        return to;
    }

    public override void RenderDetailData(DataTO to)
    {
        txt_ChKd_Name.Text = to.getValue("ChKd_Name").ToString();
        txt_ChKd_Code.Text = to.getValue("ChKd_Code").ToString();
        ddl_ChKd_Order.Text = to.getValue("ChKd_Order").ToString();
        txt_ChKd_Note.Text = to.getValue("ChKd_Note").ToString();

        hid_IsNew.Value = to.getValue("IsNew").ToString();

    }

    public override void InitialDetail()
    {
        base.InitialDetail();
        hid_IsNew.Value = "Y";
    }

    public override void RenderData(DataTO to)
    {

    }

    public override bool CheckPK(DataTO to)
    {
        return true;
    }

    protected override void AfterHandleDetailInsert()
    {
        base.AfterHandleDetailInsert();

        hid_IsNew.Value = "Y";
    }

    protected override void AfterHandleDetailUpdate()
    {
        base.AfterHandleDetailUpdate();


        hid_IsNew.Value = "Y";
    }

    protected override void AfterHandleUpdate()
    {
        base.AfterHandleUpdate();

        btn_Update.Enabled = true;
    }
    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[4].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[6].Style.Add(HtmlTextWriterStyle.Display, "none");
    }
}