using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.TO;

public partial class CoachStage_Upd_02 : System.Web.UI.UserControl
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
        this.DataBinding += new EventHandler(CoachStage_Upd_02_DataBinding);
    }

    void CoachStage_Upd_02_DataBinding(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)this.BindingContainer;
        if (!(row.DataItem is DataKey))
            throw new ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the DataGridView declaration");

        DataKey key = (DataKey)row.DataItem;

        // TODO:取得資料並顯示

        DataTO to = new DataTO();
        to.setValue("Coach_Code", key[0].ToString());
        to.setValue("Meeting_Code", key[1].ToString());
        to.setValue("Meeting_Index", key[2].ToString());
        to.setValue("Comm_Code", key[3].ToString());
        DataList1.DataSource = new AowStage_01BL().getEvaluateScore(to);
        DataList1.DataBind();
    }

    #endregion

}