using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using com.kangdainfo.online.WebBase.DB;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.UI;

public partial class SysTree : ICommonUI
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //XmlDocument xmlDom = new XmlDocument();

            //xmlDom.Load(Request.PhysicalApplicationPath + @"Web.sitemap");

            //treeMenu.Nodes.Add(MakeMenuTree((XmlElement)xmlDom.DocumentElement.ChildNodes[0], 0));

            TreeNode ProjectNode = new TreeNode(ICommonUI.ProjectName);

            ProjectNode.NavigateUrl = "/CACI/Forms/Setting/Announcement_Lis_02.aspx";

            ProjectNode.Target = "mainFrame";

            treeMenu.Nodes.Add(ProjectNode);

            UserDataTO uTo = getLoginUser();

            DataTable dt = new UserRights_01BL().getUserPermissionPrograms(uTo.User_Code);

            for (int i = 1; i < 11; i++)
            {
                DataRow[] selRows = dt.Select("Prog_Num Like '" + i.ToString() + ".%' AND (Prog_Type='Q' OR Prog_Num='10.5.1' OR Prog_Num ='10.4.3' OR Prog_Num='10.3.3' OR Prog_Num='10.7.1' ) AND NOT Prog_Num='1.2.16' ", "LV1,LV2,LV3");

                if (selRows.Length > 0)
                {
                    TreeNode parentNode = new TreeNode(i.ToString() + "." + new BaseFun().getSysCodeValue("R","K",i.ToString()));

                    parentNode.Value = "";

                    parentNode.PopulateOnDemand = false;

                    foreach (DataRow row in selRows)
                    {
                        TreeNode childNode = new TreeNode();

                        childNode.Text = row["Prog_Name"].ToString();

                        childNode.Value = @"/CACI/" + row["Prog_Path"].ToString();

                        childNode.PopulateOnDemand = false;

                        childNode.Target = "mainFrame";

                        parentNode.ChildNodes.Add(childNode);
                    }

                    ProjectNode.ChildNodes.Add(parentNode);
                }
            }
        }

        if (treeMenu.SelectedNode != null && treeMenu.SelectedNode.Value != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "GoTarget", "top." + treeMenu.SelectedNode.Target + ".location='" + treeMenu.SelectedNode.Value + "'", true);
        }
    }

    public TreeNode MakeMenuTree(XmlElement nodeElement, int level)
    {
        TreeNode treeNode = new TreeNode(); //建立新節點

        //指定節點的各屬性值
        treeNode.Text = nodeElement.GetAttribute("moduleName");
        treeNode.Target = nodeElement.GetAttribute("target");

        //用treeNode.NavigateUrl，畫面上不會postBack，造成在FireFox時顯示，紅框框會無法取消顯示
        //用treeNode.Value使網頁PostBock, 取得資料----------------------------------------------

        if (nodeElement.GetAttribute("url") == "")
            treeNode.Value = nodeElement.GetAttribute("url");
        else
            treeNode.Value = nodeElement.GetAttribute("url") + "?log=1";

        //-------------------------------------------------------------------------------------

        treeNode.PopulateOnDemand = false;

        treeNode.ToolTip = nodeElement.GetAttribute("moduleName");

        //若傳入的節點id為0，則表示它是最上層的根節點，設定根節點的圖形
        //if (nodeElement.GetAttribute("id") == "0")
        //    treeNode.ImageUrl = @"image\root.gif";

        if (nodeElement.ChildNodes.Count == 0)
            treeNode.SelectAction = TreeNodeSelectAction.Select;
        else
            treeNode.SelectAction = TreeNodeSelectAction.Expand;

        //再逐筆建立目前傳入之節點下的所有子節點的選單
        if (level < 2)
        {
            foreach (XmlElement myNodeElement in nodeElement.ChildNodes)
            {
                //取得該節點的模組ID
                string intModuleID = myNodeElement.GetAttribute("id");

                //判斷是否擁有此節點選單的權限，若擁有此節點的權限，則建立此節點的選單
                //若為最高管理者，則擁有全部的節點權限
                //If blnIsAdmin = True Or (Array.IndexOf(aryModulesID, intModuleID.ToString) <> -1) Then
                treeNode.ChildNodes.Add(MakeMenuTree(myNodeElement, level + 1));
                //Else

                //    '若沒有此節點的權限，則要再判斷是否擁有此節點下的任一節點的權限
                //    '若擁有此節點下的某個節點的權限，則仍然要建立節點
                //    If IsHasModule(myNodeElement, aryModulesID) Then
                //        treeNode.ChildNodes.Add(MakeMenuTree(myNodeElement, aryModulesID, blnIsAdmin))
                //    End If

                //End If

            }
        }

        return treeNode;
    }

    public override void SetDefault()
    {
        throw new NotImplementedException();
    }

    public override void SetProp()
    {
        throw new NotImplementedException();
    }
}