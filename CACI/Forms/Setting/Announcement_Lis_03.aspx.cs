using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.BL;

public partial class Announcement_Lis_03 : IMDListUI
{

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "公告作業─系統公告資料";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "9.1.21";
        ProgType = PAGE_ACTION.PAGE_LIST;

        MessageLabel = lblMsg;//訊息列

        //addDetailMember(new DetailDataGroup(grv_Announcement, pnl_Announcement, lbl_Announcement));

        DetailGridView = grv_Announcement;
        PageLabel = lbl_Announcement;
        DataPanel = pnl_Announcement;

        #endregion

        #region 宣告資訊

        BL = new Announcement_03BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        BackButton = btn_Back;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        BackPage = "Announcement_Lis_02.aspx";

        #endregion 導向頁面資訊

        #region 頁面檢查設定
        checkLoginType = checkLoginType.Nonessential;
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
        return to.isColumnExist("User_Code");
    }

    public override void RenderData(DataTO to)
    {
    }

    protected void grv_Announcement_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            DataTO to = new DataTO();

            to.setValue("Ann_Code", grv_Announcement.DataKeys[int.Parse(e.CommandArgument.ToString())][0].ToString());

            Session[Web_ID + Session.SessionID + "Announcement_Lis_01"] = to;

            GoURL("Announcement_Lis_01.aspx");
        }
    }

    protected void grv_Announcement_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = ICommonBL.chgDateTransferDateStr(ICommonBL.chgEnDateToChnDate(e.Row.Cells[2].Text));
        }
    }
}