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

public partial class PhoneRec_Upd_02 : IMMDListUI
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

        this.hid_PhRec_Code.Value = to.getValue("PhRec_Code").ToString();
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
        //to.setValue("PhRec_Code", "11111");

        return to.isColumnExist("PhRec_Code");
    }

    /// <summary>
    /// �]�w�{���Ѽ�(�ݹ�@)
    /// </summary>
    public override void SetProp()
    {
        #region �@�P��T

        ProgCd = this.GetType().BaseType.Name;//�{���N��
        ProgNm = "�q�ܬ����@�~�w�ɤJ�q�ܬ����k����श�\��";//�{���W��
        ProgLabel = lblProg;//���D�]�{���N��+�{���W�١^
        ProgNum = "4.1.6";
        ProgType = PAGE_ACTION.PAGE_UPDATE;

        MessageLabel = lblMsg;//�T���C
        addDetailMember(new DetailDataGroup(grv_Aow, pnl_grv_Aow, lblPage1));
        addDetailMember(new DetailDataGroup(grv_Coach, pnl_grv_Coach, lblPage2));
        addDetailMember(new DetailDataGroup(grv_Consulting, pnl_grv_Consulting, lblPage3));

        #endregion

        #region �ŧi��T

        BL = new PhoneRec_01BL();//�޿�h

        #endregion

        #region ���s�w�q

        BackButton = btn_Back;

        #endregion

        #region �ɦV������T

        BackPage = "PhoneRec_Upd_01.aspx";

        #endregion �ɦV������T

        #region �����ˬd�]�w

        checkLoginType = checkLoginType.Need;

        #endregion

        //addDetailMember(new DetailDataGroup(grv_CoachStage, pnl_CoachStage, lbl_CoachStage));
        //addDetailMember(new DetailDataGroup(grv_Meeting, pnl_Meeting, lbl_Meeting));
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


        DataTO to = (DataTO)Session[ICommonUI.Web_ID + Session.SessionID + "PhoneRec_Upd_02"];
        if (to != null && to.getValue("PhRec_Code").ToString() != "")
        {
            this.hid_PhRec_Code.Value = to.getValue("PhRec_Code").ToString();
            this.hid_PhRec_ComCode.Value = to.getValue("PhRec_ComCode").ToString();
            this.hid_PRcRp_Code.Value = to.getValue("PRcRp_Code").ToString();
        }
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

        //grv_Aow.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Aow_TemplateSelection);
        //grv_Aow.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Aow_TemplateDataModeSelection);

        grv_Coach.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Coach_TemplateSelection);
        grv_Coach.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Coach_TemplateDataModeSelection);

        grv_Consulting.TemplateSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventHandler(grv_Consulting_TemplateSelection);
        grv_Consulting.TemplateDataModeSelection += new com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventHandler(grv_Consulting_TemplateDataModeSelection);
    }
    void grv_Aow_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\ConsultingHistory_Lis_01.ascx";
    }

    void grv_Aow_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }
    void grv_Consulting_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\ConsultingHistory_Lis_01.ascx";
    }

    void grv_Consulting_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }

    void grv_Coach_TemplateSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateSelectionEventArgs e)
    {
        e.TemplateFilename = "\\CACI\\UserControl\\CoachHistory_Lis_01.ascx";
    }

    void grv_Coach_TemplateDataModeSelection(object sender, com.kangdainfo.online.WebControl.DataGridViewTemplateDataModeSelectionEventArgs e)
    {
        e.TemplateDataMode = com.kangdainfo.online.WebControl.TemplateDataModes.Table;
    }
    protected void Coach_SelectAll(object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)(this.grv_Coach.HeaderRow.Cells[0].FindControl("cbHead"))).Checked;
        foreach (GridViewRow gvRow in grv_Coach.Rows)
        {
            ((CheckBox)(gvRow.Cells[0].FindControl("cbItem"))).Checked = isChecked;
        }
    }
    protected void Aow_SelectAll(object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)(this.grv_Aow.HeaderRow.Cells[0].FindControl("cbHead"))).Checked;
        foreach (GridViewRow gvRow in grv_Aow.Rows)
        {
            ((CheckBox)(gvRow.Cells[0].FindControl("cbItem"))).Checked = isChecked;
        }
    }
    protected void Consulting_SelectAll(object sender, EventArgs e)
    {
        bool isChecked = ((CheckBox)(this.grv_Consulting.HeaderRow.Cells[0].FindControl("cbHead"))).Checked;
        foreach (GridViewRow gvRow in grv_Consulting.Rows)
        {
            ((CheckBox)(gvRow.Cells[0].FindControl("cbItem"))).Checked = isChecked;
        }
    }
    protected void btn_Archive_Click(object sender, ImageClickEventArgs e)
    {
        String SelectData = "";

        //DataTable dt_Coach = ICommonUI.GridView2DataTable(grv_Coach);

        foreach (GridViewRow gvRow in grv_Aow.Rows)
        {
            if (((CheckBox)(gvRow.Cells[0].FindControl("cbItem"))).Checked)
                SelectData +=  this.grv_Aow.DataKeys[gvRow.RowIndex].Value.ToString() + ",";

        }
        foreach (GridViewRow gvRow in grv_Coach.Rows)
        {
            if (((CheckBox)(gvRow.Cells[0].FindControl("cbItem"))).Checked)
                SelectData +=  this.grv_Coach.DataKeys[gvRow.RowIndex].Value.ToString() + ",";

        }
        foreach (GridViewRow gvRow in grv_Consulting.Rows)
        {
            if (((CheckBox)(gvRow.Cells[0].FindControl("cbItem"))).Checked)
                SelectData += this.grv_Consulting.DataKeys[gvRow.RowIndex].Value.ToString() + ",";

        }
        if (SelectData != "")
        {
            SelectData = SelectData.Substring(0, SelectData.Length - 1);
        }
        new PhoneRec_01BL().doArchive2PhoneRec(SelectData.Split(','), this.hid_PhRec_Code.Value);

    }
}