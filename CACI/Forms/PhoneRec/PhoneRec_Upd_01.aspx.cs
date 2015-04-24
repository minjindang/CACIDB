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
using System.IO;
using System.Data;

public partial class PhoneRec_Upd_01 : IUpdateUI
{
    ///// <summary>
    ///// �N������ƥ����������@�Ӷǿ骫��(TO)(�ݹ�@)
    ///// </summary>
    ///// <returns>�ǿ骫��</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();
        to.setValue("PhRec_ComCode", hid_PhRec_ComCode.Value);
        to.setValue("PhRec_Code", hid_PhRec_Code.Value);
        to.setValue("PRcRp_Code", hid_PRcRp_Code.Value);
        to.setValue("PhRec_ComName", txt_PhRec_ComName.Text);
        //to.setValue("PhRec_Tonum", txt_PhRec_Tonum.Text);
        to.setValue("PhRec_CtName", txt_PhRec_CtName.Text);
        to.setValue("PhRec_CtTel", txt_PhRec_CtTel.Text);
        to.setValue("PhRec_CtMail", txt_PhRec_CtMail.Text);

        to.setValue("CntClass_Code", ddl_CntClass_Code.SelectedValue);
        to.setValue("PhRec_Question", txt_PhRec_Question.Text);

        to.setValue("PRcRp_Date", PhoneRec_01BL.chgChnDateToEnDate(txt_PRcRp_Date.Text));
        to.setValue("PRcRp_Text", txt_PRcRp_Text.Text);
        to.setValue("PRcRp_Handle", ddl_PRcRp_Handle.SelectedValue);

        return to;
    }

    /// <summary>
    /// �]�w�{���Ѽ�(�ݹ�@)
    /// </summary>
    public override void SetProp()
    {
        #region �@�P��T

        ProgCd = this.GetType().BaseType.Name;//�{���N��
        ProgNm = "�q�ܰO���@�~�w��ƭק�";//�{���W��
        ProgLabel = lblProg;//���D�]�{���N��+�{���W�١^
        ProgNum = "4.1.4";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//�T���C
        #endregion

        #region �ŧi��T

        BL = new PhoneRec_01BL();//�޿�h

        #endregion

        #region ���s�w�q

        UpdateButton = btn_Update;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion

        #region �ɦV������T

        BackPage = "PhoneRec_Qry_01.aspx";

        #endregion �ɦV������T

        #region �����ˬd�]�w

        //checkLoginType = checkLoginType.Need;
        checkLoginType = checkLoginType.Need;

        #endregion
    }

    /// <summary>
    /// �]�w��l��(�ݹ�@)
    /// </summary>
    public override void SetDefault()
    {
        hid_PhRec_ComCode.Value = "N";
        hid_PhRec_Code.Value = "N";
        hid_PRcRp_Code.Value = "N";
        BaseFun bf = new BaseFun();

        ddl_CntClass_Code.DataSource = bf.getSysCodeByKind("C", "Y");
        ddl_CntClass_Code.DataTextField = "Sys_CdText";
        ddl_CntClass_Code.DataValueField = "Sys_CdCode";
        ddl_CntClass_Code.DataBind();

        ddl_PRcRp_Handle.DataSource = bf.getSysCodeByKind("C", "R");
        ddl_PRcRp_Handle.DataTextField = "Sys_CdText";
        ddl_PRcRp_Handle.DataValueField = "Sys_CdCode";
        ddl_PRcRp_Handle.DataBind();

    }

    protected void txt_PhRec_Tonum_TextChanged(object sender, EventArgs e)
    {
        BaseFun bf = new BaseFun();
        //���q���
        DataTable compData = bf.getCompany(txt_PhRec_Tonum.Text);

        if (compData.Rows.Count > 0)
        {
            hid_PhRec_ComCode.Value = compData.Rows[0]["Com_Code"].ToString();
            txt_PhRec_ComName.Text = compData.Rows[0]["Com_Name"].ToString();
            txt_PhRec_CtName.Text = compData.Rows[0]["Com_CttName"].ToString();
            txt_PhRec_CtTel.Text = compData.Rows[0]["Com_CttTel"].ToString();
            txt_PhRec_CtMail.Text = compData.Rows[0]["Com_CttMail"].ToString();
        }
        else
        {
            hid_PhRec_ComCode.Value = "N";
        }
    }
    public override void RenderData(DataTO to)
    {
        if (!string.IsNullOrEmpty(to.getValue("PhRec_ComCode").ToString()))
            hid_PhRec_ComCode.Value = to.getValue("PhRec_ComCode").ToString();
        if (!string.IsNullOrEmpty(to.getValue("PhRec_Code").ToString()))
            hid_PhRec_Code.Value = to.getValue("PhRec_Code").ToString();
        if (!string.IsNullOrEmpty(to.getValue("PRcRp_Code").ToString()))
            hid_PRcRp_Code.Value = to.getValue("PRcRp_Code").ToString();

        txt_PhRec_ComName.Text = to.getValue("PhRec_ComName").ToString();
        txt_PhRec_CtName.Text = to.getValue("PhRec_CtName").ToString();
        txt_PhRec_CtTel.Text = to.getValue("PhRec_CtTel").ToString();
        txt_PhRec_Tonum.Text = to.getValue("PhRec_Tonum").ToString();
        txt_PhRec_CtMail.Text = to.getValue("PhRec_CtMail").ToString();

        ddl_CntClass_Code.SelectedValue = to.getValue("CntClass_Code").ToString();
        txt_PhRec_Question.Text = to.getValue("PhRec_Question").ToString();

        txt_PRcRp_Date.Text = to.getValue("PRcRp_Date").ToString();
        txt_PRcRp_Text.Text = to.getValue("PRcRp_Text").ToString();
        ddl_PRcRp_Handle.SelectedValue = to.getValue("PRcRp_Handle").ToString();


    }
    public override bool CheckPK(DataTO to)
    {
        return to.isColumnExist("PhRec_Code");
    }
    protected void btn_Archive_Click(object sender, ImageClickEventArgs e)
    {
        //�ǰe�Ѽ�To
        DataTO to = new DataTO();
        to.setValue("PhRec_ComCode", hid_PhRec_ComCode.Value);
        to.setValue("PhRec_Code", hid_PhRec_Code.Value);
        to.setValue("PRcRp_Code", hid_PRcRp_Code.Value);
        Session[ICommonUI.Web_ID + Session.SessionID + "PhoneRec_Upd_02"] = to;
        GoURL("\\CACI\\Forms\\PhoneRec\\PhoneRec_Upd_02.aspx");
    }
}