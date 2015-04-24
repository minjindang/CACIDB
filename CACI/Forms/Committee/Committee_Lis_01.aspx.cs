using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class Committee_Lis_01 : IMDListUI
{

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "顧問委員管理作業─內網資料查看畫面 ";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "3.1.2";
        ProgType = PAGE_ACTION.PAGE_LIST;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grvQuery;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        #endregion

        #region 宣告資訊

        BL = new Committee_01BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        BackButton = btn_Back;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        BackPage = "Committee_Qry_01.aspx";

        #endregion 導向頁面資訊

        #region 頁面檢查設定
        checkLoginType = checkLoginType.Need;
        #endregion
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        BaseFun bf = new BaseFun();
        //輔導方式
        ckl_Comm_CoachWay.Items.Clear();
        ckl_Comm_CoachWay.Items.Add("獎補助審查");
        ckl_Comm_CoachWay.Items.Add("輔導服務");
        ckl_Comm_CoachWay.Items.Add("顧問駐診服務");
        //輔導項目
        ckl_Comm_CoTerms.DataSource = bf.getSysCodeByKind("C", "K");
        ckl_Comm_CoTerms.DataTextField = "Sys_CdText";
        ckl_Comm_CoTerms.DataValueField = "Sys_CdCode";
        ckl_Comm_CoTerms.DataBind();
    }

    public override void RenderData(DataTO to)
    {
        lbl_Comm_Name.Text = to.getValue("Comm_Name").ToString();
        lbl_Comm_IDNO.Text = to.getValue("Comm_IDNO").ToString();
        lbl_Comm_Title.Text = to.getValue("Comm_Title").ToString();
        lbl_Comm_ComName.Text = to.getValue("Comm_ComName").ToString();
        lbl_Comm_Tel.Text = to.getValue("Comm_Tel").ToString();
        lbl_Comm_Cell.Text = to.getValue("Comm_Cell").ToString();
        lbl_Comm_Mail.Text = to.getValue("Comm_Mail").ToString();
        lbl_Comm_Account.Text = to.getValue("Comm_Account").ToString();
        lbl_Comm_Pass.Attributes.Add("value", to.getValue("Comm_Pass").ToString());
        lbl_Comm_Bank_Num.Text = to.getValue("Comm_Bank_Num").ToString();
        lbl_Comm_Bank_Name.Text = to.getValue("Bank_Name").ToString();
        lbl_Comm_Bankno.Text = to.getValue("Comm_Bankno").ToString();
        lbl_Comm_BkName.Text = to.getValue("Comm_BkName").ToString();

        if (to.getValue("Comm_CoachWay").ToString() != "")
        {
            char[] comm_CoachWay = to.getValue("Comm_CoachWay").ToString().ToCharArray();
            for (int i = 0; i < comm_CoachWay.Length; i++)
            {
                if (comm_CoachWay[i] == '1')
                    ckl_Comm_CoachWay.Items[i].Selected = true;
            }
        }
        if (to.getValue("Comm_CoTerms").ToString() != "")
        {
            foreach (ListItem li in ckl_Comm_CoTerms.Items)
            {
                if (to.getValue("Comm_CoTerms").ToString().IndexOf(li.Value) != -1)
                    li.Selected = true;
            }
        }
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Comm_Code");
    }

    
}