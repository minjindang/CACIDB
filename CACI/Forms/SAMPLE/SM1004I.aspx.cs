using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;

public partial class SM1004I : IMQDInsertUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

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
        ProgNm = "Master-QueryDetail 新增作業範本";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "0.4";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//訊息列

        DataGridView = grvQuery;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        QueryDataGridView = grvQueryData;
        QueryDataPanel = pnlQueryGridView;
        QueryPageLabel = lblQueryPage;

        ValidationDetailGroupID = "grvQuery";

        #endregion

        #region 宣告資訊

        BL = new SM1004BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        InsertButton = btn_Insert;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        QueryDetailButton = btnDTL_Query;
        InsertDetailButton = btnDTL_INSERT;
        ClearDetailButton = btnDTL_CLEAR;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        BackPage = "SM1004Q.aspx";

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

    public override ROW_CMD_TYPE GetRowCommand(string strCmd)
    {
        ROW_CMD_TYPE type = ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
        
        switch (strCmd)
        {
            case "del" :
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

        if( txt_DDcol_2.Text != "")
            to.setValue("DDcol_2", txt_DDcol_2.Text);

        if( txt_DDcol_3.Text != "" )
            to.setValue("DDcol_3", txt_DDcol_3.Text);
        
        return to;
    }

    public override void InitialDetail()
    {
        base.InitialDetail();

        txt_DDcol_2.Text = "";
        txt_DDcol_3.Text = "";
    }
}