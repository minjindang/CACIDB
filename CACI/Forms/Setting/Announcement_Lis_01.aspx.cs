using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.BL;

public partial class Announcement_Lis_01 : IListUI
{

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "公告作業─資料查看";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "9.1.10";
        ProgType = PAGE_ACTION.PAGE_LIST;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Announcement_01BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        BackButton = btn_Back;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        string[] exPage = Request.ServerVariables["HTTP_REFERER"].ToString().Split('/');

        if (exPage[exPage.Length - 1] == "Announcement_Lis_02.aspx" || hid_From.Value == "Announcement_Lis_02.aspx")
        {
            hid_From.Value = "Announcement_Lis_02.aspx";
            BackPage = "Announcement_Lis_02.aspx";
        }
        else
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
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Ann_Code");
    }

    public override void RenderData(DataTO to)
    {
        lbl_Ann_Code.Text = to.getValue("Ann_Code").ToString();

        lbl_Ann_Type.Text = to.getValue("Ann_Type_val").ToString();

        lbl_Ann_BgnTime.Text = ICommonBL.chgEnDateToChnDate(to.getValue("Ann_BgnTime").ToString());

        lbl_Ann_EndTime.Text = ICommonBL.chgEnDateToChnDate(to.getValue("Ann_EndTime").ToString());

        //lbl_Pj_Code.Text = to.getValue("Pj_Code").ToString();

        for (int i = 0; i < 3; i++)
        {
            if (to.getValue("Ann_Taker").ToString().Substring(i, 1) == "1")
            {
                chk_Ann_Taker.Items[i].Selected = true;
            }
        }

        lbl_Ann_Name.Text = to.getValue("Ann_Name").ToString();

        lbl_Ann_Text.Text = to.getValue("Ann_Text").ToString();

        
    }
}