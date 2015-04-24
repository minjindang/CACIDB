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

public partial class RPOUT_Qry_05 : IQueryUI
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
        if (Year.Text != "")
            to.setValue("Pj_StartDate", Year.Text);
        //專案名稱
        if (pj_name.Text != "")
            to.setValue("Pj_Name", pj_name.SelectedValue);
        //申請日期起
        if (Aow_SDate.Text != "")
            to.setValue("Aow_SDate", Aow_SDate.Text);        
        //申請日期迄
        if (Aow_EDate.Text != "")
            to.setValue("Aow_EDate", Aow_EDate.Text);
        //階段名稱
        if (sel_Pj_Stage.Text != "")
            to.setValue("Stage_Name", sel_Pj_Stage.SelectedValue);
        //排序方式
        if (sel_Sort.Text != "")
            to.setValue("Sort", sel_Sort.SelectedValue);       
        
        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        //年度
        if (Year.Text != "")
            Year.Text = to.getValue("Year").ToString();
        //專案名稱
        if (pj_name.Text != "")
            pj_name.Text = to.getValue("Pj_Name").ToString();
        //申請日期起
        if (to.getValue("Aow_SDate").ToString() != "")
            Aow_SDate.Text = to.getValue("Aow_SDate").ToString();
        //申請日期迄
        if (to.getValue("Aow_EDate").ToString() != "")
            Aow_EDate.Text = to.getValue("Aow_EDate").ToString();
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "獎補助專案進入決審彙整表";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "8.2.5";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new RPOUT_05BL();//邏輯層

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

        ListPage = "RPOUT_Qry_05.aspx"; //顯示頁
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
        btn_PrintToExl.Enabled = false;
        btn_PrintToXML.Enabled = false;
        btn_PrintToPDF.Enabled = false;

        BaseFun bf = new BaseFun();
        //sel_Pj_Stage.DataSource = new RPOUT_CommonBL().getProjectStage();
        //sel_Pj_Stage.DataBind();
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
                SelectData += "'"+this.grvQuery.DataKeys[GR.RowIndex].Value.ToString() + "',";
            }
        }
        if (SelectData != "")
        {
            SelectData = SelectData.Substring(0, SelectData.Length - 1);
        }
        string FileName = (int.Parse(System.DateTime.Now.ToString("yyyyMMdd"))-19110000).ToString() + System.DateTime.Now.ToString("hhmmss") ;
        ReportDocument rpt = new ReportDocument();
        RPOUT_05BL BL_05 = new RPOUT_05BL();
        rpt.Load(Server.MapPath("RPOUT_Prt_05.rpt"));
        DataTO conds = PopulateData();
        rpt.SetDataSource(BL_05.getPrintInfo(conds,SelectData));
        rpt.SummaryInfo.ReportTitle = "獎補助專案進入決審彙整表";
        FileName = rpt.SummaryInfo.ReportTitle + FileName;
        rpt.ExportToHttpResponse(Type, Response, true, Server.UrlEncode(FileName));


    }
    protected void Year_TextChanged(object sender, EventArgs e)
    {
        pj_name.DataSource = new RPOUT_CommonBL().getProjectName(Year.Text);
        pj_name.DataBind();
        sel_Pj_Stage.DataSource = new RPOUT_CommonBL().getAwSg_Verify(pj_name.SelectedValue);
        sel_Pj_Stage.DataBind();
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
    protected void pj_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        sel_Pj_Stage.DataSource = new RPOUT_CommonBL().getAwSg_Verify(pj_name.SelectedValue);
        sel_Pj_Stage.DataBind();
    }
}