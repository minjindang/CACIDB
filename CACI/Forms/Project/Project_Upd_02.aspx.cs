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
using System.Data;
using System.IO;
using CuteWebUI;

public partial class Project_Upd_02 : IMDUpdateUI
{

    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();
        to.setValue("Pj_Kind", "B");
        to.setValue("Pj_Code", this.lbl_Pj_Code.Text);
        to.setValue("Pj_BgnDate", Project_01BL.chgChnDateToEnDate(txt_Pj_BgnDate.Text));
        to.setValue("Pj_EndDate", Project_01BL.chgChnDateToEnDate(txt_Pj_EndDate.Text));
        to.setValue("Pj_StartDate", Project_01BL.chgChnDateToEnDate(txt_Pj_StartDate.Text));
        if (to.isColumnExist("Pj_Fund"))
            to.setValue("Pj_Fund", Convert.ToInt32(this.txt_Pj_Fund.Text.Replace(",", string.Empty)) * 1000);
        if (!to.isColumnExist("Pj_Name"))
            to.setValue("Pj_Name", txt_Pj_Name.Text);
        if (!to.isColumnExist("Pj_Trans"))
            to.setValue("Pj_Trans", ddl_Pj_Trans.SelectedValue);
        if (!to.isColumnExist("Pj_User_Code"))
            to.setValue("Pj_User_Code", ddl_UserAcc.SelectedValue);
        if (!to.isColumnExist("Pj_WebExp"))
            to.setValue("Pj_WebExp", Editor1.Content);
        if (!to.isColumnExist("Pj_PjIntro"))
            to.setValue("Pj_PjIntro", txt_Pj_PjIntro.Text);
        if (!to.isColumnExist("Pj_PjNote"))
            to.setValue("Pj_PjNote", txt_Pj_PjNote.Text);

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

        //if (RadAsyncUpload1.UploadedFiles.Count > 0)
        //    to.setValue("Pj_PjFile", RadAsyncUpload1.UploadedFiles[0].FileName);
        //輔導專案
        to.setValue("Pj_PjFill", "6");
        return to;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "專案設定作業─輔導專案資料修改";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "1.2.7";
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

        BL = new Project_01BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        string[] exPage = Request.ServerVariables["HTTP_REFERER"].ToString().Split('/');

        if (exPage[exPage.Length - 1] == "Announcement_Lis_02.aspx" || hid_From_Page.Value == "Announcement_Lis_02.aspx")
        {
            hid_From_Page.Value = "Announcement_Lis_02.aspx";
            BackPage = "Announcement_Lis_02.aspx";
        }
        else
            BackPage = "Project_Qry_01.aspx";

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
        //專案經費
        txt_Pj_Fund.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        BaseFun bf = new BaseFun();
        ddl_UserDep.DataSource = bf.getUserDep();
        ddl_UserDep.DataBind();

        //ddl_Stage_MtKind.DataSource = bf.getMeetingType("B", false);
        //ddl_Stage_MtKind.DataBind();

        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);
        else
        {
            Directory.Delete(tempPath, true);
            Directory.CreateDirectory(tempPath);
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
        to.setValue("OStage_Index", Old_Stage_Index.Value);
        to.setValue("Stage_Index", ddl_Stage_Index.SelectedValue);
        to.setValue("Stage_Date", Project_01BL.chgChnDateToEnDate(txt_Stage_Date.Text));
        if (rad_Stage_Kind_1.Checked)
            to.setValue("Stage_Kind", "1");
        else if (rad_Stage_Kind_2.Checked)
            to.setValue("Stage_Kind", "2");
        else if (rad_Stage_Kind_3.Checked)
            to.setValue("Stage_Kind", "3");
        else if (rad_Stage_Kind_4.Checked)
            to.setValue("Stage_Kind", "4");
        to.setValue("Stage_Name", txt_Stage_Name.Text);
        to.setValue("Stage_Text", txt_Stage_Text.Text);
        string Stage_RmEmpl = "";
        Stage_RmEmpl += chl_Stage_RmEmpl.Items[0].Selected ? "1" : "0";
        Stage_RmEmpl += chl_Stage_RmEmpl.Items[1].Selected ? "1" : "0";
        Stage_RmEmpl += chl_Stage_RmEmpl.Items[2].Selected ? "1" : "0";
        to.setValue("Stage_RmEmpl", Stage_RmEmpl);
        to.setValue("Stage_RmDays", txt_Stage_RmDays.Text);
        to.setValue("Stage_RmText", txt_Stage_RmText.Text);
        to.setValue("Stage_RmFlag", ddl_Stage_RmFlag.SelectedValue);
        return to;
    }

    protected void ddl_UserDep_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_UserAcc.DataSource = new BaseFun().getDepUser(ddl_UserDep.SelectedValue);
        ddl_UserAcc.DataBind();
    }

    public override void RenderDetailData(DataTO to)
    {
        txt_Stage_Name.Text = to.getValue("Stage_Name").ToString();
        Old_Stage_Index.Value = to.getValue("Stage_Index").ToString();
        ddl_Stage_Index.SelectedValue = to.getValue("Stage_Index").ToString();
        txt_Stage_Date.Text = to.getValue("Stage_Date").ToString();
        txt_Stage_Text.Text = to.getValue("Stage_Text").ToString();
        //ddl_Stage_IsMeeting.SelectedValue = to.getValue("Stage_IsMeeting").ToString();
        //ddl_Stage_MtKind.SelectedValue = to.getValue("Stage_MtKind").ToString();
        txt_Stage_RmDays.Text = to.getValue("Stage_RmDays").ToString();
        ddl_Stage_RmFlag.SelectedValue = to.getValue("Stage_RmFlag").ToString();
        if (to.getValue("Stage_RmEmpl").ToString().Substring(0, 1) == "1")
            chl_Stage_RmEmpl.Items[0].Selected = true;
        if (to.getValue("Stage_RmEmpl").ToString().Substring(1, 1) == "1")
            chl_Stage_RmEmpl.Items[1].Selected = true;
        if (to.getValue("Stage_RmEmpl").ToString().Substring(2, 1) == "1")
            chl_Stage_RmEmpl.Items[2].Selected = true;
        txt_Stage_RmText.Text = to.getValue("Stage_RmText").ToString();
    }

    public override void RenderData(DataTO to)
    {
        lbl_Pj_Code.Text = to.getValue("Pj_Code").ToString();
        this.txt_Pj_BgnDate.Text = Project_01BL.chgEnDateToChnDate(to.getValue("Pj_BgnDate").ToString().Split(' ')[0]);
        this.txt_Pj_EndDate.Text = Project_01BL.chgEnDateToChnDate(to.getValue("Pj_EndDate").ToString().Split(' ')[0]);
        this.txt_Pj_StartDate.Text = Project_01BL.chgEnDateToChnDate(to.getValue("Pj_StartDate").ToString().Split(' ')[0]);
        this.txt_Pj_Fund.Text = (Convert.ToInt32(to.getValue("Pj_Fund").ToString()) / 1000).ToString();
        txt_Pj_Name.Text = to.getValue("Pj_Name").ToString();
        ddl_Pj_Trans.SelectedValue = to.getValue("Pj_Trans").ToString();
        ddl_UserDep.SelectedValue = new BaseFun().getUserDep(to.getValue("Pj_User_Code").ToString());
        ddl_UserAcc.DataSource = new BaseFun().getDepUser(ddl_UserDep.SelectedValue);
        ddl_UserAcc.DataBind();
        ddl_UserAcc.SelectedValue = to.getValue("Pj_User_Code").ToString();
        Editor1.Content = to.getValue("Pj_WebExp").ToString();
        txt_Pj_PjIntro.Text = to.getValue("Pj_PjIntro").ToString();
        txt_Pj_PjNote.Text = to.getValue("Pj_PjNote").ToString();

        if (to.getValue("Pj_PjFile").ToString() != "")
        {
            // TODO:顯示以上傳資訊
        }
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Pj_Code");
    }

    protected void Uploader_1_FileUploaded(object sender, UploaderEventArgs e)
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

    protected void Uploader_1_AttachmentRemoveClicked(object sender, AttachmentItemEventArgs args)
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

    //protected void RadAsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    //{
    //    //string tempPath = Request.PhysicalApplicationPath + @"UploadFile\Project\" + lbl_Pj_Code.Text;

    //    //if (!Directory.Exists(tempPath))
    //    //    Directory.CreateDirectory(tempPath);
    //    //else
    //    //{
    //    //    Directory.Delete(tempPath, true);
    //    //    Directory.CreateDirectory(tempPath);
    //    //}

    //    //string filePath = tempPath + @"\" + e.File.FileName;

    //    //e.File.SaveAs(filePath);
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

    protected override void AfterHandleUpdate()
    {
        base.AfterHandleUpdate();
        //if (!string.IsNullOrEmpty(this.lbl_Pj_Code.Text))
        //{
        //    string targetPath = Request.PhysicalApplicationPath + @"UploadFile\Project\" + lbl_Pj_Code.Text;
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

    protected override bool BeforeHandleUpdate(DataTO mto, DataTable dt)
    {
        if (string.IsNullOrEmpty(mto.getValue("Pj_WebExp").ToString()))
        {
            string errMsg = "外網專案說明：欄位不得為空";
            ShowMsgBox(this.Page, errMsg);

            return true;
        }
        else
        {
            return false;
        }
    }

    protected void pnlGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BaseFun bf = new BaseFun();
            //提醒人員
            e.Row.Cells[8].Text = bf.getRmEmpl(e.Row.Cells[11].Text);
            //階段類型
            e.Row.Cells[4].Text = bf.getSysCodeValue("S", "K", e.Row.Cells[12].Text);
        }
        e.Row.Cells[11].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[12].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[13].Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    protected override void AfterRenderDetailData(System.Data.DataTable dt)
    {
        base.AfterRenderDetailData(dt);
        int count = dt.Rows.Count;
        ddl_Stage_Index.Items.Clear();
        for (int i = 0; i < count; i++)
        {
            ddl_Stage_Index.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
        }

        ddl_Stage_Index.Items.Add(new ListItem((count + 1).ToString(), (count + 1).ToString()));

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

            if (dt.Rows.Count > 0 && Convert.ToInt32(to.getValue("Stage_Index").ToString()) <= Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Stage_Index"].ToString()))
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (Convert.ToInt32(dt.Rows[i]["Stage_Index"].ToString()) >= Convert.ToInt32(to.getValue("Stage_Index").ToString()))
                    {
                        dt.Rows[i]["Stage_Index"] = (Convert.ToInt32(dt.Rows[i]["Stage_Index"].ToString()) + 1).ToString();
                    }
                }
            }

            dt.DefaultView.Sort = "Stage_Index ASC";

            return true;
        }
        else
            return false;
    }

    private bool checkSmpStageData(out string Msg)
    {
        Msg = "";
        if (string.IsNullOrEmpty(txt_Stage_Date.Text))
            Msg += "\\r\\n請輸入【執行日期】";

        if (ddl_Stage_RmFlag.SelectedValue == "Y")
        {
            if (chl_Stage_RmEmpl.Items[0].Selected == false &&
                chl_Stage_RmEmpl.Items[1].Selected == false &&
                chl_Stage_RmEmpl.Items[2].Selected == false)
            {
                Msg += "\\r\\n請選擇【提醒人員】";
            }

            if (txt_Stage_RmDays.Text == "")
                Msg += "\\r\\n請輸入【提前提醒天數】";

            if (txt_Stage_RmText.Text == "")
                Msg += "\\r\\n請輸入【提醒文稿】";
        }

        if (Msg != "")
            return false;
        else
            return true;
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

            int OSpStage_Index = Convert.ToInt32(to.getValue("OStage_Index").ToString());
            int SpStage_Index = Convert.ToInt32(to.getValue("Stage_Index").ToString());

            if (OSpStage_Index > SpStage_Index)
            {
                // 當階段往上更動時

                DataRow[] rows = dt.Select("Stage_Index >= '" + to.getValue("Stage_Index").ToString() + "' AND Stage_Index <= '" + to.getValue("OStage_Index").ToString() + "' ");

                if (rows.Length > 0)
                {
                    rows[rows.Length - 1]["Stage_Index"] = "T";

                    for (int i = 0; i < rows.Length - 1; i++)
                    {
                        rows[i]["Stage_Index"] = (Convert.ToInt32(rows[i]["Stage_Index"].ToString()) + 1);
                    }

                    rows[rows.Length - 1]["Stage_Index"] = SpStage_Index.ToString();
                }
            }
            else if (OSpStage_Index < SpStage_Index)
            {
                // 當階段往下更動時

                DataRow[] rows = dt.Select("Stage_Index >= '" + to.getValue("OStage_Index").ToString() + "' AND Stage_Index <= '" + to.getValue("Stage_Index").ToString() + "' ");

                if (rows.Length > 0)
                {
                    rows[0]["Stage_Index"] = "T";

                    for (int i = 1; i < rows.Length; i++)
                    {
                        rows[i]["Stage_Index"] = (Convert.ToInt32(rows[i]["Stage_Index"].ToString()) - 1);
                    }

                    rows[0]["Stage_Index"] = SpStage_Index.ToString();
                }
            }

            dt.DefaultView.Sort = "Stage_Index ASC";

            return true;
        }
        else
            return false;
    }

    protected void txt_Pj_StartDate_TextChanged(object sender, EventArgs e)
    {
        if (txt_Pj_StartDate.Text != "")
        {
            DateTime preStepDate = Convert.ToDateTime(ICommonBL.chgChnDateToEnDate(txt_Pj_StartDate.Text));

            DataTable dt = ICommonUI.GridView2DataTable(grvQuery);

            foreach (DataRow row in dt.Rows)
            {
                int stageDays = 0;
                if (!string.IsNullOrEmpty(row["Stage_Days"].ToString()))
                    stageDays = Convert.ToInt16(row["Stage_Days"]);
                row["Stage_Date"] = ICommonBL.chgEnDateToChnDate(preStepDate.AddDays(stageDays).ToString("yyyy/MM/dd"));

                preStepDate = preStepDate.AddDays(stageDays);
            }

            RenderDetailData(dt);
            UpdatePanel4.Update();
            //UpdatePanel3.Update();
        }
    }
}