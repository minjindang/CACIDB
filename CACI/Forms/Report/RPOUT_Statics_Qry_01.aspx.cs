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

public partial class RPOUT_Statics_Qry_01 : IQueryUI
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

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    // <summary>
    // 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    // </summary>
    // <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();
        //年度
        if (ddl_Cnst_CntDate.SelectedValue != "-1")
        {
            to.setValue("Cnst_CntDate", ddl_Cnst_CntDate.SelectedValue);
        }
        //詢問日期區間起
        if (dtb_Cnst_CntDate_Bgn.Text != "")
        {
            to.setValue("Cnst_CntDate_Bgn", dtb_Cnst_CntDate_Bgn.Text);
        }
        //詢問日期區間迄
        if (dtb_Cnst_CntDate_End.Text != "")
        {
            to.setValue("Cnst_CntDate_End", dtb_Cnst_CntDate_End.Text);
        }
        return to;
    }

    // <summary>
    // 將Session取到的資料顯示在畫面上(需實作)
    // </summary>
    // <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        //年度
        if (to.getValue("Cnst_CntDate").ToString() != "")
            ddl_Cnst_CntDate.SelectedValue = to.getValue("Cnst_CntDate").ToString();
        //詢問日期區間起
        if (to.getValue("Cnst_CntDate").ToString() != "")
            dtb_Cnst_CntDate_Bgn.Text = to.getValue("Cnst_CntDate").ToString();
        //詢問日期區間迄
        if (to.getValue("Cnst_CntDate").ToString() != "")
            dtb_Cnst_CntDate_End.Text = to.getValue("Cnst_CntDate").ToString();
    }

    // <summary>
    // 設定程式參數(需實作)
    // </summary>
    public override void SetProp()
    {
        #region 共同資訊

        //PageDropDownList = ddlPage;//每頁筆數（查詢條件）
        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "諮詢案件來源分析統計";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "7.1.2";
        ProgType = PAGE_ACTION.PAGE_QUERY;
        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new RPOUT_Statics_01BL();//邏輯層

        #endregion

        #region GridView資訊
        DataGridView = grvQuery;//GridView設定
        //PagePanel = pnlPageInfo;//頁數資訊
        DataPanel = pnlGridView;//GridView區塊設定
        PageLabel = lblPageCount;//總筆數
        //PageNumDropDownList = ddlPageNum;//目前頁數
        //PageUpButton = lnkPageUP;//上一頁
        //PageDownButton = lnkPageDown;//下一頁
        #endregion

        #region 按鈕定義 start

        QueryButton = btn_Query;
        ClearButton = btn_Clear;
        //MarkButton = btn_Mark;

        #endregion 按鈕定義 end

        #region 導向頁面資訊
        //InsertPage = "RPOUT_Statics_Qry_01.aspx";//新增頁
        //ModifyPage = "RPOUT_Statics_Qry_01.aspx";//修改頁
        //ListPage = "RPOUT_Statics_Qry_01.aspx"; //顯示頁
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
        //年度
        ddl_Cnst_CntDate.Items.Clear();
        ddl_Cnst_CntDate.AppendDataBoundItems = true;
        ddl_Cnst_CntDate.DataSource = ((RPOUT_Statics_01BL)BL).getYear();
        ddl_Cnst_CntDate.DataTextField = "Year";                        //將『年度』顯示到使用者介面
        ddl_Cnst_CntDate.DataValueField = "AD";                         //對應資料庫內『年度』
        ddl_Cnst_CntDate.DataBind();
    }

    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
        ReportDocument rpt = new ReportDocument();
        RPOUT_Statics_01BL Statics_01BL = new RPOUT_Statics_01BL();
        rpt.Load(Server.MapPath("RPOUT_Statics_Prt_01.rpt"));
        DataTO conds = PopulateData();
        rpt.SetDataSource(Statics_01BL.getPrintInfo(conds, ""));
        string FileName = ProgNm + (int.Parse(System.DateTime.Now.ToString("yyyyMMdd")) - 19110000).ToString() + System.DateTime.Now.ToString("hhmmss");
        rpt.ExportToHttpResponse(Type, Response, true, Server.UrlEncode(FileName));
    }
}