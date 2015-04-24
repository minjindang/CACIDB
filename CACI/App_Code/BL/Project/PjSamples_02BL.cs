using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;
using System.IO;

/// <summary>
/// SAMPLEBL 的摘要描述
/// </summary>
public class PjSamples_02BL : ICommonBL, IMDUIBL
{
    #region IMDUIBL 成員

    void IMDUIBL.InsertData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        to.setValue("PjSp_Code", getNewSerialNo(DataBase.CACIDB, "PS"));

        //if (to.isColumnExist("PjSp_PjFile"))
        //    to.updateValue("PjSp_PjFile", "/CACI/UploadFile/PjSample/" + to.getValue("PjSp_Code").ToString() + "/" + to.getValue("PjSp_PjFile").ToString());
        ////檔案上傳
        if (to.isColumnExist("PjSp_PjFile"))
        {
            string defaultPath = HttpContext.Current.Request.PhysicalApplicationPath + @"UploadFile\PjSample\" + to.getValue("PjSp_Code");
            if (!Directory.Exists(defaultPath))
                Directory.CreateDirectory(defaultPath);
            File.Move(to.getValue("PjSp_PjFile").ToString(), Path.Combine(defaultPath, new FileInfo(to.getValue("PjSp_PjFile").ToString()).Name));

            to.updateValue("PjSp_PjFile", "/CACI/UploadFile/PjSample/" + to.getValue("PjSp_Code") + "/" + new FileInfo(to.getValue("PjSp_PjFile").ToString()).Name);
        }

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..PjSamples", to));

        foreach (DataRow row in dt.Rows)
        {
            DataTO dto = new DataTO();

            foreach (DataColumn column in dt.Columns)
            {
                if ((column.ColumnName == "SpStage_Days" || column.ColumnName == "SpStage_RmDays") && row[column.ColumnName].ToString() == "")
                    continue;

                dto.setValue(column.ColumnName, row[column.ColumnName].ToString());
            }

            dto.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());

            dto.setValue("PjSp_Code", to.getValue("PjSp_Code").ToString());

            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..SmpStage", dto));
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    bool IMDUIBL.IsDataExist(DataTO to)
    {
        if (!to.isColumnExist("PjSp_Code"))
            return false;
        else
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("PjSamples", to);
    }

    void IMDUIBL.LoadData(DataTO to, DataTable dt)
    {
        //SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("PjSamples", to));
        string sqlStr = "SELECT * FROM CACIDB..PjSamples WHERE PjSp_Code = @PjSp_Code";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@PjSp_Code", to.getValue("PjSp_Code").ToString());
        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(cmd);
        if (sr.Read())
        {
            for (int i = 0; i < sr.FieldCount; i++)
            {
                if (!to.isColumnExist(sr.GetName(i)))
                {
                    to.setValue(sr.GetName(i), sr[sr.GetName(i)].ToString());
                }
            }
        }

        sr.Close();

        DataTO SmpStageTo = new DataTO();

        SmpStageTo.setValue("PjSp_Code", to.getValue("PjSp_Code"));

        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..SmpStage", SmpStageTo), dt);
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        //if (to.isColumnExist("PjSp_PjFile"))
        //    to.updateValue("PjSp_PjFile", "/CACI/UploadFile/PjSample/" + to.getValue("PjSp_Code").ToString() + "/" + to.getValue("PjSp_PjFile").ToString());
        if (to.isColumnExist("PjSp_PjFile"))
        {
            string defaultPath = HttpContext.Current.Request.PhysicalApplicationPath + @"UploadFile\PjSample\" + to.getValue("PjSp_Code").ToString();

            if (Directory.Exists(defaultPath))
            {
                foreach (string file in Directory.GetFiles(defaultPath))
                {
                    if (file.StartsWith("PjSp_"))
                        File.Delete(Path.Combine(defaultPath, file));
                }
            }
            else
            {
                Directory.CreateDirectory(defaultPath);
            }

            File.Move(to.getValue("PjSp_PjFile").ToString(), Path.Combine(defaultPath, new FileInfo(to.getValue("PjSp_PjFile").ToString()).Name));

            to.updateValue("PjSp_PjFile", "/CACI/UploadFile/PjSample/" + to.getValue("PjSp_Code").ToString() + "/" + new FileInfo(to.getValue("PjSp_PjFile").ToString()).Name);
        }

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..SmpStage", to));

        DataTO dScoreTO = new DataTO();

        dScoreTO.setValue("Score_PjCode", to.getValue("PjSp_Code").ToString());

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..Score", to));

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..PjSamples", to));

        foreach (DataRow row in dt.Rows)
        {
            DataTO dto = new DataTO();

            foreach (DataColumn column in dt.Columns)
            {
                if ((column.ColumnName == "SpStage_Days" || column.ColumnName == "SpStage_RmDays") && row[column.ColumnName].ToString() == "")
                    continue;

                dto.setValue(column.ColumnName, row[column.ColumnName].ToString());
            }

            dto.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());

            dto.setValue("PjSp_Code", to.getValue("PjSp_Code").ToString());

            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..SmpStage", dto));
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    #endregion

    #region 自訂程序

    public DataTO getSmpStageData(string PjSp_Code, int SpStage_Index)
    {
        DataTO smpStageTo = new DataTO();

        smpStageTo.setValue("PjSp_Code", PjSp_Code);
        smpStageTo.setValue("SpStage_Index", SpStage_Index);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..SmpStage", smpStageTo));

        if (sr.Read())
        {
            for (int i = 0; i < sr.FieldCount; i++)
            {
                if (!smpStageTo.isColumnExist(sr.GetName(i)))
                {
                    smpStageTo.setValue(sr.GetName(i), sr[sr.GetName(i)].ToString());
                }
            }
        }

        sr.Close();

        return smpStageTo;
    }

    #endregion
}