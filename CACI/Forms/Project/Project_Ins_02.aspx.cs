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

public partial class Project_Ins_02 : IMDInsertUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();
        to.setValue("Pj_Kind", "B");
        if (!to.isColumnExist("Pj_BgnDate"))
            to.setValue("Pj_BgnDate", Project_01BL.chgChnDateToEnDate(txt_Pj_BgnDate.Text));
        if (!to.isColumnExist("Pj_EndDate"))
            to.setValue("Pj_EndDate", Project_01BL.chgChnDateToEnDate(txt_Pj_EndDate.Text));
        if (!to.isColumnExist("Pj_StartDate"))
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
                if (new FileInfo(_path).Name.StartsWith("PF_"))
                {
                    to.setValue("Pj_PjFile", _path);
                }
            }
        }
        //輔導專案 
        to.setValue("Pj_PjFill", "6");
        //to.setValue("Pj_PjFile", hyp_Pj_PjFile.NavigateUrl);

        return to;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "專案設定作業─輔導專案資料新增畫面";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "1.2.6";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//訊息列

        DetailGridView = grvQuery;
        PageLabel = lblPage;
        DataPanel = pnlGridView;

        ValidationDetailGroupID = "grvQuery";

        #endregion

        #region 宣告資訊

        BL = new Project_01BL();//邏輯層

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

        DataTO to = (DataTO)Session[ICommonUI.Web_ID + Session.SessionID + "Project_Ins_02"];
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
                string pj_SpCode = dt.Rows[0].ItemArray[0].ToString();
                DataTable stageDt = bf.getTableData("SmpStage", to);
                stageDt.Columns.Add(new DataColumn("Stage_Date"));
                for (int i = 0; i < stageDt.Columns.Count; i++)
                {
                    if (stageDt.Columns[i].ColumnName.IndexOf("Sp") == 0)
                        stageDt.Columns[i].ColumnName = stageDt.Columns[i].ColumnName.Substring(2);
                }
                RenderDetailData(stageDt);
                int stmpCount = stageDt.Rows.Count;
                ddl_Stage_Index.Items.Clear();
                for (int i = 0; i < stmpCount; i++)
                {
                    this.ddl_Stage_Index.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
                }
                ddl_Stage_Index.Items.Add(new ListItem((stmpCount + 1).ToString(), (stmpCount + 1).ToString()));
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
        //to.setValue("Stage_Days", Project_01BL.chgChnDateToEnDate(txt_Stage_Days.Text));
        
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

    /// <summary>
    /// 將選取的Detail資料顯示於畫面
    /// </summary>
    /// <param name="to"></param>
    public override void RenderDetailData(DataTO to)
    {
        txt_Stage_Name.Text = to.getValue("Stage_Name").ToString();
        Old_Stage_Index.Value = to.getValue("Stage_Index").ToString();
        ddl_Stage_Index.SelectedValue = to.getValue("Stage_Index").ToString();
        txt_Stage_Date.Text = to.getValue("Stage_Date").ToString();
        txt_Stage_Text.Text = to.getValue("Stage_Text").ToString();
        ddl_Stage_RmFlag.SelectedValue = to.getValue("Stage_RmFlag").ToString();
        if (to.getValue("Stage_RmEmpl").ToString().Substring(0, 1) == "1")
            chl_Stage_RmEmpl.Items[0].Selected = true;
        if (to.getValue("Stage_RmEmpl").ToString().Substring(1, 1) == "1")
            chl_Stage_RmEmpl.Items[1].Selected = true;
        if (to.getValue("Stage_RmEmpl").ToString().Substring(2, 1) == "1")
            chl_Stage_RmEmpl.Items[2].Selected = true;
        txt_Stage_RmText.Text = to.getValue("Stage_RmText").ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void AfterHandleDetailUpdate()
    {
        base.AfterHandleDetailUpdate();
        //hid_Skill_Code.Value = "N";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="to"></param>
    protected override void AfterHandleInsert(DataTO to)
    {
        base.AfterHandleInsert(to);

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
    }

    /// <summary>
    /// Detail 編輯區初始化
    /// </summary>
    public override void InitialDetail()
    {
        base.InitialDetail();

        //hid_Skill_Code.Value = "N";
        //txt_Ski_Num.Text = "";
        //txt_Skill_Note.Text = "";
    }

    protected void ddl_UserDep_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_UserAcc.DataSource = new BaseFun().getDepUser(ddl_UserDep.SelectedValue);
        ddl_UserAcc.DataBind();
    }

    protected void RadAsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        string filePath = tempPath + @"\" + e.File.FileName;
        try
        {
            e.File.SaveAs(filePath);
        }
        catch (Exception)
        {
        }
    }

    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BaseFun bf = new BaseFun();
            //提醒人員
            e.Row.Cells[8].Text = bf.getRmEmpl(e.Row.Cells[12].Text);
            //階段類型
            e.Row.Cells[4].Text = bf.getSysCodeValue("S", "K", e.Row.Cells[11].Text);
        }
        e.Row.Cells[11].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[12].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[13].Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    protected override void AfterHandleDetailInsert()
    {
        base.AfterHandleDetailInsert();

        Old_Stage_Index.Value = "";
        // 階段順序多增加一個
        ddl_Stage_Index.Items.Add(new ListItem((ddl_Stage_Index.Items.Count).ToString(), (ddl_Stage_Index.Items.Count).ToString()));
        ddl_Stage_Index.SelectedValue = (ddl_Stage_Index.Items.Count - 1).ToString();
    }
    protected void txt_Pj_StartDate_TextChanged(object sender, EventArgs e)
    {
        // 當輸入值後，依照各階段日差，更新各階段執行日期

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

    protected override bool BeforeHandleInsert(DataTO ito, DataTable dt)
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
}