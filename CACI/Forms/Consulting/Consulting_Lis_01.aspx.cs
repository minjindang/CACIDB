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

public partial class Consulting_Lis_01 : IMMDListUI
{

    /*
     * grv_Score
     * grv_SmpStage
     */

    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "諮詢作業─內網資料查看畫面";
        ProgNum = "6.1.2";
        ProgType = PAGE_ACTION.PAGE_LIST;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grv_CntReply, pnlGridView, lblPage));
        addDetailMember(new DetailDataGroup(grv_Phone, pnlGridView2, lblPage2));

        #endregion

        #region 宣告資訊
        BL = new Consulting_01BL();
        #endregion

        #region 按鈕定義 start

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
        //產業類別
        ddl_Com_MnSectors.DataSource = bf.getSysCodeByKind("I", "D");
        ddl_Com_MnSectors.DataTextField = "Sys_CdText";
        ddl_Com_MnSectors.DataValueField = "Sys_CdCode";
        ddl_Com_MnSectors.DataBind();
        //詢問方式
        ddl_Cnst_Status.DataSource = bf.getSysCodeByKind("N", "S");
        ddl_Cnst_Status.DataTextField = "Sys_CdText";
        ddl_Cnst_Status.DataValueField = "Sys_CdCode";
        ddl_Cnst_Status.DataBind();
        //詢問方式
        ddl_Cnst_CntWay.DataSource = bf.getSysCodeByKind("C", "R");
        ddl_Cnst_CntWay.DataTextField = "Sys_CdText";
        ddl_Cnst_CntWay.DataValueField = "Sys_CdCode";
        ddl_Cnst_CntWay.DataBind();
        //詢問類別
        ckl_CntClass_Code.DataSource = bf.getSysCodeByKind("C", "Y");
        ckl_CntClass_Code.DataTextField = "Sys_CdText";
        ckl_CntClass_Code.DataValueField = "Sys_CdCode";
        ckl_CntClass_Code.DataBind();

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

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Cnst_Code");
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


    public override void RenderData(DataTO to)
    {
        this.hid_Com_Code.Value = to.getValue("Com_Code").ToString();
        this.lbl_Com_Name.Text = to.getValue("Com_Name").ToString();
        this.lbl_Com_Boss.Text = to.getValue("Com_Boss").ToString();
        this.lbl_Com_SetupTime.Text = to.getValue("Com_SetupTime").ToString().Split(' ')[0];
        this.lbl_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
        this.lbl_Com_Capital.Text = to.getValue("Com_Capital").ToString();
        this.lbl_Com_EmpNum.Text = to.getValue("Com_EmpNum").ToString();
        this.lbl_Com_LTover.Text = to.getValue("Com_LTover").ToString();
        this.ddl_Com_MnSectors.SelectedValue = to.getValue("Com_MnSectors").ToString();
        this.lbl_Com_OPMode.Text = to.getValue("Com_OPMode").ToString();
        this.lbl_Com_OPStatus.Text = to.getValue("Com_OPStatus").ToString();
        this.lbl_Com_MnProduct.Text = to.getValue("Com_MnProduct").ToString();
        this.lbl_Com_Tel.Text = to.getValue("Com_Tel").ToString();
        this.lbl_Com_Fax.Text = to.getValue("Com_Fax").ToString();
        this.lbl_Com_OPAddr.Text = to.getValue("Com_OPAddr").ToString();
        this.lbl_Com_Url.Text = to.getValue("Com_Url").ToString();
        this.lbl_Com_Email.Text = to.getValue("Com_Email").ToString();
        this.lbl_Com_CttName.Text = to.getValue("Com_CttName").ToString();
        this.lbl_Com_CttTel.Text = to.getValue("Com_Name").ToString();
        this.lbl_Com_CttMail.Text = to.getValue("_Com_CttMail").ToString();
        this.hid_Cnst_Code.Value = to.getValue("_Cnst_Code").ToString();
        this.ddl_Cnst_CntWay.SelectedValue = to.getValue("Cnst_CntWay").ToString();
        this.ckl_CntClass_Code.Text = to.getValue("CntClass_Code").ToString();
        this.lbl_Cnst_CntText.Text = to.getValue("Cnst_CntText").ToString();
        this.ddl_Cnst_Status.SelectedValue = to.getValue("Cnst_Status").ToString();
        this.lbl_Com_CttTel.Text = to.getValue("Com_CttTel").ToString();
    }
}