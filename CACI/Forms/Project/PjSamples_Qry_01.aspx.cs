using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class PjSamples_Qry_01 : IQueryUI
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

        if (txt_PjSp_Name.Text != "")
            to.setValue("PjSp_Name", txt_PjSp_Name.Text);

        if( ddl_PjSp_Kind.SelectedValue != "")
            to.setValue("PjSp_Kind", ddl_PjSp_Kind.SelectedValue);

        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        txt_PjSp_Name.Text = to.getValue("PjSp_Name").ToString();
        ddl_PjSp_Kind.SelectedValue = to.getValue("PjSp_Kind").ToString();
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "範本設定作業─範本資料查詢畫面";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "1.1.1";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new PjSamples_01BL();//邏輯層

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
        InsertPage = "PjSamples_Ins_01.aspx";//新增頁
        ModifyPage = "PjSamples_Upd_01.aspx";//修改頁
        ListPage = "PjSamples_Lis_01.aspx"; //顯示頁
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
        txt_PjSp_Name.Text = "";

        ddl_PjSp_Kind.DataSource = new BaseFun().getSysCodeByKind("P", "K");
        ddl_PjSp_Kind.DataBind();
    }

    protected override void ProcessRowDataBound(int idx, GridViewRow row, System.Data.DataRowView view)
    {
        base.ProcessRowDataBound(idx, row, view);

        if (row.RowType == DataControlRowType.DataRow)
        {
            ((ImageButton)row.Cells[1].Controls[0]).OnClientClick = "if(!confirm('確定要刪除?')){return false;}";
        }

        row.Cells[5].Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    protected void btn_Insert2_Click(object sender, ImageClickEventArgs e)
    {
        InsertPage = "PjSamples_Ins_02.aspx";
        GoInsertURL();
    }

    protected override bool BeforeDoModify(int rowIdx, DataTO to)
    {
        base.BeforeDoModify(rowIdx, to);

        if (grvQuery.Rows[rowIdx].Cells[5].Text == "B")
            ModifyPage = "PjSamples_Upd_02.aspx";

        return true;
    }

    protected override bool BeforeDoList(int rowIdx, DataTO to)
    {
        base.BeforeDoList(rowIdx, to);

        if (grvQuery.Rows[rowIdx].Cells[5].Text == "B")
            ListPage = "PjSamples_Lis_02.aspx";

        return true;
    }
}