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

public partial class UserControl_ral_Chkd_Code : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BaseFun bf = new BaseFun();

        DataTable ChOrderDt = bf.getSysCodeByKind("C", "K");
     
        this.pnl_ChKd_Code.Controls.Clear();
        for (int i = 0; i < ChOrderDt.Rows.Count; i++)
        {
            DataTable ChKdDt = new DataTable();
            DataTO ChKdTo = new DataTO();

            this.pnl_ChKd_Code.Controls.Add(new LiteralControl("<tr>"));
            this.pnl_ChKd_Code.Controls.Add(new LiteralControl("<td class='title_2c'>"));
            this.pnl_ChKd_Code.Controls.Add(new LiteralControl(ChOrderDt.Rows[i]["Sys_CdText"].ToString() + ":"));
            this.pnl_ChKd_Code.Controls.Add(new LiteralControl("</td>"));

            ChKdTo.setValue("ChKd_Order", ChOrderDt.Rows[i]["Sys_CdCode"].ToString());
            this.pnl_ChKd_Code.Controls.Add(new LiteralControl("<td class='text_more' colspan='3'>"));
            ChKdDt = bf.getTableData("CoachKind", ChKdTo);
            for (int j = 0; j < ChKdDt.Rows.Count; j++)
            {
                RadioButton ChKdrab = new RadioButton();
                ChKdrab.GroupName = "ChKd_CodeGroup";
                ChKdrab.Text = ChKdDt.Rows[j]["ChKd_Code"].ToString();
                ChKdrab.LabelAttributes.Add("style", "Display:none");

                if (this.hid_ChKd_Code.Value == ChKdDt.Rows[j]["ChKd_Code"].ToString())
                    ChKdrab.Checked = true;

                this.pnl_ChKd_Code.Controls.Add(ChKdrab);
                this.pnl_ChKd_Code.Controls.Add(new LiteralControl(ChKdDt.Rows[j]["ChKd_Name"].ToString()));
                this.pnl_ChKd_Code.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));
                if (j != 0 && j % 2 == 0)
                {
                    this.pnl_ChKd_Code.Controls.Add(new LiteralControl("<br />"));
                }
            }
            this.pnl_ChKd_Code.Controls.Add(new LiteralControl("</td>"));
            this.pnl_ChKd_Code.Controls.Add(new LiteralControl("</tr>"));
        }


    }
    public string ChKd_Code_Items
    {
        get
        {
            //this.hid_ChKd_Code.Value = "-1";
            foreach (Control c in pnl_ChKd_Code.Controls)
            {
                if (c is RadioButton)
                {
                    RadioButton rb = c as RadioButton;

                        if (rb.Checked)
                            this.hid_ChKd_Code.Value =  rb.Text;
                    
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