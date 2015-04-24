<%@ Application Language="C#" %>

<script runat="server">


    void Application_Start(object sender, EventArgs e) 
    {
        // 應用程式啟動時執行的程式碼

        com.kangdainfo.online.WebBase.UI.ICommonUI.LoginPage = "/CACI/Forms/Setting/Announcement_Lis_02.aspx";
        com.kangdainfo.online.WebBase.UI.ICommonUI.ProjectName = "文化創意產業資料庫管理系統";
        
        string strPathName = Server.MapPath("\\CACI\\") + "Web.sitemap";  

        Encoding oENC = Encoding.UTF8;

        System.Xml.XmlTextWriter oXML = new System.Xml.XmlTextWriter(strPathName, oENC);

        oXML.Formatting = System.Xml.Formatting.Indented;

        oXML.WriteStartDocument();                  

        oXML.WriteStartElement("siteMap");
        
        oXML.WriteAttributeString("xmlns", @"http://schemas.microsoft.com/AspNet/SiteMap-File-1.0"); 
        
        oXML.WriteStartElement("siteMapNode");
        
        oXML.WriteAttributeString("title", "文化創意產業資料庫管理系統");
        oXML.WriteAttributeString("moduleName", "文化創意產業資料庫管理系統");
        oXML.WriteAttributeString("description", "回到首頁");
        oXML.WriteAttributeString("target", "mainFrame");
        oXML.WriteAttributeString("url", "");

        System.Data.DataTable dt = new UserRights_01BL().getAllProgramData();

        for (int i = 1; i < 11; i++)
        {
            System.Data.DataRow[] selRows = dt.Select("Prog_Num Like '" + i.ToString() + ".%' ", "LV1,LV2,LV3");

            if (selRows.Length > 0)
            {
                oXML.WriteStartElement("siteMapNode");
                oXML.WriteAttributeString("id", i.ToString());

                string ClassName = new BaseFun().getSysCodeValue("R","K",i.ToString());

                oXML.WriteAttributeString("title", i.ToString() + "." + ClassName);
                oXML.WriteAttributeString("moduleName", i.ToString() + "." + ClassName);
                oXML.WriteAttributeString("description", i.ToString() + "." + ClassName);
                oXML.WriteAttributeString("target", "mainFrame");
                oXML.WriteAttributeString("url", "");
                
                foreach (System.Data.DataRow row in selRows)
                {
                    oXML.WriteStartElement("siteMapNode");
                    oXML.WriteAttributeString("id", row["Prog_Num"].ToString());
                    oXML.WriteAttributeString("title", row["Prog_Name"].ToString());
                    oXML.WriteAttributeString("moduleName", row["Prog_Name"].ToString());
                    oXML.WriteAttributeString("description", row["Prog_Name"].ToString());
                    oXML.WriteAttributeString("target", "mainFrame");
                    oXML.WriteAttributeString("url", row["Prog_Path"].ToString());

                    oXML.WriteEndElement();
                }
                
                oXML.WriteEndElement();
            }
        }

        oXML.WriteEndElement();
        
        oXML.WriteEndDocument();  
        oXML.Flush(); 
        oXML.Close();                
        oXML = null; 

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  應用程式關閉時執行的程式碼

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 發生未處理錯誤時執行的程式碼

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 啟動新工作階段時執行的程式碼

    }

    void Session_End(object sender, EventArgs e) 
    {
        // 工作階段結束時執行的程式碼。 
        // 注意: 只有在 Web.config 檔將 sessionstate 模式設定為 InProc 時，
        // 才會引發 Session_End 事件。如果將工作階段模式設定為 StateServer 
        // 或 SQLServer，就不會引發這個事件。

    }
       
</script>
