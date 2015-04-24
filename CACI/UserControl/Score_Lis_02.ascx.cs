using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.TO;

public partial class Score_Lis_02 : System.Web.UI.UserControl
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
        this.DataBinding += new EventHandler(Score_Lis_01_DataBinding);
    }

    void Score_Lis_01_DataBinding(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)this.BindingContainer;
        if (!(row.DataItem is DataKey))
            throw new ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the DataGridView declaration");

        DataKey key = (DataKey)row.DataItem;

        // TODO:取得資料並顯示

        DataTO scoreTo = new ProjectBase().getScoreData(key[0].ToString());

        lbl_Score_Items.Text = scoreTo.getValue("Score_Items").ToString();
        lbl_Score_Max.Text = scoreTo.getValue("Score_Max").ToString();
        lbl_Score_Percent.Text = scoreTo.getValue("Score_Percent").ToString();
    }

    #endregion
}