using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class SM1002M : IMDUpdateUI
{

    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        if (!to.isColumnExist("Mcol_1"))
            to.setValue("Mcol_1", lbl_Mcol_1.Text);

        if (!to.isColumnExist("Mcol_2"))
            to.setValue("Mcol_2", txt_Mcol_2.Text);
        else
            to.updateValue("Mcol_2", txt_Mcol_2.Text);

        if (!to.isColumnExist("Mcol_3"))
            to.setValue("Mcol_3", txt_Mcol_3.Text);
        else
            to.updateValue("Mcol_3", txt_Mcol_3.Text);

        return to;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "Master-Detail 修改作業範本";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "0.2";
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

        BL = new SM1002BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        BackPage = "SM1002Q.aspx";

        #endregion 導向頁面資訊

        #region 頁面檢查設定
        checkLoginType = checkLoginType.None;
        #endregion
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        lbl_Mcol_1.Text = "";
        txt_Mcol_2.Text = "";
        txt_Mcol_3.Text = "";
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

        if (lbl_Dcol_1.Text == "N")
        {
            if (DetailGridView.DataKeys.Count == 0)
                to.setValue("Dcol_1", 1);
            else
                to.setValue("Dcol_1", Convert.ToInt32(DetailGridView.DataKeys[DetailGridView.DataKeys.Count - 1]["Dcol_1"]) + 1);
        }
        else
            to.setValue("Dcol_1", Convert.ToInt32(lbl_Dcol_1.Text));

        to.setValue("Dcol_2", txt_Dcol_2.Text);
        to.setValue("Dcol_3", txt_Dcol_2.Text);

        return to;
    }

    public override void RenderDetailData(DataTO to)
    {
        lbl_Dcol_1.Text = to.getValue("Dcol_1").ToString();
        txt_Dcol_2.Text = to.getValue("Dcol_2").ToString();
        txt_Dcol_3.Text = to.getValue("Dcol_3").ToString();
    }

    protected override void AfterHandleDetailUpdate()
    {
        base.AfterHandleDetailUpdate();

        lbl_Dcol_1.Text = "N";
    }

    public override void InitialDetail()
    {
        base.InitialDetail();

        lbl_Dcol_1.Text = "N";
        txt_Dcol_2.Text = "";
        txt_Dcol_3.Text = "";
    }

    public override void RenderData(DataTO to)
    {
        lbl_Mcol_1.Text = to.getValue("Mcol_1").ToString();

        txt_Mcol_2.Text = to.getValue("Mcol_2").ToString();

        txt_Mcol_3.Text = to.getValue("Mcol_3").ToString();
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Mcol_1");
    }
}