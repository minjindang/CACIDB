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
using com.kangdainfo.online.WebControl;

public partial class Meeting_Upd_03 : IMMDListUI
{

    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "會議管理作業─內網記錄設定畫面";
        ProgNum = "3.2.12";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grv_MtgTimes1, pnlGridView, lblPage));
        addDetailMember(new DetailDataGroup(grv_MtgTimes2, pnlGridView3, lblPage3));

        #endregion

        #region 宣告資訊
        BL = new Meeting_02BL();
        #endregion

        #region 按鈕定義 start

        //UpdateButton = btn_Update;
        //ClearButton = btn_Clear;
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

        //專案資料
        ddl_UserDep.DataSource = bf.getUserDep();
        ddl_UserDep.DataBind();
    }

    protected override void SetHandler()
    {
        base.SetHandler();
        grv_MtgTimes1.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_MtgTimes1_TemplateDataModeSelection);
        grv_MtgTimes1.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_MtgTimes1_TemplateSelection);
        grv_MtgTimes2.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_MtgTimes2_TemplateDataModeSelection);
        grv_MtgTimes2.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_MtgTimes2_TemplateSelection);
    }

    void grv_MtgTimes1_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_MtgTimes2_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\Meeting_Upd_03.ascx";
    }
    void grv_MtgTimes2_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_MtgTimes1_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\Meeting_Upd_01.ascx";
    }


    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("Meeting_Code");
    }

    public override void RenderData(DataTO to)
    {
        BaseFun bf = new BaseFun();
        if (!string.IsNullOrEmpty(to.getValue("Meeting_User_Code").ToString()))
        {
            ddl_UserDep.SelectedValue = bf.getUserDep(to.getValue("Meeting_User_Code").ToString());
            ddl_Meeting_User_Code.DataSource = bf.getDepUser(ddl_UserDep.SelectedValue);
            ddl_Meeting_User_Code.DataBind();
            ddl_Meeting_User_Code.SelectedValue = to.getValue("Meeting_User_Code").ToString();
        }
        lbl_Pj_Kind.Text = to.getValue("Pj_Kind").ToString();
        this.lbl_Pj_Name.Text = to.getValue("Pj_Name").ToString();
        this.lbl_Pj_PjIntro.Text = to.getValue("Pj_PjIntro").ToString();
        this.lbl_Pj_PjNote.Text = to.getValue("Pj_PjNote").ToString();
        this.lbl_Meeting_BgnTime.Text = to.getValue("Meeting_BgnTime").ToString();
        this.lbl_Meeting_EndTime.Text = to.getValue("Meeting_EndTime").ToString();
        this.lbl_Meeting_Name.Text = to.getValue("Meeting_Name").ToString();
        this.lbl_Meeting_Code.Text = to.getValue("Meeting_Code").ToString();
        if (lbl_Pj_Kind.Text == "A")
            lbl_Pj_Kind_Name.Text = "獎補助專案";
        else
            lbl_Pj_Kind_Name.Text = "輔導專案";
        //利用User_Code查詢User_Name
        this.lbl_Pj_User_Code.Text = bf.getUserNameByCode(to.getValue("Pj_User_Code").ToString());
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        //委員紀錄
        //string txt_Record_Text = string.Empty;
        DataTO insertTo = new DataTO();
        DataKey key ; // 第一層GridView的DataKeyNames
        DataKey detailKey; // 第二層GridView的DataKeyNames
        DataKey detailDetailKey; // 第三層GridView的DataKeyNames
        GridView grvQuery;// 第二層GridView
        GridView detailGrvQuery;// 第三層GridView
        DropDownList ddl_Eval_Status;
        TextBox txt_Record_Text;
        foreach (GridViewRow item in grv_MtgTimes2.Rows)
        {
            key = grv_MtgTimes2.DataKeys[item.RowIndex];
            grvQuery = (GridView)(((UserControl)((((Panel)item.Cells[0].FindControl("DCP").Controls[0]).Controls[0].Controls[0]))).FindControl("grvQuery"));
            foreach (GridViewRow itemDetail in grvQuery.Rows)
	        {
                txt_Record_Text = (TextBox)itemDetail.Cells[1].FindControl("txt_Record_Text");
                if(!string.IsNullOrEmpty(txt_Record_Text.Text))
                {
                    insertTo = new DataTO();
                    detailKey = grvQuery.DataKeys[itemDetail.RowIndex];
                    insertTo.setValue("Meeting_Code", key[0].ToString());
                    insertTo.setValue("Meeting_Index", key[1].ToString());
                    insertTo.setValue("Comm_Code", detailKey[0].ToString());
                    insertTo.setValue("Rec_Info", "\\getDate()");
                    insertTo.setValue("User_Code", getLoginUser().User_Code);
                    //取得檔案
                    string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;
                    string filter = key[1].ToString() + "_" + detailKey[0].ToString() + "*";
                    foreach (string _path in Directory.GetFiles(tempPath, filter))
                    {
                        string targetPath = Request.PhysicalApplicationPath + @"UploadFile\Meeting\" + key[0].ToString();
                        if (!Directory.Exists(targetPath))
                            Directory.CreateDirectory(targetPath); 
                        //刪除TARGET舊檔案
                        foreach (string _delPath in Directory.GetFiles(targetPath, filter))
                            File.Delete(_delPath);
                        //移動TEMP檔案至TARGET
                        string destFileName = System.IO.Path.GetFileName(_path);
                        string destFile = System.IO.Path.Combine(targetPath, destFileName);

                        System.IO.File.Copy(_path, destFile, true);
                        insertTo.setValue("Record_File", destFile);
                        Directory.Delete(_path, true);
                    }
                   

                    //insertTo.setValue("Record_File", "");
                    insertTo.setValue("Record_Text", txt_Record_Text.Text);
                    if (itemDetail.Cells[3].Text == "Y")
                        ((Meeting_02BL)BL).insertToMtgRecord(insertTo);
                    else
                        ((Meeting_02BL)BL).updateToMtgRecord(insertTo);
                }
	        }
            
        }
        //會議記錄內容
        //GridView a = (GridView)(((UserControl)((((Panel)this.grv_MtgTimes1.Rows[0].Cells[0].FindControl("DCP").Controls[0]).Controls[0].Controls[0]))).FindControl("grvQuery"));
        //GridView b = (GridView)(((UserControl)((((Panel)a.Rows[0].Cells[0].FindControl("DCP").Controls[0]).Controls[0].Controls[0]))).FindControl("grvQuery"));
        //TextBox c = (TextBox)b.Rows[0].Cells[1].FindControl("txt_Eval_Note");
        foreach (GridViewRow item in grv_MtgTimes1.Rows)
        {
            key = grv_MtgTimes1.DataKeys[item.RowIndex];
            grvQuery = (GridView)(((UserControl)((((Panel)item.Cells[0].FindControl("DCP").Controls[0]).Controls[0].Controls[0]))).FindControl("grvQuery"));
            foreach (GridViewRow itemDetail in grvQuery.Rows)
            {
                detailKey = grvQuery.DataKeys[itemDetail.RowIndex];
                detailGrvQuery = (GridView)(((UserControl)((((Panel)itemDetail.Cells[0].FindControl("DCP").Controls[0]).Controls[0].Controls[0]))).FindControl("grvQuery"));
                foreach (GridViewRow itemDetailDetail in detailGrvQuery.Rows)
                {
                    ddl_Eval_Status = ((DropDownList)itemDetailDetail.FindControl("ddl_Eval_Status"));
                    if (ddl_Eval_Status.SelectedValue == "") //請選擇
                        continue;
                    insertTo = new DataTO();
                    detailDetailKey = detailGrvQuery.DataKeys[itemDetailDetail.RowIndex];
                    insertTo.setValue("Aow_Code", itemDetail.Cells[2].Text);
                    insertTo.setValue("Meeting_Code", detailKey[0].ToString());
                    insertTo.setValue("Meeting_Index", detailKey[1].ToString());
                    insertTo.setValue("Com_Code", detailKey[2].ToString());
                    insertTo.setValue("Comm_Code", detailDetailKey[0].ToString());
                    insertTo.setValue("Rec_Info", "\\getDate()");
                    insertTo.setValue("User_Code", getLoginUser().User_Code);
                    //insertTo.setValue("Record_File", "");
                    insertTo.setValue("Eval_Note", ((TextBox)itemDetailDetail.FindControl("txt_Eval_Note")).Text);

                    insertTo.setValue("Eval_Status", ddl_Eval_Status.SelectedValue);
                    if (itemDetailDetail.Cells[3].Text == "Y")
                        ((Meeting_02BL)BL).insertToEvaluations(insertTo);
                    else
                        ((Meeting_02BL)BL).updateToEvaluations(insertTo);
                }
            }

        }
    }
}