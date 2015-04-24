using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class Allowance_Qry_01 : IQueryUI
{
    /// <summary>
    /// 將GridView中的控制項Command屬性,轉換為UI中用以判斷動作的列舉值(需實作)
    /// </summary>
    /// <param name="strCmd">控制項Command屬性</param>
    /// <returns>欲執行之動作</returns>
    /// 
    private bool isMod = true;

    public override ROW_CMD_TYPE GetRowCommand(string strCmd)
    {
        ROW_CMD_TYPE cmdType = ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
        switch (strCmd)
        {
            case "mod":
                cmdType = ROW_CMD_TYPE.ROW_CMD_MODIFY;
                isMod = true;
                break;
            case "maintain":
                cmdType = ROW_CMD_TYPE.ROW_CMD_MODIFY;
                //ModifyPage = "AowStage_Upd_01.aspx";
                isMod = false;
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
        if (txt_Aow_Code.Text != "")
            to.setValue("Aow_Code", txt_Aow_Code.Text);
        if (txt_Com_Name.Text != "")
            to.setValue("Com_Name", txt_Com_Name.Text);
        if (ddl_Aow_Status.SelectedValue != "-1")
            to.setValue("Aow_Status", ddl_Aow_Status.SelectedValue);
        if (txt_Pj_StartDate.Text != "")
            to.setValue("Pj_StartDate", txt_Pj_StartDate.Text);
        if (txt_Pj_EndDate.Text != "")
            to.setValue("Pj_EndDate", txt_Pj_EndDate.Text);
        if (txt_Stage_Name.Text != "")
            to.setValue("Stage_Name", txt_Stage_Name.Text);
        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        txt_Pj_Name.Text = to.getValue("Pj_Name").ToString() ;
        txt_Aow_Code.Text = to.getValue("Aow_Code").ToString();
        txt_Com_Name.Text = to.getValue("Com_Name").ToString();
        txt_Stage_Name.Text = to.getValue("Stage_Name").ToString();
        ddl_Aow_Status.SelectedValue = to.getValue("Aow_Status").ToString();
        txt_Pj_StartDate.Text = to.getValue("Pj_StartDate").ToString();
        txt_Pj_EndDate.Text = to.getValue("Pj_EndDate").ToString();
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）
        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "獎補助作業─內網資料查詢";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "2.1.1";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Allowance_01BL();//邏輯層

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
        InsertPage = "Allowance_Qry_02.aspx";//新增頁
        ModifyPage = "Allowance_Upd_01.aspx";//修改頁
        ListPage = "Allowance_Lis_01.aspx"; //顯示頁
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
        ddl_Aow_Status.Items.Clear();
        ddl_Aow_Status.Items.Add(new ListItem("請選擇", "-1"));
        ddl_Aow_Status.AppendDataBoundItems = true;
        ddl_Aow_Status.DataSource = new BaseFun().getSysCodeByKind("C", "S");
        ddl_Aow_Status.DataTextField = "Sys_CdText";
        ddl_Aow_Status.DataValueField = "Sys_CdCode";
        ddl_Aow_Status.DataBind();
      
    }

    protected override void ProcessRowDataBound(int idx, GridViewRow row, System.Data.DataRowView view)
    {
        base.ProcessRowDataBound(idx, row, view);

        if (row.RowType == DataControlRowType.DataRow)
        {
            ((ImageButton)row.Cells[1].Controls[0]).OnClientClick = "if(!confirm('確定要刪除?')){return false;}";
        }
    }
 

    protected override bool BeforeDoDelete(DataTO qto)
    {
       // if (((Allowance_01BL)BL).checkAllowance(qto))
      ///  {
          //  customMSG_FAIL = "目前已有獎補助案或輔導案正在辦理，不允許刪除";
           // return false;
       // }
        return true;
    }

    protected override bool BeforeDoModify(int rowIdx, DataTO to)
    {
        base.BeforeDoModify(rowIdx, to);
        if(isMod)
            ModifyPage = string.Format("Allowance_Upd_0{0}.aspx",grvQuery.Rows[rowIdx].Cells[7].Text);
        else
            ModifyPage = "AowStage_Upd_01.aspx";

        return true;
    }

    protected override bool BeforeDoList(int rowIdx, DataTO to)
    {
        base.BeforeDoList(rowIdx, to);
        ListPage = string.Format("Allowance_Lis_0{0}.aspx", grvQuery.Rows[rowIdx].Cells[7].Text);

        return true;
    }

    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[7].Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    protected void btnBatch_Click(object sender, EventArgs e)
    {
        GoURL("\\CACI\\Forms\\Allowance\\Allowance_Upd_05.aspx");
    }
}