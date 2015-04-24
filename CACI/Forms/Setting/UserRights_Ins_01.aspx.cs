using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.UI;
using com.kangdainfo.online.WebBase.TO;

public partial class UserRights_Ins_01 : IInsertUI
{
    ///// <summary>
    ///// 將頁面資料全部收集為一個傳輸物件(TO)(需實作)
    ///// </summary>
    ///// <returns>傳輸物件</returns>
    public override DataTO PopulateData()
    {
        DataTO to = new DataTO();

        to.setValue("User_Code", txt_User_Code.Text);
        to.setValue("UsDp_Code", ddl_UsDp_Code.SelectedValue);
        to.setValue("User_Name", txt_User_Name.Text);
        to.setValue("User_AcStatus", (chk_User_AcStatus.Checked ? "L" : ""));
        to.setValue("User_Level", (chk_User_Level.Checked ? "S" : "N"));
        to.setValue("User_Tel", txt_User_Tel.Text);
        to.setValue("User_Cell", txt_User_Cell.Text);

        List<DataTO> permissions = new List<DataTO>();

        foreach (ListItem item in lis_SaveProgram.Items)
        {
            DataTO pto = new DataTO();

            pto.setValue("User_Code", txt_User_Code.Text);
            pto.setValue("Prog_Num", item.Value);
            pto.setValue("Rec_InfoID", getLoginUser().User_Code);

            permissions.Add(pto);
        }

        to.setValue("permissions", permissions);

        return to;
    }

    /// <summary>
    /// 設定程式參數(需實作)
    /// </summary>
    public override void SetProp()
    {
        #region 共同資訊

        ProgCd = this.GetType().BaseType.Name;//程式代號
        ProgNm = "人員權限管理作業─資料新增";//程式名稱
        ProgLabel = lblProg;//標題（程式代號+程式名稱）
        ProgNum = "9.1.6";
        ProgType = PAGE_ACTION.PAGE_INSERT;

        MessageLabel = lblMsg;//訊息列

        #endregion

        #region 宣告資訊

        BL = new UserRights_01BL();//邏輯層

        #endregion

        #region 按鈕定義 start

        InsertButton = btn_Insert;
        ClearButton = btn_Clear;
        BackButton = btn_Back;

        #endregion 按鈕定義 end

        #region 導向頁面資訊

        BackPage = "UserRights_Qry_01.aspx";

        #endregion 導向頁面資訊

        #region 頁面檢查設定
        checkLoginType = checkLoginType.Need;
        #endregion
    }

    /// <summary>
    /// 設定初始值(需實作)
    /// </summary>
    public override void SetDefault()
    {
        txt_User_Code.Text = "";
        txt_User_Name.Text = "";
        chk_User_AcStatus.Checked = false;
        chk_User_Level.Checked = false;
        txt_User_Tel.Text = "";
        txt_User_Cell.Text = "";

        ddl_UsDp_Code.DataSource = new BaseFun().getUserDep();
        ddl_UsDp_Code.DataBind();

        lis_Program.DataSource = ((UserRights_01BL)BL).getAllProgramData();
        lis_Program.DataBind();

        lis_SaveProgram.DataSource = null;
        lis_SaveProgram.DataBind();

    }
    protected void btn_Forward_Click(object sender, EventArgs e)
    {
        for (int i = lis_Program.Items.Count - 1; i > -1; i--)
        {
            if (lis_Program.Items[i].Selected)
            {
                if (lis_SaveProgram.Items.Count > 0)
                {
                    bool isAdd = false;
                    for (int t = 0; t < lis_SaveProgram.Items.Count; t++)
                    {
                        if (BaseFun.ProgNumIsBigger(lis_SaveProgram.Items[t].Value, lis_Program.Items[i].Value))
                        {
                            lis_SaveProgram.Items.Insert(t, new ListItem(lis_Program.Items[i].Text, lis_Program.Items[i].Value));
                            isAdd = true;
                            break;
                        }
                    }

                    if(!isAdd)
                        lis_SaveProgram.Items.Add(new ListItem(lis_Program.Items[i].Text, lis_Program.Items[i].Value));
                }
                else
                    lis_SaveProgram.Items.Add(new ListItem(lis_Program.Items[i].Text, lis_Program.Items[i].Value));

                lis_Program.Items.RemoveAt(i);
            }
        }
    }
    protected void btn_Retreat_Click(object sender, EventArgs e)
    {
        for (int i = lis_SaveProgram.Items.Count - 1; i > -1; i--)
        {
            if (lis_SaveProgram.Items[i].Selected)
            {
                if (lis_Program.Items.Count > 0)
                {
                    bool isAdd = false;
                    for (int t = 0; t < lis_Program.Items.Count; t++)
                    {
                        if (BaseFun.ProgNumIsBigger(lis_Program.Items[t].Value, lis_SaveProgram.Items[i].Value))
                        {
                            lis_Program.Items.Insert(t, new ListItem(lis_SaveProgram.Items[i].Text, lis_SaveProgram.Items[i].Value));
                            isAdd = true;
                            break;
                        }
                    }

                    if (!isAdd)
                        lis_Program.Items.Add(new ListItem(lis_SaveProgram.Items[i].Text, lis_SaveProgram.Items[i].Value));
                }
                else
                    lis_Program.Items.Add(new ListItem(lis_SaveProgram.Items[i].Text, lis_SaveProgram.Items[i].Value));

                lis_SaveProgram.Items.RemoveAt(i);
            }
        }
    }
}