using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class Coach_Qry_01 : IQueryUI
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
        if (txt_Coach_Code.Text != "")
            to.setValue("Coach_Code", txt_Coach_Code.Text);

        if (txt_Pj_Name.Text != "")
            to.setValue("Pj_Name", txt_Pj_Name.Text);

        if (txt_Com_Boss.Text != "")
            to.setValue("Com_Boss", txt_Com_Boss.Text);
        
        if (txt_Com_Name.Text != "")
            to.setValue("Com_Name", txt_Com_Name.Text);
        
        if (txt_Coach_DateS.Text != "")
            to.setValue("Coach_DateS", txt_Coach_DateS.Text);
        
        if (txt_Coach_DateE.Text != "")
            to.setValue("Coach_DateE", txt_Coach_DateE.Text);

        string selectedChKd_CodeTerms = this.CoachKindCheckboxList1.ChKd_Code_Items;

        if (selectedChKd_CodeTerms != "")
            to.setValue("ChKd_Code", selectedChKd_CodeTerms);

        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        //申請編號
        if (txt_Coach_Code.Text != "")
            txt_Coach_Code.Text = to.getValue("Coach_Code").ToString();
       
        if (txt_Com_Name.Text != "")
            txt_Com_Name.Text = to.getValue("Com_Name").ToString();

        if (txt_Pj_Name.Text != "")
            txt_Pj_Name.Text = to.getValue("Pj_Name").ToString();

        if (txt_Com_Boss.Text != "")
            txt_Com_Boss.Text = to.getValue("Com_Boss").ToString();


        //輔導時間起
        if (txt_Coach_DateS.Text != "")
            txt_Coach_DateS.Text = to.getValue("Coach_DateS").ToString();
        //輔導時間迄
        if (txt_Coach_DateE.Text != "")
            txt_Coach_DateE.Text = to.getValue("Coach_DateE").ToString();

        //處理結果
        /*if (to.getValue("Cnst_Status").ToString() != "")
        {
            foreach (ListItem li in ckl_Cnst_Stat.Items)
            {
                if (to.getValue("Cnst_Status").ToString().IndexOf(li.Value) != -1)
                    li.Selected = true;
            }
        }*/
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "輔導資料查詢維護─內網資料查詢畫面  ";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "5.1.1";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Coach_01BL();//邏輯層

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
        InsertPage = "Coach_Qry_02.aspx";//新增頁
        ModifyPage = "Coach_Upd_01.aspx";//修改頁
        ListPage = "Coach_Lis_01.aspx"; //顯示頁
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
        //BaseFun bf = new BaseFun();
       //Control ctlNewTrial = this.Page.LoadControl("~/UserControl/CoachKindCheckboxList.ascx");

            //SetUserControlProperty(ctlNewTrial, "ID", "Usrctrl" + index.ToString());

           //SetUserControlProperty(ctlNewTrial, "CustID", index.ToString());

            //SetUserControlProperty(ctlNewTrial, "CustName", index.ToString()+":name");


        //this.pnl_ChKd_Code.Controls.Clear();
        //this.pnl_ChKd_Code.Controls.Add(ctlNewTrial);

        // Label br = new Label();

           // br.Text = "index=" + index.ToString()+ "<br/>";

            //this.Panel1.Controls.Add(ctlNewTrial);
        
           // this.Panel1.Controls.Add(br);


    }

    protected override bool BeforeDoDelete(DataTO qto)
    {

        return true;
    }
    protected override bool BeforeDoModify(int rowIdx, DataTO to)
    {
        base.BeforeDoModify(rowIdx, to);
        if (isMod)
            ModifyPage = "Coach_Upd_01.aspx";
        else
            ModifyPage = "Coach_Upd_02.aspx";

        return true;
    }
    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            ((ImageButton)e.Row.Cells[1].Controls[0]).OnClientClick = "if(!confirm('確定要刪除?')){return false;}";
    }
}
