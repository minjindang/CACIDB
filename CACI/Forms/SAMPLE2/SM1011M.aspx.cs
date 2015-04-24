﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class SM1011M : IUpdateUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        if (!to.isColumnExist("Mcol_1"))
            to.setValue("Mcol_1", lbl_Mcol_1.Text);
        else
            to.updateValue("Mcol_1", lbl_Mcol_1.Text);

        if (!to.isColumnExist("Mcol_2"))
            to.setValue("Mcol_2", txt_Mcol_2.Text);
        else
            to.updateValue("Mcol_2", txt_Mcol_2.Text);

        if (!to.isColumnExist("Mcol_3"))
            to.setValue("Mcol_3", txt_Mcol_3.Text);
        else
            to.updateValue("Mcol_3", txt_Mcol_3.Text);

        return to;
    }

    /// <summary>
    /// 檢核主識別項是否存在(需實作)
    /// </summary>
    /// <param name="to">資料傳輸物件, 應由上層頁面於跳頁時自動塞入Session</param>
    /// <returns>主識別項是否存在</returns>
    public override bool CheckPK(DataTO to)
    {
        if (to.isColumnExist("Mcol_1"))
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
        lbl_Mcol_1.Text = to.getValue("Mcol_1").ToString();
        txt_Mcol_2.Text = to.getValue("Mcol_2").ToString();
        txt_Mcol_3.Text = to.getValue("Mcol_3").ToString();
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "Master修改作業範本";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "0.1";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new SM1001BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        BackPage = "SM1011Q.aspx";

        #endregion 導向頁面資訊
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        lbl_Mcol_1.Text = "";
        txt_Mcol_2.Text = "";
        txt_Mcol_3.Text = "";
    }
}