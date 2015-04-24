using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.TO;

public partial class SmpStage_Lis_02 : System.Web.UI.UserControl
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
        this.DataBinding += new EventHandler(SmpStage_Lis_01_DataBinding);
    }

    void SmpStage_Lis_01_DataBinding(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)this.BindingContainer;
        if (!(row.DataItem is DataKey))
            throw new ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the DataGridView declaration");

        DataKey key = (DataKey)row.DataItem;

        // TODO:取得資料並顯示

        DataTO smpStageTo = new PjSamples_02BL().getSmpStageData(key[0].ToString(),int.Parse(key[1].ToString()));

        lbl_SpStage_Name.Text = smpStageTo.getValue("SpStage_Name").ToString();
        lbl_SpStage_Index.Text = smpStageTo.getValue("SpStage_Index").ToString();

        switch (smpStageTo.getValue("SpStage_Kind").ToString())
        {
            case "1" :
                rad_SpStage_Kind_1.Checked = true;
                break;
            case "2" :
                rad_SpStage_Kind_2.Checked = true;
                lbl_SpStage_Days.Text = smpStageTo.getValue("SpStage_Days").ToString();
                break;
            case "3" :
                rad_SpStage_Kind_3.Checked = true;
                break;
            case "4" :
                rad_SpStage_Kind_4.Checked = true;
                break;
        }

        lbl_SpStage_Text.Text = smpStageTo.getValue("SpStage_Text").ToString();

        lbl_SpStage_RmFlag.Text = smpStageTo.getValue("SpStage_RmFlag").ToString() == "Y" ? "是" : "否";

        //提醒人員
        string[] codeMean = { "申請單位", "承辦人", "顧問人員" };
        char[] RmEmpItems = smpStageTo.getValue("SpStage_RmEmpl").ToString().ToCharArray();
        string RmEmpResult = string.Empty;
        for (int i = 0; i < RmEmpItems.Length; i++)
        {
            if (RmEmpItems[i] == '1')
                RmEmpResult += codeMean[i] + "、";
        }
        if (!string.IsNullOrEmpty(RmEmpResult))
            RmEmpResult = RmEmpResult.Substring(0, RmEmpResult.Length - 1);
        else
            RmEmpResult = "無";
        lbl_SpStage_RmEmpl.Text = RmEmpResult;
        lbl_SpStage_RmDays.Text = smpStageTo.getValue("SpStage_RmDays").ToString();

        lbl_SpStage_RmText.Text = smpStageTo.getValue("SpStage_RmText").ToString();
    }

    #endregion
}