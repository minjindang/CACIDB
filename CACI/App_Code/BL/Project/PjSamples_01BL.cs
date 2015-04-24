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
public class PjSamples_01BL : ICommonBL, IQueryBL, IMMDUIBL
{
    #region IQueryBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT RANK() OVER(ORDER BY PjSp_Code) AS Serial,PjSp_Code,PjSp_Name,PjSp_Kind " +
                        "FROM CACIDB..PjSamples " +
                        "WHERE 1=1";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            if (to.getAllColumnName()[i] == "PjSp_Name")
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + " Like '%' + @" + to.getAllColumnName()[i] + " + '%' ";
            else
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];

            cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
        }

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT RANK() OVER(ORDER BY PjSp_Code) AS Serial,PjSp_Code,PjSp_Name,PjSp_Kind " +
                        "FROM CACIDB..PjSamples " +
                        "WHERE 1=1";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            if (to.getAllColumnName()[i] == "PjSp_Name")
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + " Like '%' + @" + to.getAllColumnName()[i] + " + '%' ";
            else
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];

            cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
        }

        cmd.CommandText += " Order By " + sortStr;

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    void IQueryBL.DeleteData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..PjSamples", to));

        DataTO dTo = new DataTO();

        dTo.setValue("PjSp_Code", to.getValue("PjSp_Code").ToString());

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..SmpStage", dTo));

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    #endregion

    #region IMMDUIBL 成員

    void IMMDUIBL.InsertData(DataTO to, DataSet ds)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        //to.setValue("PjSp_Code", getNewSerialNo(DataBase.CACIDB, "PS"));

        //if (to.isColumnExist("PjSp_PjFile"))
        //    to.updateValue("PjSp_PjFile", "/CACI/UploadFile/PjSample/" + to.getValue("PjSp_Code").ToString() + "/" + to.getValue("PjSp_PjFile").ToString());

        to.setValue("PjSp_Code", PjSamples_01BL.getNewSerialNo(DataBase.CACIDB, "PS"));
        //檔案上傳
        if (to.isColumnExist("PjSp_PjFile"))
        {
            string defaultPath = HttpContext.Current.Request.PhysicalApplicationPath + @"UploadFile\PjSample\" + to.getValue("PjSp_Code");
            if (!Directory.Exists(defaultPath))
                Directory.CreateDirectory(defaultPath);
            File.Move(to.getValue("PjSp_PjFile").ToString(), Path.Combine(defaultPath, new FileInfo(to.getValue("PjSp_PjFile").ToString()).Name));

            to.updateValue("PjSp_PjFile", "/CACI/UploadFile/PjSample/" + to.getValue("PjSp_Code") + "/" + new FileInfo(to.getValue("PjSp_PjFile").ToString()).Name);
        }
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..PjSamples", to));

        for (int i = 0; i < ds.Tables.Count; i++)
        {
            foreach (DataRow row in ds.Tables[i].Rows)
            {
                DataTO dto = new DataTO();

                foreach (DataColumn column in ds.Tables[i].Columns)
                {
                    if ((column.ColumnName == "SpStage_Days" || column.ColumnName == "SpStage_RmDays") && row[column.ColumnName].ToString() == "")
                        continue;

                    dto.setValue(column.ColumnName, row[column.ColumnName].ToString());
                }

                dto.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());

                if (ds.Tables[i].TableName == "grv_Score")
                {
                    dto.setValue("Score_PjCode", to.getValue("PjSp_Code").ToString());

                    cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Score", dto));
                }
                else if (ds.Tables[i].TableName == "grv_SmpStage")
                {
                    dto.setValue("PjSp_Code", to.getValue("PjSp_Code").ToString());

                    cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..SmpStage", dto));
                }
            }
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }
    bool IMMDUIBL.IsDataExist(DataTO to)
    {

        if (!to.isColumnExist("PjSp_Code"))
            return false;
        else
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("PjSamples", to);
    }

    void IMMDUIBL.LoadData(DataTO to, DataSet ds)
    {
        //SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("PjSamples", to));
        string sqlStr = "SELECT * FROM CACIDB..PjSamples WHERE PjSp_Code = @PjSp_Code";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@PjSp_Code", to.getValue("PjSp_Code").ToString());
        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(cmd) ;
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

        DataTO ScoreTO = new DataTO();

        ScoreTO.setValue("Score_PjCode", to.getValue("PjSp_Code"));

        DataTable ScoreDt = new DataTable("grv_Score");

        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..Score",ScoreTO),ScoreDt);

        ds.Tables.Add(ScoreDt);

        DataTO SmpStageTo = new DataTO();

        SmpStageTo.setValue("PjSp_Code", to.getValue("PjSp_Code"));

        DataTable SmpStageDt = new DataTable("grv_SmpStage");

        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..SmpStage",SmpStageTo),SmpStageDt);

        ds.Tables.Add(SmpStageDt);
    }

    DataTable IMMDUIBL.QueryDetailData(string QueryGridViewID, DataTO to)
    {
        throw new NotImplementedException();
    }

    void IMMDUIBL.UpdateData(DataTO to, DataSet ds)
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

        for (int i = 0; i < ds.Tables.Count; i++)
        {
            foreach (DataRow row in ds.Tables[i].Rows)
            {
                DataTO dto = new DataTO();

                foreach (DataColumn column in ds.Tables[i].Columns)
                {
                    if ((column.ColumnName == "SpStage_Days" || column.ColumnName == "SpStage_RmDays") && row[column.ColumnName].ToString() == "")
                        continue;

                    dto.setValue(column.ColumnName, row[column.ColumnName].ToString());
                }

                dto.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());

                if (ds.Tables[i].TableName == "grv_Score")
                {
                    dto.setValue("Score_PjCode", to.getValue("PjSp_Code").ToString());

                    cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Score", dto));
                }
                else if (ds.Tables[i].TableName == "grv_SmpStage")
                {
                    dto.setValue("PjSp_Code", to.getValue("PjSp_Code").ToString());

                    cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..SmpStage", dto));
                }
            }
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