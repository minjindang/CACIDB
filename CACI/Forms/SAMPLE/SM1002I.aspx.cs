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

public partial class SM1002I : IMDInsertUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        //if( !to.isColumnExist("ID"))
        //    to.setValue("ID", txt_ID.Text);
        //else
        //    to.updateValue("ID", txt_ID.Text);


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
        ProgNm = "Master-Detail 新增作業範本";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "0.2";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grvQuery;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        ValidationDetailGroupID = "grvQuery";

        #endregion

        #region 宣告資訊

        BL = new SM1002BL();//邏輯層

        #endregion

        #region 按鈕定義

        InsertDetailButton = btnDTL_INSERT;
        UpdateDetailButton = btnDTL_UPDATE;
        ClearDetailButton = btnDTL_CLEAR;

        InsertButton = btn_Insert;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion

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
        txt_Mcol_2.Text = "";
        txt_Mcol_3.Text = "";
    }

    /// <summary>
    /// 設定GridView Button 事件動作
    /// </summary>
    /// <param name="strCmd"></param>
    /// <returns></returns>
    public override ROW_CMD_TYPE GetRowCommand(string strCmd)
    {
        ROW_CMD_TYPE type = ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
        
        switch (strCmd)
        {
            case "select" :
                type = ROW_CMD_TYPE.ROW_CMD_MODIFY;
                break;
            case "del" :
                type = ROW_CMD_TYPE.ROW_CMD_DELETE;
                break;
            default:
                break;
        }

        return type;
    }

    /// <summary>
    /// 蒐集Detal編輯區資料
    /// </summary>
    /// <returns></returns>
    public override DataTO PopulateDetailData()
    {
        DataTO to = new DataTO();

        if (hid_Dcol_1.Value == "N")
        {
            if( DetailGridView.DataKeys.Count == 0 )
                to.setValue("Dcol_1", 1);
            else
                to.setValue("Dcol_1", Convert.ToInt32(DetailGridView.DataKeys[DetailGridView.DataKeys.Count - 1]["Dcol_1"]) + 1);
        }
        else
            to.setValue("Dcol_1", Convert.ToInt32(hid_Dcol_1.Value));

        to.setValue("Dcol_2", txt_Dcol_2.Text);
        to.setValue("Dcol_3", txt_Dcol_3.Text);
        
        return to;
    }

    /// <summary>
    /// 將選取的Detail資料顯示於畫面
    /// </summary>
    /// <param name="to"></param>
    public override void RenderDetailData(DataTO to)
    {
        hid_Dcol_1.Value = to.getValue("Dcol_1").ToString();
        txt_Dcol_2.Text = to.getValue("Dcol_2").ToString();
        txt_Dcol_3.Text = to.getValue("Dcol_3").ToString();
    }

    protected override void AfterHandleDetailUpdate()
    {
        base.AfterHandleDetailUpdate();

        hid_Dcol_1.Value = "N";
    }

    /// <summary>
    /// Detail 編輯區初始化
    /// </summary>
    public override void InitialDetail()
    {
        base.InitialDetail();

        hid_Dcol_1.Value = "N";
        txt_Dcol_2.Text = "";
        txt_Dcol_3.Text = "";
    }
}