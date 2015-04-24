using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.TO;

public partial class AowStage_Upd_01 : System.Web.UI.UserControl
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
        ddl_AwSg_Verify.DataSource = new BaseFun().getSysCodeByKind("S", "S");
        ddl_AwSg_Verify.DataTextField = "Sys_CdText";
        ddl_AwSg_Verify.DataValueField = "Sys_CdCode";
        ddl_AwSg_Verify.DataBind();
        this.DataBinding += new EventHandler(AowStage_Upd_01_DataBinding);
        grv_Committee.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Committee_TemplateDataModeSelection);
        grv_Committee.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Committee_TemplateSelection);

    }

    void AowStage_Upd_01_DataBinding(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)this.BindingContainer;
        if (!(row.DataItem is DataKey))
            throw new ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the DataGridView declaration");

        DataKey key = (DataKey)row.DataItem;

        // TODO:取得資料並顯示

        DataTO to = new AowStage_01BL().getAowStageData(key[0].ToString(), key[1].ToString(),Convert.ToInt32(key[2]));
        hf_Pj_Code.Value = key[0].ToString();
        hf_Aow_Code.Value = key[1].ToString();
        hf_Stage_Index.Value = key[2].ToString();
        //if(!string.IsNullOrEmpty(to.getValue("Stage_Date").ToString()))
            //lbl_Stage_Date.Text = AowStage_01BL.chgEnDateToChnDate(to.getValue("Stage_Date").ToString().Split(' ')[0]);
        lbl_Stage_Text.Text = to.getValue("Stage_Text").ToString();
        if (!string.IsNullOrEmpty(to.getValue("AwSg_Date").ToString()))
            txt_AwSg_Date.Text = AowStage_01BL.chgEnDateToChnDate(to.getValue("AwSg_Date").ToString().Split(' ')[0]);
        txt_AwSg_Text.Text = to.getValue("AwSg_Text").ToString();
        if (!string.IsNullOrEmpty(to.getValue("AwSg_Verify").ToString()))
            ddl_AwSg_Verify.SelectedValue = to.getValue("AwSg_Verify").ToString();
        if (!string.IsNullOrEmpty(to.getValue("Rec_Info").ToString()))
            hf_IsNew.Value = "N";
        //Meeting
        if (!string.IsNullOrEmpty(to.getValue("Meeting_Code").ToString()) && !string.IsNullOrEmpty(to.getValue("Meeting_Index").ToString()))
        {
            paMeetingInfo.Visible = true;
            DataTO ecTo = new DataTO();
            ecTo.setValue("Aow_Code", key[1].ToString());
            ecTo.setValue("Meeting_Code", to.getValue("Meeting_Code").ToString());
            ecTo.setValue("Meeting_Index", to.getValue("Meeting_Index").ToString());
            //this.Label1.Text = key[1].ToString() + ":" + to.getValue("Meeting_Code").ToString() + ":" + to.getValue("Meeting_Index").ToString();
            this.grv_Committee.DataSource = new AowStage_01BL().getEvaluateCommittee(ecTo);
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
        e.TemplateFilename = "\\CACI\\UserControl\\AowStage_Upd_02.ascx";
    }

    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTO to = new DataTO();
        to.setValue("Aow_Code", this.hf_Aow_Code.Value);
        to.setValue("Pj_Code", this.hf_Pj_Code.Value);
        to.setValue("Stage_Index", hf_Stage_Index.Value);
        to.setValue("AwSg_Date", Allowance_01BL.chgChnDateToEnDate(txt_AwSg_Date.Text));
        to.setValue("AwSg_Verify", ddl_AwSg_Verify.SelectedValue);
        to.setValue("AwSg_Text", txt_AwSg_Text.Text);
        if (hf_IsNew.Value == "N")
            new AowStage_01BL().updateAowStageData(to);
        else
        {
            new AowStage_01BL().insertAowStageData(to);
            hf_IsNew.Value = "N";
        }
    }
}