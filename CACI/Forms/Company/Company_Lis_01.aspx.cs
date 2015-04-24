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

public partial class Company_Lis_01 : IMMDListUI
{
    ///// <summary>
    ///// �N������ƥ����������@�Ӷǿ骫��(TO)(�ݹ�@)
    ///// </summary>
    ///// <returns>�ǿ骫��</returns>
    public override void RenderData(DataTO to)
    {
        BaseFun bf = new BaseFun();

        //�򥻸��
        this.lbl_Com_Name.Text = to.getValue("Com_Name").ToString();
        this.lbl_Com_Tonum.Text = to.getValue("Com_Tonum").ToString();
        this.lbl_Com_Boss.Text = to.getValue("Com_Boss").ToString();
        this.lbl_Com_BsIDNO.Text = to.getValue("Com_BsIDNO").ToString();

        //��´�Φ�(�n�O����)
        this.ddl_Com_OrgForm.DataSource = bf.getSysCodeByKind("R", "T");
        this.ddl_Com_OrgForm.DataBind();
        this.ddl_Com_OrgForm.SelectedValue = to.getValue("Com_OrgForm").ToString();

        this.lbl_Com_Capital.Text = to.getValue("Com_Capital").ToString();
        this.lbl_Com_EmpNum.Text = to.getValue("Com_EmpNum").ToString();

        //�D�n���~�O
        this.ddl_Com_MnSectors.DataSource = bf.getSysCodeByKind("I", "D");
        this.ddl_Com_MnSectors.DataBind();
        this.ddl_Com_MnSectors.SelectedValue = to.getValue("Com_MnSectors").ToString();
        this.lbl_Com_MnOther.Text = to.getValue("Com_MnOther").ToString();

        //���n���~�O
        this.ddl_Com_SbSectors.DataSource = bf.getSysCodeByKind("I", "D");
        this.ddl_Com_SbSectors.DataBind();
        this.ddl_Com_SbSectors.SelectedValue = to.getValue("Com_SbSectors").ToString();
        this.lbl_Com_SbOther.Text = to.getValue("Com_SbOther").ToString();

        this.lbl_Com_MnProduct.Text = to.getValue("Com_MnProduct").ToString();

        //����p����T
        this.lbl_Com_Tel.Text = to.getValue("Com_Tel").ToString();
        this.lbl_Com_Fax.Text = to.getValue("Com_Fax").ToString();
        this.lbl_Com_OPAddr.Text = to.getValue("Com_OPAddr").ToString();
        this.lbl_Com_Url.Text = to.getValue("Com_Url").ToString();
        this.lbl_Com_Email.Text = to.getValue("Com_Email").ToString();

        //�p���H��T
        this.lbl_Com_CttName.Text = to.getValue("Com_CttName").ToString();
        this.lbl_Com_CttCell.Text = to.getValue("Com_CttCell").ToString();
        this.lbl_Com_CttTel.Text = to.getValue("Com_CttTel").ToString();
        this.lbl_Com_CttMail.Text = to.getValue("Com_CttMail").ToString();

        //���˪���ƤW��

        //string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        //if (!Directory.Exists(tempPath))
        //    Directory.CreateDirectory(tempPath);
        //else
        //{
        //    Directory.Delete(tempPath, true);
        //    Directory.CreateDirectory(tempPath);
        //}

        //�ɮפU��
        this.hpl_ComMnPd_file1.Text = "�s��";
        this.hpl_ComMnPd_file1.NavigateUrl = to.getValue("Com_MnPdFile1").ToString();
        this.hpl_ComAtt_file2.Text = "�s��";
        this.hpl_ComAtt_file2.NavigateUrl = bf.getComAtt_Path(to.getValue("Com_Code").ToString(), "RP");
        this.hpl_ComAtt_file3.Text = "�s��";
        this.hpl_ComAtt_file3.NavigateUrl = bf.getComAtt_Path(to.getValue("Com_Code").ToString(), "FA");
        this.hpl_ComAtt_file4.Text = "�s��";
        this.hpl_ComAtt_file4.NavigateUrl = bf.getComAtt_Path(to.getValue("Com_Code").ToString(), "OT");
    }
    
    public override bool CheckPK(DataTO to)
    {
        if (to.isColumnExist("Com_Code"))
            return true;
        else
            return false;
    }
    /// <summary>
    /// �]�w�{���Ѽ�(�ݹ�@)
    /// </summary>
    public override void SetProp()
    {
        #region �@�P��T

        ProgCd = this.GetType().BaseType.Name;//�{���N��
        ProgNm = "���~�i���޲z�@�~�w������Ƭd�ݵe��";//�{���W��
        ProgLabel = lblProg;//���D�]�{���N��+�{���W�١^
        ProgNum = "7.1.3";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//�T���C

        addDetailMember(new DetailDataGroup(grv_Aow, pnl_Aow, lblPage2));//�ӽиɧU�׾��{
        addDetailMember(new DetailDataGroup(grv_Coach1, pnl_Coach1, lblPage3));//�Ը߾��{
        addDetailMember(new DetailDataGroup(grv_Coach2, pnl_Coach2, lblPage4));//���ɾ��{
        //addDetailMember(new DetailDataGroup(grv_Money, pnl_Money, lblPage5));//�ӽпĸ���{
        addDetailMember(new DetailDataGroup(grv_Phone, pnl_Phone, lblPage6));//�q�ܰO��
        addDetailMember(new DetailDataGroup(grv_Evaluations, pnl_Evaluations, lblPage7));//�ަҰO��

        #endregion

        #region �ŧi��T

        BL = new Company_01BL();//�޿�h

        #endregion

        #region ���s�w�q

        BackButton = btn_Back;

        #endregion

        #region �ɦV������T

        BackPage = "Company_Qry_01.aspx";

        #endregion �ɦV������T

        #region �����ˬd�]�w

        checkLoginType = checkLoginType.Need;

        #endregion

    }

    /// <summary>
    /// �]�w��l��(�ݹ�@)
    /// </summary>
    public override void SetDefault()
    {
        BaseFun bf = new BaseFun();

        //�򥻸��
        this.lbl_Com_Name.Text = "";
        this.lbl_Com_Tonum.Text = "";
        this.lbl_Com_Boss.Text = "";
        this.lbl_Com_BsIDNO.Text = "";

        //��´�Φ�(�n�O����)
        this.ddl_Com_OrgForm.DataSource = bf.getSysCodeByKind("R", "T");
        this.ddl_Com_OrgForm.DataBind();

        this.lbl_Com_Capital.Text = "";
        this.lbl_Com_EmpNum.Text = "";

        //�D�n���~�O
        this.ddl_Com_MnSectors.DataSource = bf.getSysCodeByKind("I", "D");
        this.ddl_Com_MnSectors.DataBind();
        this.lbl_Com_MnOther.Text = "";

        //���n���~�O
        this.ddl_Com_SbSectors.DataSource = bf.getSysCodeByKind("I", "D");
        this.ddl_Com_SbSectors.DataBind();
        this.lbl_Com_SbOther.Text = "";

        this.lbl_Com_MnProduct.Text = "";

        //����p����T
        this.lbl_Com_Tel.Text = "";
        this.lbl_Com_Fax.Text = "";
        this.lbl_Com_OPAddr.Text = "";
        this.lbl_Com_Url.Text = "";
        this.lbl_Com_Email.Text = "";

        //�p���H��T
        this.lbl_Com_CttName.Text = "";
        this.lbl_Com_CttCell.Text = "";
        this.lbl_Com_CttTel.Text = "";
        this.lbl_Com_CttMail.Text = "";

        //���˪���ƤW��
        //string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

        //if (!Directory.Exists(tempPath))
        //    Directory.CreateDirectory(tempPath);
        //else
        //{
        //    Directory.Delete(tempPath, true);
        //    Directory.CreateDirectory(tempPath);
        //}
    }

    protected void btn_ComAtt_file1_Click(object sender, EventArgs e)
    {
        //String Url = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host +(Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + this.ComAtt_Path1.Value);
        //this.GoURL(Url);
    }

    protected override void SetHandler()
    {
        base.SetHandler();

        grv_Aow.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Aow_TemplateSelection);
        grv_Aow.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Aow_TemplateDataModeSelection);

        grv_Coach1.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Coach1_TemplateSelection);
        grv_Coach1.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Coach1_TemplateDataModeSelection);

        grv_Coach2.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Coach2_TemplateSelection);
        grv_Coach2.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Coach2_TemplateDataModeSelection);

        grv_Phone.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Phone_TemplateSelection);
        grv_Phone.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Phone_TemplateDataModeSelection);

        grv_Evaluations.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Evaluations_TemplateSelection);
        grv_Evaluations.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Evaluations_TemplateDataModeSelection);
    }

    void grv_Aow_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\Allowance_Qry_02.ascx";
    }

    void grv_Aow_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_Coach1_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\Coach_Qry_02.ascx";
    }

    void grv_Coach1_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_Coach2_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\Coach_Qry_03.ascx";
    }

    void grv_Coach2_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_Phone_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\PhoneRec_Qry_02.ascx";
    }

    void grv_Phone_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_Evaluations_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\Evaluations_Qry_02.ascx";
    }

    void grv_Evaluations_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }
}