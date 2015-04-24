using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class SysTop : ICommonUI
{
    public override void SetDefault()
    {
        if (getLoginUser() != null)
        {
            UserDataTO userTo = getLoginUser();

            lbl_Unit_Name.Text = new BaseFun().getDepName(userTo.UsDp_Code);
            lbl_User_Name.Text = userTo.User_Name;
            lbl_Permission.Text = (userTo.User_Level == "S" ? "系統管理者" : "一般使用者");
        }
    }

    public override void SetProp()
    {
        BL = new SysTopBL();
        ProgNum = "0.0.0";
        ProgCd = this.GetType().BaseType.Name;
        ProgType = PAGE_ACTION.PAGE_LIST;

        ProgLabel = lbl_Prog;//標題（程式代號+程式名稱）
        _MessageLabel = lbl_Msg;

        checkLoginType = com.kangdainfo.online.WebBase.UI.checkLoginType.None;
    }
}