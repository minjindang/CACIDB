using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.BL;

public partial class Announcement_Upd_01 : IUpdateUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        to.setValue("Ann_Code", lbl_Ann_Code.Text);

        if (ddl_Ann_Type.SelectedValue != "")
            to.setValue("Ann_Type", ddl_Ann_Type.SelectedValue);

        if (txt_Ann_BgnTime.Text != "")
            to.setValue("Ann_BgnTime", ICommonBL.chgChnDateToEnDate(txt_Ann_BgnTime.Text));

        if (txt_Ann_EndTime.Text != "")
            to.setValue("Ann_EndTime", ICommonBL.chgChnDateToEnDate(txt_Ann_EndTime.Text));

        if (ddl_Pj_Code.SelectedValue != "")
            to.setValue("Pj_Code", ddl_Pj_Code.SelectedValue);

        string str_Ann_Taker = "";

        for (int i = 0; i < 3; i++)
        {
            if (chk_Ann_Taker.Items[i].Selected)
                str_Ann_Taker += "1";
            else
                str_Ann_Taker += "0";
        }

        to.setValue("Ann_Taker", str_Ann_Taker);

        if (txt_Ann_Name.Text != "")
            to.setValue("Ann_Name", txt_Ann_Name.Text);

        if (txt_Ann_Text.Text != "")
            to.setValue("Ann_Text", txt_Ann_Text.Text);

        return to;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "公告作業─資料修改";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "9.1.12";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Announcement_01BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        BackPage = "Announcement_Qry_01.aspx";

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
        ddl_Ann_Type.DataSource = new BaseFun().getSysCodeByKind("A", "K");
        ddl_Ann_Type.DataBind();

        ddl_Pj_Code.DataSource = ((Announcement_01BL)BL).getActiveProject();
        ddl_Pj_Code.DataBind();
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Ann_Code");
    }

    public override void RenderData(DataTO to)
    {
        lbl_Ann_Code.Text = to.getValue("Ann_Code").ToString();

        ddl_Ann_Type.SelectedValue = to.getValue("Ann_Type").ToString();

        txt_Ann_BgnTime.Text = ICommonBL.chgEnDateToChnDate(to.getValue("Ann_BgnTime").ToString());

        txt_Ann_EndTime.Text = ICommonBL.chgEnDateToChnDate(to.getValue("Ann_EndTime").ToString());

        ddl_Pj_Code.SelectedValue = to.getValue("Pj_Code").ToString();

        for (int i = 0; i < 3; i++)
        {
            if (to.getValue("Ann_Taker").ToString().Substring(i, 1) == "1")
            {
                chk_Ann_Taker.Items[i].Selected = true;
            }
        }

        txt_Ann_Name.Text = to.getValue("Ann_Name").ToString();

        txt_Ann_Text.Text = to.getValue("Ann_Text").ToString();

    }
}