using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.DB;
using CuteWebUI;

public partial class PjSamples_Upd_02 : IMDUpdateUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        to.setValue("PjSp_Code", lbl_PjSp_Code.Text);
        to.setValue("PjSp_Kind", "B");
        to.setValue("PjSp_Name", txt_PjSp_Name.Text);
        to.setValue("PjSp_Trans", ddl_PjSp_Trans.SelectedValue);
        to.setValue("PjSp_User_Code", ddl_UserAcc.SelectedValue);
        to.setValue("PjSp_WebExp", Editor1.Content);
        to.setValue("PjSp_PjIntro", txt_PjSp_PjIntro.Text);
        to.setValue("PjSp_PjNote", txt_PjSp_PjNote.Text);

        //if (RadAsyncUpload1.UploadedFiles.Count > 0)
        //    to.setValue("PjSp_PjFile", RadAsyncUpload1.UploadedFiles[0].FileName);
        //Add temp to TO
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;
        if (Directory.Exists(tempPath))
        {
            foreach (string _path in Directory.GetFiles(tempPath))
            {
                if (new FileInfo(_path).Name.StartsWith("PjSp_"))
                {
                    to.setValue("PjSp_PjFile", _path);
                }
            }
        }
        to.setValue("PjSp_PjFill", "6");

        return to;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "輔導範本資料修改作業";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "1.1.7";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grv_SmpStage;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        ValidationDetailGroupID = "grv_SmpStage";

        #endregion

        #region 宣告資訊

        BL = new PjSamples_02BL();//邏輯層

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

        BackPage = "PjSamples_Qry_01.aspx";

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
        ddl_UserDep.DataSource = new BaseFun().getUserDep();
        ddl_UserDep.DataBind();

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
            case "select" :
                type = ROW_CMD_TYPE.ROW_CMD_MODIFY;
                break;
            case "del" :
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

        to.setValue("OSpStage_Index", Old_SpStage_Index.Value);
        to.setValue("SpStage_Index", ddl_SpStage_Index.SelectedValue);

        if (rad_SpStage_Kind_1.Checked)
            to.setValue("SpStage_Kind", "1");
        else if (rad_SpStage_Kind_2.Checked)
        {
            to.setValue("SpStage_Kind", "2");
            to.setValue("SpStage_Days", txt_SpStage_Days.Text);
        }
        else if (rad_SpStage_Kind_3.Checked)
            to.setValue("SpStage_Kind", "3");

        to.setValue("SpStage_Name", txt_SpStage_Name.Text);
        to.setValue("SpStage_Text", txt_SpStage_Text.Text);
        to.setValue("SpStage_RmFlag", ddl_SpStage_RmFlag.SelectedValue);

        string SpStage_RmEmpl = "";

        SpStage_RmEmpl += chl_SpStage_RmEmpl.Items[0].Selected ? "1" : "0";
        SpStage_RmEmpl += chl_SpStage_RmEmpl.Items[1].Selected ? "1" : "0";
        SpStage_RmEmpl += chl_SpStage_RmEmpl.Items[2].Selected ? "1" : "0";

        to.setValue("SpStage_RmEmpl", SpStage_RmEmpl);
        to.setValue("SpStage_RmDays", txt_SpStage_RmDays.Text);
        to.setValue("SpStage_RmText", txt_SpStage_RmText.Text);
        
        return to;
    }

    /// <summary>
    /// 將選取的Detail資料顯示於畫面
    /// </summary>
    /// <param name="to"></param>
    public override void RenderDetailData(DataTO to)
    {
        txt_SpStage_Name.Text = to.getValue("SpStage_Name").ToString();
        Old_SpStage_Index.Value = to.getValue("SpStage_Index").ToString();
        ddl_SpStage_Index.SelectedValue = to.getValue("SpStage_Index").ToString();

        switch (to.getValue("SpStage_Kind").ToString())
        {
            case "1":
                rad_SpStage_Kind_1.Checked = true;
                break;
            case "2":
                rad_SpStage_Kind_2.Checked = true;
                txt_SpStage_Days.Text = to.getValue("SpStage_Days").ToString();
                break;
            case "3":
                rad_SpStage_Kind_3.Checked = true;
                break;
        }

        txt_SpStage_Text.Text = to.getValue("SpStage_Text").ToString();
        ddl_SpStage_RmFlag.SelectedValue = to.getValue("SpStage_RmFlag").ToString();

        if (to.getValue("SpStage_RmEmpl").ToString().Substring(0, 1) == "1")
            chl_SpStage_RmEmpl.Items[0].Selected = true;
        if (to.getValue("SpStage_RmEmpl").ToString().Substring(1, 1) == "1")
            chl_SpStage_RmEmpl.Items[1].Selected = true;
        if (to.getValue("SpStage_RmEmpl").ToString().Substring(2, 1) == "1")
            chl_SpStage_RmEmpl.Items[2].Selected = true;

        txt_SpStage_RmText.Text = to.getValue("SpStage_RmText").ToString();
    }

    protected void ddl_UserDep_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_UserAcc.DataSource = new BaseFun().getDepUser(ddl_UserDep.SelectedValue);
        ddl_UserAcc.DataBind();
    }

    private bool checkSmpStageData(out string Msg)
    {
        Msg = "";
        if (rad_SpStage_Kind_2.Checked && txt_SpStage_Days.Text == "")
            Msg += "\\r\\n請輸入【與前階段日差】";

        if (ddl_SpStage_RmFlag.SelectedValue == "Y")
        {
            if (chl_SpStage_RmEmpl.Items[0].Selected == false &&
                chl_SpStage_RmEmpl.Items[1].Selected == false &&
                chl_SpStage_RmEmpl.Items[2].Selected == false)
            {
                Msg += "\\r\\n請選擇【提醒人員】";
            }

            if (txt_SpStage_RmDays.Text == "")
                Msg += "\\r\\n請輸入【提前提醒天數】";

            if (txt_SpStage_RmText.Text == "")
                Msg += "\\r\\n請輸入【提醒文稿】";
        }

        if (Msg != "")
            return false;
        else
            return true;
    }

    protected override void AfterHandleDetailUpdate()
    {
        base.AfterHandleDetailUpdate();

        Old_SpStage_Index.Value = "";
    }

    protected override void AfterHandleDetailInsert()
    {
        base.AfterHandleDetailInsert();

        Old_SpStage_Index.Value = "";
        // 階段順序多增加一個
        ddl_SpStage_Index.Items.Add(new ListItem((ddl_SpStage_Index.Items.Count).ToString(), (ddl_SpStage_Index.Items.Count).ToString()));
        ddl_SpStage_Index.SelectedValue = (ddl_SpStage_Index.Items.Count - 1).ToString();
    }

    protected override bool BeforeHandleDetailInsert(DataTO to, System.Data.DataTable dt)
    {
        if (base.BeforeHandleDetailInsert(to, dt))
        {
            string errMsg = "";

            if (!checkSmpStageData(out errMsg))
            {
                ShowMsgBox(this.Page, errMsg);

                return false;
            }

            if (dt.Rows.Count > 0 && Convert.ToInt32(to.getValue("SpStage_Index").ToString()) <= Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["SpStage_Index"].ToString()))
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (Convert.ToInt32(dt.Rows[i]["SpStage_Index"].ToString()) >= Convert.ToInt32(to.getValue("SpStage_Index").ToString()))
                    {
                        dt.Rows[i]["SpStage_Index"] = (Convert.ToInt32(dt.Rows[i]["SpStage_Index"].ToString()) + 1).ToString();
                    }
                }
            }

            dt.DefaultView.Sort = "SpStage_Index ASC";

            return true;
        }
        else
            return false;
    }

    protected override bool BeforeHandleDetailUpdate(DataTO to, System.Data.DataTable dt)
    {
        if (base.BeforeHandleDetailUpdate(to, dt))
        {
            string errMsg = "";

            if (!checkSmpStageData(out errMsg))
            {
                ShowMsgBox(this, errMsg);
                return false;
            }

            int OSpStage_Index = Convert.ToInt32(to.getValue("OSpStage_Index").ToString());
            int SpStage_Index = Convert.ToInt32(to.getValue("SpStage_Index").ToString());

            if (OSpStage_Index > SpStage_Index)
            {
                // 當階段往上更動時

                DataRow[] rows = dt.Select("SpStage_Index >= '" + to.getValue("SpStage_Index").ToString() + "' AND SpStage_Index <= '" + to.getValue("OSpStage_Index").ToString() + "' ");

                if (rows.Length > 0)
                {
                    rows[rows.Length - 1]["SpStage_Index"] = "T";

                    for (int i = 0; i < rows.Length - 1; i++)
                    {
                        rows[i]["SpStage_Index"] = (Convert.ToInt32(rows[i]["SpStage_Index"].ToString()) + 1);
                    }

                    rows[rows.Length - 1]["SpStage_Index"] = SpStage_Index.ToString();
                }
            }
            else if (OSpStage_Index < SpStage_Index)
            {
                // 當階段往下更動時

                DataRow[] rows = dt.Select("SpStage_Index >= '" + to.getValue("OSpStage_Index").ToString() + "' AND SpStage_Index <= '" + to.getValue("SpStage_Index").ToString() + "' ");

                if (rows.Length > 0)
                {
                    rows[0]["SpStage_Index"] = "T";

                    for (int i = 1; i < rows.Length; i++)
                    {
                        rows[i]["SpStage_Index"] = (Convert.ToInt32(rows[i]["SpStage_Index"].ToString()) - 1);
                    }

                    rows[0]["SpStage_Index"] = SpStage_Index.ToString();
                }
            }

            dt.DefaultView.Sort = "SpStage_Index ASC";

            return true;
        }
        else
            return false;
    }

    protected override void AfterHandleUpdate()
    {
        base.AfterHandleUpdate();
        if (!string.IsNullOrEmpty(lbl_PjSp_Code.Text))
        {
            string targetPath = Request.PhysicalApplicationPath + @"UploadFile\PjSample\" + lbl_PjSp_Code.Text;
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

                Directory.Delete(sourcePath, true);
            }
        }
        //hideImgBtn(UpdateButton);
    }

    /// <summary>
    /// Detail 編輯區初始化
    /// </summary>
    public override void InitialDetail()
    {
        base.InitialDetail();
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("PjSp_Code");
    }

    public override void RenderData(DataTO to)
    {
        lbl_PjSp_Code.Text = to.getValue("PjSp_Code").ToString();
        txt_PjSp_Name.Text = to.getValue("PjSp_Name").ToString();
        ddl_PjSp_Trans.SelectedValue = to.getValue("PjSp_Trans").ToString();
        ddl_UserDep.SelectedValue = new BaseFun().getUserDep(to.getValue("PjSp_User_Code").ToString());
        ddl_UserAcc.DataSource = new BaseFun().getDepUser(ddl_UserDep.SelectedValue);
        ddl_UserAcc.DataBind();
        ddl_UserAcc.SelectedValue = to.getValue("PjSp_User_Code").ToString();
        Editor1.Content = to.getValue("PjSp_WebExp").ToString();
        txt_PjSp_PjIntro.Text = to.getValue("PjSp_PjIntro").ToString();
        txt_PjSp_PjNote.Text = to.getValue("PjSp_PjNote").ToString();
        string pjSp_PjFile = string.Empty;
        if(!string.IsNullOrEmpty(to.getValue("PjSp_PjFile").ToString()))
        {
            pjSp_PjFile = to.getValue("PjSp_PjFile").ToString();
            lbtn_PjSp_PjFile.CommandArgument = pjSp_PjFile;
            lbtn_PjSp_PjFile.Text = pjSp_PjFile.Split('/')[pjSp_PjFile.Split('/').Length - 1];
        }

    }

    protected override void AfterRenderData(DataTO to, DataTable dt)
    {
        base.AfterRenderData(to, dt);

        int stmpCount = dt.Rows.Count;

        for (int i = 0; i < stmpCount; i++)
        {
            ddl_SpStage_Index.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
        }

        ddl_SpStage_Index.Items.Add(new ListItem((stmpCount + 1).ToString(), (stmpCount + 1).ToString()));
    }

    protected override void SetHandler()
    {
        base.SetHandler();
        ((ScriptManager)this.Master.FindControl("ToolkitScriptManager1")).RegisterPostBackControl(btn_Clear);
        //((ScriptManager)this.Master.FindControl("RadScriptManager1")).RegisterPostBackControl(RadAsyncUpload1);
    }

    protected void lbtn_PjSp_PjFile_Command(object sender, CommandEventArgs e)
    {
        OpenURL(this.Page, "http://" + Request.Url.Authority + e.CommandArgument.ToString());
    }



    protected void Uploader_FileUploaded(object sender, UploaderEventArgs e)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("PjSp_"))
                File.Delete(_path);
        }

        string filePath = tempPath + @"\PjSp_" + e.FileName;

        e.CopyTo(filePath);

        ((UploadAttachments)sender).InsertButton.Enabled = false;
    }

    protected void Uploader_AttachmentRemoveClicked(object sender, AttachmentItemEventArgs args)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("PjSp_"))
                File.Delete(_path);
        }

        ((UploadAttachments)sender).InsertButton.Enabled = true;
    }
    

}