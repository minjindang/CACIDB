using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using CrystalDecisions.CrystalReports.Engine;

public partial class RPOUT_Qry_07 : IQueryUI
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

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "媒合計畫繳件明細表";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "8.2.7";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new RPOUT_07BL();//邏輯層

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
        //專案名稱
        ddl_Pj_Name.DataSource = ((RPOUT_07BL)BL).getPj_Name();
        ddl_Pj_Name.DataTextField = "Pj_Name";
        ddl_Pj_Name.DataValueField = "Pj_Name";
        ddl_Pj_Name.DataBind();
        
        //階段名稱
        ddl_Pj_Stage.Items.Clear();
        ddl_Pj_Stage.AppendDataBoundItems = true;
        ddl_Pj_Stage.Items.Add(new ListItem("請選擇", "-1"));
      
        //提案組別
        ddl_ApPj_ApGroup.DataSource = new BaseFun().getSysCodeByKind("P", "G");
        ddl_ApPj_ApGroup.DataTextField = "Sys_CdText";                 //將『提案組別』顯示到使用者介面
        ddl_ApPj_ApGroup.DataValueField = "Sys_CdCode";                //對應資料庫內『提案組別』
        ddl_ApPj_ApGroup.DataBind();

        //產業別
        dll_ApPj_Msectors.DataSource = new BaseFun().getSysCodeByKind("I", "D");
        dll_ApPj_Msectors.DataTextField = "Sys_CdText";                 //將『產業別』顯示到使用者介面
        dll_ApPj_Msectors.DataValueField = "Sys_CdCode";                //對應資料庫內『產業別代號』
        dll_ApPj_Msectors.DataBind();

        //匯出Excel、匯出XML、查看列印預設狀態為disable
        Button_Check();
    }

    protected void ddl_Pj_Name_ddlChanged(object sender, EventArgs e)
    {
        //階段名稱
        DataTO conds = PopulateData();
        ddl_Pj_Stage.DataSource = ((RPOUT_07BL)BL).getStage_Name(conds, ddl_Pj_Name.SelectedValue);
        ddl_Pj_Stage.DataTextField = "Stage_Name";
        ddl_Pj_Stage.DataValueField = "Stage_Index";
        ddl_Pj_Stage.DataBind();
    }

    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();       
     
        //專案名稱
        if (ddl_Pj_Name.Text != "")
            to.setValue("Pj_Name", ddl_Pj_Name.Text);
        //階段名稱
        if (ddl_Pj_Stage.SelectedValue != "")
            to.setValue("Pj_Stage", ddl_Pj_Stage.SelectedValue);
        //提案組別
        if (ddl_ApPj_ApGroup.SelectedValue != "")
            to.setValue("ApPj_ApGroup", ddl_ApPj_ApGroup.SelectedValue);
        //審核結果
        if (sel_AwSg_Verify.SelectedValue != "")
            to.setValue("AwSg_Verify", sel_AwSg_Verify.SelectedValue);
        //公司統編
        if (txt_Com_Tonum.Text != "")
            to.setValue("Com_Tonum", txt_Com_Tonum.Text);
        //公司名稱
        if (txt_Com_Name.Text != "")
            to.setValue("Com_Name", txt_Com_Name.Text);
        //產業別
        if (dll_ApPj_Msectors.SelectedValue != "")
            to.setValue("ApPj_Msectors", dll_ApPj_Msectors.SelectedValue);
        //資本額起
        if (txt_Com_CapitalS.Text != "")
            to.setValue("Com_CapitalS", txt_Com_CapitalS.Text);
        //資本額迄
        if (txt_Com_CapitalE.Text != "")
            to.setValue("Com_CapitalE", txt_Com_CapitalE.Text);
        //是否得獎
        if (sel_CompanyPrice.SelectedValue != "")
            to.setValue("CompanyPrice", sel_CompanyPrice.SelectedValue);
        //申請日期區間起
        if (txt_Aow_DateS.Text != "")
            to.setValue("Aow_DateS", txt_Aow_DateS.Text);
        //申請日期區間迄
        if (txt_Aow_DateE.Text != "")
            to.setValue("Aow_DateE", txt_Aow_DateE.Text);

        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        //申請日期區間起
        if (to.getValue("Aow_DateS").ToString() != "")
            txt_Aow_DateS.Text = to.getValue("Aow_DateS").ToString();
        //申請日期區間迄
        if (to.getValue("Aow_DateE").ToString() != "")
            txt_Aow_DateE.Text = to.getValue("Aow_DateE").ToString();
        //階段名稱
        if (to.getValue("Pj_Stage").ToString() != "")
            ddl_Pj_Stage.SelectedValue = to.getValue("Pj_Stage").ToString();
        //專案名稱
        if (to.getValue("Pj_Name").ToString() != "")
            ddl_Pj_Name.Text = to.getValue("Pj_Name").ToString();
        //提案組別
        if (to.getValue("ApPj_ApGroup").ToString() != "")
            ddl_ApPj_ApGroup.SelectedValue = to.getValue("ApPj_ApGroup").ToString();
        //審核結果
        if (to.getValue("AwSg_Verify").ToString() != "")
            sel_AwSg_Verify.SelectedValue = to.getValue("AwSg_Verify").ToString();
        //公司統編
        if (to.getValue("Com_Tonum").ToString() != "")
            txt_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
        //公司名稱
        if (to.getValue("Com_Name").ToString() != "")
            txt_Com_Name.Text = to.getValue("Com_Name").ToString();
        //產業別
        if (to.getValue("ApPj_Msectors").ToString() != "")
            dll_ApPj_Msectors.SelectedValue = to.getValue("ApPj_Msectors").ToString();
        //資本額起
        if (to.getValue("Com_CapitalS").ToString() != "")
            txt_Com_CapitalS.Text = to.getValue("Com_CapitalS").ToString();
        //資本額迄
        if (to.getValue("Com_CapitalE").ToString() != "")
            txt_Com_CapitalE.Text = to.getValue("Com_CapitalE").ToString();
        //是否得獎
        if (to.getValue("CompanyPrice").ToString() != "")
            sel_CompanyPrice.SelectedValue = to.getValue("CompanyPrice").ToString();
    }

    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (!String.IsNullOrEmpty(e.Row.Cells[5].Text)) { }
            //else
            //{
                e.Row.Cells[5].Text = new BaseFun().getCurrencySymbol(Convert.ToInt32(e.Row.Cells[5].Text)/1000) + "千元";
            //}
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
        RPOUT_07BL BL_07 = new RPOUT_07BL();
        rpt.Load(Server.MapPath("RPOUT_Prt_07.rpt"));
        DataTO conds = PopulateData();
        rpt.SetDataSource(BL_07.getPrintDatas(conds, SelectData));
        rpt.SummaryInfo.ReportTitle = "媒合計畫繳件明細表";
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
