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

public partial class PhoneRec_Lis_01 : IMMDListUI
{

    /*
     * grv_Score
     * grv_SmpStage
     */

    public override void SetProp()
    {
        #region �@�P��T

        ProgCd = this.GetType().BaseType.Name;
        ProgNm = "�q�ܬ����@�~�w������Ƭd�ݵe��";
        ProgNum = "4.1.2";
        ProgType = PAGE_ACTION.PAGE_LIST;

        ProgLabel = lblProg;
        MessageLabel = lblMsg;

        addDetailMember(new DetailDataGroup(grv_Phone, pnlGridView2, lblPage2));

        #endregion

        #region �ŧi��T
        BL = new PhoneRec_02BL();
        #endregion

        #region ���s�w�q start

        BackButton = btn_Back;

        #endregion

        #region �ɦV������T
        BackPage = "PhoneRec_Qry_01.aspx";
        #endregion

        #region �����ˬd�]�w
        checkLoginType = checkLoginType.Need;
        #endregion
    }

    public override void SetDefault()
    {
        BaseFun bf = new BaseFun();

    }

    protected override void SetHandler()
    {
        base.SetHandler();

        //�q�ܬ���
        grv_Phone.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Phone_TemplateSelection);
        grv_Phone.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Phone_TemplateDataModeSelection);


    }

    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("PhRec_Code");
    }

    void grv_Phone_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_Phone_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\PhRec_Lis_01.ascx";
    }


    public override void RenderData(DataTO to)
    {
        this.hid_Com_Code.Value = to.getValue("Com_Code").ToString();
        this.lbl_Com_Name.Text = to.getValue("Com_Name").ToString();
        this.lbl_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
        this.lbl_Com_CttName.Text = to.getValue("Com_CttName").ToString();
        this.lbl_Com_CttMail.Text = to.getValue("Com_CttMail").ToString();
        this.lbl_PhRec_Question.Text = to.getValue("PhRec_Question").ToString();
        //this.ckl_CntClass_Code.Text = to.getValue("CntClass_Code").ToString();

    }
}