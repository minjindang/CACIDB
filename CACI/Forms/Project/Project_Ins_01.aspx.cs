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

public partial class Project_Ins_01 : IMMDInsertUI
{

    /*
     * grv_Score
     * grv_PjStage
     */

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
            case "grv_PjStage":

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
            case "grv_PjStage":

                break;
            default:

                break;
        }
    }

    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();
        to.setValue("Pj_Kind", "A");
        if (!string.IsNullOrEmpty(lbl_PjSp_Code.Text))
        {
            if (!to.isColumnExist("PjSp_Code"))
                to.setValue("PjSp_Code", lbl_PjSp_Code.Text);
        }
        if (!to.isColumnExist("Pj_BgnDate"))
            to.setValue("Pj_BgnDate", Project_01BL.chgChnDateToEnDate(txt_Pj_BgnDate.Text));
        if (!to.isColumnExist("Pj_EndDate"))
            to.setValue("Pj_EndDate", Project_01BL.chgChnDateToEnDate(txt_Pj_EndDate.Text));
        if (!to.isColumnExist("Pj_StartDate"))
            to.setValue("Pj_StartDate", Project_01BL.chgChnDateToEnDate(txt_Pj_StartDate.Text));
        if (!to.isColumnExist("Pj_Fund"))
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

        //if (RadAsyncUpload1.UploadedFiles.Count > 0)
        //    to.setValue("Pj_PjFile", RadAsyncUpload1.UploadedFiles[0].FileName);

        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (Directory.Exists(tempPath))
        {
            foreach (string _path in Directory.GetFiles(tempPath))
            {
                if (new FileInfo(_path).Name.StartsWith("PF_"))
                {
                    to.setValue("Pj_PjFile", _path);
                }
            }
        }

        if (!to.isColumnExist("Pj_PjFill"))
        {
            to.setValue("Pj_PjFill", ddl_Pj_PjFill.SelectedValue);
        }

        return to;
    }

    public override DataTO PopulateDetailData(string DetailGridViewID)
    {
        DataTO to = new DataTO();

        switch (DetailGridViewID)
        {
            case "grv_Score":
                if (hid_Score_Code.Value == "")
                    to.setValue("Score_Code", ICommonBL.getNewSerialNo(DataBase.CACIDB, "JC"));
                else
                    to.setValue("Score_Code", hid_Score_Code.Value);

                to.setValue("Score_Items", txt_Score_Items.Text);
                to.setValue("Score_Max", txt_Score_Max.Text);
                to.setValue("Score_Percent", txt_Score_Percent.Text);
                break;
            case "grv_PjStage":
                to.setValue("OStage_Index", Old_Stage_Index.Value);
                to.setValue("Stage_Index", ddl_Stage_Index.SelectedValue);
                to.setValue("Stage_Date", txt_Stage_Date.Text);

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
                to.setValue("Stage_IsMeeting", ddl_Stage_IsMeeting.SelectedValue);
                to.setValue("Stage_MtKind", ddl_Stage_MtKind.SelectedValue);
                to.setValue("Stage_RmFlag", ddl_Stage_RmFlag.SelectedValue);

                string Stage_RmEmpl = "";

                Stage_RmEmpl += chl_Stage_RmEmpl.Items[0].Selected ? "1" : "0";
                Stage_RmEmpl += chl_Stage_RmEmpl.Items[1].Selected ? "1" : "0";
                Stage_RmEmpl += chl_Stage_RmEmpl.Items[2].Selected ? "1" : "0";

                to.setValue("Stage_RmEmpl", Stage_RmEmpl);
                to.setValue("Stage_RmDays", txt_Stage_RmDays.Text);
                to.setValue("Stage_RmText", txt_Stage_RmText.Text);

                break;
            case "grv_CommGroup":
                to.setValue("CmGp_Num", this.ddl_CmGp_Num.SelectedValue);
                to.setValue("CmGp_Name", txt_CmGp_Name.Text);
                to.setValue("CmGp_NumName", this.ddl_CmGp_Num.SelectedItem.Text);
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
            case "grv_PjStage":

                txt_Stage_Name.Text = to.getValue("Stage_Name").ToString();
                Old_Stage_Index.Value = to.getValue("Stage_Index").ToString();
                ddl_Stage_Index.SelectedValue = to.getValue("Stage_Index").ToString();
                txt_Stage_Date.Text = to.getValue("Stage_Date").ToString();

                switch (to.getValue("Stage_Kind").ToString())
                {
                    case "1":
                        rad_Stage_Kind_1.Checked = true;
                        break;
                    case "2":
                        rad_Stage_Kind_2.Checked = true;
                        break;
                    case "3":
                        rad_Stage_Kind_3.Checked = true;
                        break;
                    case "4":
                        rad_Stage_Kind_4.Checked = true;
                        break;
                }

                txt_Stage_Text.Text = to.getValue("Stage_Text").ToString();
                ddl_Stage_IsMeeting.SelectedValue = to.getValue("Stage_IsMeeting").ToString();
                ddl_Stage_MtKind.SelectedValue = to.getValue("Stage_MtKind").ToString();
                ddl_Stage_RmFlag.SelectedValue = to.getValue("Stage_RmFlag").ToString();

                if (to.getValue("Stage_RmEmpl").ToString().Substring(0, 1) == "1")
                    chl_Stage_RmEmpl.Items[0].Selected = true;
                if (to.getValue("Stage_RmEmpl").ToString().Substring(1, 1) == "1")
                    chl_Stage_RmEmpl.Items[1].Selected = true;
                if (to.getValue("Stage_RmEmpl").ToString().Substring(2, 1) == "1")
                    chl_Stage_RmEmpl.Items[2].Selected = true;

                txt_Stage_RmText.Text = to.getValue("Stage_RmText").ToString();

                break;
            case "grv_CommGroup":
                ddl_CmGp_Num.SelectedValue = to.getValue("CmGp_Num").ToString();
                txt_CmGp_Name.Text = to.getValue("CmGp_Name").ToString();
                break;
            default:
                break;

        }
    }

    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "專案設定作業─獎補助專案資料新增畫面";
        ProgNum = "1.2.3";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grv_Score, pnlGridView, lblPage, btnDTL_INSERT, btnDTL_UPDATE, btnDTL_CLEAR, "grv_Score"));
        addDetailMember(new DetailDataGroup(grv_PjStage, pnlGridView2, lblPage2, btnDTL_INSERT2, btnDTL_UPDATE2, btnDTL_CLEAR2, "grv_PjStage"));
        addDetailMember(new DetailDataGroup(grv_CommGroup, pnlGridView3, lblPage3, btnDTL_INSERT3, btnDTL_UPDATE3, btnDTL_CLEAR3, "grv_CommGroup"));

        #endregion

        #region 宣告資訊
        BL = new Project_01BL();
        #endregion

        #region 按鈕定義 start

        InsertButton = btn_Insert;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊
        BackPage = "Project_Qry_01.aspx";
        #endregion

        #region 頁面檢查設定
        checkLoginType = checkLoginType.Need;
        #endregion
    }

    public override void SetDefault()
    {
        //專案經費
        txt_Pj_Fund.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();
        txt_Pj_Fund.Text = "0";
        BaseFun bf = new BaseFun();
        ddl_UserDep.DataSource = bf.getUserDep();
        ddl_UserDep.DataBind();

        ddl_CmGp_Num.DataSource = bf.getSysCodeByKind("P", "G");
        ddl_CmGp_Num.DataTextField = "Sys_CdText";
        ddl_CmGp_Num.DataValueField = "Sys_CdCode";
        ddl_CmGp_Num.DataBind();

        ddl_Stage_MtKind.DataSource = bf.getMeetingType("A", false);
        ddl_Stage_MtKind.DataBind();

        //申請表單樣式
        ddl_Pj_PjFill.DataSource = bf.getPjClass();
        ddl_Pj_PjFill.DataTextField = "PjCs_Name";
        ddl_Pj_PjFill.DataValueField = "PjCs_Code";
        ddl_Pj_PjFill.DataBind();

        DataTO to = (DataTO)Session[ICommonUI.Web_ID + Session.SessionID + "Project_Ins_01"];
        if (to != null && to.getValue("PjSp_Code").ToString() != "")
        {
            DataTable dt = bf.getTableData("PjSamples", to);
            lbl_PjSp_Code.Text = to.getValue("PjSp_Code").ToString();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl_UserDep.SelectedValue = bf.getUserDep(dt.Rows[0]["PjSp_User_Code"].ToString());
                ddl_UserAcc.DataSource = bf.getDepUser(ddl_UserDep.SelectedValue);
                ddl_UserAcc.DataBind();
                ddl_UserAcc.SelectedValue = dt.Rows[0]["PjSp_User_Code"].ToString();
                txt_Pj_Name.Text = dt.Rows[0]["PjSp_Name"].ToString();
                ddl_Pj_Trans.SelectedValue = dt.Rows[0]["PjSp_Trans"].ToString();
                Editor1.Content = dt.Rows[0]["PjSp_WebExp"].ToString();
                txt_Pj_PjIntro.Text = dt.Rows[0]["PjSp_PjIntro"].ToString();
                txt_Pj_PjNote.Text = dt.Rows[0]["PjSp_Note"].ToString();
                //rad_Pj_PjFill_1.Checked = true;
                ddl_Pj_PjFill.SelectedValue = dt.Rows[0]["PjSp_PjFill"].ToString();
                string pj_SpCode = dt.Rows[0].ItemArray[0].ToString();
                DataTable stageDt = bf.getTableData("SmpStage", to);
                stageDt.Columns.Add(new DataColumn("Stage_Date"));
                for (int i = 0; i < stageDt.Columns.Count; i++)
                {
                    if (stageDt.Columns[i].ColumnName.IndexOf("Sp") == 0)
                        stageDt.Columns[i].ColumnName = stageDt.Columns[i].ColumnName.Substring(2);
                }
                RenderDetailGridViewData("grv_PjStage", stageDt);
                int stmpCount = stageDt.Rows.Count;
                ddl_Stage_Index.Items.Clear();
                for (int i = 0; i < stmpCount; i++)
                {
                    this.ddl_Stage_Index.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
                }
                ddl_Stage_Index.Items.Add(new ListItem((stmpCount + 1).ToString(), (stmpCount + 1).ToString()));
                DataTO scoreTo = new DataTO();
                scoreTo.setValue("Score_PjCode", to.getValue("PjSp_Code"));
                DataTable scoreDt = bf.getTableData("Score", scoreTo);
                RenderDetailGridViewData("grv_Score", scoreDt);
            }
        }

        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);
        else
        {
            Directory.Delete(tempPath, true);
            Directory.CreateDirectory(tempPath);
        }
    }

    protected override void AfterHandleInsert(DataTO to, DataSet ds)
    {
        base.AfterHandleInsert(to, ds);

        if (to.getValue("Pj_Code") != null && to.getValue("Pj_Code").ToString() != "")
        {
            string targetPath = Request.PhysicalApplicationPath + @"UploadFile\Project\" + to.getValue("Pj_Code");
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

        //hideImgBtn(btn_Insert);
    }

    protected override void AfterHandleDetailInsert(DetailDataGroup dataGridGroup)
    {
        base.AfterHandleDetailInsert(dataGridGroup);

        switch (dataGridGroup.DataGridView.ID)
        {
            case "grv_Score":

                hid_Score_Code.Value = "";

                break;
            case "grv_PjStage":
                Old_Stage_Index.Value = "";
                // 階段順序多增加一個
                ddl_Stage_Index.Items.Add(new ListItem((ddl_Stage_Index.Items.Count).ToString(), (ddl_Stage_Index.Items.Count).ToString()));
                ddl_Stage_Index.SelectedValue = (ddl_Stage_Index.Items.Count - 1).ToString();

                break;
        }
    }

    protected override bool BeforeHandleInsert(DataTO ito, DataSet ds)
    {
        if (string.IsNullOrEmpty(ito.getValue("Pj_WebExp").ToString()))
        {
            string errMsg = "外網專案說明：欄位不得為空";
            ShowMsgBox(this.Page, errMsg);

            return false;
        }
        else 
        {
            return true;        
        }
    }

    protected override bool BeforeHandleDetailInsert(DetailDataGroup dataGridGroup, DataTO to, System.Data.DataTable dt)
    {
        if (base.BeforeHandleDetailInsert(dataGridGroup, to, dt))
        {
            if (dataGridGroup.DataGridView.ID == "grv_PjStage")
            {
                string errMsg = "";

                if (!checkStageData(out errMsg))
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
            if (dataGridGroup.DataGridView.ID == "grv_PjStage")
            {
                string errMsg = "";

                if (!checkStageData(out errMsg))
                {
                    ShowMsgBox(this, errMsg);
                    return false;
                }

                int OStage_Index = Convert.ToInt32(to.getValue("OStage_Index").ToString());
                int Stage_Index = Convert.ToInt32(to.getValue("Stage_Index").ToString());

                if (OStage_Index > Stage_Index)
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

                        rows[rows.Length - 1]["Stage_Index"] = Stage_Index.ToString();
                    }
                }
                else if (OStage_Index < Stage_Index)
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

                        rows[0]["Stage_Index"] = Stage_Index.ToString();
                    }
                }

                dt.DefaultView.Sort = "Stage_Index ASC";
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
            case "grv_PjStage":
                Old_Stage_Index.Value = "";

                break;
        }
    }

    protected void ddl_UserDep_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_UserAcc.DataSource = new BaseFun().getDepUser(ddl_UserDep.SelectedValue);
        ddl_UserAcc.DataBind();
    }

    private bool checkStageData(out string Msg)
    {
        Msg = "";
        if (rad_Stage_Kind_2.Checked && txt_Stage_Date.Text == "")
            Msg += "\\r\\n請輸入【執行日期】";

        if (ddl_Stage_IsMeeting.SelectedValue == "Y" && ddl_Stage_MtKind.SelectedValue == "")
            Msg += "\\r\\n請選擇【會議性質】";

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

    protected void grv_CommGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            ((ImageButton)e.Row.Cells[0].Controls[0]).OnClientClick = "if(!confirm('確定要刪除?')){return false;}";
        e.Row.Cells[4].Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    protected void txt_Pj_StartDate_TextChanged(object sender, EventArgs e)
    {
        // 當輸入值後，依照各階段日差，更新各階段執行日期

        if (txt_Pj_StartDate.Text != "")
        {
            DateTime preStepDate = Convert.ToDateTime(ICommonBL.chgChnDateToEnDate(txt_Pj_StartDate.Text));

            DataTable dt = ICommonUI.GridView2DataTable(grv_PjStage);

            foreach (DataRow row in dt.Rows)
            {
                int stageDays = 0;
                if (!string.IsNullOrEmpty(row["Stage_Days"].ToString()))
                    stageDays = Convert.ToInt16(row["Stage_Days"]);
                row["Stage_Date"] = ICommonBL.chgEnDateToChnDate(preStepDate.AddDays(stageDays).ToString("yyyy/MM/dd"));

                preStepDate = preStepDate.AddDays(stageDays);
            }

            RenderDetailGridViewData("grv_PjStage", dt);

            UpdatePanel3.Update();
        }
    }

    protected void Uploader_Pj_PjFile_FileUploaded(object sender, UploaderEventArgs e)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("PF_"))
                File.Delete(_path);
        }

        string filePath = tempPath + @"\PF_" + e.FileName;

        e.CopyTo(filePath);

        ((UploadAttachments)sender).InsertButton.Enabled = false;
    }

    protected void Uploader_Pj_PjFile_AttachmentRemoveClicked(object sender, AttachmentItemEventArgs args)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith("PF_"))
                File.Delete(_path);
        }

        ((UploadAttachments)sender).InsertButton.Enabled = true;
    }

    protected void grv_PjStage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BaseFun bf = new BaseFun();
            //會議性質
            e.Row.Cells[8].Text = bf.getMeetingTypeName(e.Row.Cells[14].Text);
            //提醒人員
            e.Row.Cells[11].Text = bf.getRmEmpl(e.Row.Cells[15].Text);
            //階段類型
            e.Row.Cells[4].Text = bf.getSysCodeValue("S", "K", e.Row.Cells[16].Text);
        }
        e.Row.Cells[14].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[15].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[16].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[17].Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        DataTO queryTo = new DataTO();
        queryTo.setValue("PjCs_Code", this.ddl_Pj_PjFill.SelectedValue);
        DataTable dt = new BaseFun().getTableData("PjClass", queryTo);
        if (dt != null && dt.Rows.Count > 0)
        {
            DataTO paramTo = new DataTO();
            paramTo.setValue("IsPreview","Y");
            Session[ICommonUI.Web_ID + Session.SessionID + string.Format("Allowance_Ins_{0}", this.ddl_Pj_PjFill.SelectedValue.PadLeft(2,'0'))] = paramTo;
            //GoURL("\\CACI\\Forms\\Project\\Project_Ins_01.aspx");
            OpenURL(this.Page, string.Format("/CACI/{0}", dt.Rows[0]["PjCs_Path"].ToString()));
        }
    }

    
}