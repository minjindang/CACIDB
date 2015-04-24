using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.DB;

public partial class SM1003L : IMMDListUI
{
    public override void RenderData(DataTO to)
    {
        lbl_Mcol_1.Text = to.getValue("Mcol_1").ToString();
        txt_Mcol_2.Text = to.getValue("Mcol_2").ToString();
        txt_Mcol_3.Text = to.getValue("Mcol_3").ToString();
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Mcol_1");
    }

    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "Master-Detail-Detail修改作業範本";
        
        ProgNum = "0.3";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grvQuery, pnlGridView, lblPage));
        addDetailMember(new DetailDataGroup(grvQuery2, pnlGridView2, lblPage2));

        #endregion

        #region 宣告資訊
        BL = new SM1003BL();
        #endregion

        #region 按鈕定義
        BackButton = btn_Back;
        #endregion

        #region 導向頁面資訊
        BackPage = "SM1003Q.aspx";
        #endregion

        #region 頁面檢查設定
        checkLoginType = checkLoginType.None;
        #endregion
    }

    public override void SetDefault()
    {
        lbl_Mcol_1.Text = "";
        txt_Mcol_2.Text = "";
        txt_Mcol_3.Text = "";
    }
}