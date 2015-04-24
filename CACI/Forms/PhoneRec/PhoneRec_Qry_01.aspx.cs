using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class PhoneRec_Qry_01 : IQueryUI
{
    private bool isMod = true;
    /// <summary>
    /// �NGridView�������Command�ݩ�,�ഫ��UI���ΥH�P�_�ʧ@���C�|��(�ݹ�@)
    /// </summary>
    /// <param name="strCmd">���Command�ݩ�</param>
    /// <returns>�����椧�ʧ@</returns>
    public override ROW_CMD_TYPE GetRowCommand(string strCmd)
    {
        ROW_CMD_TYPE cmdType = ROW_CMD_TYPE.ROW_CMD_UNKNOWN;
        switch (strCmd)
        {
            case "mod":
                cmdType = ROW_CMD_TYPE.ROW_CMD_MODIFY;
                isMod = true;
                break;
            case "maintain":
                cmdType = ROW_CMD_TYPE.ROW_CMD_MODIFY;
                isMod = false;
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
    ///// �N������ƥ����������@�Ӷǿ骫��(TO)(�ݹ�@)
    ///// </summary>
    ///// <returns>�ǿ骫��</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();
        //�ӽнs��
        if (txt_PhRec_ComName.Text != "")
            to.setValue("PhRec_ComName", txt_PhRec_ComName.Text);

        if (txt_PhRec_Tonum.Text != "")
            to.setValue("PhRec_Tonum", txt_PhRec_Tonum.Text);

        if (txt_PhRec_CtName.Text != "")
            to.setValue("PhRec_CtName", txt_PhRec_CtName.Text);

        if (txt_PhRec_CtTel.Text != "")
            to.setValue("PhRec_CtTel", txt_PhRec_CtTel.Text);


        string selectedCntClass_CodTerms = "-1";
        foreach (ListItem li in this.cbl_CntClass_Code.Items)
        {
            if (li.Selected)
                selectedCntClass_CodTerms += "," + li.Value;
        }
        if (selectedCntClass_CodTerms != "-1")
            to.setValue("CntClass_Code", selectedCntClass_CodTerms);
        else
            to.setValue("CntClass_Code", "-1");

        return to;
    }

    /// <summary>
    /// �NSession���쪺�����ܦb�e���W(�ݹ�@)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        //�ӽнs��
        if (txt_PhRec_ComName.Text != "")
            txt_PhRec_ComName.Text = to.getValue("PhRec_ComName").ToString();

        if (txt_PhRec_Tonum.Text != "")
            txt_PhRec_Tonum.Text = to.getValue("PhRec_Tonum").ToString();

        if (txt_PhRec_CtName.Text != "")
            txt_PhRec_CtName.Text = to.getValue("PhRec_CtName").ToString();

        if (txt_PhRec_CtTel.Text != "")
            txt_PhRec_CtTel.Text = to.getValue("PhRec_CtTel").ToString();


        //�B�z���G
        if (to.getValue("CntClass_Code").ToString() != "")
        {
            foreach (ListItem li in cbl_CntClass_Code.Items)
            {
                if (to.getValue("CntClass_Code").ToString().IndexOf(li.Value) != -1)
                    li.Selected = true;
            }
        }
    }

    /// <summary>
    /// �]�w�{���Ѽ�(�ݹ�@)
    /// </summary>
    public override void SetProp()
    {
        #region �@�P��T

        PageDropDownList = ddlPage;//�C�����ơ]�d�߱���^

        ProgCd = this.GetType().BaseType.Name;//�{���N��
        ProgNm = "�q�ܰO���@�~�w������Ƭd�ߵe��  ";//�{���W����
        ProgLabel = lblProg;//���D�]�{���N��+�{���W�١^
        ProgNum = "4.1.1";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//�T���C

        #endregion

        #region �ŧi��T

        BL = new PhoneRec_01BL();//�޿�h

        #endregion

        #region GridView��T
        DataGridView = grvQuery;//GridView�]�w
        PagePanel = pnlPageInfo;//���Ƹ�T
        DataPanel = pnlGridView;//GridView�϶��]�w
        PageLabel = lblPageCount;//�`����
        PageNumDropDownList = ddlPageNum;//�ثe����
        PageUpButton = lnkPageUP;//�W�@��
        PageDownButton = lnkPageDown;//�U�@��
        #endregion

        #region ���s�w�q start

        QueryButton = btn_Query;
        ClearButton = btn_Clear;
        InsertButton = btn_Insert;

        #endregion ���s�w�q end

        #region �ɦV������T
        InsertPage = "PhoneRec_Ins_01.aspx";//�s�W��
        ModifyPage = "PhoneRec_Upd_01.aspx";//�קﭶ
        ListPage = "PhoneRec_Lis_01.aspx"; //��ܭ�
        #endregion �ɦV������T

        #region �n�J�ˬd�Ҧ�
        //checkLoginType = com.kangdainfo.online.WebBase.UI.checkLoginType.Need;
        checkLoginType = checkLoginType.Need;
        #endregion
    }

    /// <summary>
    /// �]�w��l��(�ݹ�@)
    /// </summary>
    public override void SetDefault()
    {
        BaseFun bf = new BaseFun();
        cbl_CntClass_Code.DataSource = bf.getSysCodeByKind("C", "Y");
        cbl_CntClass_Code.DataTextField = "Sys_CdText";
        cbl_CntClass_Code.DataValueField = "Sys_CdCode";
        cbl_CntClass_Code.RepeatDirection = RepeatDirection.Horizontal;
        cbl_CntClass_Code.RepeatColumns = 6;
        cbl_CntClass_Code.DataBind();

    }

    protected override bool BeforeDoDelete(DataTO qto)
    {

        return true;
    }
    protected override bool BeforeDoModify(int rowIdx, DataTO to)
    {
        base.BeforeDoModify(rowIdx, to);
        if (isMod)
            ModifyPage = "PhoneRec_Upd_01.aspx";
        else
            ModifyPage = "PhoneRec_Upd_02.aspx";

        return true;
    }
    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            ((ImageButton)e.Row.Cells[1].Controls[0]).OnClientClick = "if(!confirm('�T�w�n�R��?')){return false;}";
    }
}
