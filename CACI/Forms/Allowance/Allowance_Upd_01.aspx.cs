using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;
using System.Data;

public partial class Allowance_Upd_01 : IMDUpdateUI
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
        allowanceTo.setValue("Aow_Code", this.lbl_Aow_Code.Text);
        allowanceTo.setValue("Com_Code", this.lbl_Com_Code.Text);
        if (!string.IsNullOrEmpty(txt_Aow_GPName.Text))//團體名稱
            allowanceTo.setValue("Aow_GPName", txt_Aow_GPName.Text);
        if (!string.IsNullOrEmpty(txt_Aow_RegNum.Text))//團體立案字號
            allowanceTo.setValue("Aow_RegNum", txt_Aow_RegNum.Text);
        if (!string.IsNullOrEmpty(txt_Aow_FMan.Text))//代表人
            allowanceTo.setValue("Aow_FMan", txt_Aow_FMan.Text);
        if (!string.IsNullOrEmpty(txt_Aow_FMIDNO.Text))//身分證字號
            allowanceTo.setValue("Aow_FMIDNO", txt_Aow_FMIDNO.Text);

        to.setValue("Allowance", allowanceTo);
        //判別單位資料是否已存在
        to.setValue("IsExists", lbl_IsExists.Text);
        //單位資料
        DataTO comTo = new DataTO();
        comTo.setValue("Com_Name", txt_Com_Name.Text);//申請單位
        comTo.setValue("Com_Tonum", txt_Com_Tonum.Text);//身分證字號
        comTo.setValue("Com_CttCell", txt_Com_CttCell.Text);//手機
        comTo.setValue("Com_BsGender", rbl_Com_BsGender.SelectedValue);//性別
        comTo.setValue("Com_BsTel", txt_Com_BsTel.Text);//電話(日)
        if (!string.IsNullOrEmpty(txt_Com_BsNightTel.Text))
            comTo.setValue("Com_BsNightTel", txt_Com_BsNightTel.Text);//電話(夜)
        comTo.setValue("Com_CttName", txt_Com_CttName.Text);//聯絡人
        if (!string.IsNullOrEmpty(txt_Com_CttTel.Text))
            comTo.setValue("Com_CttTel", txt_Com_CttTel.Text); //聯絡人電話
        if (!string.IsNullOrEmpty(ddl_Com_PostCode.SelectedValue))
            comTo.setValue("Com_PostCode", ddl_Com_PostCode.SelectedValue);//公司登記所在地
        if (!string.IsNullOrEmpty(txt_Com_Fax.Text))
            comTo.setValue("Com_Fax", this.txt_Com_Fax.Text);//傳真
        comTo.setValue("Com_Email", txt_Com_Email.Text);//E-MAIL
        comTo.setValue("Com_OPAddr", txt_Com_OPAddr.Text);//地址
        comTo.setValue("Com_Url", txt_Com_Url.Text);//網址
        comTo.setValue("Com_Code", this.lbl_Com_Code.Text);
        comTo.setValue("Com_Account", lbl_Com_Account.Text);
        comTo.setValue("Com_Pass", lbl_Com_Pass.Text);
        to.setValue("Company", comTo);
        //計劃資料
        DataTO apPjTo = new DataTO();
        apPjTo.setValue("ApPj_Name", txt_ApPj_Name.Text);
        apPjTo.setValue("Aow_Code", this.lbl_Aow_Code.Text);
        apPjTo.setValue("ApPj_BgnDate", Allowance_01BL.chgChnDateToEnDate(txt_ApPj_BgnDate.Text));
        apPjTo.setValue("ApPj_EndDate", Allowance_01BL.chgChnDateToEnDate(txt_ApPj_EndDate.Text));
        apPjTo.setValue("ApPj_Msectors", rbl_ApPj_Msectors.SelectedValue);
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
        ProgNm = "獎補助作業─內網創業圓夢計畫資料修改畫面 ";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "2.1.4";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grvQuery;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        InsertDetailButton = btnDTL_INSERT;
        UpdateDetailButton = btnDTL_UPDATE;
        ClearDetailButton = btnDTL_CLEAR;

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        ValidationDetailGroupID = "grvQuery";

        #endregion

        #region 宣告資訊

        BL = new Allowance_01BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion 按鈕定義 end

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
        //總經費&申請補助(三位隔開)
        txt_ApPj_TotAmt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        txt_ApPj_OthAmt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        txt_ApPj_AowAmt.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

        lbl_IsExists.Text = "N";
        BaseFun bf = new BaseFun();

        //負責人性別
        rbl_Com_BsGender.DataSource = bf.getSysCodeByKind("S", "E");
        rbl_Com_BsGender.DataTextField = "Sys_CdText";
        rbl_Com_BsGender.DataValueField = "Sys_CdCode";
        rbl_Com_BsGender.DataBind();
        //其他團隊成員資料(性別)
        rbl_Com_BsGender2.DataSource = bf.getSysCodeByKind("S", "E");
        rbl_Com_BsGender2.DataTextField = "Sys_CdText";
        rbl_Com_BsGender2.DataValueField = "Sys_CdCode";
        rbl_Com_BsGender2.DataBind();
        //產業別:
        rbl_ApPj_Msectors.DataSource = bf.getSysCodeByKind("I", "D");
        rbl_ApPj_Msectors.DataTextField = "Sys_CdText";
        rbl_ApPj_Msectors.DataValueField = "Sys_CdCode";
        rbl_ApPj_Msectors.DataBind();
        rbl_ApPj_Msectors.Items[0].Selected = true;
      
        //商品類別
        //ddl_ApPj_ProdCls.DataSource = bf.getSysCodeByKind("A", "P");
        //ddl_ApPj_ProdCls.DataTextField = "Sys_CdText";
        //ddl_ApPj_ProdCls.DataValueField = "Sys_CdCode";
        //ddl_ApPj_ProdCls.DataBind();
        //公司登記所在地
        ddl_Com_PostCode.DataSource = bf.getTableData("PostCode");
        ddl_Com_PostCode.DataTextField = "AreaName";
        ddl_Com_PostCode.DataValueField = "Post_Code";
        ddl_Com_PostCode.DataBind();
        DataTO to = (DataTO)Session[ICommonUI.Web_ID + Session.SessionID + "Allowance_Upd_01"];
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
    }

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

    public override DataTO PopulateDetailData()
    {
        DataTO to = new DataTO();
        //to.setValue("Aas_Code", "");
        to.setValue("Com_Name", txt_Com_Name2.Text);
        to.setValue("Com_Tonum", txt_Com_Tonum2.Text);
        to.setValue("Com_BsGender", rbl_Com_BsGender2.SelectedValue);
        to.setValue("Com_Tel", txt_Com_Tel2.Text);
        to.setValue("IsExists", lbl_IsExists2.Text);
        to.setValue("Com_Account", lbl_Com_Account2.Text);
        to.setValue("Com_Pass", lbl_Com_Pass2.Text);
        to.setValue("Com_Code", lbl_Com_Code2.Text);
        to.setValue("Com_OPAddr", txt_Com_OPAddr2.Text);
        return to;
    }

    public override void RenderDetailData(DataTO to)
    {
        txt_Com_Name2.Text = to.getValue("Com_Name").ToString();
        txt_Com_Tonum2.Text = to.getValue("Com_Tonum").ToString();
        txt_Com_Tel2.Text = to.getValue("Com_Tel").ToString();
        txt_Com_OPAddr2.Text = to.getValue("Com_OPAddr").ToString();
        if (!string.IsNullOrEmpty(rbl_Com_BsGender2.SelectedValue))
            rbl_Com_BsGender2.SelectedValue = to.getValue("Com_BsGender").ToString();
    }


    public override void RenderData(DataTO to)
    {
        //Primary Key
        this.lbl_Com_Code.Text = to.getValue("Com_Code").ToString();
        this.lbl_Aow_Code.Text = to.getValue("Aow_Code").ToString();
        this.lbl_Aas_Code.Text = to.getValue("Aas_Code").ToString();
        //團體
        txt_Aow_GPName.Text = to.getValue("Aow_GPName").ToString();
        txt_Aow_RegNum.Text = to.getValue("Aow_RegNum").ToString();
        txt_Aow_FMan.Text = to.getValue("Aow_FMan").ToString();
        txt_Aow_FMIDNO.Text = to.getValue("Aow_FMIDNO").ToString();
        //單位
        txt_Com_Name.Text = to.getValue("Com_Name").ToString();
        txt_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
        txt_Com_CttCell.Text = to.getValue("Com_CttCell").ToString();
        if (!string.IsNullOrEmpty(to.getValue("Com_BsGender").ToString()))
            rbl_Com_BsGender.SelectedValue = to.getValue("Com_BsGender").ToString();
        txt_Com_BsTel.Text = to.getValue("Com_BsTel").ToString();
        txt_Com_BsNightTel.Text = to.getValue("Com_BsNightTel").ToString();
        txt_Com_CttName.Text = to.getValue("Com_CttName").ToString();
        txt_Com_CttTel.Text = to.getValue("Com_CttTel").ToString();
        ddl_Com_PostCode.SelectedValue = to.getValue("Com_PostCode").ToString();
        txt_Com_BsTel.Text = to.getValue("Com_BsTel").ToString();
        txt_Com_BsNightTel.Text = to.getValue("Com_BsNightTel").ToString();
        txt_Com_Fax.Text = to.getValue("Com_Fax").ToString();
        txt_Com_BsTel.Text = to.getValue("Com_BsTel").ToString();
        txt_Com_Email.Text = to.getValue("Com_Email").ToString();
        txt_Com_OPAddr.Text = to.getValue("Com_OPAddr").ToString();
        txt_Com_Url.Text = to.getValue("Com_Url").ToString();
        txt_Com_CttName.Text = to.getValue("Com_CttName").ToString();
        txt_Com_CttTel.Text = to.getValue("Com_CttTel").ToString();
        txt_Com_CttCell.Text = to.getValue("Com_CttCell").ToString();
        lbl_Com_Account.Text = to.getValue("Com_Account").ToString();
        lbl_Com_Pass.Text = to.getValue("Com_Pass").ToString();
        //計劃資料
        txt_ApPj_Name.Text = to.getValue("ApPj_Name").ToString();
        txt_ApPj_BgnDate.Text = to.getValue("ApPj_BgnDate").ToString();
        txt_ApPj_EndDate.Text = to.getValue("ApPj_EndDate").ToString();
        rbl_ApPj_Msectors.SelectedValue = to.getValue("ApPj_Msectors").ToString();
        ddl_ApPj_ApGroup.SelectedValue = to.getValue("ApPj_ApGroup").ToString();
        txt_ApPj_TotAmt.Text = (Convert.ToInt32(to.getValue("ApPj_TotAmt").ToString()) / 1000).ToString();
        txt_ApPj_AowAmt.Text = (Convert.ToInt32(to.getValue("ApPj_AowAmt").ToString()) / 1000).ToString();
        txt_ApPj_OthAmt.Text = (Convert.ToInt32(to.getValue("ApPj_OthAmt").ToString()) / 1000).ToString(); 
        txt_ApPj_Goal.Text = to.getValue("ApPj_Goal").ToString();
        txt_ApPj_Policies.Text = to.getValue("ApPj_Policies").ToString();
        txt_ApPj_Profit.Text = to.getValue("ApPj_Profit").ToString();
        txt_ApPj_Solution.Text = to.getValue("ApPj_Solution").ToString();
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Aow_Code");
    }

    protected void txt_Com_Tonum_TextChanged(object sender, EventArgs e)
    {

        DataTO queryTo = new DataTO();
        queryTo.setValue("Com_Tonum", this.txt_Com_Tonum.Text);
        DataTable dt = new DataTable();
        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Company", queryTo), dt);
        if (dt.Rows.Count > 0)
        {
            lbl_IsExists.Text = "Y";
            lbl_Com_Code.Text = dt.Rows[0]["Com_Code"].ToString();
            lbl_Com_Account.Text = dt.Rows[0]["Com_Account"].ToString();
            lbl_Com_Pass.Text = dt.Rows[0]["Com_Pass"].ToString();
            txt_Com_Name.Text = dt.Rows[0]["Com_Name"].ToString();
            txt_Com_Tonum.Text = dt.Rows[0]["Com_Tonum"].ToString();
            txt_Com_CttCell.Text = dt.Rows[0]["Com_CttCell"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["Com_BsGender"].ToString()))
                rbl_Com_BsGender.SelectedValue = dt.Rows[0]["Com_BsGender"].ToString();
            txt_Com_BsTel.Text = dt.Rows[0]["Com_BsTel"].ToString();
            txt_Com_BsNightTel.Text = dt.Rows[0]["Com_BsNightTel"].ToString();
            txt_Com_CttName.Text = dt.Rows[0]["Com_CttName"].ToString();
            txt_Com_CttTel.Text = dt.Rows[0]["Com_CttTel"].ToString();
            ddl_Com_PostCode.SelectedValue = dt.Rows[0]["Com_PostCode"].ToString();
            txt_Com_Fax.Text = dt.Rows[0]["Com_Fax"].ToString();
            txt_Com_Email.Text = dt.Rows[0]["Com_Email"].ToString();
            txt_Com_OPAddr.Text = dt.Rows[0]["Com_OPAddr"].ToString();
            txt_Com_Url.Text = dt.Rows[0]["Com_Url"].ToString();

            up_Com_Name.Update();
            //up_Com_Tonum.Update();
            up_Com_CttCell.Update();
            up_Com_BsGender.Update();
            up_Com_BsTel.Update();
            up_Com_BsNightTel.Update();
            up_Com_CttName.Update();
            up_Com_CttTel.Update();
            up_Com_PostCode.Update();
            up_Com_Fax.Update();
            up_Com_Email.Update();
            up_Com_OPAddr.Update();
            up_Com_Url.Update();
        }
        else
        {
            lbl_IsExists.Text = "N";
            BaseFun bf = new BaseFun();
            lbl_Com_Account.Text = bf.generAccount(txt_Com_Tonum.Text);
            lbl_Com_Pass.Text = bf.generPassword();
        }
    }

    protected void txt_Com_Tonum2_TextChanged(object sender, EventArgs e)
    {
        DataTO queryTo = new DataTO();
        queryTo.setValue("Com_Tonum", this.txt_Com_Tonum2.Text);
        DataTable dt = new DataTable();
        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Company", queryTo), dt);
        if (dt.Rows.Count > 0)
        {
            lbl_IsExists2.Text = "Y";
            txt_Com_Name2.Text = dt.Rows[0]["Com_Name"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["Com_BsGender"].ToString()))
                rbl_Com_BsGender2.SelectedValue = dt.Rows[0]["Com_BsGender"].ToString();
            txt_Com_Tel2.Text = dt.Rows[0]["Com_Tel"].ToString();
            txt_Com_OPAddr2.Text = dt.Rows[0]["Com_OPAddr"].ToString();
            lbl_Com_Account2.Text = dt.Rows[0]["Com_Account"].ToString();
            lbl_Com_Pass2.Text = dt.Rows[0]["Com_Pass"].ToString();
            lbl_Com_Code2.Text = dt.Rows[0]["Com_Code"].ToString();
            up_Com_Name2.Update();
            up_Com_BsGender2.Update();
            up_Com_Tel2.Update();
            up_Com_OPAddr2.Update();
        }
        else
        {
            lbl_IsExists2.Text = "N";
            BaseFun bf = new BaseFun();
            lbl_Com_Account2.Text = bf.generAccount(txt_Com_Tonum2.Text);
            lbl_Com_Pass2.Text = bf.generPassword();
        }
    }
    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Text = new BaseFun().getSysCodeValue("S", "E", e.Row.Cells[11].Text);
        }
        e.Row.Cells[7].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[8].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[9].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[10].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[11].Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    protected void grvQuery_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Text = new BaseFun().getSysCodeValue("S", "E", e.Row.Cells[11].Text);
        }
        e.Row.Cells[7].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[8].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[9].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[10].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[11].Style.Add(HtmlTextWriterStyle.Display, "none");
    }
}