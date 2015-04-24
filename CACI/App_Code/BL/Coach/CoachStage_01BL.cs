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
/// CoachStage_01BL 的摘要描述
/// </summary>
public class CoachStage_01BL : ICommonBL, IMDUIBL
{
    public CoachStage_01BL()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    void IMDUIBL.InsertData(DataTO to, DataTable dt)
    {
        throw new NotImplementedException();
    }

    bool IMDUIBL.IsDataExist(DataTO to)
    {
        if (to.isColumnExist("Coach_Code"))
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Coach", to);
        else
            return false;
    }

    void IMDUIBL.LoadData(DataTO to, DataTable dt)
    {
        BaseFun bf = new BaseFun();
        Char[] splitStr = { '_' };
        string sqlstr = "SELECT a.Pj_Code,b.Coach_Code,b.ChKd_Code,a.Stage_Index,a.Stage_Name ,dbo.chgToChnDate(dateadd(d, a.Stage_Days, a.Stage_Date)) as CoachStage_Date ,d.Pj_Name,e.*, " +
                        "a.Stage_Text,ISNULL(dbo.getSysCodeText('O','S',c.ChSg_Verify ) ,'未審查') as  ChSg_Verify " +
                        "FROM CACIDB.dbo.PjStage a " +
                        "LEFT JOIN CACIDB.dbo.Coach b " +
                        "ON a.Pj_Code = b.Pj_Code " +
                        "LEFT JOIN CACIDB.dbo.CoachStage c " +
                        "ON a.Pj_Code = c.Pj_Code and b.Coach_Code = c.Coach_Code " +
                        "LEFT JOIN CACIDB.dbo.Project d " +
                        "ON a.Pj_Code = d.Pj_Code " +
                        "LEFT JOIN CACIDB.dbo.Company e " +
                        "ON b.Com_Code = e.Com_Code " +
                        "WHERE a.Pj_Code = @Pj_Code AND b.Coach_Code = @Coach_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);
        cmd.Parameters.AddWithValue("@Pj_Code", to.getValue("Pj_Code"));
        cmd.Parameters.AddWithValue("@Coach_Code", to.getValue("Coach_Code"));
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (dt.Columns[i].ColumnName.Split(splitStr)[0].Equals("Com") || dt.Columns[i].ColumnName.Equals("Pj_Name") || dt.Columns[i].ColumnName.Equals("ChKd_Code"))
                to.setValue(dt.Columns[i].ColumnName, dt.Rows[0][dt.Columns[i].ColumnName]);
        }
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        throw new NotImplementedException();
    }

    public DataTO getCoachStageData(string Pj_Code, string Coach_Code, int Stage_Index)
    {
        DataTO to = new DataTO();

        to.setValue("Coach_Code", Coach_Code);
        to.setValue("Pj_Code", Pj_Code);
        to.setValue("Stage_Index", Stage_Index);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..CoachStage", to));

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

        string sqlstr = "SELECT DISTINCT Stage_Text ,dbo.chgToChnDate(dateadd(d, a.Stage_Days, a.Stage_Date)) as CoachStage_Date " +
                        "FROM CACIDB.dbo.PjStage a " +
                        "WHERE a.Pj_Code = @Pj_Code AND a.Stage_Index = @Stage_Index ";

        SqlCommand cmd = new SqlCommand(sqlstr);
        cmd.Parameters.AddWithValue("@Pj_Code", to.getValue("Pj_Code"));
        cmd.Parameters.AddWithValue("@Stage_Index", to.getValue("Stage_Index"));
        DataTable dt = new DataTable();
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            to.setValue(dt.Columns[i].ColumnName, dt.Rows[0][dt.Columns[i].ColumnName]);
        }
        return to;
    }

    public void insertCoachStageData(DataTO to)
    {
       // to.setValue("Rec_InfoID", "\\" + to.getValue("User_Code").ToString());
        //to.setValue("Rec_Info", "\\getDate()");
        try
        {
            new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CoachStage", to));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }

    public void updateCoachStageData(DataTO to)
    {
        //to.setValue("Rec_InfoID", "\\" + to.getValue("Rec_InfoID").ToString());
       // to.setValue("Rec_Info", "\\getDate()");
        try
        {
            //System.Diagnostics.Debug.WriteLine(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CoachStage", to).ToString());
            new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CoachStage", to));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }

    public DataTable  getEvaluateCommittee(DataTO to)
    {
        DataTable dt = new DataTable();
        string sql = "SELECT distinct a.Coach_Code,a.Meeting_Code,a.Meeting_Index ,a.Comm_Code,b.Comm_Name " +
                    "FROM CACIDB.dbo.Evaluations a " +
                    "LEFT JOIN CACIDB.dbo.Committee b " +
                     "ON a.Comm_Code = b.Comm_Code " +
                    "LEFT JOIN CACIDB.dbo.CoachMeeting c " +
                    "ON a.Meeting_Code = c.Meeting_Code " +
                    "WHERE c.Coach_Code = @Coach_Code AND a.Meeting_Code = @Meeting_Code AND a.Meeting_Index = @Meeting_Index ";
        SqlCommand cmd = new SqlCommand(sql);
        cmd.Parameters.AddWithValue("@Coach_Code", to.getValue("Coach_Code").ToString());
        cmd.Parameters.AddWithValue("@Meeting_Code", to.getValue("Meeting_Code").ToString());
        cmd.Parameters.AddWithValue("@Meeting_Index", to.getValue("Meeting_Index").ToString());
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }

    public DataTable getEvaluateScore(DataTO to)
    {
        DataTable dt = new DataTable();
        string sql = "SELECT b.Score_Items,b.Score_Max,a.Tail_Text,a.Tail_Score " +
                    "FROM [CACIDB].dbo.[EvalTail] a " +
                    "LEFT JOIN [CACIDB].dbo.Score b " +
                    "ON a.Score_Code = b.Score_Code " +
                    "LEFT JOIN [CACIDB].dbo.CoachMeeting c " +
                    "ON a.Meeting_Code = c.Meeting_Code " +
                    "WHERE c,Coach_Code = @Coach_Code AND a.Meeting_Code = @Meeting_Code " +
                    "AND a.Meeting_Index = @Meeting_Index AND Comm_Code = @Comm_Code ";
        SqlCommand cmd = new SqlCommand(sql);
        cmd.Parameters.AddWithValue("@Coach_Code", to.getValue("Coach_Code").ToString());
        cmd.Parameters.AddWithValue("@Meeting_Code", to.getValue("Meeting_Code").ToString());
        cmd.Parameters.AddWithValue("@Meeting_Index", to.getValue("Meeting_Index").ToString());
        cmd.Parameters.AddWithValue("@Comm_Code", to.getValue("Comm_Code").ToString());
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }


}