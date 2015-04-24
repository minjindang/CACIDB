using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class Consulting_Qry_01 : IQueryUI
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
        //申請編號
        if (txt_Cnst_Code.Text != "")
            to.setValue("Cnst_Code", txt_Cnst_Code.Text);
        //申請編號
        if (txt_Com_Name.Text != "")
            to.setValue("Com_Code", txt_Com_Name.Text);
        //諮詢時間起
        if (txt_Cnst_CntDateS.Text != "")
            to.setValue("Cnst_CntDateS", txt_Cnst_CntDateS.Text);
        //諮詢時間迄
        if (txt_Cnst_CntDateE.Text != "")
            to.setValue("Cnst_CntDateE", txt_Cnst_CntDateE.Text);
        //處理結果
        string selectedCnst_StatTerms = "-1";
        foreach (ListItem li in ckl_Cnst_Stat.Items)
        {
            if (li.Selected)
                selectedCnst_StatTerms += "," + li.Value ;
        }
        if (selectedCnst_StatTerms != "-1")
            to.setValue("Cnst_Status", selectedCnst_StatTerms);
        else
            to.setValue("Cnst_Status", "-1");
        //詢問類別
        string selectedCntClass_CodeTerms = "-1";
        foreach (ListItem li in ckl_CntClass_Code.Items)
        {
            if (li.Selected)
                selectedCntClass_CodeTerms += "," + li.Value;
        }
        if (selectedCntClass_CodeTerms != "-1")
            to.setValue("CntClass_Code", selectedCntClass_CodeTerms);
        else
            to.setValue("CntClass_Code", "-1");
        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        //申請編號
        if (txt_Cnst_Code.Text != "")
            txt_Cnst_Code.Text = to.getValue("Cnst_Code").ToString();
        //申請編號
        if (txt_Com_Name.Text != "")
            txt_Com_Name.Text = to.getValue("txt_Com_Name").ToString();
        //諮詢時間起
        if (txt_Cnst_CntDateS.Text != "")
            txt_Cnst_CntDateS.Text = to.getValue("txt_Cnst_CntDateS").ToString();
        //諮詢時間迄
        if (to.getValue("txt_Cnst_CntDateE").ToString() != "")
            txt_Cnst_CntDateE.Text = to.getValue("txt_Cnst_CntDateE").ToString();

        //處理結果
        if (to.getValue("Cnst_Status").ToString() != "")
        {
            foreach (ListItem li in ckl_Cnst_Stat.Items)
            {
                if (to.getValue("Cnst_Status").ToString().IndexOf(li.Value) != -1)
                    li.Selected = true;
            }
        }
        //詢問類別
        if (to.getValue("CntClass_Code").ToString() != "")
        {
            foreach (ListItem li in ckl_CntClass_Code.Items)
            {
                if (to.getValue("CntClass_Code").ToString().IndexOf(li.Value) != -1)
                    li.Selected = true;
            }
        }
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "諮詢作業─內網資料查詢畫面  ";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "6.1.2";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Consulting_01BL();//邏輯層

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
        InsertPage = "Consulting_Ins_01.aspx";//新增頁
        ModifyPage = "Consulting_Upd_01.aspx";//修改頁
        ListPage = "Consulting_Lis_01.aspx"; //顯示頁
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

        //處理結果
        ckl_Cnst_Stat.DataSource = bf.getSysCodeByKind("N", "S");
        ckl_Cnst_Stat.DataTextField = "Sys_CdText";
        ckl_Cnst_Stat.DataValueField = "Sys_CdCode";
        ckl_Cnst_Stat.DataBind();

        //詢問類別
        ckl_CntClass_Code.DataSource = bf.getSysCodeByKind("C", "Y");
        ckl_CntClass_Code.DataTextField = "Sys_CdText";
        ckl_CntClass_Code.DataValueField = "Sys_CdCode";
        ckl_CntClass_Code.RepeatColumns = 6;
        ckl_CntClass_Code.DataBind();
    }

    protected override bool BeforeDoDelete(DataTO qto)
    {

        return true;
    }
    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((ImageButton)e.Row.Cells[1].Controls[0]).OnClientClick = "if(!confirm('確定要刪除?')){return false;}";
            if (e.Row.Cells[6].Text.Equals("F") || e.Row.Cells[6].Text.Equals("T"))
                ((ImageButton)e.Row.Cells[0].Controls[0]).Visible = false;
           
            
        }
        e.Row.Cells[6].Style.Add(HtmlTextWriterStyle.Display, "none");
    }
}
