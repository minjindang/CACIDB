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
using CuteWebUI;

public partial class Committee_Ins_01 : IMDInsertUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        if (!to.isColumnExist("Comm_Name"))
            to.setValue("Comm_Name", txt_Comm_Name.Text);

        if (!to.isColumnExist("Comm_IDNO"))
            to.setValue("Comm_IDNO", txt_Comm_IDNO.Text);

        if (!to.isColumnExist("Comm_Title"))
            to.setValue("Comm_Title", txt_Comm_Title.Text);

        if (!to.isColumnExist("Comm_ComName"))
            to.setValue("Comm_ComName", txt_Comm_ComName.Text);

        if (!to.isColumnExist("Comm_Tel"))
            to.setValue("Comm_Tel", txt_Comm_Tel.Text);

        if (!to.isColumnExist("Comm_Cell"))
            to.setValue("Comm_Cell", txt_Comm_Cell.Text);

        if (!to.isColumnExist("Comm_Mail"))
            to.setValue("Comm_Mail", txt_Comm_Mail.Text);

        if (!to.isColumnExist("Comm_Account"))
            to.setValue("Comm_Account", txt_Comm_Account.Text);

        if (!to.isColumnExist("Comm_Pass"))
            to.setValue("Comm_Pass", txt_Comm_Pass.Text);

        if (!to.isColumnExist("Comm_Bank_Num"))
            to.setValue("Comm_Bank_Num", txt_Comm_Bank_Num.Text);

        if (!to.isColumnExist("Comm_Bankno"))
            to.setValue("Comm_Bankno", txt_Comm_Bankno.Text);
        
        if (!to.isColumnExist("Comm_BkName"))
            to.setValue("Comm_BkName", txt_Comm_BkName.Text);

        //if (RadAsyncUpload1.UploadedFiles.Count > 0)
        //{
        //    to.setValue("Comm_BkFile", RadAsyncUpload1.UploadedFiles[0].FileName);
        //}

        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (Directory.Exists(tempPath))
        {
            foreach (string _path in Directory.GetFiles(tempPath))
            {
                if (new FileInfo(_path).Name.StartsWith("Bk_"))
                {
                    to.setValue("Comm_BkFile", _path);
                }
            }
        }

        string selectedComm_CoTerms = string.Empty;
        foreach (ListItem li in ckl_Comm_CoTerms.Items)
        {
            if (li.Selected)
                selectedComm_CoTerms += li.Value;
        }
        if (!to.isColumnExist("Comm_CoTerms"))
            to.setValue("Comm_CoTerms", selectedComm_CoTerms);
        else
            to.updateValue("Comm_CoTerms", selectedComm_CoTerms);
        string selectedComm_CoachWay = string.Empty;
        foreach (ListItem li in ckl_Comm_CoachWay.Items)
        {
            if (li.Selected)
                selectedComm_CoachWay += "1";
            else
                selectedComm_CoachWay += "0";
        }
        if (!to.isColumnExist("Comm_CoachWay"))
            to.setValue("Comm_CoachWay", selectedComm_CoachWay);
        else
            to.updateValue("Comm_CoachWay", selectedComm_CoachWay);
        return to;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "顧問委員管理作業─內網資料新增畫面";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "3.1.3";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grvQuery;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        ValidationDetailGroupID = "grvQuery";

        #endregion

        #region 宣告資訊

        BL = new Committee_01BL();//邏輯層

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

        BackPage = "Committee_Qry_01.aspx";

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
        BaseFun bf = new BaseFun();
        //輔導方式
        ckl_Comm_CoachWay.Items.Clear();
        ckl_Comm_CoachWay.Items.Add("獎補助審查");
        ckl_Comm_CoachWay.Items.Add("輔導服務");
        ckl_Comm_CoachWay.Items.Add("顧問駐診服務");
        //輔導項目
        ckl_Comm_CoTerms.DataSource = bf.getSysCodeByKind("C", "K");
        ckl_Comm_CoTerms.DataBind();
        //專長
        ddl_Skill.DataSource = bf.getSkillSample();
        ddl_Skill.DataBind();

        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;
        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);
        else
        {
            Directory.Delete(tempPath, true);
            Directory.CreateDirectory(tempPath);
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

        //if (hid_Skill_Code.Value == "N")
        //{
        //    if( DetailGridView.DataKeys.Count == 0 )
        //        to.setValue("Skill_Code", 1);
        //    else
        //        to.setValue("Skill_Code", Convert.ToInt32(DetailGridView.DataKeys[DetailGridView.DataKeys.Count - 1]["Skill_Code"]) + 1);
        //}
        //else
        //    to.setValue("Skill_Code", Convert.ToInt32(hid_Skill_Code.Value));

        to.setValue("Ski_Num", ddl_Skill.SelectedValue);
        to.setValue("Ski_Name", ddl_Skill.SelectedItem.Text);
        to.setValue("Skill_Note", txt_Skill_Note.Text);

        return to;
    }

    /// <summary>
    /// 將選取的Detail資料顯示於畫面
    /// </summary>
    /// <param name="to"></param>
    public override void RenderDetailData(DataTO to)
    {
        //hid_Skill_Code.Value = to.getValue("Skill_Code").ToString();
        ddl_Skill.SelectedValue = to.getValue("Ski_Num").ToString();
        txt_Skill_Note.Text = to.getValue("Skill_Note").ToString();
    }

    protected override void AfterHandleDetailUpdate()
    {
        base.AfterHandleDetailUpdate();
        //hid_Skill_Code.Value = "N";
    }

    protected override void AfterHandleInsert(DataTO to)
    {
        //base.AfterHandleInsert(to);
        //if (to.getValue("Comm_Code") != null && to.getValue("Comm_Code").ToString() != "")
        //{
        //    string targetPath = Request.PhysicalApplicationPath + @"UploadFile\Committee\" + to.getValue("Comm_Code");
        //    if (!Directory.Exists(targetPath))
        //        Directory.CreateDirectory(targetPath);
        //    string sourcePath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;
        //    string fileName = string.Empty;
        //    string destFile = string.Empty;
        //    if (System.IO.Directory.Exists(sourcePath))
        //    {
        //        string[] files = System.IO.Directory.GetFiles(sourcePath);
        //        foreach (string s in files)
        //        {
        //            fileName = System.IO.Path.GetFileName(s);
        //            destFile = System.IO.Path.Combine(targetPath, fileName);
        //            System.IO.File.Move(s, destFile);
        //        }
        //        Directory.Delete(sourcePath, true);
        //    }
        //}
    }

    /// <summary>
    /// Detail 編輯區初始化
    /// </summary>
    public override void InitialDetail()
    {
        base.InitialDetail();

        //hid_Skill_Code.Value = "N";
        //txt_Ski_Num.Text = "";
        txt_Skill_Note.Text = "";
    }

    protected void txt_Comm_IDNO_TextChanged(object sender, EventArgs e)
    {
        BaseFun bf = new BaseFun();
        txt_Comm_Account.Text = bf.generCommitteeAccount(txt_Comm_IDNO.Text);

        UpdatePanel2.Update();

        txt_Comm_Pass.Attributes.Add("value", bf.generPassword());

        UpdatePanel3.Update();
    }

    protected void txt_Comm_Bank_Num_TextChanged(object sender, EventArgs e)
    {
        lbl_Comm_Bank_Name.Text = new BaseFun().getBankName(txt_Comm_Bank_Num.Text);
    }

    protected void Uploader_Comm_BkFile_FileUploaded(object sender, UploaderEventArgs e)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("Bk_"))
                File.Delete(_path);
        }

        string filePath = tempPath + @"\Bk_" + e.FileName;

        e.CopyTo(filePath);

        ((UploadAttachments)sender).InsertButton.Enabled = false;
    }

    protected void Uploader_Comm_BkFile_AttachmentRemoveClicked(object sender, AttachmentItemEventArgs args)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("Bk_"))
                File.Delete(_path);
        }

        ((UploadAttachments)sender).InsertButton.Enabled = true;
    }

    //protected void RadAsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    //{
    //    string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;
    //    //if (!Directory.Exists(tempPath))
    //    //    Directory.CreateDirectory(tempPath);
    //    //else
    //    //{
    //    //    Directory.Delete(tempPath, true);
    //    //    Directory.CreateDirectory(tempPath);
    //    //}
    //    string filePath = tempPath + @"\" + e.File.FileName;
    //    try
    //    {
    //        e.File.SaveAs(filePath);
    //    }
    //    catch (Exception)
    //    {
    //    }
    //}
}