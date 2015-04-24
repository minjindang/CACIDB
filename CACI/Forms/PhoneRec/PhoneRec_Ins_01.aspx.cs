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

public partial class PhoneRec_Ins_01 : IInsertUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        to.setValue("PhRec_ComCode", hid_PhRec_ComCode.Value);
        to.setValue("PhRec_ComName", txt_PhRec_ComName.Text);
        //to.setValue("PhRec_Tonum", txt_PhRec_Tonum.Text);
        to.setValue("PhRec_CtName", txt_PhRec_CtName.Text);
        to.setValue("PhRec_CtTel", txt_PhRec_CtTel.Text);
        to.setValue("PhRec_CtMail", txt_PhRec_CtMail.Text);

        to.setValue("CntClass_Code", ddl_CntClass_Code.SelectedValue);
        to.setValue("PhRec_Question", txt_PhRec_Question.Text);

        to.setValue("PRcRp_Date", PhoneRec_01BL.chgChnDateToEnDate(txt_PRcRp_Date.Text));
        to.setValue("PRcRp_Text", txt_PRcRp_Text.Text);
        to.setValue("PRcRp_Handle", ddl_PRcRp_Handle.SelectedValue);

        return to;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "電話記錄作業─資料新增";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "4.1.3";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//訊息列
        #endregion

        #region 宣告資訊

        BL = new PhoneRec_01BL();//邏輯層

        #endregion

        #region 按鈕定義

        InsertButton = btn_Insert;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊

        BackPage = "PhoneRec_Qry_01.aspx";

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
        hid_PhRec_ComCode.Value = "N";

        BaseFun bf = new BaseFun();

        ddl_CntClass_Code.DataSource = bf.getSysCodeByKind("C", "Y");
        ddl_CntClass_Code.DataTextField = "Sys_CdText";
        ddl_CntClass_Code.DataValueField = "Sys_CdCode";
        ddl_CntClass_Code.DataBind();

        ddl_PRcRp_Handle.DataSource = bf.getSysCodeByKind("C", "R");
        ddl_PRcRp_Handle.DataTextField = "Sys_CdText";
        ddl_PRcRp_Handle.DataValueField = "Sys_CdCode";
        ddl_PRcRp_Handle.DataBind();

    }

    protected void txt_PhRec_Tonum_TextChanged(object sender, EventArgs e)
    {
        BaseFun bf = new BaseFun();
        //公司資料
        DataTable compData = bf.getCompany(txt_PhRec_Tonum.Text);

        if (compData.Rows.Count > 0)
        {
            hid_PhRec_ComCode.Value = compData.Rows[0]["Com_Code"].ToString();
            txt_PhRec_ComName.Text = compData.Rows[0]["Com_Name"].ToString();
            txt_PhRec_CtName.Text = compData.Rows[0]["Com_CttName"].ToString();
            txt_PhRec_CtTel.Text = compData.Rows[0]["Com_CttTel"].ToString();
            txt_PhRec_CtMail.Text = compData.Rows[0]["Com_CttMail"].ToString();
        }
        else
        {
            hid_PhRec_ComCode.Value = "N";
        }
    }
}