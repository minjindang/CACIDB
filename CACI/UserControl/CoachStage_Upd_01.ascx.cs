using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.TO;

public partial class CoachStage_Upd_01 : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    #region Web Form Designer generated code

    protected override void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);
    }

    private void InitializeComponent()
    {
        ddl_ChSg_Verify.DataSource = new BaseFun().getSysCodeByKind("O", "S");
        ddl_ChSg_Verify.DataTextField = "Sys_CdText";
        ddl_ChSg_Verify.DataValueField = "Sys_CdCode";
        ddl_ChSg_Verify.DataBind();
        this.DataBinding += new EventHandler(CoachStage_Upd_01_DataBinding);
        grv_Committee.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Committee_TemplateDataModeSelection);
        grv_Committee.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Committee_TemplateSelection);

    }

    void CoachStage_Upd_01_DataBinding(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)this.BindingContainer;
        if (!(row.DataItem is DataKey))
            throw new ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the DataGridView declaration");

        DataKey key = (DataKey)row.DataItem;

        // TODO:取得資料並顯示
        CoachStage_01BL BL = new CoachStage_01BL();

        DataTO to = new CoachStage_01BL().getCoachStageData(key[0].ToString(), key[1].ToString(), Convert.ToInt32(key[2]));
        hf_Pj_Code.Value = key[0].ToString();
        hf_Coach_Code.Value = key[1].ToString();
        hf_Stage_Index.Value = key[2].ToString();
        if (!string.IsNullOrEmpty(to.getValue("CoachStage_Date").ToString()))
            lbl_CoachStage_Date.Text = to.getValue("CoachStage_Date").ToString();
        lbl_Stage_Text.Text = to.getValue("Stage_Text").ToString();
        if (!string.IsNullOrEmpty(to.getValue("ChSg_Date").ToString()))
            txt_ChSg_Date.Text = CoachStage_01BL.chgEnDateToChnDate(to.getValue("ChSg_Date").ToString().Split(' ')[0]);
        txt_ChSg_Text.Text = to.getValue("ChSg_Text").ToString();
        if (!string.IsNullOrEmpty(to.getValue("ChSg_Verify").ToString()))
            ddl_ChSg_Verify.SelectedValue = to.getValue("ChSg_Verify").ToString();
        if (!string.IsNullOrEmpty(to.getValue("Rec_Info").ToString()))
            hf_IsNew.Value = "N";
        //Meeting
        if (!string.IsNullOrEmpty(to.getValue("Meeting_Code").ToString()) && !string.IsNullOrEmpty(to.getValue("Meeting_Index").ToString()))
        {
            paMeetingInfo.Visible = true;
            DataTO ecTo = new DataTO();
            ecTo.setValue("Coach_Code", key[1].ToString());
            ecTo.setValue("Meeting_Code", to.getValue("Meeting_Code").ToString());
            ecTo.setValue("Meeting_Index", to.getValue("Meeting_Index").ToString());
            //this.Label1.Text = key[1].ToString() + ":" + to.getValue("Meeting_Code").ToString() + ":" + to.getValue("Meeting_Index").ToString();
            this.grv_Committee.DataSource = new CoachStage_01BL().getEvaluateCommittee(ecTo);
            
            grv_Committee.DataBind();
        }
        else
            paMeetingInfo.Visible = false;
    }

    void grv_Committee_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_Committee_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\CoachStage_Upd_02.ascx";
    }

    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTO to = new DataTO();
        to.setValue("Coach_Code", this.hf_Coach_Code.Value);
        to.setValue("Pj_Code", this.hf_Pj_Code.Value);
        to.setValue("Stage_Index", hf_Stage_Index.Value);
        to.setValue("ChSg_Date", CoachStage_01BL.chgChnDateToEnDate(txt_ChSg_Date.Text));
        to.setValue("ChSg_Verify", ddl_ChSg_Verify.SelectedValue);
        to.setValue("ChSg_Text", txt_ChSg_Text.Text);

        UserDataTO session = (UserDataTO)this.Session["__WEB_TRN_" + Session.SessionID + "LoginUserTo"];
        to.setValue("Rec_InfoID", "\\'"+session.getValue("User_Code")+"'");
        to.setValue("Rec_Info", "\\getDate()");
        if(hf_IsNew.Value == "N")
            new CoachStage_01BL().updateCoachStageData(to);
        else
            new CoachStage_01BL().insertCoachStageData(to);
    }
}