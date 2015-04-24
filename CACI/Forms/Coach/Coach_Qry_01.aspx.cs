using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class Coach_Qry_01 : IQueryUI
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
        if (txt_Coach_Code.Text != "")
            to.setValue("Coach_Code", txt_Coach_Code.Text);

        if (txt_Pj_Name.Text != "")
            to.setValue("Pj_Name", txt_Pj_Name.Text);

        if (txt_Com_Boss.Text != "")
            to.setValue("Com_Boss", txt_Com_Boss.Text);
        
        if (txt_Com_Name.Text != "")
            to.setValue("Com_Name", txt_Com_Name.Text);
        
        if (txt_Coach_DateS.Text != "")
            to.setValue("Coach_DateS", txt_Coach_DateS.Text);
        
        if (txt_Coach_DateE.Text != "")
            to.setValue("Coach_DateE", txt_Coach_DateE.Text);

        string selectedChKd_CodeTerms = this.CoachKindCheckboxList1.ChKd_Code_Items;

        if (selectedChKd_CodeTerms != "")
            to.setValue("ChKd_Code", selectedChKd_CodeTerms);

        return to;
    }

    /// <summary>
    /// �NSession���쪺�����ܦb�e���W(�ݹ�@)
    /// </summary>
    /// <returns></returns>
    public override void LoadSessionTO(DataTO to)
    {
        //�ӽнs��
        if (txt_Coach_Code.Text != "")
            txt_Coach_Code.Text = to.getValue("Coach_Code").ToString();
       
        if (txt_Com_Name.Text != "")
            txt_Com_Name.Text = to.getValue("Com_Name").ToString();

        if (txt_Pj_Name.Text != "")
            txt_Pj_Name.Text = to.getValue("Pj_Name").ToString();

        if (txt_Com_Boss.Text != "")
            txt_Com_Boss.Text = to.getValue("Com_Boss").ToString();


        //���ɮɶ��_
        if (txt_Coach_DateS.Text != "")
            txt_Coach_DateS.Text = to.getValue("Coach_DateS").ToString();
        //���ɮɶ���
        if (txt_Coach_DateE.Text != "")
            txt_Coach_DateE.Text = to.getValue("Coach_DateE").ToString();

        //�B�z���G
        /*if (to.getValue("Cnst_Status").ToString() != "")
        {
            foreach (ListItem li in ckl_Cnst_Stat.Items)
            {
                if (to.getValue("Cnst_Status").ToString().IndexOf(li.Value) != -1)
                    li.Selected = true;
            }
        }*/
    }

    /// <summary>
    /// �]�w�{���Ѽ�(�ݹ�@)
    /// </summary>
    public override void SetProp()
    {
        #region �@�P��T

        PageDropDownList = ddlPage;//�C�����ơ]�d�߱���^

        ProgCd = this.GetType().BaseType.Name;//�{���N��
        ProgNm = "���ɸ�Ƭd�ߺ��@�w������Ƭd�ߵe��  ";//�{���W��
        ProgLabel = lblProg;//���D�]�{���N��+�{���W�١^
        ProgNum = "5.1.1";
        ProgType = PAGE_ACTION.PAGE_QUERY;

        MessageLabel = lblMsg;//�T���C

        #endregion

        #region �ŧi��T

        BL = new Coach_01BL();//�޿�h

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
        InsertPage = "Coach_Qry_02.aspx";//�s�W��
        ModifyPage = "Coach_Upd_01.aspx";//�קﭶ
        ListPage = "Coach_Lis_01.aspx"; //��ܭ�
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
        //BaseFun bf = new BaseFun();
       //Control ctlNewTrial = this.Page.LoadControl("~/UserControl/CoachKindCheckboxList.ascx");

            //SetUserControlProperty(ctlNewTrial, "ID", "Usrctrl" + index.ToString());

           //SetUserControlProperty(ctlNewTrial, "CustID", index.ToString());

            //SetUserControlProperty(ctlNewTrial, "CustName", index.ToString()+":name");


        //this.pnl_ChKd_Code.Controls.Clear();
        //this.pnl_ChKd_Code.Controls.Add(ctlNewTrial);

        // Label br = new Label();

           // br.Text = "index=" + index.ToString()+ "<br/>";

            //this.Panel1.Controls.Add(ctlNewTrial);
        
           // this.Panel1.Controls.Add(br);


    }

    protected override bool BeforeDoDelete(DataTO qto)
    {

        return true;
    }
    protected override bool BeforeDoModify(int rowIdx, DataTO to)
    {
        base.BeforeDoModify(rowIdx, to);
        if (isMod)
            ModifyPage = "Coach_Upd_01.aspx";
        else
            ModifyPage = "Coach_Upd_02.aspx";

        return true;
    }
    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            ((ImageButton)e.Row.Cells[1].Controls[0]).OnClientClick = "if(!confirm('�T�w�n�R��?')){return false;}";
    }
}
