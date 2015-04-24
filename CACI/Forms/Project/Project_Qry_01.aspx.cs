using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.BL;

public partial class Project_Qry_01 : IQueryUI
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
        if (txt_Pj_Name.Text != "")
            to.setValue("Pj_Name", txt_Pj_Name.Text);
        if (ddl_Pj_Kind.SelectedValue != "-1")
            to.setValue("Pj_Kind", ddl_Pj_Kind.SelectedValue);
        if (txt_Pj_BgnDate.Text != "")
            to.setValue("Pj_BgnDate", txt_Pj_BgnDate.Text);
        if (txt_Pj_FinishDate.Text != "")
            to.setValue("Pj_FinishDate", txt_Pj_FinishDate.Text);
        if (txt_Pj_StartDate.Text != "")
            to.setValue("Pj_StartDate", txt_Pj_StartDate.Text);
        if (txt_Pj_EndDate.Text != "")
            to.setValue("Pj_EndDate", txt_Pj_EndDate.Text);
        if (ddl_Pj_User.SelectedValue != "-1")
            to.setValue("Pj_User_Code", ddl_Pj_User.SelectedValue);
        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        txt_Pj_Name.Text = to.getValue("Pj_Name").ToString();
        ddl_Pj_Kind.SelectedValue = to.getValue("Pj_Kind").ToString();
        txt_Pj_BgnDate.Text = to.getValue("Pj_BgnDate").ToString();
        txt_Pj_FinishDate.Text = to.getValue("Pj_FinishDate").ToString();
        txt_Pj_StartDate.Text = to.getValue("Pj_StartDate").ToString();
        txt_Pj_EndDate.Text = to.getValue("Pj_EndDate").ToString();
        ddl_Pj_User.SelectedValue = to.getValue("Pj_User_Code").ToString();
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）
        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "專案設定作業─專案資料查詢畫面";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "1.2.1";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Project_01BL();//邏輯層

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
        InsertPage = "Project_Qry_02.aspx";//新增頁
        ModifyPage = "Project_Upd_01.aspx";//修改頁
        ListPage = "Project_Lis_01.aspx"; //顯示頁
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
        ddl_Pj_Kind.Items.Clear();
        ddl_Pj_Kind.Items.Add(new ListItem("請選擇", "-1"));
        ddl_Pj_Kind.AppendDataBoundItems = true;
        ddl_Pj_Kind.DataSource = new BaseFun().getSysCodeByKind("P", "K");
        ddl_Pj_Kind.DataTextField = "Sys_CdText";
        ddl_Pj_Kind.DataValueField = "Sys_CdCode";
        ddl_Pj_Kind.DataBind();
        //承辦人
        BaseFun bf = new BaseFun();
        ddl_Pj_User.Items.Clear();
        ddl_Pj_User.Items.Add(new ListItem("請選擇", "-1"));
        ddl_Pj_User.AppendDataBoundItems = true;
        ddl_Pj_User.DataSource = bf.getTableData("UserAcc");
        ddl_Pj_User.DataTextField = "User_Name";
        ddl_Pj_User.DataValueField = "User_Code";
        ddl_Pj_User.DataBind();
    }

    protected override void ProcessRowDataBound(int idx, GridViewRow row, System.Data.DataRowView view)
    {
        base.ProcessRowDataBound(idx, row, view);

        if (row.RowType == DataControlRowType.DataRow)
        {
            ((ImageButton)row.Cells[1].Controls[0]).OnClientClick = "if(!confirm('確定要刪除?')){return false;}";
        }

        row.Cells[7].Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    
    protected void btn_Insert2_Click(object sender, ImageClickEventArgs e)
    {
        InsertPage = "PjSamples_Ins_02.aspx";
        GoInsertURL();
    }

    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[6].Style.Add(HtmlTextWriterStyle.Display, "none");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Text = Project_01BL.chgEnDateToChnDate(e.Row.Cells[5].Text.Split(' ')[0]);
        }
    }

    protected override bool BeforeDoDelete(DataTO qto)
    {
        if (((Project_01BL)BL).checkAllowance(qto))
        {
            customMSG_FAIL = "目前已有獎補助案或輔導案正在辦理，不允許刪除";
            return false;
        }
        return true;
    }

    protected override bool BeforeDoModify(int rowIdx, DataTO to)
    {
        if (base.BeforeDoModify(rowIdx, to))
        {
            if (Convert.ToDateTime(grvQuery.Rows[rowIdx].Cells[7].Text) >= DateTime.Now)
            {
                if (grvQuery.Rows[rowIdx].Cells[6].Text == "B")
                    ModifyPage = "Project_Upd_02.aspx";

                return true;
            }
            else
            {
                ShowMsgMix(Page, lblMsg, "專案已超過網路申請開放時間(" + ICommonBL.chgEnDateToChnDate(grvQuery.Rows[rowIdx].Cells[7].Text) + ")，不允許更新", com.kangdainfo.online.WebBase.BL.MSG_TP.MSG_TP_WARN);

                return false;
            }
        }
        else
            return false;
    }

    protected override bool BeforeDoList(int rowIdx, DataTO to)
    {
        base.BeforeDoList(rowIdx, to);

        if (grvQuery.Rows[rowIdx].Cells[6].Text == "B")
            ListPage = "Project_Lis_02.aspx";

        return true;
    }
}