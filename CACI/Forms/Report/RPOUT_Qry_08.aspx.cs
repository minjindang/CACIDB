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

public partial class RPOUT_Qry_08 : IQueryUI
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
        //年度
        if (txt_Pj_StartDate.Text != "")
            to.setValue("txt_Pj_StartDate", txt_Pj_StartDate.Text);
        //專案名稱
        if (sel_Pj_Name.SelectedValue != "")
            to.setValue("sel_Pj_Name", sel_Pj_Name.SelectedValue);
        //階段名稱
        if (sel_Pj_Stage.SelectedValue != "")
            to.setValue("sel_Pj_Stage", sel_Pj_Stage.SelectedValue);
        //委員
        if (sel_Comm_Code.SelectedValue != "")
            to.setValue("sel_Comm_Code", sel_Comm_Code.SelectedValue);
        //申請單位
        if (txt_Com_Name.Text != "")
            to.setValue("txt_Com_Name", txt_Com_Name.Text);
        //票數
        if (txt_Eval_Count.Text != "")
            to.setValue("txt_Eval_Count", txt_Eval_Count.Text);
        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        //年度
        if (to.getValue("txt_Pj_StartDate").ToString() != "")
            txt_Pj_StartDate.Text = to.getValue("txt_Pj_StartDate").ToString();
        //專案名稱
        if (to.getValue("sel_Pj_Name").ToString() != "")
            sel_Pj_Name.SelectedValue = to.getValue("sel_Pj_Name").ToString();
        //階段名稱
        if (to.getValue("sel_Pj_Stage").ToString() != "")
            sel_Pj_Stage.SelectedValue = to.getValue("sel_Pj_Stage").ToString();
        //委員
        if (to.getValue("sel_Comm_Code").ToString() != "")
            sel_Comm_Code.SelectedValue = to.getValue("sel_Comm_Code").ToString();
        //申請單位
        if (to.getValue("txt_Com_Name").ToString() != "")
            txt_Com_Name.Text = to.getValue("txt_Com_Name").ToString();
        //票數
        if (to.getValue("txt_Eval_Count").ToString() != "")
            txt_Eval_Count.Text = to.getValue("txt_Eval_Count").ToString();
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "「文建會補助直轄市及縣(市)政府推動文化創意產業發展」初審審查彙整表";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "8.2.8";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new RPOUT_08BL();//邏輯層

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
        sel_Comm_Code.DataSource = new RPOUT_CommonBL().getCommittee();
        sel_Comm_Code.DataBind();
        Button_Check();
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
        RPOUT_08BL _bl = new RPOUT_08BL();
        rpt.Load(Server.MapPath("RPOUT_Prt_08.rpt"));
        DataTO conds = PopulateData();
        rpt.SetDataSource(_bl.getPrintDatas(conds, SelectData));

        ReportDocument sub01 = rpt.Subreports[0];
        sub01.SetDataSource(_bl.getDataTable_Sub01());
        ReportDocument sub02 = rpt.Subreports[1];
        sub02.SetDataSource(_bl.getDataTable_Sub02());

        rpt.SummaryInfo.ReportTitle = "「文建會補助直轄市及縣(市)政府推動文化創意產業發展」初審審查彙整表";
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
