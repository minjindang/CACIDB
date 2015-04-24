using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.TO;

public partial class PjStage_Lis_02 : System.Web.UI.UserControl
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
        this.DataBinding += new EventHandler(PjStage_Lis_01_DataBinding);
    }

    void PjStage_Lis_01_DataBinding(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)this.BindingContainer;
        if (!(row.DataItem is DataKey))
            throw new ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the DataGridView declaration");

        DataKey key = (DataKey)row.DataItem;

        // TODO:取得資料並顯示

        DataTO pjStageTo = new Project_01BL().getPjStageData(key[0].ToString(),int.Parse(key[1].ToString()));

        lbl_Stage_Name.Text = pjStageTo.getValue("Stage_Name").ToString();
        lbl_Stage_Index.Text = pjStageTo.getValue("Stage_Index").ToString();

        switch (pjStageTo.getValue("Stage_Kind").ToString())
        {
            case "1" :
                rad_Stage_Kind_1.Checked = true;
                break;
            case "2" :
                rad_Stage_Kind_2.Checked = true;
                lbl_Stage_Days.Text = pjStageTo.getValue("Stage_Days").ToString();
                break;
            case "3" :
                rad_Stage_Kind_3.Checked = true;
                break;
            case "4" :
                rad_Stage_Kind_4.Checked = true;
                break;
        }
        if(!string.IsNullOrEmpty(pjStageTo.getValue("Stage_Date").ToString()))
            lbl_Stage_Date.Text = Project_01BL.chgEnDateToChnDate(pjStageTo.getValue("Stage_Date").ToString());
        lbl_Stage_Text.Text = pjStageTo.getValue("Stage_Text").ToString();

        lbl_Stage_IsMeeting.Text = pjStageTo.getValue("Stage_IsMeeting").ToString() == "Y" ? "是" : "否";
        BaseFun bf = new BaseFun();
        //會議性質
        lbl_Stage_MtKind.Text = bf.getMeetingTypeName(pjStageTo.getValue("Stage_MtKind").ToString());

        lbl_Stage_RmFlag.Text = pjStageTo.getValue("Stage_RmFlag").ToString() == "Y" ? "是" : "否";
        //提醒人員
        lbl_Stage_RmEmpl.Text = bf.getRmEmpl(pjStageTo.getValue("Stage_RmEmpl").ToString());

        lbl_Stage_RmDays.Text = pjStageTo.getValue("Stage_RmDays").ToString();

        lbl_Stage_RmText.Text = pjStageTo.getValue("Stage_RmText").ToString();
    }

    #endregion
}