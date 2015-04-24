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

public partial class RPOUT_Qry_10 : IQueryUI
{
    private BaseFun bf = new BaseFun();

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
    public override DataTO PopulateData(){
        DataTO to = new DataTO();
        //年度(TextBox)
        if (txt_Pj_StartDate.Text != "")
        {
            to.setValue("Pj_StartDate", txt_Pj_StartDate.Text);
        }
        //專案名稱
        if (ddl_Pj_Name.SelectedValue != "-1")
        {
            to.setValue("Pj_Name", ddl_Pj_Name.SelectedValue);
        }
        //初審階段
        if (ddl_Stage_Name_Bgn.SelectedValue != "-1")
        {
            to.setValue("Stage_Index_S", ddl_Stage_Name_Bgn.SelectedValue);
        }
        //最終審查階段
        if (ddl_Stage_Name_End.SelectedValue != "-1")
        {
            to.setValue("Stage_Index_E", ddl_Stage_Name_End.SelectedValue);
        }

        return to;
    }

    // <summary>
        // 將Session取到的資料顯示在畫面上(需實作)
        // </summary>
        // <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        //年度(TextBox)
        if (to.getValue("Pj_StartDate").ToString() != "")
            txt_Pj_StartDate.Text = to.getValue("Pj_StartDate").ToString();
        //專案名稱
        if (to.getValue("Pj_Name").ToString() != "")
            ddl_Pj_Name.SelectedValue = to.getValue("Pj_Name").ToString();
        //初審階段
        if (to.getValue("Stage_Index_S").ToString() != "")
            ddl_Stage_Name_Bgn.SelectedValue = to.getValue("Stage_Index_S").ToString();
        //最終審查階段
        if (to.getValue("Stage_Index_E").ToString() != "")
            ddl_Stage_Name_End.SelectedValue = to.getValue("Stage_Index_E").ToString();
        //初審審查結果
        if (to.getValue("Sys_CdText").ToString() != "")
            ddl_Sys_CdText_Bgn.SelectedValue = to.getValue("Sys_CdText").ToString();
        //最終審查結果
        if (to.getValue("Sys_CdText").ToString() != "")
            ddl_Sys_CdText_End.SelectedValue = to.getValue("Sys_CdText").ToString();
    }

    /// <summary>
        /// 設定程式參數(需實作)
        /// </summary>
    public override void SetProp(){

            #region 共同資訊

            PageDropDownList = ddlPage;//每頁筆數（查詢條件）
            ProgCd = this.GetType().BaseType.Name;//程式代號
            ProgNm = "獎補助縣市政府專案綜合資料表";//程式名稱
            ProgLabel = lblProg;//標題（程式代號+程式名稱）
            ProgNum = "7.1.2";
            ProgType = PAGE_ACTION.PAGE_QUERY;
            MessageLabel = lblMsg;//訊息列

            #endregion

            #region 宣告資訊
            BL = new RPOUT_10BL();//邏輯層
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
            //InsertPage = "RPOUT_Qry_10.aspx";//新增頁
            //ModifyPage = "RPOUT_Qry_10.aspx";//修改頁
            //ListPage = "RPOUT_Qry_10.aspx"; //顯示頁
            #endregion 導向頁面資訊

            #region 頁面檢查設定
            checkLoginType = checkLoginType.Need;
            #endregion
        }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
        {
            //匯出Excel、匯出XML、查看列印按鈕狀態為Disable
            btn_PrintToExl.Enabled = false;
            btn_PrintToXML.Enabled = false;
            btn_PrintToPDF.Enabled = false;
            Button_Check();
            
            //專案名稱
            ddl_Pj_Name.Items.Clear();
            ddl_Pj_Name.AppendDataBoundItems = true;
            ddl_Pj_Name.Items.Add(new ListItem("請選擇", "-1"));

            //初審階段
            ddl_Stage_Name_Bgn.Items.Clear();
            ddl_Stage_Name_Bgn.AppendDataBoundItems = true;
            ddl_Stage_Name_Bgn.Items.Add(new ListItem("請選擇", "-1"));
            
            //最終審查階段
            ddl_Stage_Name_End.Items.Clear();
            ddl_Stage_Name_End.AppendDataBoundItems = true;
            ddl_Stage_Name_End.Items.Add(new ListItem("請選擇", "-1"));
        
            //初審審查結果
            ddl_Sys_CdText_Bgn.Items.Clear();
            ddl_Sys_CdText_Bgn.AppendDataBoundItems = true;
            ddl_Sys_CdText_Bgn.Items.Add(new ListItem("請選擇", "-1"));
            ddl_Sys_CdText_Bgn.DataSource = bf.getSysCodeByKind("S", "S");
            ddl_Sys_CdText_Bgn.DataTextField = "Sys_CdText";                 //將『初審審查結果』顯示到使用者介面
            ddl_Sys_CdText_Bgn.DataValueField = "Sys_CdCode";                //對應資料庫內『初審審查結果』
            ddl_Sys_CdText_Bgn.DataBind();

            //最終審查結果    
            ddl_Sys_CdText_End.Items.Clear();
            ddl_Sys_CdText_End.AppendDataBoundItems = true;
            ddl_Sys_CdText_End.Items.Add(new ListItem("請選擇", "-1"));
            ddl_Sys_CdText_End.DataSource = bf.getSysCodeByKind("S", "S");
            ddl_Sys_CdText_End.DataTextField = "Sys_CdText";                 //將『最終審查結果』顯示到使用者介面
            ddl_Sys_CdText_End.DataValueField = "Sys_CdCode";                //對應資料庫內『最終審查結果』
            ddl_Sys_CdText_End.DataBind();           
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

    protected void txt_Pj_StartDate_TextChanged(object sender, EventArgs e)
    {
        //專案名稱
        ddl_Pj_Name.Items.Clear();
        ddl_Pj_Name.Items.Add(new ListItem("請選擇", "-1"));
        DataTO conds = PopulateData();        
        ddl_Pj_Name.DataSource = ((RPOUT_10BL)BL).getPj_Name(conds, txt_Pj_StartDate.Text);
        ddl_Pj_Name.DataTextField = "Pj_Name";
        ddl_Pj_Name.DataValueField = "Pj_Code";
        ddl_Pj_Name.DataBind();
    }

    protected void ddl_Pj_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        //初審階段
        ddl_Stage_Name_Bgn.Items.Clear();  
        DataTO conds = PopulateData();
        ddl_Stage_Name_Bgn.DataSource = ((RPOUT_10BL)BL).getStage_Name(conds, ddl_Pj_Name.SelectedValue);
        ddl_Stage_Name_Bgn.DataTextField = "Stage_Name";
        ddl_Stage_Name_Bgn.DataValueField = "Stage_Index";
        ddl_Stage_Name_Bgn.DataBind();

        //最終審查階段
        ddl_Stage_Name_End.Items.Clear();
        ddl_Stage_Name_End.DataSource = ((RPOUT_10BL)BL).getStage_Name(conds, ddl_Pj_Name.SelectedValue);
        ddl_Stage_Name_End.DataTextField = "Stage_Name";
        ddl_Stage_Name_End.DataValueField = "Stage_Index";
        ddl_Stage_Name_End.DataBind();
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
        RPOUT_10BL BL_10 = new RPOUT_10BL();
        rpt.Load(Server.MapPath("RPOUT_Prt_10.rpt"));
        DataTO conds = PopulateData();
        rpt.SetDataSource(BL_10.getPrintInfo(conds, SelectData));
        string FileName = ProgNm + (int.Parse(System.DateTime.Now.ToString("yyyyMMdd")) - 19110000).ToString() + System.DateTime.Now.ToString("hhmmss");
        rpt.ExportToHttpResponse(Type, Response, true, Server.UrlEncode(FileName));
        //rpt.SummaryInfo.ReportTitle = "Report10";
        //rpt.ExportToHttpResponse(Type, Response, true, rpt.SummaryInfo.ReportTitle);
    }

   
}