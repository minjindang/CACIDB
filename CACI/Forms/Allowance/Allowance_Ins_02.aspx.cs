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

public partial class Allowance_Ins_02 : IMDInsertUI
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
        allowanceTo.setValue("Pj_Code", lbl_Pj_Code.Text);
        allowanceTo.setValue("Com_Code", lbl_Com_Code.Text);
        allowanceTo.setValue("Aow_RegNum", txt_Com_Tonum.Text);
        allowanceTo.setValue("Aow_PJPM", txt_Com_Boss.Text);
        allowanceTo.setValue("Aow_PMTel", txt_Com_BsTel.Text);
        to.setValue("Allowance", allowanceTo);
        //判別單位資料是否已存在
        to.setValue("IsExists", lbl_IsExists.Text);
        //單位(公司)基本資料
        DataTO comTo = new DataTO();
        comTo.setValue("Com_Code", this.lbl_Com_Code.Text);
        comTo.setValue("Com_Name", txt_Com_Name.Text);
        comTo.setValue("Com_Tonum", txt_Com_Tonum.Text);
        comTo.setValue("Com_Capital", txt_Com_Capital.Text);
        comTo.setValue("Com_LTover", txt_Com_LTover.Text);
        comTo.setValue("Com_OrgForm", ddl_Com_OrgForm.SelectedValue);
        comTo.setValue("Com_Fax", this.txt_Com_Fax.Text);
        comTo.setValue("Com_Boss", txt_Com_Boss.Text);
        comTo.setValue("Com_BsTel", txt_Com_BsTel.Text);
        comTo.setValue("Com_CttName", txt_Com_CttName.Text);
        comTo.setValue("Com_CttTel", txt_Com_CttTel.Text);
        comTo.setValue("Com_Email", txt_Com_Email.Text);
        comTo.setValue("Com_OPAddr", txt_Com_OPAddr.Text);
        comTo.setValue("Com_Url", txt_Com_Url.Text);
        comTo.setValue("Com_MnProduct", txt_Com_MnProduct.Text);
        comTo.setValue("Com_MnSectors", rbl_Com_MnSectors.SelectedValue);
        comTo.setValue("Com_Account", lbl_Com_Account.Text);
        comTo.setValue("Com_Pass", lbl_Com_Pass.Text);
        comTo.setValue("Com_PostCode", ddl_Com_PostCode.SelectedValue);
        to.setValue("Company", comTo);
        //計劃資料
        DataTO apPjTo = new DataTO();
        apPjTo.setValue("ApPj_Name", txt_ApPj_Name.Text);
        apPjTo.setValue("ApPj_BgnDate", Allowance_01BL.chgChnDateToEnDate(txt_ApPj_BgnDate.Text));
        apPjTo.setValue("ApPj_EndDate", Allowance_01BL.chgChnDateToEnDate(txt_ApPj_EndDate.Text));
        apPjTo.setValue("ApPj_ProdCls", ddl_ApPj_ProdCls.SelectedValue);
        apPjTo.setValue("ApPj_ApGroup", ddl_ApPj_ApGroup.SelectedValue);
        apPjTo.setValue("ApPj_TotAmt", Convert.ToInt32(this.txt_ApPj_TotAmt.Text.Replace(",", string.Empty)) * 1000);
        apPjTo.setValue("ApPj_OthAmt", Convert.ToInt32(this.txt_ApPj_OthAmt.Text.Replace(",", string.Empty)) * 1000);
        apPjTo.setValue("ApPj_AowAmt", Convert.ToInt32(this.txt_ApPj_AowAmt.Text.Replace(",", string.Empty)) * 1000);
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
        ProgNm = "獎補助作業─內網一般徵件計畫資料新增畫面";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "2.1.14";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grvQuery;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        ValidationDetailGroupID = "grvQuery";

        #endregion

        #region 宣告資訊

        BL = new Allowance_02BL();//邏輯層

        #endregion

        #region 按鈕定義

        InsertDetailButton = btnDTL_INSERT;
        UpdateDetailButton = btnDTL_UPDATE;
        ClearDetailButton = btnDTL_CLEAR;

        InsertButton = btn_Insert;
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
        //總經費&申請補助
        txt_ApPj_TotAmt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        txt_ApPj_OthAmt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        txt_ApPj_AowAmt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        //補助金額
        txt_Aas_Amount.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

        lbl_IsExists.Text = "N";
        BaseFun bf = new BaseFun();
        //組織形式
        ddl_Com_OrgForm.DataSource = bf.getSysCodeByKind("R", "T");
        ddl_Com_OrgForm.DataTextField = "Sys_CdText";
        ddl_Com_OrgForm.DataValueField = "Sys_CdCode";
        ddl_Com_OrgForm.DataBind();
        //產業別:
        rbl_Com_MnSectors.DataSource = bf.getSysCodeByKind("I", "D");
        rbl_Com_MnSectors.DataTextField = "Sys_CdText";
        rbl_Com_MnSectors.DataValueField = "Sys_CdCode";
        rbl_Com_MnSectors.DataBind();
        rbl_Com_MnSectors.Items[0].Selected = true;
        //商品類別
        ddl_ApPj_ProdCls.DataSource = bf.getSysCodeByKind("A", "P");
        ddl_ApPj_ProdCls.DataTextField = "Sys_CdText";
        ddl_ApPj_ProdCls.DataValueField = "Sys_CdCode";
        ddl_ApPj_ProdCls.DataBind();
        //公司登記所在地
        ddl_Com_PostCode.DataSource = bf.getTableData("PostCode");
        ddl_Com_PostCode.DataTextField = "AreaName";
        ddl_Com_PostCode.DataValueField = "Post_Code";
        ddl_Com_PostCode.DataBind();
        DataTO to = (DataTO)Session[ICommonUI.Web_ID + Session.SessionID + "Allowance_Ins_02"];
        if (to != null && to.getValue("Pj_Code").ToString() != "")
        {
            lbl_Pj_Code.Text = to.getValue("Pj_Code").ToString();
            DataTO queryTo = new DataTO();
            queryTo.setValue("Pj_Code", to.getValue("Pj_Code").ToString());
            //申請組別
            ddl_ApPj_ApGroup.DataSource = bf.getTableData("CommGroup", queryTo);
            ddl_ApPj_ApGroup.DataTextField = "CmGp_Name";
            ddl_ApPj_ApGroup.DataValueField = "CmGp_Num";
            ddl_ApPj_ApGroup.DataBind();
        }
        else if (to != null && to.getValue("IsPreview").ToString() == "Y")
        {
            btn_Insert.Visible = false;
            btn_Clear.Visible = false;
            BackPage = "Project_Ins_01.aspx";
        }
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
        //to.setValue("Aas_Code", "");
        to.setValue("Aas_PjName", txt_Aas_PjName.Text);
        to.setValue("Aas_PjUnit", txt_Aas_PjUnit.Text);
        to.setValue("Aas_Amount", txt_Aas_Amount.Text + "千元");
        to.setValue("Aas_Year", txt_Aas_Year.Text);
        return to;
    }

    /// <summary>
    /// 將選取的Detail資料顯示於畫面
    /// </summary>
    /// <param name="to"></param>
    public override void RenderDetailData(DataTO to)
    {
        txt_Aas_PjName.Text = to.getValue("Aas_PjName").ToString();
        txt_Aas_PjUnit.Text = to.getValue("Aas_PjUnit").ToString();
        txt_Aas_Amount.Text = to.getValue("Aas_Amount").ToString().Replace("千元", string.Empty);
        txt_Aas_Year.Text = to.getValue("Aas_Year").ToString();
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
    }


    protected void txt_Com_Tonum_TextChanged(object sender, EventArgs e)
    {
        DataTO queryTo = new DataTO();
        queryTo.setValue("Com_Tonum", txt_Com_Tonum.Text);
        DataTable dt = new DataTable();
        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Company", queryTo), dt);
        if (dt.Rows.Count > 0)
        {
            lbl_IsExists.Text = "Y";
            lbl_Com_Code.Text = dt.Rows[0]["Com_Code"].ToString();
            lbl_Com_Account.Text = dt.Rows[0]["Com_Account"].ToString();
            lbl_Com_Pass.Text = dt.Rows[0]["Com_Pass"].ToString();
            txt_Com_Name.Text = dt.Rows[0]["Com_Name"].ToString();
            txt_Com_Capital.Text = dt.Rows[0]["Com_Capital"].ToString();
            txt_Com_LTover.Text = dt.Rows[0]["Com_LTover"].ToString();
            if(!string.IsNullOrEmpty(dt.Rows[0]["Com_OrgForm"].ToString()))
                ddl_Com_OrgForm.SelectedValue = dt.Rows[0]["Com_OrgForm"].ToString();
            txt_Com_Fax.Text = dt.Rows[0]["Com_Fax"].ToString();
            txt_Com_Boss.Text = dt.Rows[0]["Com_Boss"].ToString();
            txt_Com_BsTel.Text = dt.Rows[0]["Com_BsTel"].ToString();
            txt_Com_CttName.Text = dt.Rows[0]["Com_CttName"].ToString();
            txt_Com_CttTel.Text = dt.Rows[0]["Com_CttTel"].ToString();
            txt_Com_Email.Text = dt.Rows[0]["Com_Email"].ToString();
            txt_Com_OPAddr.Text = dt.Rows[0]["Com_OPAddr"].ToString();
            txt_Com_Url.Text = dt.Rows[0]["Com_Url"].ToString();
            txt_Com_MnProduct.Text = dt.Rows[0]["Com_MnProduct"].ToString();
            ddl_Com_PostCode.SelectedValue = dt.Rows[0]["Com_PostCode"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["Com_MnSectors"].ToString()))
                rbl_Com_MnSectors.SelectedValue = dt.Rows[0]["Com_MnSectors"].ToString();
            up_Com_Name.Update();
            up_Com_Capital.Update();
            up_Com_LTover.Update();
            up_Com_OrgForm.Update();
            up_Com_Fax.Update();
            up_Com_Boss.Update();
            up_Com_BsTel.Update();
            up_Com_CttName.Update();
            up_Com_CttTel.Update();
            up_Com_Email.Update();
            up_Com_OPAddr.Update();
            up_Com_Url.Update();
            up_Com_MnProduct.Update();
            up_Com_MnSectors.Update();
            up_Com_PostCode.Update();
        }
        else
        {
            BaseFun bf = new BaseFun();
            lbl_Com_Account.Text = bf.generAccount(txt_Com_Tonum.Text);
            lbl_Com_Pass.Text = bf.generPassword();
        }
    }
}