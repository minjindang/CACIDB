using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;

public partial class UserControl_cbl_Chkd_Code : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BaseFun bf = new BaseFun();

        DataTable ChOrderDt = bf.getSysCodeByKind("C", "K");

        //this.pnl_ChKd_Code.Controls.Clear();

        this.tab_ChKd_Code.Controls.Clear();
        for (int i = 0; i < ChOrderDt.Rows.Count; i++)
        {
            #region Old

            //DataTable ChKdDt = new DataTable();
            //DataTO ChKdTo = new DataTO();
            //CheckBoxList ChKdCbl = new CheckBoxList();
            //this.pnl_ChKd_Code.Controls.Add(new LiteralControl("<span>"));
            //this.pnl_ChKd_Code.Controls.Add(new LiteralControl(ChOrderDt.Rows[i]["Sys_CdText"].ToString()+":"));
            ////this.pnl_ChKd_Code.Controls.Add(new LiteralControl("</td>"));
            //ChKdTo.setValue("ChKd_Order", ChOrderDt.Rows[i]["Sys_CdCode"].ToString());

            //ChKdDt = bf.getTableData("CoachKind", ChKdTo);

            //ChKdCbl.DataSource = ChKdDt;
            //ChKdCbl.DataTextField = "ChKd_Name";
            //ChKdCbl.DataValueField = "ChKd_Code";
            //ChKdCbl.RepeatDirection = RepeatDirection.Horizontal;
            //ChKdCbl.RepeatColumns = 3;
            //ChKdCbl.DataBind();

            //this.pnl_ChKd_Code.Controls.Add(ChKdCbl);
            //this.pnl_ChKd_Code.Controls.Add(new LiteralControl("</span>"));
            //this.pnl_ChKd_Code.Controls.Add(new LiteralControl("<hr>"));

            #endregion

            DataTable ChKdDt = new DataTable();
            DataTO ChKdTo = new DataTO();
            CheckBoxList ChKdCbl = new CheckBoxList();

            ChKdTo.setValue("ChKd_Order", ChOrderDt.Rows[i]["Sys_CdCode"].ToString());

            TableRow row = new TableRow();

            TableCell cell = new TableCell();

            cell.Width = new Unit("80px");

            cell.HorizontalAlign = HorizontalAlign.Right;

            cell.Text = ChOrderDt.Rows[i]["Sys_CdText"].ToString() + ":";

            row.Cells.Add(cell);

            TableCell cell2 = new TableCell();

            ChKdDt = bf.getTableData("CoachKind", ChKdTo);

            ChKdCbl.DataSource = ChKdDt;
            ChKdCbl.DataTextField = "ChKd_Name";
            ChKdCbl.DataValueField = "ChKd_Code";
            ChKdCbl.RepeatDirection = RepeatDirection.Horizontal;

            if (ChOrderDt.Rows[i]["Sys_CdCode"].ToString() == "FA")
                ChKdCbl.RepeatColumns = 2;
            ChKdCbl.RepeatLayout = RepeatLayout.Flow;
            ChKdCbl.Width = new Unit("500px");
            ChKdCbl.DataBind();

            cell2.Controls.Add(ChKdCbl);

            row.Cells.Add(cell2);

            tab_ChKd_Code.Rows.Add(row);
        }
    }
    public string ChKd_Code_Items
    {
        get
        {
            this.hid_ChKd_Code.Value = "";

            foreach (TableRow row in tab_ChKd_Code.Rows)
            {
                foreach (Control c in row.Cells[1].Controls)
                {
                    if (c is CheckBoxList)
                    {
                        CheckBoxList ck = c as CheckBoxList;
                        foreach (ListItem li in ck.Items)
                        {
                            if (li.Selected)
                                this.hid_ChKd_Code.Value += (this.hid_ChKd_Code.Value != "" ? "," : "" ) + li.Value;
                        }
                    }
                }
            }

            return this.hid_ChKd_Code.Value;
        }

        set
        {
            this.hid_ChKd_Code.Value = value;
        }
    }
}