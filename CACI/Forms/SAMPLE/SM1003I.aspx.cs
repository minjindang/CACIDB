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

public partial class SM1003I : IMMDInsertUI
{

    public override ROW_CMD_TYPE GetRowCommand(string DetailGridViewID, string strCmd)
    {
        switch (DetailGridViewID)
        {
            case "grvQuery":

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
            case "grvQuery2":

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
            default:
                return ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
                break;
        }


    }

    protected override void InitialDetail(string dataGridViewID)
    {
        switch (dataGridViewID)
        {
            case "grvQuery":
                hid_Dcol_1.Value = "N";
                
                break;
            case "grvQuery2":
                hid_Dcol_21.Value = "N";
                
                break;
            default:
                
                break;
        }
    }

    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        to.setValue("Mcol_2", txt_Mcol_2.Text);
        to.setValue("Mcol_3", txt_Mcol_3.Text);

        return to;
    }

    public override DataTO PopulateDetailData(string DetailGridViewID)
    {
        DataTO to = new DataTO();

        switch (DetailGridViewID)
        {
            case "grvQuery":
                if (hid_Dcol_1.Value == "N")
                {
                    if (grvQuery.DataKeys.Count == 0)
                        to.setValue("Dcol_1", 1);
                    else
                        to.setValue("Dcol_1", Convert.ToInt32(grvQuery.DataKeys[grvQuery.DataKeys.Count - 1]["Dcol_1"]) + 1);
                }
                else
                    to.setValue("Dcol_1", Convert.ToInt32(hid_Dcol_1.Value));

                to.setValue("Dcol_2", txt_Dcol_2.Text);
                to.setValue("Dcol_3", txt_Dcol_3.Text);
                break;
            case "grvQuery2":
                if (hid_Dcol_21.Value == "N")
                {
                    if (grvQuery2.DataKeys.Count == 0)
                        to.setValue("Dcol_21", 1);
                    else
                        to.setValue("Dcol_21", Convert.ToInt32(grvQuery2.DataKeys[grvQuery2.DataKeys.Count - 1]["Dcol_21"]) + 1);
                }
                else
                    to.setValue("Dcol_21", Convert.ToInt32(hid_Dcol_21.Value));
                to.setValue("Dcol_22", txt_Dcol_22.Text);
                to.setValue("Dcol_23", txt_Dcol_23.Text);
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
            case "grvQuery":
                hid_Dcol_1.Value = to.getValue("Dcol_1").ToString();
                txt_Dcol_2.Text = to.getValue("Dcol_2").ToString();
                txt_Dcol_3.Text = to.getValue("Dcol_3").ToString();

                break;
            case "grvQuery2":
                hid_Dcol_21.Value = to.getValue("Dcol_21").ToString();
                txt_Dcol_22.Text = to.getValue("Dcol_22").ToString();
                txt_Dcol_23.Text = to.getValue("Dcol_23").ToString();

                break;
            default:
                break;

        }
    }

    protected override bool BeforeHandleDetailInsert(DetailDataGroup dataGridGroup, DataTO to, System.Data.DataTable dt)
    {
        return base.BeforeHandleDetailInsert(dataGridGroup, to, dt);
    }

    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "Master-Detail-Detail新增作業範本";
        ProgNum = "0.3";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grvQuery, pnlGridView, lblPage, btnDTL_INSERT, btnDTL_UPDATE, btnDTL_CLEAR, "grvQuery"));
        addDetailMember(new DetailDataGroup(grvQuery2, pnlGridView2, lblPage2, btnDTL_INSERT2, btnDTL_UPDATE2, btnDTL_CLEAR2, "grvQuery2"));

        #endregion

        #region 宣告資訊
        BL = new SM1003BL();
        #endregion

        #region 按鈕定義 start

        InsertButton = btn_Insert;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊
        BackPage = "SM1003Q.aspx";
        #endregion

        #region 頁面檢查設定
        checkLoginType = checkLoginType.None;
        #endregion
    }

    public override void SetDefault()
    {
        txt_Mcol_2.Text = "";
        txt_Mcol_3.Text = "";
    }

    protected override void AfterHandleDetailInsert(DetailDataGroup dataGridGroup)
    {
        base.AfterHandleDetailInsert(dataGridGroup);

        switch (dataGridGroup.DataGridView.ID)
        {
            case "grvQuery":

                hid_Dcol_1.Value = "N";
                txt_Dcol_2.Text = "";
                txt_Dcol_3.Text = "";

                break;
            case "grvQuery2":

                hid_Dcol_21.Value = "N";
                txt_Dcol_22.Text = "";
                txt_Dcol_23.Text = "";

                break;
        }
    }

    protected override void AfterHandleDetailUpdate(DetailDataGroup dataGridGroup)
    {
        base.AfterHandleDetailUpdate(dataGridGroup);

        switch (dataGridGroup.DataGridView.ID)
        {
            case "grvQuery":

                hid_Dcol_1.Value = "N";
                txt_Dcol_2.Text = "";
                txt_Dcol_3.Text = "";

                break;
            case "grvQuery2":

                hid_Dcol_21.Value = "N";
                txt_Dcol_22.Text = "";
                txt_Dcol_23.Text = "";

                break;
        }
    }


}