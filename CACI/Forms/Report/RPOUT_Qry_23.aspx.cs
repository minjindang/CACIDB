using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.BL;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting.WebControls;


public partial class RPOUT_Qry_23 : IQueryUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    


    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        //申請之案件編號
        if (Aow_Code.Text != "")
            to.setValue("Aow_Code", Aow_Code.Text);
        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        //申請之案件編號
        if (to.getValue("Aow_Code").ToString() != "")
            Aow_Code.Text = to.getValue("Aow_Code").ToString();        
    }
    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        //PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "輔導案件匯整一般表";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "8.4.5";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new RPOUT_15BL();//邏輯層

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

        #region 頁面檢查設定
        checkLoginType = checkLoginType.None;
        #endregion
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    /// 
    public override void SetDefault()
    {
        //BaseFun bf = new BaseFun();
    }

    protected override void SetHandler()
    {
        base.SetHandler();
        //TODO 更換Control代碼
        ((ScriptManager)this.Master.FindControl("ToolkitScriptManager1")).RegisterPostBackControl(btn_PrintExcel);
        ((ScriptManager)this.Master.FindControl("ToolkitScriptManager1")).RegisterPostBackControl(btn_PrintXml);
        ((ScriptManager)this.Master.FindControl("ToolkitScriptManager1")).RegisterPostBackControl(btn_PrintPdf);
    }


    protected void StrTextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void grvQuery_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void SelectAll(object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)(grvQuery.HeaderRow.Cells[0].FindControl("cbh"))).Checked;
        foreach (GridViewRow gvRow in grvQuery.Rows)
        {
            ((CheckBox)(gvRow.Cells[0].FindControl("chk"))).Checked = isChecked;
        }
    }

    protected void btn_PrintToExl_Click(object sender, EventArgs e)
    {
        PrintToFile(CrystalDecisions.Shared.ExportFormatType.Excel);
    }

    protected void btn_PrintToXML_Click(object sender, EventArgs e)
    {
        PrintToFile(CrystalDecisions.Shared.ExportFormatType.Xml);
    }

    protected void btn_PrintToPDF_Click(object sender, ImageClickEventArgs e)
    {
        PrintToFile(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
    }

    public void PrintToFile(CrystalDecisions.Shared.ExportFormatType Type)
    {
        string SelectData = "";
        string FileName = (int.Parse(System.DateTime.Now.ToString("yyyyMMdd")) - 19110000).ToString() + System.DateTime.Now.ToString("hhmmss");
        foreach (GridViewRow GR in grvQuery.Rows)
        {
            CheckBox CB = (CheckBox)GR.FindControl("chk");
            if (CB.Checked)
            {
                SelectData += "'" + grvQuery.DataKeys[GR.RowIndex].Value.ToString() + "',";
            }
        }
        if (SelectData != "")
        {
            SelectData = SelectData.Substring(0, SelectData.Length - 1);
        }
        ReportDocument rpt = new ReportDocument();
        RPOUT_23BL BL_23 = new RPOUT_23BL();
        rpt.Load(Server.MapPath("RPOUT_Prt_23.rpt"));
        DataTO conds = PopulateData();
        rpt.SetDataSource(BL_23.getPrintInfo(conds, SelectData));
        ReportDocument sub01 = rpt.Subreports[0];
        sub01.SetDataSource(BL_23.getPrintInfo_Sub01(conds));
        //sub01.SetParameterValue("Pm-DataTable1.Aow_Code", "");
        rpt.SummaryInfo.ReportTitle = "推動文化創意產業發展綜合資料表";
        FileName = rpt.SummaryInfo.ReportTitle + FileName;
        rpt.ExportToHttpResponse(Type, Response, true, Server.UrlEncode(FileName));
        
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
            CheckBox CB = (CheckBox)GR.FindControl("chk");
            if (CB.Checked)
            {
                Flag = true;
            }
        }
        if (Flag)
        {
            showImgBtn(btn_PrintExcel);
            showImgBtn(btn_PrintXml);
            showImgBtn(btn_PrintPdf);
        }
        else
        {
            hideImgBtn(btn_PrintExcel);
            hideImgBtn(btn_PrintXml);
            hideImgBtn(btn_PrintPdf);
        }
    }

    public override ROW_CMD_TYPE GetRowCommand(string strCmd)
    {
        throw new NotImplementedException();
    }
}
