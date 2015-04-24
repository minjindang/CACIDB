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
using CrystalDecisions.Web;
using CrystalDecisions.CrystalReports.EMFServerMessageFactory;
using System.Data.SqlClient;

public partial class RPOUT_Qry_test_24 : IQueryUI
{
    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        //PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "行政院文化建設委員會輔導藝文產業創新育成補助作業要點計畫綜合資料表";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "0.1";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new RPOUT_test_24BL();//邏輯層

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
        InsertPage = "RPOUT_Qry_test_24.aspx";//新增頁
        ModifyPage = "RPOUT_Qry_test_24.aspx";//修改頁
        ListPage = "RPOUT_Qry_test_24.aspx"; //顯示頁
        #endregion 導向頁面資訊

        #region 頁面檢查設定
        checkLoginType = checkLoginType.None;
        #endregion
    }
    
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

        if (txt_Aow_Code.Text != "")
        {
            to.setValue("Aow_Code", txt_Aow_Code.Text);
        }

        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        txt_Aow_Code.Text = to.getValue("Aow_Code").ToString();
    }   

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        txt_Aow_Code.Text = "";
    }
    
    protected override void SetHandler()
    {
        base.SetHandler();
        ((ScriptManager)this.Master.FindControl("RadScriptManager1")).RegisterPostBackControl(btn_PrintToExl);
        ((ScriptManager)this.Master.FindControl("RadScriptManager1")).RegisterPostBackControl(btn_PrintToXML);
        ((ScriptManager)this.Master.FindControl("RadScriptManager1")).RegisterPostBackControl(btn_PrintToPDF);
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
        RPOUT_test_24BL BL_24 = new RPOUT_test_24BL();
        rpt.Load(Server.MapPath("RPOUT_Prt_24.rpt"));
        DataTO conds = PopulateData();
        rpt.SetDataSource(BL_24.getPrintInfo(conds));
        rpt.SummaryInfo.ReportTitle = "Report24";
        rpt.ExportToHttpResponse(Type, Response, true, rpt.SummaryInfo.ReportTitle);
    }
}