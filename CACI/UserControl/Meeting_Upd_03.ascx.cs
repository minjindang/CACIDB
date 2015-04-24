using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.kangdainfo.online.WebBase.TO;
using System.Data;
using System.Data.SqlClient;
using com.kangdainfo.online.WebBase.DB;
using System.IO;
using CuteWebUI;
public partial class Meeting_Upd_03 : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Web Form Designer generated code

    protected override void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);
    }

    private void InitializeComponent()
    {
        this.DataBinding += new EventHandler(grvQuery_DataBinding);
    }

    void grvQuery_DataBinding(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)this.BindingContainer;
        if (!(row.DataItem is DataKey))
            throw new ArgumentException("Please change the TemplateDataMode attribute to 'Table' in the DataGridView declaration");

        DataKey key = (DataKey)row.DataItem;
        this.hid_tmpPath.Value = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;
        // TODO:取得資料並顯示
        string sqlStr = string.Empty;
        if (row.Cells[2].Text == "A")//獎補助
        {
            sqlStr = "SELECT b.Comm_Code,b.Comm_Name ,e.Record_Text ,e.Record_File,CASE  WHEN e.Record_Text IS NULL THEN 'Y' ELSE 'N' END AS IsNew, d.Meeting_Index " +
                    "FROM CACIDB..PjJudge a " +
                    "LEFT JOIN CACIDB..Committee b " +
                    "ON a.Comm_Code = b.Comm_Code " +
                    "LEFT JOIN CACIDB..Meeting c " +
                    "ON a.Pj_Code = c.Pj_Code " +
                    "LEFT JOIN CACIDB..MtgTimes d " +
                    "ON c.Meeting_Code = d.Meeting_Code " +
                    "LEFT JOIN CACIDB..MtgRecord e " +
                    "ON e.Meeting_Code = d.Meeting_Code AND e.Meeting_Index = d.Meeting_Index AND e.Comm_Code = b.Comm_Code " +
                    "WHERE a.Pj_Code = @Pj_Code AND d.Meeting_Code = @Meeting_Code AND d.Meeting_Index = @Meeting_Index ";
            SqlCommand cmd = new SqlCommand(sqlStr);
            cmd.Parameters.AddWithValue("@Meeting_Code", key[0].ToString());
            cmd.Parameters.AddWithValue("@Meeting_Index", key[1].ToString());
            cmd.Parameters.AddWithValue("@Pj_Code", row.Cells[3].Text);
            DataTable dt = new DataTable();
            new SQLAgent(DataBase.CACIDB).select(cmd, dt);
            grvQuery.DataSource = dt;
            grvQuery.DataBind();
        }
        else//輔導
        {
            sqlStr = "SELECT c.Comm_Code , c.Comm_Name ,d.Record_File ,d.Record_Text , CASE  WHEN d.Record_Text IS NULL THEN 'Y' ELSE 'N' END AS IsNew,d.Meeting_Index " +
                            "FROM CACIDB..MtgCrew b " +
                            "LEFT JOIN CACIDB.dbo.Committee c " +
                            "ON b.Comm_Code = c.Comm_Code " +
                            "LEFT JOIN CACIDB.dbo.MtgRecord d " +
                            "ON d.Comm_Code = b.Comm_Code AND d.Meeting_Code = b.Meeting_Code AND d.Meeting_Index = b.Meeting_Index " +
                            "WHERE b.Meeting_Code = @Meeting_Code AND b.Meeting_Index = @Meeting_Index ";
            SqlCommand cmd = new SqlCommand(sqlStr);
            cmd.Parameters.AddWithValue("@Meeting_Code", key[0].ToString());
            cmd.Parameters.AddWithValue("@Meeting_Index", key[1].ToString());
            DataTable dt = new DataTable();
            new SQLAgent(DataBase.CACIDB).select(cmd, dt);
            grvQuery.DataSource = dt;
            grvQuery.DataBind();
        }

    }

    #endregion


    //protected void RadAsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    //{
    //    string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

    //    string filePath = tempPath + @"\" + e.File.FileName;

    //    e.File.SaveAs(filePath);
    //}

    //protected void Uploader_FileUploaded(object sender, UploaderEventArgs e)
    //{
    //    //InsertMsg("File uploaded! " + args.FileName + ", " + args.FileSize + " bytes.");
    //    //Copys the uploaded file to a new location.
    //    //args.CopyTo(path);
    //    //You can also open the uploaded file's data stream.
    //    //System.IO.Stream data = args.OpenStream();
    //    string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;

    //    string filePath = tempPath + @"\" + e.FileName;

    //    e.MoveTo(filePath);
    //}
    protected void Uploader_FileUploaded(object sender, UploaderEventArgs e)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;
        GridViewRow GR = (GridViewRow)((UploadAttachments)sender).NamingContainer;
        String Comm_Code = grvQuery.DataKeys[GR.RowIndex].Values[0].ToString();
        String Meeting_Index = grvQuery.DataKeys[GR.RowIndex].Values[1].ToString();

        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith(Meeting_Index + "_" + Comm_Code))
                File.Delete(_path);
        }

        string filePath = tempPath + @"\" + Meeting_Index + "_" + Comm_Code + "_" + e.FileName;

        e.CopyTo(filePath);

        ((UploadAttachments)sender).InsertButton.Enabled = false;
    }

    protected void Uploader_AttachmentRemoveClicked(object sender, AttachmentItemEventArgs args)
    {
        string tempPath = Request.PhysicalApplicationPath + @"TempUploadFile\" + Session.SessionID;
        GridViewRow GR = (GridViewRow)((UploadAttachments)sender).NamingContainer;
        String Comm_Code = grvQuery.DataKeys[GR.RowIndex].Values[0].ToString();
        String Meeting_Index = grvQuery.DataKeys[GR.RowIndex].Values[1].ToString();
        
        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        foreach (string _path in Directory.GetFiles(tempPath))
        {
            if (new FileInfo(_path).Name.StartsWith(Meeting_Index + "_" + Comm_Code))
                File.Delete(_path);
        }

        ((UploadAttachments)sender).InsertButton.Enabled = true;
    }

    protected void grvQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[3].Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    public string get_tmpPath
    {
        get
        {
            return this.hid_tmpPath.Value;
        }

        set
        {
            this.hid_tmpPath.Value = value;

        }
    }
}