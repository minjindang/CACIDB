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

public partial class Allowance_Ins_03 : IInsertUI
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
        to.setValue("Allowance", allowanceTo);
        //單位(公司)基本資料
        DataTO comTo = new DataTO();
        comTo.setValue("Com_Code", lbl_Com_Code.Text);
        comTo.setValue("Com_Name", txt_Com_Name.Text);
        comTo.setValue("Com_BsGender", rbl_Com_BsGender.SelectedValue);
        comTo.setValue("Com_Tonum", txt_Com_Tonum.Text);
        comTo.setValue("Com_BsTel", txt_Com_BsTel.Text);
        comTo.setValue("Com_BsNightTel", txt_Com_BsNightTel.Text);
        comTo.setValue("Com_BsCell", txt_Com_BsCell.Text);
        comTo.setValue("Com_Fax", txt_Com_Fax.Text);
        comTo.setValue("Com_Email", txt_Com_Email.Text);
        comTo.setValue("Com_OPAddr", txt_Com_OPAddr.Text);
        comTo.setValue("Com_MnSectors", rbl_Com_MnSectors.SelectedValue);
        comTo.setValue("Com_Account", lbl_Com_Account.Text);
        comTo.setValue("Com_Pass", lbl_Com_Pass.Text);
        to.setValue("Company", comTo);
        //計劃資料
        DataTO apPjTo = new DataTO();
        apPjTo.setValue("ApPj_Name", txt_ApPj_Name.Text);
        apPjTo.setValue("ApPj_Goal", txt_ApPj_Goal.Text);
        apPjTo.setValue("ApPj_Policies", txt_ApPj_Policies.Text);
        apPjTo.setValue("ApPj_Profit", txt_ApPj_Profit.Text);
        apPjTo.setValue("ApPj_Solution", txt_ApPj_Solution.Text);
        apPjTo.setValue("ApPj_TotAmt", Convert.ToInt32(this.txt_ApPj_TotAmt.Text.Replace(",", string.Empty)) * 1000);
        apPjTo.setValue("ApPj_AowAmt", Convert.ToInt32(this.txt_ApPj_AowAmt.Text.Replace(",", string.Empty)) * 1000);
        apPjTo.setValue("ApPj_OthAmt", Convert.ToInt32(this.txt_ApPj_OthAmt.Text.Replace(",", string.Empty)) * 1000);
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
        ProgNm = "獎補助作業─內網媒合計畫資料新增畫面";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "2.1.16";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Allowance_03BL();//邏輯層

        #endregion

        #region 按鈕定義

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
        //產業別:
        rbl_Com_MnSectors.DataSource = bf.getSysCodeByKind("I", "D");
        rbl_Com_MnSectors.DataTextField = "Sys_CdText";
        rbl_Com_MnSectors.DataValueField = "Sys_CdCode";
        rbl_Com_MnSectors.DataBind();
        rbl_Com_MnSectors.Items[0].Selected = true;
        DataTO to = (DataTO)Session[ICommonUI.Web_ID + Session.SessionID + "Allowance_Ins_03"];
        if (to != null && to.getValue("Pj_Code").ToString() != "")
        {
            lbl_Pj_Code.Text = to.getValue("Pj_Code").ToString();
        }
        else if (to != null && to.getValue("IsPreview").ToString() == "Y")
        {
            btn_Insert.Visible = false;
            btn_Clear.Visible = false;
            BackPage = "Project_Ins_01.aspx";
        }
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
            if (!string.IsNullOrEmpty(dt.Rows[0]["Com_BsGender"].ToString()))
                rbl_Com_BsGender.SelectedValue = dt.Rows[0]["Com_BsGender"].ToString();
            txt_Com_Fax.Text = dt.Rows[0]["Com_Fax"].ToString();
            txt_Com_BsTel.Text = dt.Rows[0]["Com_BsTel"].ToString();
            txt_Com_BsNightTel.Text = dt.Rows[0]["Com_BsNightTel"].ToString();
            txt_Com_BsCell.Text = dt.Rows[0]["Com_BsCell"].ToString();
            txt_Com_Email.Text = dt.Rows[0]["Com_Email"].ToString();
            txt_Com_OPAddr.Text = dt.Rows[0]["Com_OPAddr"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["Com_MnSectors"].ToString()))
                rbl_Com_MnSectors.SelectedValue = dt.Rows[0]["Com_MnSectors"].ToString();
            up_Com_Name.Update();
            up_Com_BsGender.Update();
            up_Com_Fax.Update();
            up_Com_BsTel.Update();
            up_Com_BsNightTel.Update();
            up_Com_BsCell.Update();
            up_Com_Email.Update();
            up_Com_OPAddr.Update();
            up_Com_MnSectors.Update();
        }
        else
        {
            BaseFun bf = new BaseFun();
            lbl_Com_Account.Text = bf.generAccount(txt_Com_Tonum.Text);
            lbl_Com_Pass.Text = bf.generPassword();
        }
    }

    protected override void AfterHandleInsert(DataTO to)
    {
        base.AfterHandleInsert(to);

        if (to.getValue("Company") != null && to.getValue("Company").ToString() != "")
        {
            DataTO comTo = (DataTO)to.getValue("Company");
            string targetPath = Request.PhysicalApplicationPath + @"UploadFile\Company\" + comTo.getValue("Com_Code");
            if (!Directory.Exists(targetPath))
                Directory.CreateDirectory(targetPath);
            string sourcePath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;
            string fileName = string.Empty;
            string destFile = string.Empty;
            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);
                foreach (string s in files)
                {
                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Move(s, destFile);
                }
            }
            Directory.Delete(sourcePath, true);
        }

        hideImgBtn(btn_Insert);
    }

    protected void RadAsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        string filePath = tempPath + @"\" + "1_" + e.File.FileName;

        e.File.SaveAs(filePath);
    }
    protected void RadAsyncUpload2_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        string filePath = tempPath + @"\" + "2_" + e.File.FileName;

        e.File.SaveAs(filePath);
    }
    //protected void RadAsyncUpload3_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    //{
    //    string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

    //    string filePath = tempPath + @"\" + "3_" + e.File.FileName;

    //    e.File.SaveAs(filePath);
    //}
    //protected void RadAsyncUpload4_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    //{
    //    string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

    //    string filePath = tempPath + @"\" + "4_" + e.File.FileName;

    //    e.File.SaveAs(filePath);
    //}

}