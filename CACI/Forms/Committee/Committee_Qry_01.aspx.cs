using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class Committee_Qry_01 : IQueryUI
{
    /// <summary>
    /// 將GridView中的控制項Command屬性,轉換為UI中用以判斷動作的列舉值(需實作)
    /// </summary>
    /// <param name="strCmd">控制項Command屬性</param>
    /// <returns>欲執行之動作</returns>
    public override ROW_CMD_TYPE GetRowCommand(string strCmd)
    {
        ROW_CMD_TYPE cmdType = ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
        switch (strCmd)
        {
            case "mod":
                cmdType = ROW_CMD_TYPE.ROW_CMD_MODIFY;
                break;
            case "del":
                cmdType = ROW_CMD_TYPE.ROW_CMD_DELETE;
                break;
            case "show":
                cmdType = ROW_CMD_TYPE.ROW_CMD_QUERY_DETAIL;
                break;
            default:
                cmdType = ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
                break;
        }

        return cmdType;
    }

    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        if (txt_Comm_Name.Text != "")
            to.setValue("Comm_Name", txt_Comm_Name.Text);

        if (txt_Comm_ComName.Text != "")
            to.setValue("Comm_ComName", txt_Comm_ComName.Text);

        //輔導項目
        string selectedComm_CoTerms = string.Empty;
        foreach (ListItem li in ckl_Comm_CoTerms.Items)
        {
            if (li.Selected)
                selectedComm_CoTerms += li.Value;
        }
        if (selectedComm_CoTerms != "")
            to.setValue("Comm_CoTerms", selectedComm_CoTerms);

        //輔導方式
        string selectedComm_CoachWay = string.Empty;
        foreach (ListItem li in ckl_Comm_CoachWay.Items)
        {
            if (li.Selected)
                selectedComm_CoachWay += "1";
            else
                selectedComm_CoachWay += "0";
        }

        if (selectedComm_CoachWay != "000")
            to.setValue("Comm_CoachWay", selectedComm_CoachWay);

        if (ddl_Skill.SelectedValue != "")
            to.setValue("Ski_Num", ddl_Skill.SelectedValue);

        return to;
    }

    /// <summary>
    /// 將Session取到的資料顯示在畫面上(需實作)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        if (to.getValue("Comm_Name").ToString() != "")
            txt_Comm_Name.Text = to.getValue("Comm_Name").ToString();
        if (to.getValue("Comm_ComName").ToString() != "")
            txt_Comm_ComName.Text = to.getValue("Comm_ComName").ToString();
        if (to.getValue("Ski_Num").ToString() != "")
            ddl_Skill.SelectedValue = to.getValue("Ski_Num").ToString();
        if (to.getValue("Comm_CoachWay").ToString() != "")
        {
            char[] comm_CoachWay = to.getValue("Comm_CoachWay").ToString().ToCharArray();
            for (int i = 0; i < comm_CoachWay.Length; i++)
            {
                if (comm_CoachWay[i] == '1')
                    ckl_Comm_CoachWay.Items[i].Selected = true;
            }
        }
        if (to.getValue("Comm_CoTerms").ToString() != "")
        {
            foreach (ListItem li in ckl_Comm_CoTerms.Items)
            {
                if (to.getValue("Comm_CoTerms").ToString().IndexOf(li.Value) != -1)
                    li.Selected = true;
            }
        }
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊
        PageDropDownList = ddlPage;//每頁筆數（查詢條件）

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "顧問委員管理作業─內網資料查詢畫面  ";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "3.1.1";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new Committee_01BL();//邏輯層

        #endregion

        #region GridView資訊
        DataGridView = grvQuery;//GridView設定
        PagePanel = pnlPageInfo;//頁數資訊
        DataPanel = pnlGridView;//GridView區塊設定
        PageLabel = lblPageCount;//總筆數
        PageNumDropDownList = ddlPageNum;//目前頁數
        PageUpButton = lnkPageUP;//上一頁
        PageDownButton = lnkPageDown;//下一頁
        #endregion

        #region 按鈕定義 start

        QueryButton = btn_Query;
        ClearButton = btn_Clear;
        InsertButton = btn_Insert;

        #endregion 按鈕定義 end

        #region 導向頁面資訊
        InsertPage = "Committee_Ins_01.aspx";//新增頁
        ModifyPage = "Committee_Upd_01.aspx";//修改頁
        ListPage = "Committee_Lis_01.aspx"; //顯示頁
        #endregion 導向頁面資訊

        #region 登入檢查模式
        //checkLoginType = com.kangdainfo.online.WebBase.UI.checkLoginType.Need;
        checkLoginType = checkLoginType.Need;
        #endregion
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        BaseFun bf = new BaseFun();
        //身份
        //ddl_Comm_Job.DataSource = bf.getSysCodeByKind("P", "I");
        // ddl_Comm_Job.DataTextField = "Sys_CdText";
        //ddl_Comm_Job.DataValueField = "Sys_CdCode";
        //ddl_Comm_Job.DataBind();
        //輔導方式
        ckl_Comm_CoachWay.Items.Clear();
        ckl_Comm_CoachWay.Items.Add("獎補助審查");
        ckl_Comm_CoachWay.Items.Add("輔導服務");
        ckl_Comm_CoachWay.Items.Add("顧問駐診服務");
        //輔導項目
        ckl_Comm_CoTerms.DataSource = bf.getSysCodeByKind("C", "K");
        ckl_Comm_CoTerms.DataTextField = "Sys_CdText";
        ckl_Comm_CoTerms.DataValueField = "Sys_CdCode";
        ckl_Comm_CoTerms.DataBind();
        //專長
        ddl_Skill.DataSource = bf.getSkillSample();
        ddl_Skill.DataTextField = "Ski_Name";
        ddl_Skill.DataValueField = "Ski_Num";
        ddl_Skill.DataBind();
    }

    protected override bool BeforeDoDelete(DataTO qto)
    {
        if (((Committee_01BL)BL).checkProject(qto))
        {
            customMSG_FAIL = "已有相關專案案件關聯";
            return false;
        }
        if (((Committee_01BL)BL).checkMeeting(qto))
        {
            customMSG_FAIL = "會議尚未舉行,不允許刪除";
            return false;
        }
        return true;
    }

    protected override void ProcessRowDataBound(int idx, GridViewRow row, System.Data.DataRowView view)
    {
        base.ProcessRowDataBound(idx, row, view);

        if (row.RowType == DataControlRowType.DataRow)
        {
            ((ImageButton)row.Cells[1].Controls[0]).OnClientClick = "if(!confirm('確定要刪除?')){return false;}";
        }
    }
}