using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using System.IO;
using CuteWebUI;

public partial class Committee_Upd_01 : IMDUpdateUI
{

    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        if (!to.isColumnExist("Comm_Code"))
            to.setValue("Comm_Code", lblComm_Code.Text);

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
        
        return to;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "顧問委員管理作業─內網資料修改畫面 ";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "3.1.4";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grvQuery;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        InsertDetailButton = btnDTL_INSERT;
        UpdateDetailButton = btnDTL_UPDATE;
        ClearDetailButton = btnDTL_CLEAR;


        ValidationDetailGroupID = "grvQuery";

        #endregion

        #region 宣告資訊

        BL = new Committee_01BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
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
        //專長
        ddl_Skill.DataSource = bf.getSkillSample();
        ddl_Skill.DataTextField = "Ski_Name";
        ddl_Skill.DataValueField = "Ski_Num";
        ddl_Skill.DataBind();

        hid_IsNewAccount.Value = "N";
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

        to.setValue("Ski_Num", ddl_Skill.SelectedValue);
        to.setValue("Ski_Name", ddl_Skill.SelectedItem.Text);
        to.setValue("Skill_Note", txt_Skill_Note.Text);

        return to;
    }

    public override void RenderDetailData(DataTO to)
    {
        ddl_Skill.SelectedValue = to.getValue("Ski_Num").ToString();
        txt_Skill_Note.Text = to.getValue("Skill_Note").ToString();
    }

    public override void InitialDetail()
    {
        base.InitialDetail();

        //txt_Ski_Num.Text = "";
        txt_Skill_Note.Text = "";
    }

    public override void RenderData(DataTO to)
    {
        lblComm_Code.Text = to.getValue("Comm_Code").ToString();
        txt_Comm_Name.Text = to.getValue("Comm_Name").ToString();
        txt_Comm_IDNO.Text = to.getValue("Comm_IDNO").ToString();
        txt_Comm_Title.Text = to.getValue("Comm_Title").ToString();
        txt_Comm_ComName.Text = to.getValue("Comm_ComName").ToString();
        txt_Comm_Tel.Text = to.getValue("Comm_Tel").ToString();
        txt_Comm_Cell.Text = to.getValue("Comm_Cell").ToString();
        txt_Comm_Mail.Text = to.getValue("Comm_Mail").ToString();
        txt_Comm_Account.Text = to.getValue("Comm_Account").ToString();
        txt_Comm_Pass.Attributes.Add("value", to.getValue("Comm_Pass").ToString());
        txt_Comm_Bank_Num.Text = to.getValue("Comm_Bank_Num").ToString();
        lbl_Comm_Bank_Name.Text = to.getValue("Bank_Name").ToString();
        txt_Comm_Bankno.Text = to.getValue("Comm_Bankno").ToString();
        txt_Comm_BkName.Text = to.getValue("Comm_BkName").ToString();
        string Comm_BkFile = string.Empty;
        if (!string.IsNullOrEmpty(to.getValue("Comm_BkFile").ToString()))
        {
            Comm_BkFile = to.getValue("Comm_BkFile").ToString();
            lbtn_Comm_BkFile.CommandArgument = Comm_BkFile;
            lbtn_Comm_BkFile.Text = Comm_BkFile.Split('/')[Comm_BkFile.Split('/').Length - 1];
        }
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

    protected void txt_Comm_IDNO_TextChanged(object sender, EventArgs e)
    {
        if (txt_Comm_IDNO.Text == "")
        {
            txt_Comm_Account.Text = "";
            txt_Comm_Pass.Attributes.Add("value", "");

            UpdatePanel2.Update();
            UpdatePanel3.Update();
        }
        else
        {
            Random ram = new Random();

            char firstChar = (char)ram.Next(65, 90);
            char SecondChar = (char)ram.Next(65, 90);

            txt_Comm_Account.Text = firstChar.ToString() + SecondChar.ToString() + txt_Comm_IDNO.Text.Substring(4);

            hid_IsNewAccount.Value = "Y";

            UpdatePanel2.Update();

            string passWord = "";

            while (passWord.Length != 6)
            {
                passWord += ram.Next(0, 9).ToString();
            }

            txt_Comm_Pass.Attributes.Add("value", passWord);

            UpdatePanel3.Update();
        }
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

    protected void lbtn_Comm_BkFile_Command(object sender, CommandEventArgs e)
    {
        //GoURL("http://" + Request.Url.Authority + e.CommandArgument.ToString());
        OpenURL(this.Page,"http://" + Request.Url.Authority + e.CommandArgument.ToString());
    }
}