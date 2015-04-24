using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting.WebControls;

public partial class RPOUT_Qry_04 : IQueryUI
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
        //申請日期區間起
        if (txt_Aow_DateS.Text != "")
            to.setValue("txt_Aow_DateS", txt_Aow_DateS.Text);
        //申請日期區間迄
        if (txt_Aow_DateE.Text != "")
            to.setValue("txt_Aow_DateE", txt_Aow_DateE.Text);
        //年度
        if (txt_Pj_StartDate.Text != "")
            to.setValue("txt_Pj_StartDate", txt_Pj_StartDate.Text);
        //專案名稱
        if (sel_Pj_Name.SelectedValue != "")
            to.setValue("sel_Pj_Name", sel_Pj_Name.SelectedValue);
        //階段名稱
        if (sel_Pj_Stage.SelectedValue != "")
            to.setValue("sel_Pj_Stage", sel_Pj_Stage.SelectedValue);
        

        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        //申請日期區間起
        if (to.getValue("txt_Aow_DateS").ToString() != "")
            txt_Aow_DateS.Text = to.getValue("txt_Aow_DateS").ToString();
        //申請日期區間迄
        if (to.getValue("txt_Aow_DateE").ToString() != "")
            txt_Aow_DateE.Text = to.getValue("txt_Aow_DateE").ToString();
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "獎補助專案初審技審委員綜整資料表";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "8.2.4";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new RPOUT_04BL();//邏輯層

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

        #endregion 按鈕定義 end

        #region 導向頁面資訊
        #endregion 導向頁面資訊

        #region 登入檢查模式
        checkLoginType = checkLoginType.Need;
        #endregion
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        Button_Check();
        this.txt_Pj_StartDate.Text = "";
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

    protected void SelectItem(object sender, EventArgs e)
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

    protected void txt_Pj_StartDate_TextChanged(object sender, EventArgs e)
    {
        sel_Pj_Name.DataSource = new RPOUT_CommonBL().getProjectName(txt_Pj_StartDate.Text);
        sel_Pj_Name.DataBind();
        sel_Pj_Stage.DataSource = new RPOUT_CommonBL().getProjectStage(sel_Pj_Name.SelectedValue);
        sel_Pj_Stage.DataBind();
    }

    protected void sel_Pj_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        sel_Pj_Stage.DataSource = new RPOUT_CommonBL().getProjectStage(sel_Pj_Name.SelectedValue);
        sel_Pj_Stage.DataBind();
    }

    protected override void SetHandler()
    {
        base.SetHandler();
        ((ScriptManager)this.Master.FindControl("ToolkitScriptManager1")).RegisterPostBackControl(btn_PrintToExl);
        ((ScriptManager)this.Master.FindControl("ToolkitScriptManager1")).RegisterPostBackControl(btn_PrintToXML);
        ((ScriptManager)this.Master.FindControl("ToolkitScriptManager1")).RegisterPostBackControl(btn_PrintToPDF);
    }

    protected void PrintToFile(CrystalDecisions.Shared.ExportFormatType Type)
    {
        String SelectData = "";
        foreach (GridViewRow GR in grvQuery.Rows)
        {
            CheckBox CB = (CheckBox)GR.FindControl("cbItem");
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
        RPOUT_04BL BL_04 = new RPOUT_04BL();
        rpt.Load(Server.MapPath("RPOUT_Prt_04.rpt"));
        DataTO conds = PopulateData();
        rpt.SetDataSource(BL_04.getPrintDatas(conds, SelectData));

        ReportDocument sub01 = rpt.Subreports[0];
        sub01.SetDataSource(BL_04.getPrintInfo_Sub01(conds, SelectData));
        ReportDocument sub02 = rpt.Subreports[1];
        sub02.SetDataSource(BL_04.getDataTable_Sub02(conds, SelectData));

        rpt.SummaryInfo.ReportTitle = "獎補助專案初審技審委員綜整資料表";
        string FileName = rpt.SummaryInfo.ReportTitle + (int.Parse(System.DateTime.Now.ToString("yyyyMMdd")) - 19110000).ToString() + System.DateTime.Now.ToString("hhmmss");
        rpt.ExportToHttpResponse(Type, Response, true, Server.UrlEncode(FileName));
    }

    protected void btn_PrintExcel_Click(object sender, ImageClickEventArgs e)
    {
        PrintToFile(CrystalDecisions.Shared.ExportFormatType.Excel);
    }
    protected void btn_PrintXml_Click(object sender, ImageClickEventArgs e)
    {
        PrintToFile(CrystalDecisions.Shared.ExportFormatType.Xml);
    }
    protected void btn_PrintPdf_Click(object sender, ImageClickEventArgs e)
    {
        PrintToFile(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
    }
}
