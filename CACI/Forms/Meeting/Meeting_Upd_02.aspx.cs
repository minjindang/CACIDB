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

public partial class Meeting_Upd_02 : IMMDUpdateUI
{
    public override ROW_CMD_TYPE GetRowCommand(string DetailGridViewID, string strCmd)
    {
        switch (DetailGridViewID)
        {
            case "grv_MtgTimes":

                switch (strCmd)
                {
                    case "del":
                        return ROW_CMD_TYPE.ROW_CMD_DELETE;
                        break;
                    case "mod":
                        return ROW_CMD_TYPE.ROW_CMD_MODIFY;
                        break;
                    default:
                        return ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
                        break;
                }
                break;
            case "grv_Committee":

                switch (strCmd)
                {
                    case "del":
                        return ROW_CMD_TYPE.ROW_CMD_DELETE;
                        break;
                    default:
                        return ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
                        break;
                }
                break;
            case "grv_Company":
                switch (strCmd)
                {
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
            case "grv_MtgTimes":
                BaseFun bf = new BaseFun();
                DataSet ds = bf.generHouMinute();
                this.ddl_BgnHour.DataSource = ds.Tables["hoursTable"];
                this.ddl_BgnHour.DataTextField = "houhourr";
                this.ddl_BgnHour.DataValueField = "houhourr";
                this.ddl_BgnHour.DataBind();
                //
                this.ddl_EndHour.DataSource = ds.Tables["hoursTable"];
                this.ddl_EndHour.DataTextField = "houhourr";
                this.ddl_EndHour.DataValueField = "houhourr";
                this.ddl_EndHour.DataBind();
                //
                this.ddl_BgnMin.DataSource = ds.Tables["minutesTable"];
                this.ddl_BgnMin.DataTextField = "minute";
                this.ddl_BgnMin.DataValueField = "minute";
                this.ddl_BgnMin.DataBind();
                //
                this.ddl_EndMin.DataSource = ds.Tables["minutesTable"];
                this.ddl_EndMin.DataTextField = "minute";
                this.ddl_EndMin.DataValueField = "minute";
                this.ddl_EndMin.DataBind();
                break;
            //case "grv_Committee":
            //    ddl_Meeting_Index.Items.Clear();
            //    ddl_Meeting_Index.Items.Add(new ListItem("請先執行場地設定", "-1"));
            //    ddl_Meeting_Index.SelectedValue = "-1";
            //    ckl_Comm_CoachWay.Items.Clear();
            //    ckl_Comm_CoachWay.Items.Add("獎補助審查");
            //    ckl_Comm_CoachWay.Items.Add("輔導服務");
            //    ckl_Comm_CoachWay.Items.Add("顧問駐診服務");
            //    ckl_Comm_CoTerms.Items.Clear();
            //    ckl_Comm_CoTerms.DataSource = new BaseFun().getSysCodeByKind("C", "K");
            //    ckl_Comm_CoTerms.DataBind();
            //    ddl_Skill.DataSource = new BaseFun().getSkillSample();
            //    ddl_Skill.DataBind();
            //    break;
            case "grv_Company":
                ddl_Meeting_Index2.Items.Clear();
                ddl_Meeting_Index2.Items.Add(new ListItem("請先執行場地設定", "-1"));
                ddl_Meeting_Index2.SelectedValue = "-1";
                break;
            default:

                break;
        }
    }

    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();
        to.setValue("Meeting_Code", lbl_Meeting_Code.Text);
        to.setValue("Meeting_Class", ddl_Meeting_Class.SelectedValue);
        to.setValue("Meeting_Name", txt_Meeting_Name.Text);
        to.setValue("Pj_Code", ddl_Pj_Code.SelectedValue);
        to.setValue("Meeting_User_Code", ddl_Meeting_User_Code.SelectedValue);
        return to;
    }

    public override DataTO PopulateDetailData(string DetailGridViewID)
    {
        DataTO to = new DataTO();
        switch (DetailGridViewID)
        {
            case "grv_MtgTimes":
                if (lbl_Meeting_Index.Text.Equals("N"))
                    to.setValue("Meeting_Index", grv_MtgTimes.Rows.Count + 1);
                else
                    to.setValue("Meeting_Index", lbl_Meeting_Index.Text);

                to.setValue("Times_Place", txt_Times_Place.Text);
                to.setValue("Times_Bgn", txt_Times_Bgn.Text + " " + ddl_BgnHour.SelectedValue + ":" + ddl_BgnMin.SelectedValue);
                to.setValue("Times_End", txt_Times_End.Text + " " + ddl_EndHour.SelectedValue + ":" + ddl_EndMin.SelectedValue);
                break;
            //case "grv_Committee":
            //    if (txt_Comm_Name.Text != "")
            //        to.setValue("Comm_Name", txt_Comm_Name.Text);
            //    if (txt_Comm_ComName.Text != "")
            //        to.setValue("Comm_ComName", txt_Comm_ComName.Text);
            //    //輔導項目
            //    string selectedComm_CoTerms = string.Empty;
            //    foreach (ListItem li in ckl_Comm_CoTerms.Items)
            //    {
            //        if (li.Selected)
            //            selectedComm_CoTerms += li.Value;
            //    }
            //    if (selectedComm_CoTerms != "")
            //        to.setValue("Comm_CoTerms", selectedComm_CoTerms);
            //    //輔導方式
            //    string selectedComm_CoachWay = string.Empty;
            //    foreach (ListItem li in ckl_Comm_CoachWay.Items)
            //    {
            //        if (li.Selected)
            //            selectedComm_CoachWay += "1";
            //        else
            //            selectedComm_CoachWay += "0";
            //    }

            //    if (selectedComm_CoachWay != "000")
            //        to.setValue("Comm_CoachWay", selectedComm_CoachWay);

            //    if (ddl_Skill.SelectedValue != "")
            //        to.setValue("Ski_Num", ddl_Skill.SelectedValue);
            //    break;
            case "grv_Company":
                to.setValue("Pj_Code", this.ddl_Pj_Code.SelectedValue);
                to.setValue("Pj_Kind", this.lbl_Pj_Kind.Text);
                if (txt_Com_Tonum.Text != "")
                    to.setValue("Com_Tonum", txt_Com_Tonum.Text);
                if (txt_Com_Name.Text != "")
                    to.setValue("Com_Name", txt_Com_Name.Text);
                break;
            default:
                break;

        }
        lbl_Meeting_Index.Text = "N";
        return to;
    }

    public override void RenderDetailData(string DetailGridViewID, DataTO to)
    {
        switch (DetailGridViewID)
        {
            case "grv_MtgTimes":
                txt_Times_Place.Text = to.getValue("Times_Place").ToString();
                lbl_Meeting_Index.Text = to.getValue("Meeting_Index").ToString();
                if (!string.IsNullOrEmpty(to.getValue("Times_Bgn").ToString()))
                {
                    txt_Times_Bgn.Text = to.getValue("Times_Bgn").ToString().Split(' ')[0];
                    ddl_BgnHour.SelectedValue = to.getValue("Times_Bgn").ToString().Split(' ')[1].Split(':')[0];
                    ddl_BgnMin.SelectedValue = to.getValue("Times_Bgn").ToString().Split(' ')[1].Split(':')[1];
                }
                if (!string.IsNullOrEmpty(to.getValue("Times_End").ToString()))
                {
                    txt_Times_End.Text = to.getValue("Times_End").ToString().Split(' ')[0];
                    ddl_EndHour.SelectedValue = to.getValue("Times_End").ToString().Split(' ')[1].Split(':')[0];
                    ddl_EndMin.SelectedValue = to.getValue("Times_End").ToString().Split(' ')[1].Split(':')[1];
                }
                break;
            default:
                break;

        }
    }

    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "會議管理作業─內網獎補助資料修改畫面";
        ProgNum = "3.2.5";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grv_MtgTimes, pnlGridView, lblPage, btnDTL_INSERT, btnDTL_UPDATE, btnDTL_CLEAR, "grv_MtgTimes"));
        //addDetailMember(new DetailDataGroup(grv_Committee, pnlGridView2, lblPage2, grv_SearchCommittee, pnlQueryGridView2, lblQueryPage2, btnDTL_QUERY2, btnDTL_INSERT2, btnDTL_CLEAR2, "grv_Committee"));
        addDetailMember(new DetailDataGroup(grv_Company, pnlGridView3, lblPage3, grv_SearchCompany, pnlQueryGridView3, lblQueryPage3, btnDTL_QUERY3, btnDTL_INSERT3, btnDTL_CLEAR3, "grv_Company"));

        #endregion

        #region 宣告資訊
        BL = new Meeting_01BL();
        #endregion

        #region 按鈕定義 start

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊

        BackPage = "Meeting_Qry_01.aspx";

        #endregion

        #region 頁面檢查設定
        checkLoginType = checkLoginType.Need;
        #endregion
    }

    public override void SetDefault()
    {
        BaseFun bf = new BaseFun();
        //會議類別
        //ddl_Meeting_Class.Items.Clear();
        //ddl_Meeting_Class.Items.Add(new ListItem("請選擇", "-1"));
        //ddl_Meeting_Class.AppendDataBoundItems = true;
        ddl_Meeting_Class.DataSource = bf.getTableData("MeetingType");
        ddl_Meeting_Class.DataTextField = "Mety_Name";
        ddl_Meeting_Class.DataValueField = "Mety_Code";
        ddl_Meeting_Class.DataBind();
        //專案名稱
        Bind_Pj_Code();
        //專案資料
        Bind_ProjectDetail();
        //
        ddl_UserDep.DataSource = bf.getUserDep();
        ddl_UserDep.DataBind();

        lbl_Meeting_Index.Text = "N";
    }

    private void Bind_Pj_Code()
    {
        BaseFun bf = new BaseFun();
        DataTO queryTo = new DataTO();
        queryTo.setValue("Mety_Code", ddl_Meeting_Class.SelectedValue);
        string Pj_Kind = bf.getTableData("MeetingType", queryTo).Rows[0]["Pj_Kind"].ToString();
        queryTo = new DataTO();
        queryTo.setValue("Pj_Kind", Pj_Kind);
        DataTable projectInfo = bf.getTableData("Project", queryTo);
        ddl_Pj_Code.DataSource = projectInfo;
        ddl_Pj_Code.DataTextField = "Pj_Name";
        ddl_Pj_Code.DataValueField = "Pj_Code";
        ddl_Pj_Code.DataBind();
        up_Pj_Code.Update();
    }

    private void Bind_ProjectDetail()
    {
        BaseFun bf = new BaseFun();
        DataTO queryTo = new DataTO();
        queryTo.setValue("Pj_Code", this.ddl_Pj_Code.SelectedValue);
        DataTable projectInfo = bf.getTableData("Project", queryTo);
        if (projectInfo != null & projectInfo.Rows.Count > 0)
        {
            this.lbl_Pj_Name.Text = projectInfo.Rows[0]["Pj_Name"].ToString();
            this.lbl_Pj_PjIntro.Text = projectInfo.Rows[0]["Pj_PjIntro"].ToString();
            this.lbl_Pj_PjNote.Text = projectInfo.Rows[0]["Pj_PjNote"].ToString();
            this.lbl_Pj_User_Code.Text = bf.getUserNameByCode(projectInfo.Rows[0]["Pj_User_Code"].ToString());
            this.lbl_Pj_Kind.Text = projectInfo.Rows[0]["Pj_Kind"].ToString();
            if (lbl_Pj_Kind.Text == "A")
                lbl_Pj_Kind_Name.Text = "獎補助專案";
            else
                lbl_Pj_Kind_Name.Text = "輔導專案";
            //if (this.lbl_Pj_Kind.Text == "A")//獎補助專案不需要設定委員
            //{
            //    this.TabContainer1.Tabs[1].Enabled = false;
            //    up_Tab.Update();
            //}
            //else
            //{
            //    this.TabContainer1.Tabs[1].Enabled = true;
            //    up_Tab.Update();
            //}
        }
        up_Pj_Name.Update();
        up_Pj_User_Code.Update();
        up_Pj_PjIntro.Update();
        up_Pj_PjNote.Update();
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Meeting_Code");
    }

    public override void RenderMasterData(DataTO to)
    {
        if(!string.IsNullOrEmpty(to.getValue("Meeting_Class").ToString()))
            ddl_Meeting_Class.SelectedValue = to.getValue("Meeting_Class").ToString();
        Bind_Pj_Code();
        if (!string.IsNullOrEmpty(to.getValue("Meeting_User_Code").ToString()))
            ddl_Meeting_User_Code.SelectedValue = to.getValue("Meeting_User_Code").ToString();
        txt_Meeting_Name.Text = to.getValue("Meeting_Name").ToString();
        lbl_Meeting_Code.Text = to.getValue("Meeting_Code").ToString();
        BaseFun bf = new BaseFun();
        if (!string.IsNullOrEmpty(to.getValue("Meeting_User_Code").ToString()))
        {
            ddl_UserDep.SelectedValue = bf.getUserDep(to.getValue("Meeting_User_Code").ToString());
            ddl_Meeting_User_Code.DataSource = bf.getDepUser(ddl_UserDep.SelectedValue);
            ddl_Meeting_User_Code.DataBind();
            ddl_Meeting_User_Code.SelectedValue = to.getValue("Meeting_User_Code").ToString();
        }
        ddl_Pj_Code.SelectedValue = to.getValue("Pj_Code").ToString();
        Bind_ProjectDetail();
    }

    protected void ddl_UserDep_SelectedIndexChanged(object sender, EventArgs e)
    {
        BaseFun bf = new BaseFun();
        ddl_Meeting_User_Code.DataSource = bf.getDepUser(ddl_UserDep.SelectedValue);
        ddl_Meeting_User_Code.DataBind();
    }

    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        DataTable dt = ICommonUI.GridView2DataTable(grv_MtgTimes);
        if (TabContainer1.ActiveTab.HeaderText == "申請單位")
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl_Meeting_Index2.Items.Clear();
                ddl_Meeting_Index2.DataSource = dt;
                ddl_Meeting_Index2.DataTextField = "Meeting_Index";
                ddl_Meeting_Index2.DataValueField = "Meeting_Index";
                ddl_Meeting_Index2.DataBind();
                up_Tab3.Update();
            }
        }
    }

    protected void Company_SelectAll(object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)(this.grv_SearchCompany.HeaderRow.Cells[0].FindControl("cbHead"))).Checked;
        foreach (GridViewRow gvRow in grv_SearchCompany.Rows)
        {
            ((CheckBox)(gvRow.Cells[0].FindControl("cbItem"))).Checked = isChecked;
        }
    }

    public override DataTO PopulateDetailMarkedData(string DetailGridViewID)
    {
        DataTO to = base.PopulateDetailMarkedData(DetailGridViewID);
        switch (DetailGridViewID)
        {
            case "grv_Company":
                if (!to.isColumnExist("Meeting_Index"))
                {
                    to.setValue("Meeting_Index", ddl_Meeting_Index2.SelectedValue);
                    //to.setValue("Comm_Name", ddl_Committee.SelectedItem.Text);
                    //to.setValue("Comm_Code", ddl_Committee.SelectedValue);
                }
                break;
            default:
                break;
        }
        return to;
    }

    protected void grv_Company_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //e.Row.Cells[5].Style.Add(HtmlTextWriterStyle.Display, "none");
    }
}