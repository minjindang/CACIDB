using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class SM1002L : IMDListUI
{
    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "Master-Detail 顯示作業範本";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "0.2";
        ProgType = PAGE_ACTION.PAGE_LIST;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grvQuery;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        BackButton = btn_Back;

        #endregion

        #region 宣告資訊

        BL = new SM1002BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        BackButton = btn_Back;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        BackPage = "SM1002Q.aspx";

        #endregion 導向頁面資訊

        #region 頁面檢查設定
        checkLoginType = checkLoginType.None;
        #endregion
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        lbl_Mcol_1.Text = "";
        lbl_Mcol_2.Text = "";
        lbl_Mcol_3.Text = "";
    }

    public override void RenderData(DataTO to)
    {
        lbl_Mcol_1.Text = to.getValue("Mcol_1").ToString();

        lbl_Mcol_2.Text = to.getValue("Mcol_2").ToString();

        lbl_Mcol_3.Text = to.getValue("Mcol_3").ToString();
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Mcol_1");
    }
}