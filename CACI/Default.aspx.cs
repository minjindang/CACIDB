using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.UI;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[ICommonUI.Web_ID + Session.SessionID + "LoginUserTo"] == null)
        {
            UserDataTO adminTo = new BaseFun().getUserAcc("admin");
                
            Session[ICommonUI.Web_ID + Session.SessionID + "LoginUserTo"] = adminTo;

            DataTO to = new DataTO();

            to.setValue("User_Code", adminTo.User_Code);

            Session[ICommonUI.Web_ID + Session.SessionID + "Announcement_Lis_02"] = to;
        }
    }
}