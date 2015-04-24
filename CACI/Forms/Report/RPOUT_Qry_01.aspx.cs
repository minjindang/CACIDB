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

public partial class RPOUT_Qry_01 : IQueryUI
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
        //專案名稱
        if (sel_Pj_name.Text != "")
            to.setValue("Pj_Name", sel_Pj_name.SelectedValue);
        //申請單位
        if (txt_Com_Name.Text != "")
            to.setValue("Com_Name", txt_Com_Name.Text);
        //計畫名稱
        if (txt_ApPj_Name.Text != "")
            to.setValue("ApPj_Name", txt_ApPj_Name.Text);
        //商品類別
        if (sel_ApPj_ProdCls.Text != "")
            to.setValue("ApPj_ProdCls", sel_ApPj_ProdCls.SelectedValue);
        //申請組別
        if (sel_ApPj_ApGroup.Text != "")
            to.setValue("ApPj_ApGroup", sel_ApPj_ApGroup.SelectedValue);
        //產業別
        if (sel_ApPj_Msectors.Text != "")
            to.setValue("ApPj_Msectors", sel_ApPj_Msectors.SelectedValue);
        //公司登記所在地
        if (sel_Com_PostCode.Text != "")
            to.setValue("Com_PostCode", sel_Com_PostCode.SelectedValue);
        //if (txt_Com_OPAddr.Text != "")
        //    to.setValue("Com_OPAddr", txt_Com_OPAddr.Text);
        //初審/技審階段
        if (sel_AwSg_Verify_1.Text != "")
            to.setValue("AwSg_Verify_1", sel_AwSg_Verify_1.SelectedValue);
        //初審/技審階段
        if (sel_AwSg_Verify_2.Text != "")
            to.setValue("AwSg_Verify_2", sel_AwSg_Verify_2.SelectedValue);
        //申請日期起
        if (Aow_SDate.Text != "")
            to.setValue("Aow_SDate", Aow_SDate.Text);
        //申請日期迄
        if (Aow_EDate.Text != "")
            to.setValue("Aow_EDate", Aow_EDate.Text);            
        //初審結果


        //資本額區間起
        if (txt_Com_CapitalS.Text != "")
            to.setValue("Com_CapitalS", txt_Com_CapitalS.Text );
        //資本額區間迄
        if (txt_Com_CapitalE.Text != "")
            to.setValue("Com_CapitalE", txt_Com_CapitalE.Text);

        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        //專案名稱
        //if (pj_name.Text != "")
        //    pj_name.Text = to.getValue("Pj_Name").ToString();
    }


    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "奬補助專案申請一覽表  ";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "8.2.1";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new RPOUT_01BL();//邏輯層

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

        #endregion 按鈕定義 end

        #region 導向頁面資訊
        //InsertPage = "RPOUT_Qry_01.aspx";//新增頁
        //ModifyPage = "RPOUT_Qry_01.aspx";//修改頁
        ListPage = "RPOUT_Qry_01.aspx"; //顯示頁
        #endregion 導向頁面資訊

        #region 登入檢查模式
        //checkLoginType = com.kangdainfo.online.WebBase.UI.checkLoginType.Need;
        checkLoginType = checkLoginType.Need;
        #endregion
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        BaseFun bf = new BaseFun();
        //DropdownList
        //初審
        sel_First_Res.DataSource = bf.getSysCodeByKind("S", "S");
        sel_First_Res.DataTextField = "Sys_CdText";
        sel_First_Res.DataValueField = "Sys_CdCode";
        sel_First_Res.DataBind();
        //商品類別
        sel_ApPj_ProdCls.DataSource = bf.getSysCodeByKind("A", "P");
        sel_ApPj_ProdCls.DataTextField = "Sys_CdText";
        sel_ApPj_ProdCls.DataValueField = "Sys_CdCode";
        sel_ApPj_ProdCls.DataBind();
        //申請組別
        sel_ApPj_ApGroup.DataSource = bf.getSysCodeByKind("P", "G");
        sel_ApPj_ApGroup.DataTextField = "Sys_CdText";
        sel_ApPj_ApGroup.DataValueField = "Sys_CdCode";
        sel_ApPj_ApGroup.DataBind();
        //產業別
        sel_ApPj_Msectors.DataSource = bf.getSysCodeByKind("I", "D");
        sel_ApPj_Msectors.DataTextField = "Sys_CdText";
        sel_ApPj_Msectors.DataValueField = "Sys_CdCode";
        sel_ApPj_Msectors.DataBind();

        sel_Pj_name.DataSource = new RPOUT_CommonBL().getProjectName();
        sel_Pj_name.DataBind();

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
                SelectData += "'" + this.grvQuery.DataKeys[GR.RowIndex].Value.ToString() + "',";
            }
        }
        if (SelectData != "")
        {
            SelectData = SelectData.Substring(0, SelectData.Length - 1);
        }
        string FileName = (int.Parse(System.DateTime.Now.ToString("yyyyMMdd")) - 19110000).ToString() + System.DateTime.Now.ToString("hhmmss");
        ReportDocument rpt = new ReportDocument();
        RPOUT_01BL BL_01 = new RPOUT_01BL();
        rpt.Load(Server.MapPath("RPOUT_Prt_01.rpt"));
        DataTO conds = PopulateData();
        rpt.SetDataSource(BL_01.getPrintInfo(conds, SelectData));
        rpt.SummaryInfo.ReportTitle = "奬補助專案申請一覽表";
        FileName = rpt.SummaryInfo.ReportTitle + FileName;
        rpt.ExportToHttpResponse(Type, Response, true, Server.UrlEncode(FileName));

    }

    protected void sel_Pj_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        sel_AwSg_Verify_1.DataSource = new RPOUT_CommonBL().getAwSg_Verify(sel_Pj_name.SelectedValue);
        sel_AwSg_Verify_1.DataBind();
        sel_AwSg_Verify_2.DataSource = new RPOUT_CommonBL().getAwSg_Verify(sel_Pj_name.SelectedValue);
        sel_AwSg_Verify_2.DataBind();
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

}