using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class PhoneRec_Qry_01 : IQueryUI
{
    private bool isMod = true;
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
                isMod = true;
                break;
            case "maintain":
                cmdType = ROW_CMD_TYPE.ROW_CMD_MODIFY;
                isMod = false;
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
        if (txt_PhRec_ComName.Text != "")
            to.setValue("PhRec_ComName", txt_PhRec_ComName.Text);

        if (txt_PhRec_Tonum.Text != "")
            to.setValue("PhRec_Tonum", txt_PhRec_Tonum.Text);

        if (txt_PhRec_CtName.Text != "")
            to.setValue("PhRec_CtName", txt_PhRec_CtName.Text);

        if (txt_PhRec_CtTel.Text != "")
            to.setValue("PhRec_CtTel", txt_PhRec_CtTel.Text);


        string selectedCntClass_CodTerms = "-1";
        foreach (ListItem li in this.cbl_CntClass_Code.Items)
        {
            if (li.Selected)
                selectedCntClass_CodTerms += "," + li.Value;
        }
        if (selectedCntClass_CodTerms != "-1")
            to.setValue("CntClass_Code", selectedCntClass_CodTerms);
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
        if (txt_PhRec_ComName.Text != "")
            txt_PhRec_ComName.Text = to.getValue("PhRec_ComName").ToString();

        if (txt_PhRec_Tonum.Text != "")
            txt_PhRec_Tonum.Text = to.getValue("PhRec_Tonum").ToString();

        if (txt_PhRec_CtName.Text != "")
            txt_PhRec_CtName.Text = to.getValue("PhRec_CtName").ToString();

        if (txt_PhRec_CtTel.Text != "")
            txt_PhRec_CtTel.Text = to.getValue("PhRec_CtTel").ToString();


        //處理結果
        if (to.getValue("CntClass_Code").ToString() != "")
        {
            foreach (ListItem li in cbl_CntClass_Code.Items)
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
        ProgNm = "電話記錄作業─內網資料查詢畫面  ";//程式名ˋ稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "4.1.1";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new PhoneRec_01BL();//邏輯層

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
        InsertPage = "PhoneRec_Ins_01.aspx";//新增頁
        ModifyPage = "PhoneRec_Upd_01.aspx";//修改頁
        ListPage = "PhoneRec_Lis_01.aspx"; //顯示頁
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
        cbl_CntClass_Code.DataSource = bf.getSysCodeByKind("C", "Y");
        cbl_CntClass_Code.DataTextField = "Sys_CdText";
        cbl_CntClass_Code.DataValueField = "Sys_CdCode";
        cbl_CntClass_Code.RepeatDirection = RepeatDirection.Horizontal;
        cbl_CntClass_Code.RepeatColumns = 6;
        cbl_CntClass_Code.DataBind();

    }

    protected override bool BeforeDoDelete(DataTO qto)
    {

        return true;
    }
    protected override bool BeforeDoModify(int rowIdx, DataTO to)
    {
        base.BeforeDoModify(rowIdx, to);
        if (isMod)
            ModifyPage = "PhoneRec_Upd_01.aspx";
        else
            ModifyPage = "PhoneRec_Upd_02.aspx";

        return true;
    }
    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            ((ImageButton)e.Row.Cells[1].Controls[0]).OnClientClick = "if(!confirm('確定要刪除?')){return false;}";
    }
}
