﻿using System;
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

public partial class Allowance_Upd_04 : IMDUpdateUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();
        //獎補助資料表
        DataTO allowanceTo = new DataTO();
        allowanceTo.setValue("Aow_Code", lbl_Aow_Code.Text);
        allowanceTo.setValue("Aow_GPName", txt_Aow_FMIDNO.Text);
        allowanceTo.setValue("Aow_RegNum", txt_Aow_RegNum.Text);
        allowanceTo.setValue("Aow_FMan", txt_Aow_FMan.Text);
        allowanceTo.setValue("Aow_FMIDNO", txt_Aow_FMIDNO.Text);
        allowanceTo.setValue("Aow_PJPM", txt_Aow_PJPM.Text);
        allowanceTo.setValue("Aow_PMTel", txt_Aow_PMTel.Text);
        to.setValue("Allowance", allowanceTo);
        //單位(公司)基本資料
        DataTO comTo = new DataTO();
        comTo.setValue("Com_Code", lbl_Com_Code.Text);
        comTo.setValue("Com_Name", txt_Com_Name.Text);
        comTo.setValue("Com_Tonum", txt_Com_Tonum.Text);
        comTo.setValue("Com_Fax", this.txt_Com_Fax.Text);
        comTo.setValue("Com_BsTel", txt_Com_BsTel.Text);
        comTo.setValue("Com_OPAddr", txt_Com_OPAddr.Text);
        comTo.setValue("Com_Url", txt_Com_Url.Text);
        comTo.setValue("Com_MnSectors", rbl_Com_MnSectors.SelectedValue);
        comTo.setValue("Com_CttName", txt_Com_CttName.Text);
        comTo.setValue("Com_CttTel", txt_Com_CttTel.Text);
        comTo.setValue("Com_CttMail", txt_Com_CttMail.Text);
        to.setValue("Company", comTo);
        //計劃資料
        DataTO apPjTo = new DataTO();
        apPjTo.setValue("Aow_Code", lbl_Aow_Code.Text);
        apPjTo.setValue("ApPj_Name", txt_ApPj_Name.Text);
        apPjTo.setValue("ApPj_TotAmt", Convert.ToInt32(this.txt_ApPj_TotAmt.Text.Replace(",", string.Empty)) * 1000);
        apPjTo.setValue("ApPj_AowAmt", Convert.ToInt32(this.txt_ApPj_AowAmt.Text.Replace(",", string.Empty)) * 1000);
        apPjTo.setValue("ApPj_OthAmt", Convert.ToInt32(this.txt_ApPj_OthAmt.Text.Replace(",", string.Empty)) * 1000);
        apPjTo.setValue("ApPj_Goal", txt_ApPj_Goal.Text);
        apPjTo.setValue("ApPj_Policies", txt_ApPj_Policies.Text);
        apPjTo.setValue("ApPj_Profit", txt_ApPj_Profit.Text);
        apPjTo.setValue("ApPj_Solution", txt_ApPj_Solution.Text);
        to.setValue("ApPjContext", apPjTo);
        return to;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "獎補助作業─內縣市政府補助資料修改畫面";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "2.1.18";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grvQuery;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        ValidationDetailGroupID = "grvQuery";

        #endregion

        #region 宣告資訊

        BL = new Allowance_04BL();//邏輯層

        #endregion

        #region 按鈕定義

        InsertDetailButton = btnDTL_INSERT;
        UpdateDetailButton = btnDTL_UPDATE;
        ClearDetailButton = btnDTL_CLEAR;

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊

        BackPage = "Allowance_Qry_01.aspx";

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

        //總經費&申請補助靠右
        txt_ApPj_TotAmt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        txt_ApPj_OthAmt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        txt_ApPj_AowAmt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString(); 

        BaseFun bf = new BaseFun();
        //性別
        rbl_Com_BsGender.DataSource = bf.getSysCodeByKind("S", "E");
        rbl_Com_BsGender.DataTextField = "Sys_CdText";
        rbl_Com_BsGender.DataValueField = "Sys_CdCode";
        rbl_Com_BsGender.DataBind();
        //產業別:
        rbl_Com_MnSectors.DataSource = bf.getSysCodeByKind("I", "D");
        rbl_Com_MnSectors.DataTextField = "Sys_CdText";
        rbl_Com_MnSectors.DataValueField = "Sys_CdCode";
        rbl_Com_MnSectors.DataBind();
        rbl_Com_MnSectors.Items[0].Selected = true;
    }

    /// <summary>
    /// 設定GridView Button 事件動作
    /// </summary>
    /// <param name="strCmd"></param>
    /// <returns></returns>
    public override ROW_CMD_TYPE GetRowCommand(string strCmd)
    {
        ROW_CMD_TYPE type = ROW_CMD_TYPE.ROW_CMD_UNKNOWN;

        switch (strCmd)
        {
            case "select":
                type = ROW_CMD_TYPE.ROW_CMD_MODIFY;
                break;
            case "del":
                type = ROW_CMD_TYPE.ROW_CMD_DELETE;
                break;
            default:
                break;
        }

        return type;
    }

    /// <summary>
    /// 蒐集Detal編輯區資料
    /// </summary>
    /// <returns></returns>
    public override DataTO PopulateDetailData()
    {
        DataTO to = new DataTO();
        to.setValue("Aas_PjName", txt_Aas_PjName.Text);
        to.setValue("Com_Name", txt_Com_Name2.Text);
        to.setValue("Com_Tonum", txt_Com_Tonum2.Text);
        to.setValue("Com_BsGender", rbl_Com_BsGender.SelectedValue);
        to.setValue("Com_BsTel", txt_Com_BsTel.Text);
        to.setValue("Com_OPAddr", txt_Com_OPAddr2.Text);
        to.setValue("Com_Code", lbl_Com_Code2.Text);
        return to;
    }

    /// <summary>
    /// 將選取的Detail資料顯示於畫面
    /// </summary>
    /// <param name="to"></param>
    public override void RenderDetailData(DataTO to)
    {
        txt_Aas_PjName.Text = to.getValue("Aas_PjName").ToString();
        txt_Com_Name2.Text = to.getValue("Com_Name").ToString();
        txt_Com_Tonum2.Text = to.getValue("Com_Tonum").ToString();
        if(!string.IsNullOrEmpty(to.getValue("Com_BsGender").ToString()))
            rbl_Com_BsGender.SelectedValue = to.getValue("Com_BsGender").ToString();
        txt_Com_BsTel.Text = to.getValue("Com_BsTel").ToString();
        txt_Com_OPAddr2.Text = to.getValue("Com_OPAddr").ToString();
        lbl_Com_Code2.Text = to.getValue("Com_Code").ToString();
    }

    protected override void AfterHandleDetailUpdate()
    {
        base.AfterHandleDetailUpdate();
    }


    /// <summary>
    /// Detail 編輯區初始化
    /// </summary>
    public override void InitialDetail()
    {
        base.InitialDetail();
        lbl_IsExists.Text = "N";
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Aow_Code");
    }

    public override void RenderData(DataTO to)
    {
        //Primary Key
        this.lbl_Com_Code.Text = to.getValue("Com_Code").ToString();
        this.lbl_Aow_Code.Text = to.getValue("Aow_Code").ToString();
        //獎補助
        txt_Aow_GPName.Text = to.getValue("Aow_GPName").ToString();
        txt_Aow_RegNum.Text = to.getValue("Aow_RegNum").ToString();
        txt_Aow_FMan.Text = to.getValue("Aow_FMan").ToString();
        txt_Aow_FMIDNO.Text = to.getValue("Aow_FMIDNO").ToString();
        txt_Aow_PJPM.Text = to.getValue("Aow_PJPM").ToString();
        txt_Aow_PMTel.Text = to.getValue("Aow_PMTel").ToString();
        //單位(公司)基本資料
        txt_Com_Name.Text = to.getValue("Com_Name").ToString();
        txt_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
        //rbl_Com_BsGender.SelectedValue = to.getValue("Com_BsGender").ToString();
        //txt_Com_BsTel.Text = to.getValue("Com_BsTel").ToString();
        txt_Com_Fax.Text = to.getValue("Com_Fax").ToString();
        txt_Com_OPAddr.Text = to.getValue("Com_OPAddr").ToString();
        rbl_Com_MnSectors.SelectedValue = to.getValue("Com_MnSectors").ToString();
        txt_Com_CttName.Text = to.getValue("Com_CttName").ToString();
        txt_Com_CttTel.Text = to.getValue("Com_CttTel").ToString();
        txt_Com_Fax.Text = to.getValue("Com_Fax").ToString();
        txt_Com_CttMail.Text = to.getValue("Com_CttMail").ToString();
        txt_Com_Url.Text = to.getValue("Com_Url").ToString();
        txt_Com_Account.Text = to.getValue("Com_Account").ToString();
        txt_Com_Pass.Text = to.getValue("Com_Pass").ToString();
        //計劃資料
        txt_ApPj_Name.Text = to.getValue("ApPj_Name").ToString();
        txt_ApPj_Goal.Text = to.getValue("ApPj_Goal").ToString();
        txt_ApPj_Policies.Text = to.getValue("ApPj_Policies").ToString();
        txt_ApPj_Profit.Text = to.getValue("ApPj_Profit").ToString();
        txt_ApPj_Solution.Text = to.getValue("ApPj_Solution").ToString();
        txt_ApPj_TotAmt.Text = (Convert.ToInt32(to.getValue("ApPj_TotAmt").ToString()) / 1000).ToString();
        txt_ApPj_AowAmt.Text = (Convert.ToInt32(to.getValue("ApPj_AowAmt").ToString()) / 1000).ToString();
        txt_ApPj_OthAmt.Text = (Convert.ToInt32(to.getValue("ApPj_OthAmt").ToString()) / 1000).ToString();
    }
    protected void txt_Com_Tonum2_TextChanged(object sender, EventArgs e)
    {
        DataTO queryTo = new DataTO();
        queryTo.setValue("Com_Tonum", txt_Com_Tonum2.Text);
        DataTable dt = new DataTable();
        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Company", queryTo), dt);
        if (dt.Rows.Count > 0)
        {
            lbl_IsExists.Text = "Y";
            lbl_Com_Code2.Text = dt.Rows[0]["Com_Code"].ToString();
            lbl_Com_Account.Text = dt.Rows[0]["Com_Account"].ToString();
            lbl_Com_Pass.Text = dt.Rows[0]["Com_Pass"].ToString();
            txt_Com_Name.Text = dt.Rows[0]["Com_Name"].ToString();
            txt_Com_BsTel.Text = dt.Rows[0]["Com_BsTel"].ToString();
            txt_Com_OPAddr2.Text = dt.Rows[0]["Com_OPAddr"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["Com_MnSectors"].ToString()))
                rbl_Com_MnSectors.SelectedValue = dt.Rows[0]["Com_MnSectors"].ToString();
        }
        else
        {
            BaseFun bf = new BaseFun();
            lbl_Com_Account.Text = bf.generAccount(txt_Com_Tonum.Text);
            lbl_Com_Pass.Text = bf.generPassword();
        }
    }
}