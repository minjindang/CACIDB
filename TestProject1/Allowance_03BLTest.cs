using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace TestProject1
{
    
    
    /// <summary>
    ///這是 Allowance_03BLTest 的測試類別，應該包含
    ///所有 Allowance_03BLTest 單元測試
    ///</summary>
    [TestClass()]
    public class Allowance_03BLTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///取得或設定提供目前測試回合的相關資訊與功能
        ///的測試內容。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 其他測試屬性
        // 
        //您可以在撰寫測試時，使用下列的其他屬性:
        //
        //在執行類別中的第一項測試之前，先使用 ClassInitialize 執行程式碼
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //在執行類別中的所有測試之後，使用 ClassCleanup 執行程式碼
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //在執行每一項測試之前，先使用 TestInitialize 執行程式碼
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //在執行每一項測試之後，使用 TestCleanup 執行程式碼
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Allowance_03BL 建構函式 的測試
        ///</summary>
        // TODO: 請確定 UrlToTest 屬性有為 ASP.NET 網頁指定 URL (例如，
        // http://.../Default.aspx)。這是要在 Web 伺服器上執行單元測試時的必要項目，
        // 無論您是要測試頁面、Web 服務或是 WCF 服務，都是如此。
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\CACIDB\\CACI", "/CACI")]
        [UrlToTest("http://localhost/CACI")]
        public void Allowance_03BLConstructorTest()
        {
            Allowance_03BL_Accessor target = new Allowance_03BL_Accessor();
            Assert.Inconclusive("TODO: 實作程式碼以驗證目標");
        }

        /// <summary>
        ///com.kangdainfo.online.WebBase.BL.IMDUIBL.InsertData 的測試
        ///</summary>
        // TODO: 請確定 UrlToTest 屬性有為 ASP.NET 網頁指定 URL (例如，
        // http://.../Default.aspx)。這是要在 Web 伺服器上執行單元測試時的必要項目，
        // 無論您是要測試頁面、Web 服務或是 WCF 服務，都是如此。
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\CACIDB\\CACI", "/CACI")]
        [UrlToTest("http://localhost/CACI")]
        public void InsertDataTest()
        {
            // 私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。
            Assert.Inconclusive("私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。");
        }

        /// <summary>
        ///com.kangdainfo.online.WebBase.BL.IMDUIBL.IsDataExist 的測試
        ///</summary>
        // TODO: 請確定 UrlToTest 屬性有為 ASP.NET 網頁指定 URL (例如，
        // http://.../Default.aspx)。這是要在 Web 伺服器上執行單元測試時的必要項目，
        // 無論您是要測試頁面、Web 服務或是 WCF 服務，都是如此。
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\CACIDB\\CACI", "/CACI")]
        [UrlToTest("http://localhost/CACI")]
        public void IsDataExistTest()
        {
            // 私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。
            Assert.Inconclusive("私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。");
        }

        /// <summary>
        ///com.kangdainfo.online.WebBase.BL.IMDUIBL.LoadData 的測試
        ///</summary>
        // TODO: 請確定 UrlToTest 屬性有為 ASP.NET 網頁指定 URL (例如，
        // http://.../Default.aspx)。這是要在 Web 伺服器上執行單元測試時的必要項目，
        // 無論您是要測試頁面、Web 服務或是 WCF 服務，都是如此。
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\CACIDB\\CACI", "/CACI")]
        [UrlToTest("http://localhost/CACI")]
        public void LoadDataTest()
        {
            // 私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。
            Assert.Inconclusive("私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。");
        }

        /// <summary>
        ///com.kangdainfo.online.WebBase.BL.IMDUIBL.UpdateData 的測試
        ///</summary>
        // TODO: 請確定 UrlToTest 屬性有為 ASP.NET 網頁指定 URL (例如，
        // http://.../Default.aspx)。這是要在 Web 伺服器上執行單元測試時的必要項目，
        // 無論您是要測試頁面、Web 服務或是 WCF 服務，都是如此。
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\CACIDB\\CACI", "/CACI")]
        [UrlToTest("http://localhost/CACI")]
        public void UpdateDataTest()
        {
            // 私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。
            Assert.Inconclusive("私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。");
        }

        /// <summary>
        ///com.kangdainfo.online.WebBase.BL.IMasterUIBL.InsertData 的測試
        ///</summary>
        // TODO: 請確定 UrlToTest 屬性有為 ASP.NET 網頁指定 URL (例如，
        // http://.../Default.aspx)。這是要在 Web 伺服器上執行單元測試時的必要項目，
        // 無論您是要測試頁面、Web 服務或是 WCF 服務，都是如此。
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\CACIDB\\CACI", "/CACI")]
        [UrlToTest("http://localhost/CACI")]
        public void InsertDataTest1()
        {
            // 私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。
            Assert.Inconclusive("私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。");
        }

        /// <summary>
        ///com.kangdainfo.online.WebBase.BL.IMasterUIBL.IsDataExist 的測試
        ///</summary>
        // TODO: 請確定 UrlToTest 屬性有為 ASP.NET 網頁指定 URL (例如，
        // http://.../Default.aspx)。這是要在 Web 伺服器上執行單元測試時的必要項目，
        // 無論您是要測試頁面、Web 服務或是 WCF 服務，都是如此。
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\CACIDB\\CACI", "/CACI")]
        [UrlToTest("http://localhost/CACI")]
        public void IsDataExistTest1()
        {
            // 私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。
            Assert.Inconclusive("私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。");
        }

        /// <summary>
        ///com.kangdainfo.online.WebBase.BL.IMasterUIBL.LoadData 的測試
        ///</summary>
        // TODO: 請確定 UrlToTest 屬性有為 ASP.NET 網頁指定 URL (例如，
        // http://.../Default.aspx)。這是要在 Web 伺服器上執行單元測試時的必要項目，
        // 無論您是要測試頁面、Web 服務或是 WCF 服務，都是如此。
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\CACIDB\\CACI", "/CACI")]
        [UrlToTest("http://localhost/CACI")]
        public void LoadDataTest1()
        {
            // 私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。
            Assert.Inconclusive("私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。");
        }

        /// <summary>
        ///com.kangdainfo.online.WebBase.BL.IMasterUIBL.MarkData 的測試
        ///</summary>
        // TODO: 請確定 UrlToTest 屬性有為 ASP.NET 網頁指定 URL (例如，
        // http://.../Default.aspx)。這是要在 Web 伺服器上執行單元測試時的必要項目，
        // 無論您是要測試頁面、Web 服務或是 WCF 服務，都是如此。
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\CACIDB\\CACI", "/CACI")]
        [UrlToTest("http://localhost/CACI")]
        public void MarkDataTest()
        {
            // 私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。
            Assert.Inconclusive("私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。");
        }

        /// <summary>
        ///com.kangdainfo.online.WebBase.BL.IMasterUIBL.UpdateData 的測試
        ///</summary>
        // TODO: 請確定 UrlToTest 屬性有為 ASP.NET 網頁指定 URL (例如，
        // http://.../Default.aspx)。這是要在 Web 伺服器上執行單元測試時的必要項目，
        // 無論您是要測試頁面、Web 服務或是 WCF 服務，都是如此。
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\CACIDB\\CACI", "/CACI")]
        [UrlToTest("http://localhost/CACI")]
        public void UpdateDataTest1()
        {
            // 私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。
            Assert.Inconclusive("私用存取子並未保留跨組件的類別繼承，但每個私用存取子類別中都提供了靜態方法 AttachShadow()，以便在私用存取子類別之間轉換私用物件。");
        }
    }
}
