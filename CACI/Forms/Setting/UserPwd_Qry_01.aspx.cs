using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using System.Web.UI.HtmlControls;

public partial class UserPwd_Qry_01 : IQueryUI
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
                cmdType = ROW_CMD_TYPE.ROW_CMD_MODIFY;
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

        if (txt_Comm_ComName.Text != "")
            to.setValue("Comm_ComName", txt_Comm_ComName.Text);
        if (txt_Comm_Name.Text != "")
            to.setValue("Comm_Name", txt_Comm_Name.Text);
        if (txt_Comm_Account.Text != "")
            to.setValue("Comm_Account", txt_Comm_Account.Text);
        if (ddl_Comm_AcStatus.SelectedValue != "")
            to.setValue("Comm_AcStatus", ddl_Comm_AcStatus.SelectedValue);

        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        txt_Comm_ComName.Text = to.getValue("Comm_ComName").ToString();
        txt_Comm_Name.Text = to.getValue("Comm_Name").ToString();
        txt_Comm_Account.Text = to.getValue("Comm_Account").ToString();

        if (to.getValue("Comm_AcStatus").ToString() != "")
            ddl_Comm_AcStatus.SelectedValue = to.getValue("Comm_AcStatus").ToString();
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddl_Page;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "帳號重設作業─資料查詢";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "9.1.3";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new UserPwd_01BL();//邏輯層

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

        #endregion 按鈕定義 end

        #region 導向頁面資訊
        //InsertPage = "UserPwd_Ins_01.aspx";//新增頁
        ModifyPage = "UserPwd_Upd_01.aspx";//修改頁
        #endregion 導向頁面資訊

        #region 登入檢查模式
        checkLoginType = com.kangdainfo.online.WebBase.UI.checkLoginType.Need;
        #endregion
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        txt_Comm_ComName.Text = "";
        txt_Comm_Name.Text = "";
        txt_Comm_Account.Text = "";
        ddl_Comm_AcStatus.SelectedValue = "";
    }

    protected override void ProcessRowDataBound(int idx, GridViewRow row, System.Data.DataRowView view)
    {
        base.ProcessRowDataBound(idx, row, view);

        row.Cells[5].Style.Add(HtmlTextWriterStyle.Display, "none");
    }
}