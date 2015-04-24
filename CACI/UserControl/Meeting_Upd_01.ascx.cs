using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.TO;
using System.Data;
using System.Data.SqlClient;
using com.kangdainfo.online.WebBase.DB;

public partial class Meeting_Upd_01 : System.Web.UI.UserControl
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
        this.DataBinding += new EventHandler(grvQuery_DataBinding);
        grvQuery.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grvQuery_TemplateDataModeSelection);
        grvQuery.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grvQuery_TemplateSelection);
    }

    void grvQuery_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grvQuery_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\Meeting_Upd_02.ascx";
    }

    void grvQuery_DataBinding(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)this.BindingContainer;
        if (!(row.DataItem is DataKey))
            throw new ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the DataGridView declaration");
        DataKey key = (DataKey)row.DataItem;
        //// TODO:取得資料並顯示
        string sqlStr = "SELECT DISTINCT * FROM " +
                        "(SELECT b.Com_Code ,b.Com_Name ,a.Meeting_Code,a.Meeting_Index " +
                        "FROM CACIDB..MtgCom a " +
                        "LEFT JOIN CACIDB..Company b " +
                        "ON a.Com_Code = b.Com_Code " +
                        "WHERE Meeting_Code = @Meeting_Code AND Meeting_Index = @Meeting_Index " +
                        ") x " +
                        "LEFT JOIN " +
                        "( " +
                        "SELECT a.Aow_Code AS Code ,a.Com_Code ,b.Pj_Kind ,b.Pj_Code FROM Allowance a " +
                        "LEFT JOIN Project b " +
                        "ON a.Pj_Code = b.Pj_Code " +
                        "LEFT JOIN Meeting c " +
                        "ON c.Pj_Code = b.Pj_Code " +
                        "WHERE c.Meeting_Code = @Meeting_Code " +
                        "UNION " +
                        "SELECT a.Coach_Code AS Code ,a.Com_Code ,b.Pj_Kind ,b.Pj_Code FROM  Coach a " +
                        "LEFT JOIN Project b " +
                        "ON a.Pj_Code = b.Pj_Code " +
                        "LEFT JOIN Meeting c " +
                        "ON c.Pj_Code = b.Pj_Code " +
                        "WHERE c.Meeting_Code = @Meeting_Code " +
                        ") y ON x.Com_Code = y.Com_Code  " ;
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@Meeting_Code", key[0].ToString());
        cmd.Parameters.AddWithValue("@Meeting_Index", key[1].ToString());
        DataTable dt = new DataTable();
        new SQLAgent(DataBase.CACIDB).select(cmd,dt);
        grvQuery.DataSource = dt;
        grvQuery.DataBind();
    }

    #endregion

    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[3].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[4].Style.Add(HtmlTextWriterStyle.Display, "none");
    }
}