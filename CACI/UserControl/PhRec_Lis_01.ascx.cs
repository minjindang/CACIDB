using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.TO;

public partial class PhRec_Lis_01 : System.Web.UI.UserControl
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
        this.DataBinding += new EventHandler(PhRec_Lis_01_DataBinding);
    }

    void PhRec_Lis_01_DataBinding(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)this.BindingContainer;
        if (!(row.DataItem is DataKey))
            throw new ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the DataGridView declaration");

        DataKey key = (DataKey)row.DataItem;

        // TODO:取得資料並顯示

        DataTO PhoneTo = new Consulting_01BL().getPhRecData(key[0].ToString(), key[1].ToString());

        lbl_PRcRp_Date.Text = PhoneTo.getValue("PRcRp_Date").ToString();
        lbl_PRcRp_Text.Text = PhoneTo.getValue("PRcRp_Text").ToString();

    }


    #endregion
}