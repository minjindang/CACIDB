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
using System.Data.SqlClient;

public partial class Allowance_Qry_02 : ICommonUI
{
   
   
    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊
        
        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "獎補助作業─內網資料專案查詢";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "2.1.15";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Allowance_01BL();//邏輯層

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

        //QueryButton = btn_Query;
        //ClearButton = btn_Clear;
        //InsertButton = btn_Insert;

        #endregion 按鈕定義 end

        #region 導向頁面資訊
        //InsertPage = "Allowance_Ins_01.aspx";//新增頁
        //ModifyPage = "Allowance_Upd_01.aspx";//修改頁
        //ListPage = "Allowance_Lis_01.aspx"; //顯示頁
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
        string sqlStr = "SELECT Pj_Name , Pj_Code " +
                        "FROM CACIDB..Project " +
                        "WHERE GETDATE() between Pj_BgnDate and Pj_EndDate " +
                        "AND Pj_Kind = 'A' ";
        DataTable dt = new DataTable();
        new SQLAgent(DataBase.CACIDB).select(new SqlCommand(sqlStr),dt);
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
            //查詢條件To
            DataTO queryTo = new DataTO();
            queryTo.setValue("Pj_Code", ddl_Project.SelectedValue);
            DataTable dt = new DataTable();
            new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Project", queryTo), dt);
            string pjFill = dt.Rows[0]["Pj_PjFill"].ToString();
            //傳送參數To
            DataTO to = new DataTO();
            to.setValue("Pj_Code", ddl_Project.SelectedValue);
            Session[ICommonUI.Web_ID + Session.SessionID + "Allowance_Ins_0" + pjFill] = to;
            GoURL(string.Format("\\CACI\\Forms\\Allowance\\Allowance_Ins_0{0}.aspx", pjFill));
        }
    }

}