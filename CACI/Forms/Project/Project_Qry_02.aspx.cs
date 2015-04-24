using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;
using System.Data;

public partial class Project_Qry_02 : ICommonUI
{




    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊


        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "專案設定作業─選擇範本";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "1.2.16";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Project_01BL();//邏輯層

        #endregion

        #region GridView資訊
        /*DataGridView = grvQuery;//GridView設定
        PagePanel = pnlPageInfo;//頁數資訊
        DataPanel = pnlGridView;//GridView區塊設定
        PageLabel = lblPageCount;//總筆數
        PageNumDropDownList = ddlPageNum;//目前頁數
        PageUpButton = lnkPageUP;//上一頁
        PageDownButton = lnkPageDown;//下一頁*/
        #endregion

        #region 按鈕定義 start

        /*QueryButton = btn_Query;
        ClearButton = btn_Clear;
        InsertButton = btn_Insert;*/

        #endregion 按鈕定義 end

        #region 導向頁面資訊
        /*InsertPage = "Project_Ins_01.aspx";//新增頁
        ModifyPage = "Project_Upd_01.aspx";//修改頁
        ListPage = "Project_Lis_01.aspx"; //顯示頁*/
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
        ddl_PjSamples.Items.Clear();
        //ddl_PjSamples.Items.Add(new ListItem("請選擇", "-1"));
        ddl_PjSamples.AppendDataBoundItems = true;
        ddl_PjSamples.DataSource = new BaseFun().getTableData("PjSamples");
        ddl_PjSamples.DataTextField = "PjSp_Name";
        ddl_PjSamples.DataValueField = "PjSp_Code";
        ddl_PjSamples.DataBind();
    }


    protected void btn_AddCoachProject_Click(object sender, ImageClickEventArgs e)
    {
        GoURL("\\CACI\\Forms\\Project\\Project_Ins_02.aspx");
    }

    protected void btn_AddProjectBySample_Click(object sender, ImageClickEventArgs e)
    {
        DataTO to = new DataTO();
        to.setValue("PjSp_Code", ddl_PjSamples.SelectedValue);
        DataTable dt = new DataTable();
        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("PjSamples",to),dt);
        if(dt.Rows[0]["PjSp_Kind"].ToString() == "A")
        {
            Session[ICommonUI.Web_ID + Session.SessionID + "Project_Ins_01"] = to;
            GoURL("\\CACI\\Forms\\Project\\Project_Ins_01.aspx");
        }
        else 
        {
            Session[ICommonUI.Web_ID + Session.SessionID + "Project_Ins_02"] = to;
            GoURL("\\CACI\\Forms\\Project\\Project_Ins_02.aspx");
        }
    }
    protected void btn_AddAllowanceProject_Click(object sender, ImageClickEventArgs e)
    {
        GoURL("\\CACI\\Forms\\Project\\Project_Ins_01.aspx");
    }
}