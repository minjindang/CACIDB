using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.DB;
using CuteWebUI;

public partial class PjSamples_Upd_01 : IMMDUpdateUI
{
    public override ROW_CMD_TYPE GetRowCommand(string DetailGridViewID, string strCmd)
    {
        switch (DetailGridViewID)
        {
            case "grv_Score":

                switch (strCmd)
                {
                    case "mod":
                        return ROW_CMD_TYPE.ROW_CMD_MODIFY;
                        break;
                    case "del":
                        return ROW_CMD_TYPE.ROW_CMD_DELETE;
                        break;
                    default:
                        return ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
                        break;
                }
                break;
            case "grv_SmpStage":

                switch (strCmd)
                {
                    case "select":
                        return ROW_CMD_TYPE.ROW_CMD_MODIFY;
                        break;
                    case "del":
                        return ROW_CMD_TYPE.ROW_CMD_DELETE;
                        break;
                    default:
                        return ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
                        break;
                }
                break;
            default:
                return ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
                break;
        }


    }

    protected override void InitialDetail(string dataGridViewID)
    {
        switch (dataGridViewID)
        {
            case "grv_Score":
                txt_Score_Max.Text = "100";
                break;
            case "grv_SmpStage":
                
                break;
            default:
                
                break;
        }
    }

    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        to.setValue("PjSp_Code", lbl_PjSp_Code.Text);
        to.setValue("PjSp_Kind", "A");
        to.setValue("PjSp_Name", txt_PjSp_Name.Text);
        to.setValue("PjSp_Trans", ddl_PjSp_Trans.SelectedValue);
        to.setValue("PjSp_User_Code", ddl_UserAcc.SelectedValue);
        to.setValue("PjSp_WebExp", Editor1.Content);
        to.setValue("PjSp_PjIntro", txt_PjSp_PjIntro.Text);
        to.setValue("PjSp_PjNote", txt_PjSp_PjNote.Text);

        //Add temp to TO
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;
        if (Directory.Exists(tempPath))
        {
            foreach (string _path in Directory.GetFiles(tempPath))
            {
                if (new FileInfo(_path).Name.StartsWith("PjSp_"))
                {
                    to.setValue("PjSp_PjFile", _path);
                    break;
                }
            }
        }

        to.setValue("PjSp_PjFill", ddl_Pj_PjFill.SelectedValue);

        return to;
    }

    public override DataTO PopulateDetailData(string DetailGridViewID)
    {
        DataTO to = new DataTO();

        switch (DetailGridViewID)
        {
            case "grv_Score":
                if (hid_Score_Code.Value == "")
                    to.setValue("Score_Code", ICommonBL.getNewSerialNo( DataBase.CACIDB,"PC"));
                else
                    to.setValue("Score_Code", hid_Score_Code.Value);

                to.setValue("Score_Items", txt_Score_Items.Text);
                to.setValue("Score_Max", txt_Score_Max.Text);
                to.setValue("Score_Percent", txt_Score_Percent.Text);
                break;
            case "grv_SmpStage":
                to.setValue("OSpStage_Index", Old_SpStage_Index.Value);
                to.setValue("SpStage_Index", ddl_SpStage_Index.SelectedValue);
                
                if( rad_SpStage_Kind_1.Checked )
                    to.setValue("SpStage_Kind", "1");
                else if (rad_SpStage_Kind_2.Checked)
                {
                    to.setValue("SpStage_Kind", "2");
                    to.setValue("SpStage_Days", txt_SpStage_Days.Text);
                }
                else if (rad_SpStage_Kind_3.Checked)
                    to.setValue("SpStage_Kind", "3");
                else if (rad_SpStage_Kind_4.Checked)
                    to.setValue("SpStage_Kind", "4");

                to.setValue("SpStage_Name", txt_SpStage_Name.Text);
                to.setValue("SpStage_Text", txt_SpStage_Text.Text);
                to.setValue("SpStage_IsMeeting", ddl_SpStage_IsMeeting.SelectedValue);
                to.setValue("SpStage_MtKind", ddl_SpStage_MtKind.SelectedValue);
                to.setValue("SpStage_RmFlag", ddl_SpStage_RmFlag.SelectedValue);

                string SpStage_RmEmpl = "";

                SpStage_RmEmpl += chl_SpStage_RmEmpl.Items[0].Selected ? "1" : "0";
                SpStage_RmEmpl += chl_SpStage_RmEmpl.Items[1].Selected ? "1" : "0";
                SpStage_RmEmpl += chl_SpStage_RmEmpl.Items[2].Selected ? "1" : "0";

                to.setValue("SpStage_RmEmpl", SpStage_RmEmpl);
                to.setValue("SpStage_RmDays", txt_SpStage_RmDays.Text);
                to.setValue("SpStage_RmText", txt_SpStage_RmText.Text);

                break;
            default:
                break;

        }
        return to;
    }

    public override void RenderDetailData(string DetailGridViewID, DataTO to)
    {
        switch (DetailGridViewID)
        {
            case "grv_Score":
                
                hid_Score_Code.Value = to.getValue("Score_Code").ToString();
                txt_Score_Items.Text = to.getValue("Score_Items").ToString();
                txt_Score_Max.Text = to.getValue("Score_Max").ToString();
                txt_Score_Percent.Text = to.getValue("Score_Percent").ToString();

                break;
            case "grv_SmpStage":

                txt_SpStage_Name.Text = to.getValue("SpStage_Name").ToString();
                Old_SpStage_Index.Value = to.getValue("SpStage_Index").ToString();
                ddl_SpStage_Index.SelectedValue = to.getValue("SpStage_Index").ToString();

                switch (to.getValue("SpStage_Kind").ToString())
                {
                    case "1" :
                        rad_SpStage_Kind_1.Checked = true;
                        break;
                    case "2" :
                        rad_SpStage_Kind_2.Checked = true;
                        txt_SpStage_Days.Text = to.getValue("SpStage_Days").ToString();
                        break;
                    case "3" :
                        rad_SpStage_Kind_3.Checked = true;
                        break;
                    case "4":
                        rad_SpStage_Kind_4.Checked = true;
                        break;
                }

                txt_SpStage_Text.Text = to.getValue("SpStage_Text").ToString();
                ddl_SpStage_IsMeeting.SelectedValue = to.getValue("SpStage_IsMeeting").ToString();
                ddl_SpStage_MtKind.SelectedValue = to.getValue("SpStage_MtKind").ToString();
                ddl_SpStage_RmFlag.SelectedValue = to.getValue("SpStage_RmFlag").ToString();
                
                if (to.getValue("SpStage_RmEmpl").ToString().Substring(0, 1) == "1")
                    chl_SpStage_RmEmpl.Items[0].Selected = true;
                if (to.getValue("SpStage_RmEmpl").ToString().Substring(1, 1) == "1")
                    chl_SpStage_RmEmpl.Items[1].Selected = true;
                if (to.getValue("SpStage_RmEmpl").ToString().Substring(2, 1) == "1")
                    chl_SpStage_RmEmpl.Items[2].Selected = true;

                txt_SpStage_RmText.Text = to.getValue("SpStage_RmText").ToString();

                break;
            default:
                break;

        }
    }

    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "獎補助範本資料更新畫面";
        ProgNum = "1.1.4";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grv_Score, pnlGridView, lblPage, btnDTL_INSERT, btnDTL_UPDATE, btnDTL_CLEAR, "grv_Score"));
        addDetailMember(new DetailDataGroup(grv_SmpStage, pnlGridView2, lblPage2, btnDTL_INSERT2, btnDTL_UPDATE2, btnDTL_CLEAR2, "grv_SmpStage"));

        #endregion

        #region 宣告資訊
        BL = new PjSamples_01BL();
        #endregion

        #region 按鈕定義 start

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊
        BackPage = "PjSamples_Qry_01.aspx";
        #endregion

        #region 頁面檢查設定
        checkLoginType = checkLoginType.Need;
        #endregion
    }

    public override void SetDefault()
    {
        //ddl_PjSp_Kind.DataSource = new BaseFun().getSysCodeByKind("P", "K");
        //ddl_PjSp_Kind.DataBind();
        BaseFun bf = new BaseFun();
        //申請表單樣式
        ddl_Pj_PjFill.DataSource = bf.getPjClass();
        ddl_Pj_PjFill.DataTextField = "PjCs_Name";
        ddl_Pj_PjFill.DataValueField = "PjCs_Code";
        ddl_Pj_PjFill.DataBind();

        ddl_UserDep.DataSource = bf.getUserDep();
        ddl_UserDep.DataBind();

        ddl_SpStage_MtKind.DataSource = bf.getMeetingType("A", false);
        ddl_SpStage_MtKind.DataBind();

        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);
        else
        {
            Directory.Delete(tempPath, true);
            Directory.CreateDirectory(tempPath);
        }
    }

    protected override void AfterRenderData(DataTO to, DataSet ds)
    {
        base.AfterRenderData(to, ds);

        int stmpCount = ds.Tables["grv_SmpStage"].Rows.Count;

        for (int i = 0; i < stmpCount; i++)
        {
            ddl_SpStage_Index.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
        }

        ddl_SpStage_Index.Items.Add(new ListItem((stmpCount + 1).ToString(), (stmpCount + 1).ToString()));
    }

    protected override void AfterHandleUpdate()
    {
        base.AfterHandleUpdate();
        //if (!string.IsNullOrEmpty(lbl_PjSp_Code.Text))
        //{
        //    string targetPath = Request.PhysicalApplicationPath + @"UploadFile\PjSample\" + lbl_PjSp_Code.Text;
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
                
        //    Directory.Delete(sourcePath, true);
        //    }
        //}
        hideImgBtn(UpdateButton);
    }

    protected override void AfterHandleDetailInsert(DetailDataGroup dataGridGroup)
    {
        base.AfterHandleDetailInsert(dataGridGroup);

        switch (dataGridGroup.DataGridView.ID)
        {
            case "grv_Score":

                hid_Score_Code.Value = "";

                break;
            case "grv_SmpStage":
                Old_SpStage_Index.Value = "";
                // 階段順序多增加一個
                ddl_SpStage_Index.Items.Add(new ListItem((ddl_SpStage_Index.Items.Count).ToString(),(ddl_SpStage_Index.Items.Count).ToString()));
                ddl_SpStage_Index.SelectedValue = (ddl_SpStage_Index.Items.Count - 1).ToString();

                break;
        }
    }

    protected override bool BeforeHandleDetailInsert(DetailDataGroup dataGridGroup, DataTO to, System.Data.DataTable dt)
    {
        if (base.BeforeHandleDetailInsert(dataGridGroup, to, dt))
        {
            if (dataGridGroup.DataGridView.ID == "grv_SmpStage")
            {
                string errMsg = "";

                if (!checkSmpStageData(out errMsg))
                {
                    ShowMsgBox(this.Page,errMsg);

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
            }

            return true;
        }
        else
            return false;
    }


    protected override bool BeforeHandleDetailUpdate(DetailDataGroup dataGridGroup, DataTO to, DataTable dt)
    {
        if (base.BeforeHandleDetailUpdate(dataGridGroup, to, dt))
        {
            if (dataGridGroup.DataGridView.ID == "grv_SmpStage")
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
            }

            return true;
        }
        else
            return false;
    }

    protected override void AfterHandleDetailUpdate(DetailDataGroup dataGridGroup)
    {
        base.AfterHandleDetailUpdate(dataGridGroup);

        switch (dataGridGroup.DataGridView.ID)
        {
            case "grv_Score":

                hid_Score_Code.Value = "";

                break;
            case "grv_SmpStage":
                Old_SpStage_Index.Value = "";
                
                break;
        }
    }

    protected void ddl_UserDep_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_UserAcc.DataSource = new BaseFun().getDepUser(ddl_UserDep.SelectedValue);
        ddl_UserAcc.DataBind();

        UpdatePanel3.Update();
    }

    protected override void SetHandler()
    {
        base.SetHandler();
        ((ScriptManager)this.Master.FindControl("ToolkitScriptManager1")).RegisterPostBackControl(btn_Clear);
        //((ScriptManager)this.Master.FindControl("RadScriptManager1")).RegisterPostBackControl(RadAsyncUpload1);
    }

    private bool checkSmpStageData(out string Msg)
    {
        Msg = "";
        if (rad_SpStage_Kind_2.Checked && txt_SpStage_Days.Text == "")
            Msg += "\\r\\n請輸入【與前階段日差】";

        if( ddl_SpStage_IsMeeting.SelectedValue == "Y" && ddl_SpStage_MtKind.SelectedValue == "")
            Msg += "\\r\\n請選擇【會議性質】";

        if (ddl_SpStage_RmFlag.SelectedValue == "Y")
        {
            if (chl_SpStage_RmEmpl.Items[0].Selected == false &&
                chl_SpStage_RmEmpl.Items[1].Selected == false &&
                chl_SpStage_RmEmpl.Items[2].Selected == false)
            {
                Msg += "\\r\\n請選擇【提醒人員】";
            }

            if( txt_SpStage_RmDays.Text == "" )
                Msg += "\\r\\n請輸入【提前提醒天數】";

            if(txt_SpStage_RmText.Text == "")
                Msg += "\\r\\n請輸入【提醒文稿】";
        }

        if (Msg != "")
            return false;
        else
            return true;
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("PjSp_Code");
    }

    public override void RenderMasterData(DataTO to)
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

        if (to.getValue("PjSp_PjFile").ToString() != "")
        {
            // TODO:顯示以上傳檔案
        }

        ddl_Pj_PjFill.SelectedValue = to.getValue("PjSp_PjFill").ToString();
    }

    //protected void RadAsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    //{
    //    string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;
    //    string filePath = tempPath + @"\" + e.File.FileName;
    //    try
    //    {
    //        e.File.SaveAs(filePath);
    //    }
    //    catch (Exception)
    //    {
            
    //    }
    //}
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