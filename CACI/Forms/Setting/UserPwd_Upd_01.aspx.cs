using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class UserPwd_Upd_01 : IUpdateUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();
        
        to.setValue("Comm_Code", hid_Comm_Code.Value);
        
        to.setValue("Type", hid_Type.Value);
        
        to.setValue("Comm_AcStatus", ddl_Comm_AcStatus.SelectedValue);

        return to;
    }

    /// <summary>
    /// 檢核主識別項是否存在(需實作)
    /// </summary>
    /// <param name="to">資料傳輸物件, 應由上層頁面於跳頁時自動塞入Session</param>
    /// <returns>主識別項是否存在</returns>
    public override bool CheckPK(DataTO to)
    {
        return true;//to.isColumnExist("Comm_Code");
    }

    /// <summary>
    /// 將資料取出至畫面上
    /// </summary>
    /// <param name="to">傳輸物件</param>
    public override void RenderData(DataTO to)
    {
        if (to.getValue("Type").ToString() == "Committee")
        {
            hid_Comm_Code.Value = to.getValue("Comm_Code").ToString();
            hid_Type.Value = to.getValue("Type").ToString();
            lbl_Comm_Name.Text = to.getValue("Comm_Name").ToString();
            ddl_Comm_AcStatus.SelectedValue = to.getValue("Comm_AcStatus").ToString();
            hid_Email.Value = to.getValue("Comm_Mail").ToString();
            hid_Account.Value = to.getValue("Comm_Account").ToString();
            lbl_Account.Text = to.getValue("Comm_Account").ToString();
            lbl_Comm_ComName.Text = to.getValue("Comm_Name").ToString();
        }
        else if (to.getValue("Type").ToString() == "Company")
        {
            hid_Comm_Code.Value = to.getValue("Com_Code").ToString();
            hid_Type.Value = to.getValue("Type").ToString();
            lbl_Comm_Name.Text = to.getValue("Com_Name").ToString();
            ddl_Comm_AcStatus.SelectedValue = to.getValue("Com_AcStatus").ToString();
            hid_Email.Value = to.getValue("Com_Email").ToString();
            hid_Account.Value = to.getValue("Com_Account").ToString();
            lbl_Account.Text = to.getValue("Com_Account").ToString();
            lbl_Comm_ComName.Text = to.getValue("Com_Name").ToString();
        }
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "帳號重設作業─資料更新";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "9.1.3";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new UserPwd_01BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        BackPage = "UserPwd_Qry_01.aspx";

        #endregion 導向頁面資訊

        #region 頁面檢查設定
        checkLoginType = checkLoginType.Need;
        #endregion

        customMSG_SUCCESS = "狀態更新成功";
        customMSG_FAIL = "狀態更新失敗";
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        hid_Comm_Code.Value = "";
        hid_Type.Value = "";
        lbl_Comm_Name.Text = "";
    }

    /// <summary>
    /// 重新設定密碼
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_PwdReset_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string newPwd = new BaseFun().generPassword();
            
            ((UserPwd_01BL)BL).UpdatePassWord(hid_Comm_Code.Value, hid_Type.Value, newPwd);

            new BaseFun().SendEmail(new System.Net.Mail.MailAddress[] {new System.Net.Mail.MailAddress(hid_Email.Value,lbl_Comm_Name.Text)}, 
                "文化創意資料庫系統，密碼變更通知",
                "您登入系統的帳號為:" + hid_Account.Value + "，密碼為 : " + newPwd);

            ShowMsgBox(this, "密碼更新成功");
        }
        catch (Exception ex)
        {
            ShowMsgBox(this, "密碼更新失敗");
        }
    }
}