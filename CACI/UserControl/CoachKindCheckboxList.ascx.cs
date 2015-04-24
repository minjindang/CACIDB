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

public partial class UserControl_CoachKindCheckboxList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BaseFun bf = new BaseFun();

        DataTable ChOrderDt = bf.getSysCodeByKind("C", "K");

        //this.pnl_ChKd_Code.Controls.Clear();

            this.pnl_ChKd_Code.Controls.Clear();
            for (int i = 0; i < ChOrderDt.Rows.Count; i++)
            {
                DataTable ChKdDt = new DataTable();
                DataTO ChKdTo = new DataTO();
                CheckBoxList ChKdCbl = new CheckBoxList();

                this.pnl_ChKd_Code.Controls.Add(new LiteralControl(ChOrderDt.Rows[i]["Sys_CdText"].ToString()));
                ChKdTo.setValue("ChKd_Order", ChOrderDt.Rows[i]["Sys_CdCode"].ToString());

                ChKdDt = bf.getTableData("CoachKind", ChKdTo);

                ChKdCbl.DataSource = ChKdDt;
                ChKdCbl.DataTextField = "ChKd_Name";
                ChKdCbl.DataValueField = "ChKd_Code";
                ChKdCbl.RepeatDirection = RepeatDirection.Horizontal;

                ChKdCbl.DataBind();

                this.pnl_ChKd_Code.Controls.Add(ChKdCbl);
            }
        
    }
    public string ChKd_Code_Items
    {
        get
        {
            //this.hid_ChKd_Code.Value = this.pnl_ChKd_Code.Controls.Count.ToString();
            string selectedCnst_StatTerms = "-1";
            foreach (Control c in pnl_ChKd_Code.Controls)
            {   
                if(c is CheckBoxList){
                    CheckBoxList ck = c as CheckBoxList;
                    foreach (ListItem li in ck.Items)
                    {
                        if (li.Selected)
                            selectedCnst_StatTerms += "," + li.Value;
                    }
                }
            
            
            }
            return selectedCnst_StatTerms; 
        }
            
        set
        { 
            this.hid_ChKd_Code.Value = value; 
        
        }
    }

}