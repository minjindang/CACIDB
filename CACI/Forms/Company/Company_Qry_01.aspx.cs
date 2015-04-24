using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using System.Web.UI.HtmlControls;

public partial class Company_Qry_01 : IQueryUI
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
            case "show":
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

        if (txt_Pj_StartDate_Year.Text != "")
            to.setValue("Pj_StartDate_Year", txt_Pj_StartDate_Year.Text);

        if (ddl_Pj_Kind.SelectedValue != "")
            to.setValue("Pj_Kind", ddl_Pj_Kind.SelectedValue);

        if (ddl_Aow_Class.SelectedValue != "")
            to.setValue("Aow_Class", ddl_Aow_Class.SelectedValue);

        if (txt_Com_Name.Text != "")
            to.setValue("Com_Name", txt_Com_Name.Text);

        if (txt_Com_Tonum.Text != "")
            to.setValue("Com_Tonum", txt_Com_Tonum.Text);

        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        txt_Pj_StartDate_Year.Text = to.getValue("Pj_StartDate_Year").ToString();
        ddl_Pj_Kind.SelectedValue = to.getValue("Pj_Kind").ToString();
        ddl_Aow_Class.SelectedValue = to.getValue("Aow_Class").ToString();
        txt_Com_Name.Text = to.getValue("Com_Name").ToString();
        txt_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "產業履歷查詢作業";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "7.1.2";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Company_01BL();//邏輯層

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
        InsertPage = "Company_Ins_01.aspx";//新增頁
        ModifyPage = "Company_Upd_01.aspx";//修改頁
        ListPage = "Company_Lis_01.aspx"; //顯示頁
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
        ddl_Pj_Kind.DataSource = new BaseFun().getSysCodeByKind("P", "K");
        ddl_Pj_Kind.DataBind();
        
        ddl_Aow_Class.DataSource = new BaseFun().getSysCodeByKind("A", "C");
        ddl_Aow_Class.DataBind();
    }

    protected override void ProcessRowDataBound(int idx, GridViewRow row, System.Data.DataRowView view)
    {
        base.ProcessRowDataBound(idx, row, view);

        if (row.RowType == DataControlRowType.DataRow)
        {
            ((ImageButton)row.Cells[1].Controls[0]).OnClientClick = "if(!confirm('確定要刪除?')){return false;}";
        }
    }
}