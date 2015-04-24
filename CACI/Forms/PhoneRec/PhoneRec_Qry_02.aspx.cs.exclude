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

public partial class Coach_Qry_02 : ICommonUI
{
   
   
    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊
        
        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "輔導資料查詢維護─內網資料專案查詢";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "5.1.12";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Allowance_01BL();//邏輯層

        #endregion

        #region GridView資訊
        #endregion

        #region 按鈕定義 start
        #endregion 按鈕定義 end

        #region 導向頁面資訊
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
        DataTO to = new DataTO();
        to.setValue("Pj_Kind", "B");
        DataTable dt = new DataTable();
        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Project", to),dt);
        ddl_Project.Items.Clear();
        ddl_Project.Items.Add(new ListItem("請選擇", "-1"));
        ddl_Project.AppendDataBoundItems = true;
        ddl_Project.DataSource = dt;
        ddl_Project.DataTextField = "Pj_Name";
        ddl_Project.DataValueField = "Pj_Code";
        ddl_Project.DataBind();
        
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        if (ddl_Project.SelectedValue != "-1")
        {
            //傳送參數To
            DataTO to = new DataTO();
            to.setValue("Pj_Code", ddl_Project.SelectedValue);
            Session[ICommonUI.Web_ID + Session.SessionID + "Coach_Ins_01"] = to;
            GoURL("\\CACI\\Forms\\Coach\\Coach_Ins_01.aspx");
        }
    }

}