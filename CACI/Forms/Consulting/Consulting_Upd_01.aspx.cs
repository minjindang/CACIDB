using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.DB;

public partial class Consulting_Upd_01 : IMMDUpdateUI
{
    public override ROW_CMD_TYPE GetRowCommand(string DetailGridViewID, string strCmd)
    {
        switch (DetailGridViewID)
        {
            case "grv_CntReply":

                switch (strCmd)
                {
                    case "del":
                        return ROW_CMD_TYPE.ROW_CMD_DELETE;
                        break;
                    case "mod":
                        return ROW_CMD_TYPE.ROW_CMD_MODIFY;
                        break;
                    default:
                        return ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
                        break;
                }
                break;
            case "grv_Phone":

                switch (strCmd)
                {
                    case "del":
                        return ROW_CMD_TYPE.ROW_CMD_DELETE;
                        break;
                    default:
                        return ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
                        break;
                }
                break;
            default:
                return ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
                break;
        }
    }

    protected override void InitialDetail(string dataGridViewID)
    {
        switch (dataGridViewID)
        {
            case "grv_CntReply":
                break;
            case "grv_Phone":
                break;
            default:

                break;
        }
    }

    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        //單位(公司)基本資料
        //if (!to.isColumnExist("Com_Code"))
        //    to.setValue("Com_Code", hid_Com_Code.Value);
        //if (!to.isColumnExist("Com_Name"))
        //    to.setValue("Com_Name", lbl_Com_Name.Text);
        //if (!to.isColumnExist("Com_Tonum"))
        //    to.setValue("Com_Tonum", lbl_Com_Tonum.Text);
        //if (!to.isColumnExist("twRec_Info"))
        //    to.setValue("twRec_Info", lbl_twRec_Info.Text);

        to.setValue("Cnst_Code", hid_Cnst_Code.Value); //詢問代號

        to.setValue("CntClass_Code", ckl_CntClass_Code.Text); //詢問類別

        to.setValue("Cnst_CntText", txt_Cnst_CntText.Text); //詢問內容

        to.setValue("CtRepl_Date", txt_CtRepl_Date.Text); //回覆時間

        to.setValue("CtRepl_RpWay", ddl_CtRepl_RpWay.SelectedValue); // 回覆方式

        to.setValue("CtRepl_RpText", txt_CtRepl_RpText.Text); //回覆內容

        if (ddl_CtRepl_RpResult.SelectedValue != "")
            to.setValue("CtRepl_RpResult", ddl_CtRepl_RpResult.SelectedValue);

        return to;
    }

    public override DataTO PopulateDetailData(string DetailGridViewID)
    {
        DataTO to = new DataTO();
        switch (DetailGridViewID)
        {
            case "grv_CntReply":
                break;
            case "grv_Phone":
                break;
            default:
                break;

        }
        return to;
    }

    public override void RenderDetailData(string DetailGridViewID, DataTO to)
    {
        switch (DetailGridViewID)
        {
            case "grv_CntReply":
                break;
            case "grv_Phone":
                break;
            default:
                break;

        }
    }

    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "諮詢資料查詢維護─內網資料修改畫面";
        ProgNum = "6.1.4";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grv_CntReply, pnlGridView, lblPage, btnDTL_INSERT, btnDTL_UPDATE, btnDTL_CLEAR, "grv_CntReply"));
        addDetailMember(new DetailDataGroup(grv_Phone, pnlGridView2, lblPage2, btnDTL_INSERT2, btnDTL_UPDATE2, btnDTL_CLEAR2, "grv_Phone"));
        //addDetailMember(new DetailDataGroup(grv_Phone, pnlGridView2, lblPage2, grv_SearchCommittee, pnlQueryGridView2, lblQueryPage2, btnDTL_QUERY2, btnDTL_INSERT2, btnDTL_CLEAR2, "grv_Committee"));
        //addDetailMember(new DetailDataGroup(grv_Company, pnlGridView3, lblPage3, grv_SearchCompany, pnlQueryGridView3, lblQueryPage3, btnDTL_QUERY3, btnDTL_INSERT3, btnDTL_CLEAR3, "grv_Company"));
        //addDetailMember(new DetailDataGroup(grv_CntReply, pnlGridView, lblPage));
        //addDetailMember(new DetailDataGroup(grv_Phone, pnlGridView2, lblPage2));
        #endregion

        #region 宣告資訊
        BL = new Consulting_01BL();
        #endregion

        #region 按鈕定義 start

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊

        BackPage = "Consulting_Qry_01.aspx";

        #endregion

        #region 頁面檢查設定
        checkLoginType = checkLoginType.Need;
        #endregion
    }

    public override void SetDefault()
    {
        BaseFun bf = new BaseFun();
        //詢問方式
        //ddl_Cnst_CntWay.DataSource = bf.getSysCodeByKind("C", "R");
        //ddl_Cnst_CntWay.DataTextField = "Sys_CdText";
        //ddl_Cnst_CntWay.DataValueField = "Sys_CdCode";
        //ddl_Cnst_CntWay.DataBind();

        //回覆-詢問方式
        ddl_CtRepl_RpWay.DataSource = bf.getSysCodeByKind("C", "R");
        ddl_CtRepl_RpWay.DataTextField = "Sys_CdText";
        ddl_CtRepl_RpWay.DataValueField = "Sys_CdCode";
        ddl_CtRepl_RpWay.DataBind();
        //詢問類別
        ckl_CntClass_Code.DataSource = bf.getSysCodeByKind("C", "Y");
        ckl_CntClass_Code.DataTextField = "Sys_CdText";
        ckl_CntClass_Code.DataValueField = "Sys_CdCode";
        ckl_CntClass_Code.DataBind();
        //處理結果
        ddl_CtRepl_RpResult.DataSource = bf.getSysCodeByKind("C", "N");
        ddl_CtRepl_RpResult.DataTextField = "Sys_CdText";
        ddl_CtRepl_RpResult.DataValueField = "Sys_CdCode";
        ddl_CtRepl_RpResult.DataBind();
    }

    protected override void SetHandler()
    {
        base.SetHandler();

        //回覆內容
        grv_CntReply.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_CntReply_TemplateSelection);
        grv_CntReply.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_CntReply_TemplateDataModeSelection);

        //電話紀錄
        grv_Phone.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Phone_TemplateSelection);
        grv_Phone.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Phone_TemplateDataModeSelection);
    }

    void grv_CntReply_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_CntReply_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\CntReply_Lis_01.ascx";
    }

    void grv_Phone_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_Phone_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\PhRec_Lis_01.ascx";
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Cnst_Code");
    }

    public override void RenderMasterData(DataTO to)
    {
        lbl_Com_Name.Text = to.getValue("Com_Name").ToString();
        lbl_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
        lbl_Com_Boss.Text = to.getValue("Com_Boss").ToString();
        lbl_Com_CttTel.Text = to.getValue("Com_CttTel").ToString();
        lbl_Com_CttName.Text = to.getValue("Com_CttName").ToString();
        lbl_Com_CttMail.Text = to.getValue("Com_CttMail").ToString();
        lbl_twRec_Info.Text = to.getValue("twRec_Info").ToString();

        hid_Cnst_Code.Value = to.getValue("Cnst_Code").ToString();
        lbl_Cnst_CntWay.Text = new BaseFun().getSysCodeValue("C", "R", to.getValue("Cnst_CntWay").ToString());

        ckl_CntClass_Code.SelectedValue = to.getValue("CntClass_Code").ToString();
        txt_Cnst_CntText.Text = to.getValue("Cnst_CntText").ToString();

        //grv_CntReply.DataSource = ((Consulting_01BL)BL).getReplyList(hid_Cnst_Code.Value);
        //grv_CntReply.DataBind();

        //grv_Phone.DataSource = ((Consulting_01BL)BL).getPhoneRecList(hid_Cnst_Code.Value);
        //grv_Phone.DataBind();
    }

    protected override void AfterHandleUpdate()
    {
        base.AfterHandleUpdate();
        if (ddl_CtRepl_RpResult.SelectedValue == "D")
        {
            GoURL("\\CACI\\Forms\\Coach\\Coach_Qry_02.aspx");
        }
        else
        {
            DataTable dt = ((Consulting_01BL)BL).getReplyList(hid_Cnst_Code.Value);
            RenderDetailGridViewData("grv_CntReply", dt);
            up_Tab1.Update();
        }
    }
    
}