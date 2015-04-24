using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;
using com.kangdainfo.online.WebBase.BL;

public partial class PjJudge_Ins_01 : IMQDInsertUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        to.setValue("Pj_Code", hid_Pj_Code.Value);

        return to;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "委員遴選作業─資料設定";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "2.1.11";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//訊息列

        DataGridView = grvQuery;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        QueryDataGridView = grvQueryData;
        QueryDataPanel = pnlQueryGridView;
        QueryPageLabel = lblQueryPage;

        ValidationDetailGroupID = "grvQuery";

        #endregion

        #region 宣告資訊

        BL = new PjJudge_01BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        InsertButton = btn_Insert;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        QueryDetailButton = btnDTL_Query;
        InsertDetailButton = btnDTL_INSERT;
        ClearDetailButton = btnDTL_CLEAR;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        BackPage = "PjJudge_Qry_01.aspx";

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
        DataTO to = (DataTO)Session[Web_ID + Session.SessionID + "PjJudge_Ins_01"];

        DataTable dt = new DataTable();

        ((IMQDUIBL)BL).LoadData(to, dt);

        hid_Pj_Code.Value = to.getValue("Pj_Code").ToString();

        lbl_Pj_Name.Text = to.getValue("Pj_Name").ToString();

        lbl_Pj_StartDate.Text = ICommonBL.chgDateTransferDateStr(ICommonBL.chgEnDateToChnDate(to.getValue("Pj_StartDate").ToString()));

        lbl_Pj_User_Code.Text = new BaseFun().getUserAcc(to.getValue("Pj_User_Code").ToString()).User_Name;

        lbl_Pj_Fund.Text = to.getValue("Pj_Fund").ToString();

        lbl_Pj_PjIntro.Text = to.getValue("Pj_PjIntro").ToString();

        lbl_Pj_PjNote.Text = to.getValue("Pj_PjNote").ToString();

        ddl_Ski_Type.DataSource = new BaseFun().getSysCodeByKind("I", "D");
        ddl_Ski_Type.DataBind();

        ddl_CmGp_Code.DataSource = new BaseFun().getCommGroup(to.getValue("Pj_Code").ToString());
        ddl_CmGp_Code.DataBind();

        ddl_Pj_Identity.DataSource = new BaseFun().getSysCodeByKind("P", "I");
        ddl_Pj_Identity.DataBind();
    }

    public override ROW_CMD_TYPE GetRowCommand(string strCmd)
    {
        ROW_CMD_TYPE type = ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
        
        switch (strCmd)
        {
            case "del" :
                type = ROW_CMD_TYPE.ROW_CMD_DELETE;
                break;
            default:
                break;
        }

        return type;
    }

    public override DataTO PopulateDetailData()
    {
        DataTO to = new DataTO();

        if (txt_Comm_Name.Text != "")
            to.setValue("Comm_Name", txt_Comm_Name.Text);

        if (txt_Comm_ComName.Text != "")
            to.setValue("Comm_ComName", txt_Comm_ComName.Text);

        if (ddl_Ski_Num.SelectedValue != "")
            to.setValue("Ski_Num", ddl_Ski_Num.SelectedValue);
        
        return to;
    }

    protected override DataTO PopulateDetailMarkedData()
    {
        DataTO to = base.PopulateDetailMarkedData();
        
        if (!to.isColumnExist("Pj_Identity"))
        {
            to.setValue("Pj_Identity", ddl_Pj_Identity.SelectedValue);
            to.setValue("Pj_IdentityText", ddl_Pj_Identity.SelectedItem.Text);
        }

        if (to.getValue("Pj_Identity").ToString() != "A")
        {
            if (!to.isColumnExist("CmGp_Code"))
            {
                to.setValue("CmGp_Code", ddl_CmGp_Code.SelectedValue);
                to.setValue("CmGp_Name", ddl_CmGp_Code.SelectedItem.Text);
            }
        }
        else
        {
            if (!to.isColumnExist("CmGp_Code"))
            {
                to.setValue("CmGp_Code", "");
                to.setValue("CmGp_Name", "");
            }
        }

        return to;
    }
    
    public override void InitialDetail()
    {
        base.InitialDetail();
    }

    protected void ddl_Ski_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Ski_Type.SelectedValue != "")
        {
            ddl_Ski_Num.DataSource = new BaseFun().getSkillSample(ddl_Ski_Type.SelectedValue);
            ddl_Ski_Num.DataBind();
        }
        else
        {
            ddl_Ski_Num.Items.Clear();
            ddl_Ski_Num.DataBind();
        }
    }

    protected override bool BeforeHandleDetailInsert(DataTO[] tos, DataTable detailData)
    {
        if (tos.Length == 0)
        {
            ShowMsgBox(this.Page, "請至少勾選一項!!");
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[5].Style.Add(HtmlTextWriterStyle.Display, "none");
            e.Row.Cells[7].Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }

}