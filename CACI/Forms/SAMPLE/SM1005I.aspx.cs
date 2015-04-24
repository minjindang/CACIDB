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

public partial class SM1005I : IMMDInsertUI
{

    public override ROW_CMD_TYPE GetRowCommand(string DetailGridViewID, string strCmd)
    {
        switch (DetailGridViewID)
        {
            case "grvQuery":

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
            case "grvQuery2":

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
                if( txt_DDcol_2.Text != "")
                    to.setValue("DDcol_2", txt_DDcol_2.Text);
                if( txt_DDcol_3.Text != "" )
                    to.setValue("DDcol_3", txt_DDcol_3.Text);
                break;
            case "grvQuery2":
                if( txt_DDcol_22.Text != "" )
                    to.setValue("DDcol_22", txt_DDcol_22.Text);
                if (txt_DDcol_23.Text != "")
                    to.setValue("DDcol_23", txt_DDcol_23.Text);
                break;
            default:
                break;

        }
        return to;
    }

    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "Master-Query-Detail-Detail 新增作業範本";
        
        ProgNum = "0.5";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grvQuery, pnlGridView, lblPage,grvQueryData,pnlQueryGridView,lblQueryPage,btnDTL_QUERY, btnDTL_INSERT, btnDTL_CLEAR, "grvQuery"));
        addDetailMember(new DetailDataGroup(grvQuery2, pnlGridView2, lblPage2, grvQueryData2, pnlQueryGridView2, lblQueryPage2, btnDTL_QUERY2, btnDTL_INSERT2, btnDTL_CLEAR2, "grvQuery2"));

        #endregion

        #region 宣告資訊

        BL = new SM1005BL();

        #endregion

        #region 按鈕定義

        InsertButton = btn_Insert;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion

        #region 導向頁面資訊
        
        BackPage = "SM1005Q.aspx";
        
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

                txt_DDcol_2.Text = "";
                txt_DDcol_3.Text = "";

                break;
            case "grvQuery2":

                txt_DDcol_22.Text = "";
                txt_DDcol_23.Text = "";

                break;
        }
    }

    protected override void InitialDetail(string dataGridViewID)
    {
        switch (dataGridViewID)
        {
            case "grvQuery":
                txt_DDcol_2.Text = "";
                txt_DDcol_3.Text = "";
                break;
            case "grvQuery2":
                txt_DDcol_22.Text = "";
                txt_DDcol_23.Text = "";
                break;
            default:
                break;
        }
    }

    public override void RenderDetailData(string DetailGridViewID, DataTO to)
    {
        throw new NotImplementedException();
    }
}