using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using com.kangdainfo.online.WebBase.BL;
using CrystalDecisions.Shared;
using System.Data.SqlClient;

public partial class RPOUT_Qry_12 : IQueryUI
{
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

    // <summary>
    // 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    // </summary>
    // <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();
        //詢問日期區間起
        if (dtb_Cnst_CntDate_Bgn.Text != ""){
            to.setValue("Cnst_CntDate_Bgn", dtb_Cnst_CntDate_Bgn.Text);
        }

        //詢問日期區間迄
        if (dtb_Cnst_CntDate_End.Text != ""){
            to.setValue("Cnst_CntDate_End", dtb_Cnst_CntDate_End.Text);
        }

        //詢問人/公司
        if (txt_Com_Name.Text != ""){
            to.setValue("Com_Name", txt_Com_Name.Text);
        }

        //諮詢類別
        if (ddl_CntClass_Code.SelectedValue != "-1"){
            to.setValue("CntClass_Code", ddl_CntClass_Code.SelectedValue);
        }            

        return to;
    }

    // <summary>
    // 將Session取到的資料顯示在畫面上(需實作)
    // </summary>
    // <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        //詢問日期區間起
        if (to.getValue("Cnst_CntDate").ToString() != "")
            dtb_Cnst_CntDate_Bgn.Text = to.getValue("Cnst_CntDate").ToString();
        //詢問日期區間迄
        if (to.getValue("Cnst_CntDate").ToString() != "")
            dtb_Cnst_CntDate_End.Text = to.getValue("Cnst_CntDate").ToString();
        //詢問人/公司
        if (to.getValue("Com_Name").ToString() != "")
            txt_Com_Name.Text = to.getValue("Com_Name").ToString();
        //諮詢類別
        if (to.getValue("CntClass_Code").ToString() != "")
            ddl_CntClass_Code.SelectedValue = to.getValue("CntClass_Code").ToString();
    }

    // <summary>
    // 設定程式參數(需實作)
    // </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）
        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "駐診記錄匯整-一般表";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "7.1.2";
        ProgType = PAGE_ACTION.PAGE_QUERY;
        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new RPOUT_12BL();//邏輯層

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

        InsertButton = btn_Insert;
        QueryButton = btn_Query;
        ClearButton = btn_Clear;
        //MarkButton = btn_Mark;

        #endregion 按鈕定義 end

        #region 導向頁面資訊
        //InsertPage = "RPOUT_Qry_12.aspx";//新增頁
        //ModifyPage = "RPOUT_Qry_12.aspx";//修改頁
        //ListPage = "RPOUT_Qry_12.aspx"; //顯示頁
        #endregion 導向頁面資訊
        
        #region 頁面檢查設定
        checkLoginType = checkLoginType.None;
        #endregion
    }

    // <summary>
    // 設定初始值(需實作)
    // </summary>
    public override void SetDefault()
    {
        //匯出Excel、匯出XML、查看列印按鈕狀態為Disable
        btn_PrintToExl.Enabled = false;
        btn_PrintToXML.Enabled = false;
        btn_PrintToPDF.Enabled = false;
        Button_Check();

        //問題類型
        BaseFun bf = new BaseFun();
        ddl_CntClass_Code.Items.Clear();
        ddl_CntClass_Code.AppendDataBoundItems = true;
        ddl_CntClass_Code.Items.Add(new ListItem("請選擇", "-1"));
        ddl_CntClass_Code.DataSource = bf.getSysCodeByKind("C", "Y");
        ddl_CntClass_Code.DataTextField = "Sys_CdText";                 //將『問題類型』顯示到使用者介面
        ddl_CntClass_Code.DataValueField = "Sys_CdCode";                //對應資料庫內『問題類型代號』
        ddl_CntClass_Code.DataBind();     
    }

    protected void Button_OnOff(object sender, EventArgs e)
    {
        Button_Check();
    }

    protected void Button_Check()
    {
        Boolean Flag = false;
        foreach (GridViewRow GR in this.grvQuery.Rows)
        {
            CheckBox CB = (CheckBox)GR.FindControl("cbItem");
            if (CB.Checked)
            {
                Flag = true;
            }
        }
        if (Flag)
        {
            showImgBtn(btn_PrintToExl);
            showImgBtn(btn_PrintToXML);
            showImgBtn(btn_PrintToPDF);
        }
        else
        {
            hideImgBtn(btn_PrintToExl);
            hideImgBtn(btn_PrintToXML);
            hideImgBtn(btn_PrintToPDF);
        }
    }

    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BaseFun bf = new BaseFun();
            DataTO queryTo = new DataTO();
            queryTo.setValue("Com_Name", e.Row.Cells[3].Text);
            DataTable dt = bf.getTableData("Company", queryTo);
            if (dt != null && dt.Rows.Count > 0)
                e.Row.Cells[3].Text = dt.Rows[0]["Com_Name"].ToString();
        }
    }

    protected void SelectAll(object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)(grvQuery.HeaderRow.Cells[0].FindControl("cbHead"))).Checked;
        foreach (GridViewRow gvRow in grvQuery.Rows)
        {
            ((CheckBox)(gvRow.Cells[0].FindControl("cbItem"))).Checked = isChecked;
        }
        Button_Check();
    }
    
    protected override void SetHandler()
    {
        base.SetHandler();
        ((ScriptManager)this.Master.FindControl("ToolkitScriptManager1")).RegisterPostBackControl(btn_PrintToExl);
        ((ScriptManager)this.Master.FindControl("ToolkitScriptManager1")).RegisterPostBackControl(btn_PrintToXML);
        ((ScriptManager)this.Master.FindControl("ToolkitScriptManager1")).RegisterPostBackControl(btn_PrintToPDF);
    }
    
    protected void btn_PrintToExl_Click(object sender, ImageClickEventArgs e)
    {
        PrintToFile(CrystalDecisions.Shared.ExportFormatType.Excel);
    }

    protected void btn_PrintToXML_Click(object sender, ImageClickEventArgs e)
    {
        PrintToFile(CrystalDecisions.Shared.ExportFormatType.Xml);
    }

    protected void btn_PrintToPDF_Click(object sender, ImageClickEventArgs e)
    {
        PrintToFile(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
    }

    protected void PrintToFile(CrystalDecisions.Shared.ExportFormatType Type)
    {
        String SelectData = "";
        foreach (GridViewRow GR in this.grvQuery.Rows)
        {
            CheckBox CB = (CheckBox)GR.FindControl("cbItem");
            if (CB.Checked)
            {
                SelectData += "'" + this.grvQuery.DataKeys[GR.RowIndex].Value.ToString() + "',";
            }
        }
        if (SelectData != "")
        {
            SelectData = SelectData.Substring(0, SelectData.Length - 1);
        }

        ReportDocument rpt = new ReportDocument();
        RPOUT_12BL BL_12 = new RPOUT_12BL();
        rpt.Load(Server.MapPath("RPOUT_Prt_12.rpt"));
        DataTO conds = PopulateData();
        rpt.SetDataSource(BL_12.getPrintInfo(conds, SelectData));
        string FileName = ProgNm + (int.Parse(System.DateTime.Now.ToString("yyyyMMdd")) - 19110000).ToString() + System.DateTime.Now.ToString("hhmmss");
        rpt.ExportToHttpResponse(Type, Response, true, Server.UrlEncode(FileName));
    }
}