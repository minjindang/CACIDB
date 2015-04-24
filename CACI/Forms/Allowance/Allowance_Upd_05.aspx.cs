using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using System.Web.UI.HtmlControls;
using com.kangdainfo.online.WebControl;
using System.Data;

public partial class Allowance_Upd_05 : IQueryMarkUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();
        if (this.ddl_Pj_Name.SelectedValue != "")
            to.setValue("Pj_Code", this.ddl_Pj_Name.SelectedValue);
        if (this.ddl_AwSg_Verify.SelectedValue != "")
            to.setValue("AwSg_Verify", this.ddl_AwSg_Verify.SelectedValue);
        if (this.ddl_Stage_Name.SelectedValue != "")
            to.setValue("Stage_Name", this.ddl_Stage_Name.SelectedItem.Text);
        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {

    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "獎補助作業─內網批次設定畫面";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "2.1.6";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Allowance_05BL();//邏輯層

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
        MarkButton = btn_Mark;

        #endregion 按鈕定義 end

        #region 頁面檢查設定
        checkLoginType = checkLoginType.None;
        #endregion
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        BaseFun bf = new BaseFun();
        //專案名稱
        DataTO queryTo = new DataTO();
        queryTo.setValue("Pj_Kind", "A");
        this.ddl_Pj_Name.DataSource = bf.getTableData("Project", queryTo);
        ddl_Pj_Name.DataTextField = "Pj_Name";
        ddl_Pj_Name.DataValueField = "Pj_Code";
        ddl_Pj_Name.DataBind();
        //階段
        queryTo = new DataTO();
        queryTo.setValue("Pj_Code", ddl_Pj_Name.SelectedValue);
        this.ddl_Stage_Name.DataSource = bf.getTableData("PjStage", queryTo);
        ddl_Stage_Name.DataTextField = "Stage_Name";
        ddl_Stage_Name.DataValueField = "Stage_Index";
        ddl_Stage_Name.DataBind();
        //階段狀態
        ddl_AwSg_Verify.Items.Clear();
        ddl_AwSg_Verify.Items.Add(new ListItem("未通過", "N"));
        ddl_AwSg_Verify.Items.Add(new ListItem("未審查", ""));
        //ddl_AwSg_Verify.DataSource = bf.getSysCodeByKind("S", "S");
        //ddl_AwSg_Verify.DataTextField = "Sys_CdText";
        //ddl_AwSg_Verify.DataValueField = "Sys_CdCode";
        //ddl_AwSg_Verify.DataBind();
        BindStageName();
        BindConfigContent();
        ddl_AwSg_Verify2.DataSource = new BaseFun().getSysCodeByKind("S", "S");
        ddl_AwSg_Verify2.DataTextField = "Sys_CdText";
        ddl_AwSg_Verify2.DataValueField = "Sys_CdCode";
        ddl_AwSg_Verify2.DataBind();
    }

    /// <summary>
    /// 收集標註資訊
    /// </summary>
    /// <returns></returns>
    public override DataTO PopulateMarkData()
    {
        DataTO to = new DataTO();
        if (this.txt_AwSg_Date.Text != "")
            to.setValue("AwSg_Date", Allowance_05BL.chgChnDateToEnDate(this.txt_AwSg_Date.Text));
        if (this.txt_AwSg_Text.Text != "")
            to.setValue("AwSg_Text", this.txt_AwSg_Text.Text);
        to.setValue("AwSg_Verify", this.ddl_AwSg_Verify2.SelectedValue);
        if (ddl_AwSg_Verify.SelectedValue == "N")
            to.setValue("isInsert", "N");//未通過需要更新
        else
            to.setValue("isInsert", "Y");//未審查需要新增
        return to;
    }
    protected void ddl_Pj_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindStageName();
        BindConfigContent();
    }

    protected void SelectAll(object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)(grvQuery.HeaderRow.Cells[0].FindControl("cbHead"))).Checked;
        foreach (GridViewRow gvRow in grvQuery.Rows)
        {
            ((CheckBox)(gvRow.Cells[0].FindControl("cbItem"))).Checked = isChecked;
        }
    }

    private void BindStageName()
    {
        DataTO queryTo = new DataTO();
        queryTo.setValue("Pj_Code", ddl_Pj_Name.SelectedValue);
        this.ddl_Stage_Name.DataSource = new BaseFun().getTableData("PjStage", queryTo);
        ddl_Stage_Name.DataTextField = "Stage_Name";
        ddl_Stage_Name.DataValueField = "Stage_Index";
        ddl_Stage_Name.DataBind();
    }

    private void BindConfigContent()
    {
        DataTO queryTo = new DataTO();
        queryTo.setValue("Pj_Code", ddl_Pj_Name.SelectedValue);
        queryTo.setValue("Stage_Name", ddl_Stage_Name.SelectedItem.Text);
        DataTable dt = new BaseFun().getTableData("PjStage", queryTo);
        if(!string.IsNullOrEmpty(dt.Rows[0]["Stage_Date"].ToString()))
            lbl_Stage_Date.Text = Allowance_05BL.chgEnDateToChnDate(dt.Rows[0]["Stage_Date"].ToString().Split(' ')[0]);
        lbl_Stage_Text.Text = dt.Rows[0]["Stage_Text"].ToString();
    }

    protected void ddl_Stage_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindConfigContent();
    }
    protected void btn_Back_Click(object sender, ImageClickEventArgs e)
    {
        GoURL("\\CACI\\Forms\\Allowance\\Allowance_Qry_01.aspx");
    }
}