using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;
using com.kangdainfo.online.WebBase.BL;

public partial class SKillSample_Upd_01 : IMDUpdateUI 
{

    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        return to;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "專長範本資料作業─資料修改";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "10.5.1";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grvQuery;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        InsertDetailButton = btnDTL_INSERT;
        UpdateDetailButton = btnDTL_UPDATE;
        UpdateDetailButton.Visible = false;
        ClearDetailButton = btnDTL_CLEAR;

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;
        BackButton.Visible = false;

        ValidationDetailGroupID = "grvQuery";

        #endregion

        #region 宣告資訊

        BL = new SkillSample_01BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;
        BackButton.Visible = false;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

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
        BaseFun bf = new BaseFun();
        //專長分類
        ddl_Ski_Kind.DataSource = bf.getSysCodeByKind("I", "D");
        ddl_Ski_Kind.DataTextField = "Sys_CdText";
        ddl_Ski_Kind.DataValueField = "Sys_CdCode";
        ddl_Ski_Kind.DataBind();
    }

    public override ROW_CMD_TYPE GetRowCommand(string strCmd)
    {
        ROW_CMD_TYPE type = ROW_CMD_TYPE.ROW_CMD_UNKNOWN;

        switch (strCmd)
        {
            case "select":
                type = ROW_CMD_TYPE.ROW_CMD_MODIFY;
                break;
            case "del":
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
        string newMemberID = ICommonBL.getNewSerialNo(DataBase.CACIDB, "SN");
        to.setValue("Ski_Num", newMemberID);
        to.setValue("Ski_Kind", ddl_Ski_Kind.SelectedValue);
        to.setValue("Sys_CdText", ddl_Ski_Kind.SelectedItem.Text);
        to.setValue("Ski_Name", txt_Ski_Name.Text);
        return to;
    }

    public override void RenderDetailData(DataTO to)
    {
        ddl_Ski_Kind.SelectedValue = to.getValue("Ski_Kind").ToString();
        txt_Ski_Name.Text = to.getValue("Ski_Name").ToString();
        //hid_IsNew.Value = to.getValue("IsNew").ToString();
    }

    public override void InitialDetail()
    {
        base.InitialDetail();
        txt_Ski_Name.Text = "";
        //hid_IsNew.Value = "N";
    }

    public override void RenderData(DataTO to)
    {

    }

    public override bool CheckPK(DataTO to)
    {
        return true;
    }

    protected override void AfterHandleUpdate()
    {
        base.AfterHandleUpdate();
        //btn_Update.Enabled = false;
    }

    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            ((ImageButton)e.Row.Cells[0].Controls[0]).OnClientClick = "if(!confirm('確定要刪除?')){return false;}";
        e.Row.Cells[3].Visible = false;
    }
}