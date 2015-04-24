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

public partial class Allowance_Upd_03 : IUpdateUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        try
        {
            DataTO to = new DataTO();
            //獎補助資料表
            //DataTO allowanceTo = new DataTO();
            //allowanceTo.setValue("Aow_Code", lbl_Aow_Code.Text);
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
            to.setValue("Company", comTo);
            //計劃資料
            DataTO apPjTo = new DataTO();
            apPjTo.setValue("Aow_Code", lbl_Aow_Code.Text);
            apPjTo.setValue("ApPj_Name", txt_ApPj_Name.Text);
            apPjTo.setValue("ApPj_Goal", txt_ApPj_Goal.Text);
            apPjTo.setValue("ApPj_Policies", txt_ApPj_Policies.Text);
            apPjTo.setValue("ApPj_Profit", txt_ApPj_Profit.Text);
            apPjTo.setValue("ApPj_Solution", txt_ApPj_Solution.Text);
            apPjTo.setValue("ApPj_TotAmt", Convert.ToInt32(this.txt_ApPj_TotAmt.Text.Replace(",", string.Empty)) * 1000);
            apPjTo.setValue("ApPj_AowAmt", Convert.ToInt32(this.txt_ApPj_AowAmt.Text.Replace(",", string.Empty)) * 1000);
            apPjTo.setValue("ApPj_OthAmt", Convert.ToInt32(this.txt_ApPj_OthAmt.Text.Replace(",", string.Empty)) * 1000);
            apPjTo.setValue("ApPj_OthAmt", txt_ApPj_OthAmt.Text);
            to.setValue("ApPjContext", apPjTo);
            return to;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return null;
        }
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "獎補助作業─內網媒合計畫資料修改畫面";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "2.1.17";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Allowance_03BL();//邏輯層

        #endregion

        #region 按鈕定義

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
        //單位(公司)基本資料
        txt_Com_Name.Text = to.getValue("Com_Name").ToString();
        rbl_Com_BsGender.SelectedValue = to.getValue("Com_BsGender").ToString();
        txt_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
        txt_Com_BsTel.Text = to.getValue("Com_BsTel").ToString();
        txt_Com_BsNightTel.Text = to.getValue("Com_BsNightTel").ToString();
        txt_Com_BsCell.Text = to.getValue("Com_BsCell").ToString();
        txt_Com_Fax.Text = to.getValue("Com_Fax").ToString();
        txt_Com_BsTel.Text = to.getValue("Com_BsTel").ToString();
        txt_Com_Email.Text = to.getValue("Com_Email").ToString();
        txt_Com_OPAddr.Text = to.getValue("Com_OPAddr").ToString();
        txt_Com_Email.Text = to.getValue("Com_Email").ToString();
        txt_Com_OPAddr.Text = to.getValue("Com_OPAddr").ToString();
        rbl_Com_MnSectors.SelectedValue = to.getValue("Com_MnSectors").ToString();
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
        txt_ApPj_OthAmt.Text = (Convert.ToInt32(to.getValue("ApPj_OthAmt").ToString()) / 1000).ToString(); //to.getValue("ApPj_OthAmt").ToString();
    }

    protected override void AfterHandleUpdate(DataTO to)
    {
        base.AfterHandleUpdate();

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
        hideImgBtn(UpdateButton);

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
    protected void RadAsyncUpload3_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        string filePath = tempPath + @"\" + "3_" + e.File.FileName;

        e.File.SaveAs(filePath);
    }
    protected void RadAsyncUpload4_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        string filePath = tempPath + @"\" + "4_" + e.File.FileName;

        e.File.SaveAs(filePath);
    }
}