using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Project_01BL 的摘要描述
/// </summary>
public class Allowance_05BL : ICommonBL, IQueryMarkBL
{
    public Allowance_05BL()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    void IQueryMarkBL.MarkData(DataTO[] to, DataTO mto)
    {
        foreach (DataTO o in to)
        {
            DataTO inputTo = new DataTO();
            inputTo.setValue("Aow_Code", o.getValue("Aow_Code"));
            inputTo.setValue("Pj_Code", o.getValue("Pj_Code"));
            inputTo.setValue("Stage_Index", o.getValue("Stage_Index"));
            inputTo.setValue("AwSg_Date", mto.getValue("AwSg_Date"));
            inputTo.setValue("AwSg_Verify", mto.getValue("AwSg_Verify"));
            inputTo.setValue("AwSg_Text", mto.getValue("AwSg_Text"));
            //inputTo.setValue("Rec_InfoID", mto.getValue("Rec_InfoID").ToString());
            inputTo.setValue("Rec_Info", "\\getDate()");
            if (mto.getValue("isInsert").ToString() == "Y")
                new AowStage_01BL().insertAowStageData(inputTo);
            else
                new AowStage_01BL().updateAowStageData(inputTo);
        }
    }

    DataTable IQueryMarkBL.QueryDataForList(DataTO to, string sortStr)
    {
        throw new NotImplementedException();
    }

    DataTable IQueryMarkBL.QueryDataForList(DataTO to)
    {
        string sqlStr = "SELECT a.Aow_Code,a.Pj_Code ,d.Stage_Index,b.ApPj_Name,c.Com_Name,d.Stage_Name " +
                        ", ISNULL(dbo.getSysCodeText('S','S',e.AwSg_Verify ) ,'未審查') as AwSg_Verify " +
                        "FROM CACIDB.dbo.Allowance a " +
                        "LEFT JOIN CACIDB.dbo.ApPjContext b " +
                        "ON a.Aow_Code = b.Aow_Code " +
                        "LEFT JOIN CACIDB.dbo.Company c " +
                        "ON a.Com_Code = c.Com_Code " +
                        "RIGHT JOIN CACIDB.dbo.PjStage d " +
                        "ON d.Pj_Code = a.Pj_Code " +
                        "LEFT JOIn CACIDB.dbo.AowStage e " +
                        "ON e.Aow_Code = a.Aow_Code AND e.Pj_Code = a.Pj_Code AND e.Stage_Index = d.Stage_Index " +
                        "WHERE b.ApPj_Name IS NOT NULL ";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.CommandText += " AND a.Pj_Code=@Pj_Code ";
        cmd.Parameters.AddWithValue("@Pj_Code", to.getValue("Pj_Code").ToString());
        if (to.getValue("AwSg_Verify").ToString() == "N")
        {
            cmd.CommandText += " AND e.AwSg_Verify=@AwSg_Verify ";
            cmd.Parameters.AddWithValue("@AwSg_Verify", "N");
        }
        else
        {
            cmd.CommandText += " AND e.AwSg_Verify is null ";
        }
        cmd.CommandText += " AND d.Stage_Name=@Stage_Name ";
        cmd.Parameters.AddWithValue("@Stage_Name", to.getValue("Stage_Name").ToString());
        DataTable dt = new DataTable();
        try
        {
            System.Diagnostics.Debug.WriteLine(cmd.CommandText);
            new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
        return dt;
    }
}