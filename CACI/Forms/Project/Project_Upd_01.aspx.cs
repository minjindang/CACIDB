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

public partial class Project_Upd_01 : IMMDUpdateUI
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
            case "grv_CommGroup":

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

        to.setValue("Pj_Code", lbl_Pj_Code.Text);
        to.setValue("Pj_Kind", "A");
        to.setValue("Pj_BgnDate", Project_01BL.chgChnDateToEnDate(this.txt_Pj_BgnDate.Text));
        to.setValue("Pj_EndDate", Project_01BL.chgChnDateToEnDate(this.txt_Pj_EndDate.Text));
        to.setValue("Pj_StartDate", Project_01BL.chgChnDateToEnDate(this.txt_Pj_StartDate.Text));
        if(!to.isColumnExist("Pj_Fund"))
        to.setValue("Pj_Fund", Convert.ToInt32(this.txt_Pj_Fund.Text.Replace(",", string.Empty)) * 1000);
        to.setValue("Pj_Name", txt_Pj_Name.Text);
        to.setValue("Pj_Trans", ddl_Pj_Trans.SelectedValue);
        to.setValue("Pj_User_Code", ddl_UserAcc.SelectedValue);
        to.setValue("Pj_WebExp", Editor1.Content);
        to.setValue("Pj_PjIntro", txt_Pj_PjIntro.Text);
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
        to.setValue("Pj_PjFill", ddl_Pj_PjFill.SelectedValue);
        return to;
    }

    public override DataTO PopulateDetailData(string DetailGridViewID)
    {
        DataTO to = new DataTO();

        switch (DetailGridViewID)
        {
            case "grv_Score":
                if (hid_Score_Code.Value == "")
                    to.setValue("Score_Code", ICommonBL.getNewSerialNo( DataBase.CACIDB,"JC"));
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
                if( rad_Stage_Kind_1.Checked )
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
                    case "1" :
                        rad_Stage_Kind_1.Checked = true;
                        break;
                    case "2" :
                        rad_Stage_Kind_2.Checked = true;
                        break;
                    case "3" :
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

                txt_Stage_RmDays.Text = to.getValue("Stage_RmDays").ToString();
                txt_Stage_RmText.Text = to.getValue("Stage_RmText").ToString();

                hid_Meeting_Code.Value = to.getValue("Metting_Code").ToString();

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
        ProgNm = "專案設定作業─獎補助專案資料修改畫面";
        ProgNum = "1.2.4";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

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

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊

        string[] exPage = Request.ServerVariables["HTTP_REFERER"].ToString().Split('/');

        if (exPage[exPage.Length - 1] == "Announcement_Lis_02.aspx" || hid_From_Page.Value == "Announcement_Lis_02.aspx")
        {
            hid_From_Page.Value = "Announcement_Lis_02.aspx";
            BackPage = "Announcement_Lis_02.aspx";
        }
        else
            BackPage = "Project_Qry_01.aspx";

        #endregion

        #region 頁面檢查設定
        checkLoginType = checkLoginType.Need;
        #endregion
    }

    public override void SetDefault()
    {
        //ddl_Pj_Kind.DataSource = new BaseFun().getSysCodeByKind("P", "K");
        //ddl_Pj_Kind.DataBind();
        //專案經費
        txt_Pj_Fund.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

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

        int count = ds.Tables["grv_PjStage"].Rows.Count;

        for (int i = 0; i < count; i++)
        {
            ddl_Stage_Index.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
        }

        ddl_Stage_Index.Items.Add(new ListItem((count + 1).ToString(), (count + 1).ToString()));

        if (to.isColumnExist("Stage_Index"))
        {
            TabContainer1.ActiveTab = TabPanel2;

            DataTable grv_PjStage = ds.Tables["grv_PjStage"];

            DataRow[] row = grv_PjStage.Select("Stage_Index=" + to.getValue("Stage_Index").ToString());

            if (row.Length > 0)
            {
                // TODO:顯示 該 Stage_Index 資料    

                DataTO pjStageTo = new DataTO();

                for (int i = 0; i < row[0].ItemArray.Length; i++)
                {
                    pjStageTo.setValue(grv_PjStage.Columns[i].ColumnName, row[0][grv_PjStage.Columns[i].ColumnName].ToString());
                }

                RenderDetailData("grv_PjStage", pjStageTo);

                hideImgBtn(btnDTL_INSERT2);
                hideImgBtn(btnDTL_CLEAR2);
                showImgBtn(btnDTL_UPDATE2);
            }
        }
    }

    protected override void AfterHandleUpdate()
    {
        base.AfterHandleUpdate();
        if (!string.IsNullOrEmpty(this.lbl_Pj_Code.Text))
        {
            string targetPath = Request.PhysicalApplicationPath + @"UploadFile\Project\" + lbl_Pj_Code.Text;
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
                ddl_Stage_Index.Items.Add(new ListItem((ddl_Stage_Index.Items.Count).ToString(),(ddl_Stage_Index.Items.Count).ToString()));
                ddl_Stage_Index.SelectedValue = (ddl_Stage_Index.Items.Count - 1).ToString();

                hid_Meeting_Code.Value = "";

                break;
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
                    ShowMsgBox(this.Page,errMsg);

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

    protected override bool BeforeHandleUpdate(DataTO to, DataSet ds)
    {
        if (string.IsNullOrEmpty(to.getValue("Pj_WebExp").ToString()))
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
                hid_Meeting_Code.Value = "";
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
        if (txt_Stage_Date.Text == "")
            Msg += "\\r\\n請輸入【執行日期】";

        if( ddl_Stage_IsMeeting.SelectedValue == "Y" && ddl_Stage_MtKind.SelectedValue == "")
            Msg += "\\r\\n請選擇【會議性質】";

        if (ddl_Stage_RmFlag.SelectedValue == "Y")
        {
            if (chl_Stage_RmEmpl.Items[0].Selected == false &&
                chl_Stage_RmEmpl.Items[1].Selected == false &&
                chl_Stage_RmEmpl.Items[2].Selected == false)
            {
                Msg += "\\r\\n請選擇【提醒人員】";
            }

            if( txt_Stage_RmDays.Text == "" )
                Msg += "\\r\\n請輸入【提前提醒天數】";

            if(txt_Stage_RmText.Text == "")
                Msg += "\\r\\n請輸入【提醒文稿】";
        }

        if (Msg != "")
            return false;
        else
            return true;
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Pj_Code");
    }
   
    public override void RenderMasterData(DataTO to)
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

        if (Convert.ToDateTime(Project_01BL.chgChnDateToEnDate(txt_Pj_BgnDate.Text) ) < DateTime.Now)
        { 
            hideImgBtn(btn_Update);
        }

        if (to.getValue("Pj_PjFile").ToString() != "")
        {
            //TODO:顯示以上傳檔案
        }
        ddl_Pj_PjFill.SelectedValue = to.getValue("Pj_PjFill").ToString();
        //switch (to.getValue("Pj_PjFill").ToString())
        //{
        //    case "1" :
        //        rad_Pj_PjFill_1.Checked = true;
        //        break;
        //    case "2":
        //        rad_Pj_PjFill_2.Checked = true;
        //        break;
        //    case "3":
        //        rad_Pj_PjFill_3.Checked = true;
        //        break;
        //    case "4":
        //        rad_Pj_PjFill_4.Checked = true;
        //        break;
        //}
    }

    protected void grv_CommGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            ((ImageButton)e.Row.Cells[1].Controls[0]).OnClientClick = "if(!confirm('確定要刪除?')){return false;}";
        e.Row.Cells[4].Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    protected void grv_Score_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            ((ImageButton)e.Row.Cells[1].Controls[0]).OnClientClick = "if(!confirm('確定要刪除?')){return false;}";
    }

    protected void grv_PjStage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BaseFun bf = new BaseFun();
            ((ImageButton)e.Row.Cells[1].Controls[0]).OnClientClick = "if(!confirm('確定要刪除?')){return false;}";
            //會議性質
            e.Row.Cells[8].Text = bf.getMeetingTypeName(e.Row.Cells[14].Text);
            //提醒人員
            e.Row.Cells[10].Text = bf.getRmEmpl(e.Row.Cells[13].Text);
            //階段類型
            e.Row.Cells[4].Text = bf.getSysCodeValue("S", "K", e.Row.Cells[15].Text);
        }
        e.Row.Cells[14].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[15].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[13].Style.Add(HtmlTextWriterStyle.Display, "none");
        e.Row.Cells[16].Style.Add(HtmlTextWriterStyle.Display, "none");
        //if (e.Row.RowType != DataControlRowType.Pager)
        //{
            //e.Row.Cells[4].Style.Add(HtmlTextWriterStyle.Display, "none");
            //e.Row.Cells[5].Style.Add(HtmlTextWriterStyle.Display, "none");
            //e.Row.Cells[6].Style.Add(HtmlTextWriterStyle.Display, "none");
            //e.Row.Cells[8].Style.Add(HtmlTextWriterStyle.Display, "none");
            //e.Row.Cells[9].Style.Add(HtmlTextWriterStyle.Display, "none");
            //e.Row.Cells[11].Style.Add(HtmlTextWriterStyle.Display, "none");
            //e.Row.Cells[12].Style.Add(HtmlTextWriterStyle.Display, "none");
            //e.Row.Cells[13].Style.Add(HtmlTextWriterStyle.Display, "none");
        //}
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

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        DataTO queryTo = new DataTO();
        queryTo.setValue("PjCs_Code", this.ddl_Pj_PjFill.SelectedValue);
        DataTable dt = new BaseFun().getTableData("PjClass", queryTo);
        if (dt != null && dt.Rows.Count > 0)
            GoURL(string.Format("\\CACI\\{0}", dt.Rows[0]["PjCs_Path"].ToString()));
    }
}