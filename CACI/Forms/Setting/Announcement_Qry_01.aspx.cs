using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using System.Web.UI.HtmlControls;
using com.kangdainfo.online.WebBase.BL;

public partial class Announcement_Qry_01 : IQueryUI
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

        if (ddl_Ann_Type.SelectedValue != "")
            to.setValue("Ann_Type", ddl_Ann_Type.SelectedValue);

        if (ddl_UsDp_Code.SelectedValue != "")
            to.setValue("UsDp_Code", ddl_UsDp_Code.SelectedValue);

        if (ddl_Ann_Anncer.SelectedValue != "")
            to.setValue("Ann_Anncer", ddl_Ann_Anncer.SelectedValue);

        if (txt_Ann_BgnTime.Text != "")
            to.setValue("Ann_BgnTime", ICommonBL.chgChnDateToEnDate(txt_Ann_BgnTime.Text));

        if (txt_Ann_EndTime.Text != "")
            to.setValue("Ann_EndTime", ICommonBL.chgChnDateToEnDate(txt_Ann_EndTime.Text));

        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        ddl_Ann_Type.SelectedValue = to.getValue("Ann_Type").ToString();

        ddl_UsDp_Code.SelectedValue = to.getValue("UsDp_Code").ToString();

        if (ddl_UsDp_Code.SelectedValue != "")
        {
            ddl_UsDp_Code_SelectedIndexChanged(ddl_UsDp_Code, new EventArgs());
        }

        ddl_Ann_Anncer.SelectedValue = to.getValue("Ann_Anncer").ToString();

        txt_Ann_BgnTime.Text = to.getValue("Ann_BgnTime").ToString();

        txt_Ann_EndTime.Text = to.getValue("Ann_EndTime").ToString();
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "公告作業─資料查詢";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "9.1.9";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Announcement_01BL();//邏輯層

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
        InsertPage = "Announcement_Ins_01.aspx";//新增頁
        ModifyPage = "Announcement_Upd_01.aspx";//修改頁
        ListPage = "Announcement_Lis_01.aspx"; //顯示頁
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
        ddl_UsDp_Code.DataSource = new BaseFun().getUserDep();
        ddl_UsDp_Code.DataBind();

        ddl_Ann_Type.DataSource = new BaseFun().getSysCodeByKind("A", "K");
        ddl_Ann_Type.DataBind();
    }
    protected void ddl_UsDp_Code_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Ann_Anncer.DataSource = new BaseFun().getDepUser(ddl_UsDp_Code.SelectedValue);
        ddl_Ann_Anncer.DataBind();
    }
}