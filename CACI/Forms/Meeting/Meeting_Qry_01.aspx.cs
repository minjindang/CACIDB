using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using System.Data;

public partial class Meeting_Qry_01 : IQueryUI
{
    /// <summary>
    /// 將GridView中的控制項Command屬性,轉換為UI中用以判斷動作的列舉值(需實作)
    /// </summary>
    /// <param name="strCmd">控制項Command屬性</param>
    /// <returns>欲執行之動作</returns>
    /// 
    private bool isConfig = false;
    public override ROW_CMD_TYPE GetRowCommand(string strCmd)
    {
        ROW_CMD_TYPE cmdType = ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
        switch (strCmd)
        {
            case "mod":
                isConfig = false;
                cmdType = ROW_CMD_TYPE.ROW_CMD_MODIFY;
                break;
            case "config":
                isConfig = true;
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
        if (txt_Meeting_Name.Text != "")
            to.setValue("Meeting_Name", txt_Meeting_Name.Text);
        if (ddl_Meeting_Class.SelectedValue != "-1")
            to.setValue("Meeting_Class", ddl_Meeting_Class.SelectedValue);
        if (txt_Meeting_User_Code.Text != "")
            to.setValue("Meeting_User_Code", txt_Meeting_User_Code.Text);
        if (this.txt_Pj_Name.Text != "")
            to.setValue("Pj_Name", txt_Pj_Name.Text);
        if (txt_Meeting_BgnTime.Text != "")
        {
            to.setValue("Meeting_BgnTime", txt_Meeting_BgnTime.Text);
            to.setValue("BgnHour", ddl_BgnHour.SelectedValue);
            to.setValue("BgnMin", this.ddl_BgnMin.SelectedValue);
        }
        if (txt_Meeting_EndTime.Text != "")
        {
            to.setValue("Meeting_EndTime", txt_Meeting_EndTime.Text);
            to.setValue("EndHour", ddl_EndHour.SelectedValue);
            to.setValue("EndMin", this.ddl_EndMin.SelectedValue);
        }
        
        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        txt_Meeting_Name.Text = to.getValue("Meeting_Name").ToString();
        ddl_Meeting_Class.SelectedValue = to.getValue("Meeting_Class").ToString();
        txt_Meeting_User_Code.Text = to.getValue("Meeting_User_Code").ToString();
        txt_Meeting_BgnTime.Text = to.getValue("Meeting_BgnTime").ToString();
        txt_Meeting_EndTime.Text = to.getValue("Meeting_EndTime").ToString();
        ddl_BgnHour.SelectedValue = to.getValue("BgnHour").ToString();
        ddl_BgnMin.SelectedValue = to.getValue("BgnMin").ToString();
        ddl_EndHour.SelectedValue = to.getValue("EndHour").ToString();
        ddl_EndMin.SelectedValue = to.getValue("EndMin").ToString();
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）
        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "會議管理作業─內網資料查詢畫面";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "3.2.1";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Meeting_01BL();//邏輯層

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
        InsertPage = "Meeting_Ins_01.aspx";//新增頁
        ModifyPage = "Meeting_Upd_01.aspx";//修改頁
        ListPage = "Meeting_Lis_01.aspx"; //顯示頁
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
        BaseFun bf = new BaseFun();
        ddl_Meeting_Class.Items.Clear();
        ddl_Meeting_Class.Items.Add(new ListItem("請選擇", "-1"));
        ddl_Meeting_Class.AppendDataBoundItems = true;
        ddl_Meeting_Class.DataSource = bf.getTableData("MeetingType");
        ddl_Meeting_Class.DataTextField = "Mety_Name";
        ddl_Meeting_Class.DataValueField = "Mety_Code";
        ddl_Meeting_Class.DataBind();
        //
        DataSet ds = bf.generHouMinute();
        this.ddl_BgnHour.DataSource = ds.Tables["hoursTable"];
        this.ddl_BgnHour.DataTextField = "houhourr";
        this.ddl_BgnHour.DataValueField = "houhourr";
        this.ddl_BgnHour.DataBind();
        //
        this.ddl_EndHour.DataSource = ds.Tables["hoursTable"];
        this.ddl_EndHour.DataTextField = "houhourr";
        this.ddl_EndHour.DataValueField = "houhourr";
        this.ddl_EndHour.DataBind();
        //
        this.ddl_BgnMin.DataSource = ds.Tables["minutesTable"];
        this.ddl_BgnMin.DataTextField = "minute";
        this.ddl_BgnMin.DataValueField = "minute";
        this.ddl_BgnMin.DataBind();
        //
        this.ddl_EndMin.DataSource = ds.Tables["minutesTable"];
        this.ddl_EndMin.DataTextField = "minute";
        this.ddl_EndMin.DataValueField = "minute";
        this.ddl_EndMin.DataBind();
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

    protected override bool BeforeDoList(int rowIdx, DataTO to)
    {
        base.BeforeDoList(rowIdx, to);
        if (this.grvQuery.Rows[rowIdx].Cells[9].Text == "AR")
            ListPage = "Meeting_Lis_02.aspx";
        else
            ListPage = "Meeting_Lis_01.aspx";

        return true;
    }

    protected override bool BeforeDoModify(int rowIdx, DataTO to)
    {
        base.BeforeDoModify(rowIdx, to);
        if (!isConfig)
        {
            if (this.grvQuery.Rows[rowIdx].Cells[9].Text == "AR")
                ModifyPage = "Meeting_Upd_02.aspx";
            else
                ModifyPage = "Meeting_Upd_01.aspx";
        }else
            ModifyPage = "Meeting_Upd_03.aspx";

        return true;
    }

    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[9].Style.Add(HtmlTextWriterStyle.Display, "none");
    }
   
}