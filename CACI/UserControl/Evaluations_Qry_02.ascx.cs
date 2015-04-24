﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.BL;

public partial class Evaluations_Qry_02 : System.Web.UI.UserControl
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
        this.grv_Meeting.DataSource = new Company_01BL().get_EvaluationsData(key[0].ToString(), key[1].ToString(), key[2].ToString(), key[3].ToString());
        grv_Meeting.DataBind();
    }

    #endregion
}