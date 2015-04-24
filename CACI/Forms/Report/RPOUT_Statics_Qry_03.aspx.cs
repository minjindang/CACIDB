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


public partial class RPOUT_Statics_Qry_03 : IQueryUI
{       
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();
        //單位代號
        if (Year.Text != "")
            to.setValue("Coach_Date", Year.Text);

        return to;
    }
    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    public override void LoadSessionTO(DataTO to)
    {
        //單位代號
        if (Year.Text != "")
            Year.Text = to.getValue("Coach_Date").ToString();          
    }

   
    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "輔導-輔導案件分析";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "8.4.7";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new RPOUT_Statics_Lis_03BL();//邏輯層

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

        if(Session[ICommonUI.Web_ID + Session.SessionID + "RPOUT_Statics_Lis"] !=null)
        {
           DataTO to =  ((DataTO)Session[ICommonUI.Web_ID + Session.SessionID + "RPOUT_Statics_Lis"]);
           this.ddl_Pj_Name.SelectedValue = to.getValue("ddl_Pj_Name").ToString();
        }
    }
    protected void ddl_Pj_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTO to = new DataTO();
        to.setValue("ddl_Pj_Name", ddl_Pj_Name.SelectedValue);
        Session[ICommonUI.Web_ID + Session.SessionID + "RPOUT_Statics_Lis"] = to;
        GoURL(string.Format("\\CACI\\Forms\\Report\\{0}", ddl_Pj_Name.SelectedValue));
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
        ReportDocument rpt = new ReportDocument();
        RPOUT_Statics_Lis_03BL BL_03 = new RPOUT_Statics_Lis_03BL();
        rpt.Load(Server.MapPath("RPOUT_Statics_Prt_03.rpt"));
        DataTO conds = PopulateData();
        rpt.SetDataSource(((IQueryBL)BL).QueryDataForList(conds));
        rpt.SummaryInfo.ReportTitle = "輔導-輔導案件分析表-產業類別分析";
        rpt.SetParameterValue("yearCountTotal", Convert.ToDecimal(this.yearCountTotal.Value));
        rpt.SetParameterValue("accuCountTotal", Convert.ToDecimal(this.accuCountTotal.Value));
        rpt.ExportToHttpResponse(Type, Response, true, rpt.SummaryInfo.ReportTitle);
    }
    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
   {
       if (e.Row.RowType == DataControlRowType.Footer)
       {
           int count = 0;
           int count2 = 0;
           float count3 = 0;
           float count4 = 0;
           for (int i = 0; i < this.grvQuery.Rows.Count; i++)
           {

               count += Convert.ToInt16(grvQuery.Rows[i].Cells[1].Text);
               count2 += Convert.ToInt16(grvQuery.Rows[i].Cells[3].Text);


           }


           for (int i = 0; i < grvQuery.Rows.Count; i++)
           {
               try
               {
                   //總計
                   e.Row.Cells[0].Text = "總計";

                   //個數
                   e.Row.Cells[1].Text = count.ToString();
                   e.Row.Cells[3].Text = count2.ToString();
                   Decimal thisYearTatol = Convert.ToDecimal(e.Row.Cells[1].Text);
                   Decimal allYearTatol = Convert.ToDecimal(e.Row.Cells[3].Text);
                   this.accuCountTotal.Value = allYearTatol.ToString();
                   this.yearCountTotal.Value = thisYearTatol.ToString();
                   //百分比         
                   grvQuery.Rows[i].Cells[2].Text = (Math.Round((Convert.ToDecimal(grvQuery.Rows[i].Cells[1].Text.ToString()) / thisYearTatol) * 100, 2)).ToString() + "%";
                   grvQuery.Rows[i].Cells[4].Text = (Math.Round((Convert.ToDecimal(grvQuery.Rows[i].Cells[3].Text.ToString()) / allYearTatol) * 100, 2)).ToString() + "%";
                   count3 += Convert.ToInt16((Math.Round((Convert.ToDecimal(grvQuery.Rows[i].Cells[1].Text.ToString()) / thisYearTatol) * 100, 2)));
                   count4 += Convert.ToInt16((Math.Round((Convert.ToDecimal(grvQuery.Rows[i].Cells[3].Text.ToString()) / allYearTatol) * 100, 2)));
                   if (count3 > 0)
                   {
                       e.Row.Cells[2].Text = "100%";
                   }
                   else
                   {

                       e.Row.Cells[2].Text = count3.ToString() + "%";
                   }
                   if (count3 > 0)
                   {
                       e.Row.Cells[4].Text = "100%";
                   }
                   else
                   {
                       e.Row.Cells[4].Text = count4.ToString() + "%";
                   }
               }

               catch (Exception)
               {
                   
                   try
                   {
                       Decimal thisYearTatol = Convert.ToDecimal(e.Row.Cells[1].Text);
                       Decimal allYearTatol = Convert.ToDecimal(e.Row.Cells[3].Text);
                       this.accuCountTotal.Value = allYearTatol.ToString();
                       this.yearCountTotal.Value = thisYearTatol.ToString();
                       count4 += Convert.ToInt16((Math.Round((Convert.ToDecimal(grvQuery.Rows[i].Cells[3].Text.ToString()) / allYearTatol) * 100, 2)));
                       grvQuery.Rows[i].Cells[2].Text = "0%";
                       e.Row.Cells[2].Text = "0%";
                       if (count4 > 0)
                       {
                           grvQuery.Rows[i].Cells[4].Text = (Math.Round((Convert.ToDecimal(grvQuery.Rows[i].Cells[3].Text.ToString()) / allYearTatol) * 100, 2)).ToString() + "%";
                           e.Row.Cells[4].Text = "100%";
                       }
                       else
                       {
                           grvQuery.Rows[i].Cells[4].Text = "0%";
                           e.Row.Cells[4].Text = "0%";
                       }

                   }
                   catch (Exception)
                   {
                       grvQuery.Rows[i].Cells[2].Text = "0%";
                       grvQuery.Rows[i].Cells[4].Text = "0%";
                       e.Row.Cells[2].Text = "0%";
                       e.Row.Cells[4].Text = "0%";
                   }

               }

           }    
       }                    
   }
    public override ROW_CMD_TYPE GetRowCommand(string strCmd)
   {
       throw new NotImplementedException();
   }

}