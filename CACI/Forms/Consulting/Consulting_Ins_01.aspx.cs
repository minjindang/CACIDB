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
using System.IO;
using System.Data;

public partial class Consulting_Ins_01 : IInsertUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        //單位(公司)基本資料

        //單位(公司)編號
        to.setValue("Com_Code", hid_Com_Code.Value);

        //單位(公司)名稱
        to.setValue("Com_Name", txt_Com_Name.Text);
        
        //稅編/身份證字號/立案號
        to.setValue("Com_Tonum", txt_Com_Tonum.Text);
        
        //單位(公司)負責人
        to.setValue("Com_Boss", txt_Com_Boss.Text);
        
        //成立時間
        to.setValue("Com_SetupTime", txt_Com_SetupTime.Text);
        
        //資本額
        to.setValue("Com_Capital", txt_Com_Capital.Text);
        
        //員工數
        to.setValue("Com_EmpNum", txt_Com_EmpNum.Text);
        
        //最近一年營業額
        to.setValue("Com_LTover", txt_Com_LTover.Text);
        
        //產業類別
        to.setValue("Com_MnSectors", ddl_Com_MnSectors.Text);
        
        //營運模式
        to.setValue("Com_OPMode", txt_Com_OPMode.Text);
        
        //公司發展現況
        to.setValue("Com_OPStatus", txt_Com_OPStatus.Text);
        
        //主要產品及服務
        to.setValue("Com_MnProduct", txt_Com_MnProduct.Text);
        
        //單位(公司)電話
        to.setValue("Com_Tel", txt_Com_Tel.Text);
        
        //單位(公司)傳真
        to.setValue("Com_Fax", txt_Com_Fax.Text);
        
        //單位(公司)地址
        to.setValue("Com_OPAddr", txt_Com_OPAddr.Text);
        
        //單位(公司)網址
        to.setValue("Com_Url", txt_Com_Url.Text);
        
        //單位(公司)e-mail
        to.setValue("Com_Email", txt_Com_Email.Text);
        
        //聯絡人姓名
        to.setValue("Com_CttName", txt_Com_CttName.Text);
        
        //聯絡人公司電話
        to.setValue("Com_CttTel", txt_Com_CttTel.Text);
        
        //聯絡人e-mail
        to.setValue("Com_CttMail", txt_Com_CttMail.Text);
        
        //諮詢內容
        
        //詢問方式
        to.setValue("Cnst_Code", hid_Cnst_Code.Value);
        
        //詢問方式
        to.setValue("Cnst_CntWay", ddl_Cnst_CntWay.Text);
        
        //詢問類別
        to.setValue("CntClass_Code", ckl_CntClass_Code.Text);
        
        //詢問內容
        to.setValue("Cnst_CntText", txt_Cnst_CntText.Text);
        
        //案件狀態
        to.setValue("Cnst_Status", ddl_Cnst_Status.SelectedValue);
        
        //諮詢回覆

        //時間
        to.setValue("CtRepl_Date", txt_CtRepl_Date.Text);
        
        //回覆方式
        if(!string.IsNullOrEmpty(ddl_CtRepl_RpWay.SelectedValue))
            to.setValue("CtRepl_RpWay", ddl_CtRepl_RpWay.SelectedValue);
        
        //回覆內容
        to.setValue("CtRepl_RpText", txt_CtRepl_RpText.Text);
        
        //處理結果
        to.setValue("CtRepl_RpResult", ddl_CtRepl_RpResult.Text);
        //諮詢時間
        to.setValue("Cnst_CntDate", "\\getDate()");
        //人員代號
        to.setValue("User_Code", getLoginUser().User_Code);
        return to;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "諮詢作業─內網資料新增畫面";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "6.1.1";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//訊息列
        #endregion

        #region 宣告資訊

        BL = new Consulting_01BL();//邏輯層

        #endregion

        #region 按鈕定義

        InsertButton = btn_Insert;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊

        BackPage = "Consulting_Qry_01.aspx";

        #endregion 導向頁面資訊

        #region 頁面檢查設定

        //checkLoginType = checkLoginType.Need;
        checkLoginType = checkLoginType.Need;
        
        #endregion
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        hid_Com_Code.Value = "N";
        hid_Cnst_Code.Value = "N";
        BaseFun bf = new BaseFun();
        //產業類別
        ddl_Com_MnSectors.DataSource = bf.getSysCodeByKind("I", "D");
        ddl_Com_MnSectors.DataTextField = "Sys_CdText";
        ddl_Com_MnSectors.DataValueField = "Sys_CdCode";
        ddl_Com_MnSectors.DataBind();
        //詢問方式
        ddl_Cnst_Status.DataSource = bf.getSysCodeByKind("N", "S");
        ddl_Cnst_Status.DataTextField = "Sys_CdText";
        ddl_Cnst_Status.DataValueField = "Sys_CdCode";
        ddl_Cnst_Status.DataBind();
        //詢問方式
        ddl_Cnst_CntWay.DataSource = bf.getSysCodeByKind("C", "I");
        ddl_Cnst_CntWay.DataTextField = "Sys_CdText";
        ddl_Cnst_CntWay.DataValueField = "Sys_CdCode";
        ddl_Cnst_CntWay.DataBind();
        //回覆-詢問方式
        ddl_CtRepl_RpWay.DataSource = bf.getSysCodeByKind("C", "R");
        ddl_CtRepl_RpWay.DataTextField = "Sys_CdText";
        ddl_CtRepl_RpWay.DataValueField = "Sys_CdCode";
        ddl_CtRepl_RpWay.DataBind();
        //詢問類別
        ckl_CntClass_Code.DataSource = bf.getSysCodeByKind("C", "Y");
        ckl_CntClass_Code.DataTextField = "Sys_CdText";
        ckl_CntClass_Code.DataValueField = "Sys_CdCode";
        ckl_CntClass_Code.DataBind();
        //處理結果
        ddl_CtRepl_RpResult.DataSource = bf.getSysCodeByKind("C", "N");
        ddl_CtRepl_RpResult.DataTextField = "Sys_CdText";
        ddl_CtRepl_RpResult.DataValueField = "Sys_CdCode";
        ddl_CtRepl_RpResult.DataBind();
    }

    protected void afuBkFIle_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {

    }
    protected void txt_Com_Tonum_TextChanged(object sender, EventArgs e)
    {
        BaseFun bf = new BaseFun();
        //公司資料
        DataTable compData = bf.getCompany(txt_Com_Tonum.Text);

        if (compData.Rows.Count > 0)
        {
            System.Diagnostics.Debug.WriteLine(compData.Rows[0]["Com_Name"].ToString());
            hid_Com_Code.Value = compData.Rows[0]["Com_Code"].ToString();
            txt_Com_Name.Text = compData.Rows[0]["Com_Name"].ToString();
            txt_Com_Boss.Text = compData.Rows[0]["Com_Boss"].ToString();
            txt_Com_SetupTime.Text = compData.Rows[0]["Com_SetupTime"].ToString().Split(' ')[0];
            txt_Com_Capital.Text = compData.Rows[0]["Com_Capital"].ToString();
            txt_Com_EmpNum.Text = compData.Rows[0]["Com_EmpNum"].ToString();
            txt_Com_LTover.Text = compData.Rows[0]["Com_LTover"].ToString();
            ddl_Com_MnSectors.Text = compData.Rows[0]["Com_MnSectors"].ToString();
            txt_Com_OPMode.Text = compData.Rows[0]["Com_OPMode"].ToString();
            txt_Com_OPStatus.Text = compData.Rows[0]["Com_OPStatus"].ToString();
            txt_Com_MnProduct.Text = compData.Rows[0]["Com_MnProduct"].ToString();
            txt_Com_Tel.Text = compData.Rows[0]["Com_Tel"].ToString();
            txt_Com_Fax.Text = compData.Rows[0]["Com_Fax"].ToString();
            txt_Com_OPAddr.Text = compData.Rows[0]["Com_OPAddr"].ToString();
            txt_Com_Url.Text = compData.Rows[0]["Com_Url"].ToString();
            txt_Com_Email.Text = compData.Rows[0]["Com_Email"].ToString();
            txt_Com_CttName.Text = compData.Rows[0]["Com_CttName"].ToString();
            txt_Com_CttTel.Text = compData.Rows[0]["Com_CttTel"].ToString();
            txt_Com_CttMail.Text = compData.Rows[0]["Com_CttMail"].ToString();
        }
        else
        {
            hid_Com_Code.Value = "N";
        }       
    }
    protected override void AfterHandleInsert(DataTO to)
    {
        switch (to.getValue("CtRepl_RpResult").ToString())
        {
            case "D":
                Session[ICommonUI.Web_ID + Session.SessionID + "Coach_Qry_02"] = to;
                GoURL("\\CACI\\Forms\\Coach\\Coach_Qry_02.aspx");
                break;
            default:
                break;
        }
    }
    protected void ddl_Com_MnSectors_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Com_MnSectors.SelectedValue != "P")
        {
            txt_Com_MnOther.Enabled = false;
            txt_Com_MnOther.Text = "";
        }
        else
        {
            txt_Com_MnOther.Enabled = true;
        }

    }
}