using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.BL;

public partial class Coach_Qry_03 : System.Web.UI.UserControl
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
        this.DataBinding += new EventHandler(CoachStage_Lis_01_DataBinding);
    }

    void CoachStage_Lis_01_DataBinding(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)this.BindingContainer;
        if (!(row.DataItem is DataKey))
            throw new ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the DataGridView declaration");

        DataKey key = (DataKey)row.DataItem;

        // TODO:取得資料並顯示
        this.grv_Coach.DataSource = new Company_01BL().getCoachData(key[0].ToString(), key[1].ToString());
        grv_Coach.DataBind();

        this.grv_Phone.DataSource = new Company_01BL().getCoachPhoneData(key[0].ToString(), key[1].ToString());
        grv_Phone.DataBind();

    }

    #endregion
}