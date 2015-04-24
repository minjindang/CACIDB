using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class Mety_Ins_01 : IInsertUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        if (txt_Mety_Code.Text != "")
            to.setValue("Mety_Code", txt_Mety_Code.Text);

        if (ddl_Pj_Kind.SelectedValue != "")
            to.setValue("Pj_Kind", ddl_Pj_Kind.SelectedValue);

        if (txt_Mety_Name.Text != "")
            to.setValue("Mety_Name", txt_Mety_Name.Text);

        to.setValue("Mety_CanAdd", chk_Can_Add.Checked ? "Y" : "N");
        
        return to;

    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "會議類別資料作業─新增作業";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "10.6.2";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Mety_01BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        InsertButton = btn_Insert;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        BackPage = "Mety_Qry_01.aspx";

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
        ddl_Pj_Kind.DataSource = ((Mety_01BL)BL).getPj_Kind_List();
        ddl_Pj_Kind.DataBind();

        txt_Mety_Name.Text = "";

        chk_Can_Add.Checked = false;
    }
}