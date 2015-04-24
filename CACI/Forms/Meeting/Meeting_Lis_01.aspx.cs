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

public partial class Meeting_Lis_01 : IMMDListUI
{
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "會議管理作業─內網輔導資料查看畫面";
        ProgNum = "3.2.2";
        ProgType = PAGE_ACTION.PAGE_UPDATE;http://localhost:54236/CACI/Forms/Meeting/Meeting_Lis_01.aspx.cs

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        //addDetailMember(new DetailDataGroup(grv_MtgTimes, pnlGridView, lblPage, btnDTL_INSERT, btnDTL_UPDATE, btnDTL_CLEAR, "grv_MtgTimes"));
        //addDetailMember(new DetailDataGroup(grv_Committee, pnlGridView2, lblPage2, grv_SearchCommittee, pnlQueryGridView2, lblQueryPage2, btnDTL_QUERY2, btnDTL_INSERT2, btnDTL_CLEAR2, "grv_Committee"));
        //addDetailMember(new DetailDataGroup(grv_Company, pnlGridView3, lblPage3, grv_SearchCompany, pnlQueryGridView3, lblQueryPage3, btnDTL_QUERY3, btnDTL_INSERT3, btnDTL_CLEAR3, "grv_Company"));
        addDetailMember(new DetailDataGroup(grv_MtgTimes, pnlGridView, lblPage));
        addDetailMember(new DetailDataGroup(grv_Committee, pnlGridView2, lblPage2));
        addDetailMember(new DetailDataGroup(grv_Company, pnlGridView3, lblPage3));
        #endregion

        #region 宣告資訊
        BL = new Meeting_01BL();
        #endregion

        #region 按鈕定義 start

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
            this.lbl_Pj_User_Code.Text = bf.getUserNameByCode(projectInfo.Rows[0]["Pj_User_Code"].ToString());// projectInfo.Rows[0]["Pj_User_Code"].ToString();
            if ("A" == projectInfo.Rows[0]["Pj_Kind"].ToString())
                this.lbl_Pj_Kind.Text = "獎補助專案";
            else
                this.lbl_Pj_Kind.Text = "輔導專案";
        }
    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Meeting_Code");
    }

    public override void RenderData(DataTO to)
    {
        if (!string.IsNullOrEmpty(to.getValue("Meeting_Class").ToString()))
            ddl_Meeting_Class.SelectedValue = to.getValue("Meeting_Class").ToString();
        Bind_Pj_Code();
        if (!string.IsNullOrEmpty(to.getValue("Meeting_User_Code").ToString()))
            ddl_Meeting_User_Code.SelectedValue = to.getValue("Meeting_User_Code").ToString();
        lbl_Meeting_Name.Text = to.getValue("Meeting_Name").ToString();
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
}