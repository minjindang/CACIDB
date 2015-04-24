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



public partial class RPOUT_Qry_13 : IQueryUI
{

    protected override bool BefterHandleQuery(DataTO to)
    {
        if (((Cnst_CntDateS.Text) == "" && (Cnst_CntDateE.Text) == "") && ((Meeting_BgnTimeS.Text) == "" && (Meeting_BgnTimeE.Text) == ""))
        {

            ShowMsgBox(Page, "「詢問日期區間」與「輔導會議日期區間」請擇一輸入");

            return false;
        }
        else if (((Cnst_CntDateS.Text) == "" || (Cnst_CntDateE.Text) == "") && ((Meeting_BgnTimeS.Text) == "" || (Meeting_BgnTimeE.Text) == ""))
        {
            ShowMsgBox(Page, "日期區間填寫不完整");

            return false;

        }
        else
        {

            return true;
        }

    }


    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();
        //單位代號
        if (Com_Tonum.Text != "")
            to.setValue("Com_Code", Com_Tonum.Text);
        //單位名稱
        if (Com_Name.Text != "")
            to.setValue("Com_Name", Com_Name.Text);
        //詢問日期區間起
        if (Cnst_CntDateS.Text != "")
            to.setValue("Cnst_CntDateS", Cnst_CntDateS.Text);
        if (Cnst_CntDateE.Text != "")
            to.setValue("Cnst_CntDateE", Cnst_CntDateE.Text);
        //輔導會議日期區間
        if (Meeting_BgnTimeS.Text != "")
            to.setValue("Meeting_BgnTimeS", Meeting_BgnTimeS.Text);
        if (Meeting_BgnTimeE.Text != "")
            to.setValue("Meeting_BgnTimeE", Meeting_BgnTimeE.Text);



        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    public override void LoadSessionTO(DataTO to)
    {
        //單位代號
        if (to.getValue("Com_Tonum").ToString() != "")
            Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
        //單位名稱
        if (to.getValue("Com_Name").ToString() != "")
            Com_Name.Text = to.getValue("Com_Name").ToString();
        //詢問日期區間
        if (to.getValue("Cnst_CntDateS").ToString() != "")
            Cnst_CntDateS.Text = to.getValue("Cnst_CntDateS").ToString();
        if (to.getValue("Cnst_CntDateE").ToString() != "")
            Cnst_CntDateE.Text = to.getValue("Cnst_CntDateE").ToString();
        //輔導會議日期區間
        if (to.getValue("Meeting_BgnTimeS").ToString() != "") ;
        Meeting_BgnTimeS.Text = to.getValue("Meeting_bgnTimeS").ToString();
        if (to.getValue("Meeting_BgnTimeE").ToString() != "") ;
        Meeting_BgnTimeE.Text = to.getValue("Meeting_bgnTimeE").ToString();

    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "輔導-輔導駐診諮詢表一般表";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "8.4.1";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new RPOUT_13BL();//邏輯層

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
    public override void SetDefault()
    {
        btn_PrintExcel.Enabled = false;
        btn_PrintXml.Enabled = false;
        btn_PrintPdf.Enabled = false;

        BaseFun bf = new BaseFun();
        //sel_Pj_Stage.DataSource = new RPOUT_CommonBL().getProjectStage();
        //sel_Pj_Stage.DataBind();
        Button_Check();
    }

    protected override void SetHandler()
    {
        base.SetHandler();
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
        bool Flag = false;
        bool isChecked = ((CheckBox)(grvQuery.HeaderRow.Cells[0].FindControl("cbh"))).Checked;
        foreach (GridViewRow gvRow in grvQuery.Rows)
        {
            ((CheckBox)(gvRow.Cells[0].FindControl("chk"))).Checked = isChecked;
        }
        if (isChecked == true)
        {

            Flag = true;

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
        RPOUT_13BL BL_13 = new RPOUT_13BL();
        rpt.Load(Server.MapPath("RPOUT_Prt_13.rpt"));
        DataTO conds = PopulateData();
        rpt.SetDataSource(BL_13.getPrintInfo(conds, SelectData));
        rpt.SummaryInfo.ReportTitle = "輔導-輔導駐診諮詢表一般表";
        rpt.ExportToHttpResponse(Type, Response, true, rpt.SummaryInfo.ReportTitle);
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





