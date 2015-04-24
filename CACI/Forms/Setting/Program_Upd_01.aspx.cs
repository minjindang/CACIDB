using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class Program_Upd_01 : IUpdateUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        to.setValue("Prog_Num", lbl_Prog_Num.Text);
        to.setValue("Prog_Type", rad_Prog_Type.SelectedItem.Value);
        to.setValue("Prog_Class", ddl_Prog_Class.SelectedValue);
        to.setValue("Prog_Name", txt_Prog_Name.Text);
        to.setValue("Prog_Path", txt_Prog_Path.Text);

        return to;
    }

    /// <summary>
    /// 檢核主識別項是否存在(需實作)
    /// </summary>
    /// <param name="to">資料傳輸物件, 應由上層頁面於跳頁時自動塞入Session</param>
    /// <returns>主識別項是否存在</returns>
    public override bool CheckPK(DataTO to)
    {
        if (to.isColumnExist("Prog_Num"))
            return true;
        else
            return false;
    }

    /// <summary>
    /// 將資料取出至畫面上
    /// </summary>
    /// <param name="to">傳輸物件</param>
    public override void RenderData(DataTO to)
    {
        lbl_Prog_Num.Text = to.getValue("Prog_Num").ToString();
        rad_Prog_Type.SelectedValue = to.getValue("Prog_Type").ToString();
        ddl_Prog_Class.SelectedValue = to.getValue("Prog_Class").ToString();
        txt_Prog_Name.Text = to.getValue("Prog_Name").ToString();
        txt_Prog_Path.Text = to.getValue("Prog_Path").ToString();
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "程式清單管理作業─資料更新";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "9.1.17";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Program_01BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        BackPage = "Program_Qry_01.aspx";

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
        lbl_Prog_Num.Text = "";
        txt_Prog_Name.Text = "";
        txt_Prog_Path.Text = "";

        ddl_Prog_Class.DataSource = ((Program_01BL)BL).getProgramClass();
        ddl_Prog_Class.DataBind();
    }
}