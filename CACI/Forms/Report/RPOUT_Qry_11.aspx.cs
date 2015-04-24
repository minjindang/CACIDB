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

public partial class RPOUT_Qry_11 : IQueryUI
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
    
    protected void Page_Load(object sender, EventArgs e){
    
    } 
      
    // <summary>
    // 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    // </summary>
    // <returns>傳輸物件</returns>
    public override DataTO PopulateData(){
        DataTO to = new DataTO();
        //年度
        if(ddl_Cnst_CntDate.SelectedValue != "-1"){
            to.setValue("Cnst_CntDate", ddl_Cnst_CntDate.SelectedValue);
        }
        //申請單位
        if (txt_Com_Name.Text != ""){
            to.setValue("Com_Name", txt_Com_Name.Text);
        }
        //產業別
        if( ddl_Com_MnSectors.SelectedValue != "-1"){
           to.setValue("Com_MnSectors", ddl_Com_MnSectors.SelectedValue);
        }
        //諮詢類別
        if( ddl_CntClass_Code.SelectedValue != "-1"){
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
        //年度
        if (to.getValue("Cnst_CntDate").ToString() != "")
            ddl_Cnst_CntDate.SelectedValue = to.getValue("Cnst_CntDate").ToString();
        //申請單位
        if(to.getValue("Com_MnSectors").ToString() != "")            
            ddl_Com_MnSectors.SelectedValue = to.getValue("Com_MnSectors").ToString();
        //產業別
        if (to.getValue("CntClass_Code").ToString() != "")
            ddl_CntClass_Code.SelectedValue = to.getValue("CntClass_Code").ToString();
        //諮詢類別
        if (to.getValue("Com_Name").ToString() != "")
            txt_Com_Name.Text = to.getValue("Com_Name").ToString();        
    }

    /// <summary>
        /// 設定程式參數(需實作)
        /// </summary>
    public override void SetProp(){

            #region 共同資訊

            PageDropDownList = ddlPage;//每頁筆數（查詢條件）
            ProgCd = this.GetType().BaseType.Name;//程式代號
            ProgNm = "駐診諮詢需求單";//程式名稱
            ProgLabel = lblProg;//標題（程式代號+程式名稱）
            ProgNum = "7.1.2";
            ProgType = PAGE_ACTION.PAGE_QUERY;
            MessageLabel = lblMsg;//訊息列

            #endregion

            #region 宣告資訊
            BL = new RPOUT_11BL();//邏輯層
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
            //InsertPage = "RPOUT_Qry_11.aspx";//新增頁
            //ModifyPage = "RPOUT_Qry_11.aspx";//修改頁
            //ListPage = "RPOUT_Qry_11.aspx"; //顯示頁
            #endregion 導向頁面資訊

            #region 頁面檢查設定
            checkLoginType = checkLoginType.Need;
            #endregion
        }

    public override void SetDefault()
        {
            //匯出Excel、匯出XML、查看列印按鈕狀態為Disable
            btn_PrintToExl.Enabled = false;
            btn_PrintToXML.Enabled = false;
            btn_PrintToPDF.Enabled = false;
            Button_Check();
        
            //年度
            ddl_Cnst_CntDate.Items.Clear();
            ddl_Cnst_CntDate.AppendDataBoundItems = true;
            ddl_Cnst_CntDate.Items.Add(new ListItem("請選擇", "-1"));
            ddl_Cnst_CntDate.DataSource = ((RPOUT_11BL)BL).getYear();
            ddl_Cnst_CntDate.DataTextField = "Year";                        //將『年度』顯示到使用者介面
            ddl_Cnst_CntDate.DataValueField = "AD";                         //對應資料庫內『年度』
            ddl_Cnst_CntDate.DataBind();

            //產業別
            ddl_Com_MnSectors.Items.Clear();
            ddl_Com_MnSectors.AppendDataBoundItems = true;
            ddl_Com_MnSectors.Items.Add(new ListItem("請選擇", "-1"));
            ddl_Com_MnSectors.DataSource = bf.getSysCodeByKind("I", "D");
            ddl_Com_MnSectors.DataTextField = "Sys_CdText";                 //將『產業別』顯示到使用者介面
            ddl_Com_MnSectors.DataValueField = "Sys_CdCode";                //對應資料庫內『產業別代號』
            ddl_Com_MnSectors.DataBind();

            //諮詢類別    
            ddl_CntClass_Code.Items.Clear();
            ddl_CntClass_Code.AppendDataBoundItems = true;
            ddl_CntClass_Code.Items.Add(new ListItem("請選擇", "-1"));
            ddl_CntClass_Code.DataSource = bf.getSysCodeByKind("C", "Y");
            ddl_CntClass_Code.DataTextField = "Sys_CdText";                 //將『諮詢類別』顯示到使用者介面
            ddl_CntClass_Code.DataValueField = "Sys_CdCode";                //對應資料庫內『諮詢類別代號』
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
                e.Row.Cells[2].Text = new BaseFun().getSysCodeValue("I", "D", e.Row.Cells[2].Text);
                e.Row.Cells[3].Text = new BaseFun().getSysCodeValue("C", "Y", e.Row.Cells[3].Text);
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
        RPOUT_11BL BL_11 = new RPOUT_11BL();
        rpt.Load(Server.MapPath("RPOUT_Prt_11.rpt"));
        DataTO conds = PopulateData();
        rpt.SetDataSource(BL_11.getPrintInfo(conds, SelectData));        
        string FileName = ProgNm + (int.Parse(System.DateTime.Now.ToString("yyyyMMdd")) - 19110000).ToString() + System.DateTime.Now.ToString("hhmmss");
        rpt.ExportToHttpResponse(Type, Response, true, Server.UrlEncode(FileName));        
        
    }

}