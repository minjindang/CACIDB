using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.BL;

public partial class Announcement_Lis_02 : IMMDListUI
{

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "公告作業─資料顯示";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "9.1.8";
        ProgType = PAGE_ACTION.PAGE_LIST;

        MessageLabel = lblMsg;//訊息列

        addDetailMember(new DetailDataGroup(grv_Announcement, pnl_Announcement, lbl_Announcement));
        addDetailMember(new DetailDataGroup(grv_Stage, pnl_Stage, lbl_Stage));
        addDetailMember(new DetailDataGroup(grv_Metting, pnl_Metting, lbl_Metting));
        addDetailMember(new DetailDataGroup(grv_Judge, pnl_Judge, lbl_Judge));

        #endregion

        #region 宣告資訊

        BL = new Announcement_02BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        BackButton = btn_Back;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        BackPage = "Announcement_Qry_01.aspx";

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
        return getLoginUser() != null;
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

    protected void grv_Stage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            int index = int.Parse(e.CommandArgument.ToString());

            DataTO to = new DataTO();

            to.setValue("Pj_Code", grv_Stage.DataKeys[index][0].ToString());

            to.setValue("Stage_Index", grv_Stage.DataKeys[index][1].ToString());

            Session[Web_ID + Session.SessionID + "Project_Upd_01"] = to;

            if (grv_Stage.Rows[index].Cells[3].Text == "A")
            {
                GoURL("/CACI/Forms/Project/Project_Upd_01.aspx");
            }
            else if (grv_Stage.Rows[index].Cells[3].Text == "B")
            {
                GoURL("/CACI/Forms/Project/Project_Upd_02.aspx");
            }
        }
    }

    protected void grv_Stage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = ICommonBL.chgDateTransferDateStr(ICommonBL.chgEnDateToChnDate(e.Row.Cells[2].Text));
        }

        e.Row.Cells[3].Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    protected void grv_Metting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            int index = int.Parse(e.CommandArgument.ToString());

            DataTO to = new DataTO();

            to.setValue("Meeting_Code", grv_Metting.DataKeys[index][0].ToString());

            Session[Web_ID + Session.SessionID + "Metting_Upd_01"] = to;

            GoURL("/CACI/Forms/Metting/Metting_Upd_01.aspx");
        }
    }

    protected void grv_Metting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           e.Row.Cells[2].Text = ICommonBL.chgDateTransferDateStr(ICommonBL.chgEnDateToChnDate(e.Row.Cells[2].Text));
        }
    }

    protected void grv_Judge_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            int index = int.Parse(e.CommandArgument.ToString());

            DataTO to = new DataTO();

            to.setValue("Pj_Code", grv_Judge.DataKeys[index][0].ToString());

            Session[Web_ID + Session.SessionID + "Project_Ins_01"] = to;

            GoURL("/CACI/Forms/Project/PjJudge_Ins_01.aspx");
        }
    }

    protected void grv_Judge_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = ICommonBL.chgDateTransferDateStr(ICommonBL.chgEnDateToChnDate(e.Row.Cells[2].Text));
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session[ICommonUI.Web_ID + Session.SessionID + "Announcement_Lis_03"] = Session[ICommonUI.Web_ID + Session.SessionID + "Announcement_Lis_02"];
        GoURL("/CACI/Forms/Setting/Announcement_Lis_03.aspx");
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session[ICommonUI.Web_ID + Session.SessionID + "Announcement_Lis_04"] = Session[ICommonUI.Web_ID + Session.SessionID + "Announcement_Lis_02"];
        GoURL("/CACI/Forms/Setting/Announcement_Lis_04.aspx");
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session[ICommonUI.Web_ID + Session.SessionID + "Announcement_Lis_05"] = Session[ICommonUI.Web_ID + Session.SessionID + "Announcement_Lis_02"];
        GoURL("/CACI/Forms/Setting/Announcement_Lis_05.aspx");
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Session[ICommonUI.Web_ID + Session.SessionID + "Announcement_Lis_06"] = Session[ICommonUI.Web_ID + Session.SessionID + "Announcement_Lis_02"];
        GoURL("/CACI/Forms/Setting/Announcement_Lis_06.aspx");
    }
}