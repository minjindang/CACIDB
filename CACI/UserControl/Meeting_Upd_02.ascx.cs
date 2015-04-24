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

public partial class Meeting_Upd_02 : System.Web.UI.UserControl
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
    }

    void grvQuery_DataBinding(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)this.BindingContainer;
        if (!(row.DataItem is DataKey))
            throw new ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the DataGridView declaration");

        DataKey key = (DataKey)row.DataItem;

        //// TODO:取得資料並顯示
        string sqlStr = string.Empty;
        if (row.Cells[2].Text == "A")//獎補助
        {
            sqlStr = "SELECT b.Comm_Code,b.Comm_Name ,e.Eval_Status ,e.Eval_Note,CASE  WHEN e.Eval_Status IS NULL THEN 'Y' ELSE 'N' END AS IsNew " +
                            "FROM CACIDB..PjJudge a " +
                            "LEFT JOIN CACIDB..Committee b " +
                            "ON a.Comm_Code = b.Comm_Code " +
                            "LEFT JOIN CACIDB..Meeting c " +
                            "ON a.Pj_Code = c.Pj_Code " +
                            "LEFT JOIN CACIDB..MtgTimes d " +
                            "ON c.Meeting_Code = d.Meeting_Code " +
                            "LEFT JOIN CACIDB..Evaluations e " +
                            "ON e.Meeting_Code = d.Meeting_Code AND e.Meeting_Index = e.Meeting_Index AND e.Comm_Code = b.Comm_Code " +
                            "WHERE a.CmGp_Code IN " +
                            "(" +
                            "SELECT b.ApPj_ApGroup FROM CACIDB..Allowance AS a " +
                            "LEFT JOIN CACIDB..ApPjContext AS b " +
                            "ON a.Aow_Code = b.Aow_Code " +
                            "WHERE a.Pj_Code = @Pj_Code AND Com_Code = @Com_Code " +
                            ") AND a.Pj_Code = @Pj_Code AND d.Meeting_Code = @Meeting_Code AND d.Meeting_Index = @Meeting_Index ";
            SqlCommand cmd = new SqlCommand(sqlStr);
            cmd.Parameters.AddWithValue("@Meeting_Code", key[0].ToString());
            cmd.Parameters.AddWithValue("@Meeting_Index", key[1].ToString());
            cmd.Parameters.AddWithValue("@Com_Code", key[2].ToString());
            cmd.Parameters.AddWithValue("@Pj_Code", row.Cells[3].Text);
            DataTable dt = new DataTable();
            new SQLAgent(DataBase.CACIDB).select(cmd, dt);
            grvQuery.DataSource = dt;
            grvQuery.DataBind();
        }
        else//輔導
        {
            sqlStr = "SELECT c.Comm_Code , c.Comm_Name ,d.Eval_Status ,d.Eval_Note , CASE  WHEN d.Eval_Status IS NULL THEN 'Y' ELSE 'N' END AS IsNew " +
                            "FROM CACIDB..MtgCom a " +
                            "LEFT JOIN CACIDB..MtgCrew b " +
                            "ON a.Comm_Code = b.Comm_Code AND a.Meeting_Code = b.Meeting_Code AND a.Meeting_Index = b.Meeting_Index " +
                            "LEFT JOIN CACIDB.dbo.Committee c " +
                            "ON a.Comm_Code = c.Comm_Code " +
                            "LEFT JOIN CACIDB.dbo.Evaluations d " +
                            "ON d.Com_Code= a.Com_Code AND d.Comm_Code = a.Comm_Code AND d.Meeting_Code = b.Meeting_Code AND d.Meeting_Index = b.Meeting_Index " +
                            "WHERE a.Comm_Code <> '' AND a.Meeting_Code = @Meeting_Code AND a.Meeting_Index = @Meeting_Index " +
                            "AND a.Com_Code = @Com_Code ";
            SqlCommand cmd = new SqlCommand(sqlStr);
            cmd.Parameters.AddWithValue("@Meeting_Code", key[0].ToString());
            cmd.Parameters.AddWithValue("@Meeting_Index", key[1].ToString());
            cmd.Parameters.AddWithValue("@Com_Code", key[2].ToString());
            DataTable dt = new DataTable();
            new SQLAgent(DataBase.CACIDB).select(cmd, dt);
            grvQuery.DataSource = dt;
            grvQuery.DataBind();
        }
       
    }

    #endregion

    protected void txt_Eval_Note_TextChanged(object sender, EventArgs e)
    {
        TextBox myself = (TextBox)sender;
        GridViewRow dr = (GridViewRow)myself.NamingContainer;
        DataKey key = (DataKey)dr.DataItem;
    }
    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddl_Eval_Status");
            ddl.DataSource = new BaseFun().getSysCodeByKind("S", "S");
            ddl.DataTextField = "Sys_CdText";
            ddl.DataValueField = "Sys_CdCode";
            ddl.DataBind();
            Label lbl = (Label)e.Row.FindControl("lbl_Eval_Status");
            if(!string.IsNullOrEmpty(lbl.Text))
                ddl.SelectedValue = lbl.Text;
        }
        e.Row.Cells[3].Style.Add(HtmlTextWriterStyle.Display, "none");
    }
}