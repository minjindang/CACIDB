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
/// AowStage_01BL 的摘要描述
/// </summary>
public class AowStage_01BL : ICommonBL, IMDUIBL
{
	public AowStage_01BL()
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
        if (to.isColumnExist("Aow_Code"))
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Allowance", to);
        else
            return false;
    }

    void IMDUIBL.LoadData(DataTO to, DataTable dt)
    {
        string sqlstr = "SELECT a.Pj_Code,b.Aow_Code,a.Stage_Index,a.Stage_Name ,ISNULL(dbo.chgToChnDate(a.Stage_Date),'N/A') as Stage_Date,  " +
                        "a.Stage_Text,ISNULL(dbo.getSysCodeText('S','S',c.AwSg_Verify ) ,'未審查') as AwSg_Verify  " +
                        "FROM CACIDB.dbo.PjStage a " +
                        "LEFT JOIN CACIDB.dbo.Allowance b " +
                        "ON a.Pj_Code = b.Pj_Code " +
                        "LEFT JOIN CACIDB.dbo.AowStage c " +
                        "ON a.Pj_Code = c.Pj_Code and b.Aow_Code = c.Aow_Code and a.Stage_Index = c.Stage_Index " +
                        "WHERE a.Pj_Code = @Pj_Code AND b.Aow_Code = @Aow_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);
        cmd.Parameters.AddWithValue("@Pj_Code", to.getValue("Pj_Code"));
        cmd.Parameters.AddWithValue("@Aow_Code", to.getValue("Aow_Code"));
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        //to.setValue("AowStage_Update", dt);

        //to.getValue("AowStage_Update")
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        throw new NotImplementedException();
    }

    public DataTO getAowStageData(string Pj_Code, string Aow_Code, int Stage_Index)
    {
        DataTO to = new DataTO();
        string sql = "SELECT a.Stage_Text ,a.Stage_Date, " +
                    "c.AwSg_Date ,c.AwSg_Text,c.AwSg_Verify, " +
                    "c.Aow_Code ,c.Pj_Code ,c.Stage_Index ,c.Rec_Info, " +
                    "d.Meeting_Name ,d.Meeting_BgnTime ,d.Meeting_Code ,e.Meeting_Index " +
                    "FROM CACIDB..PjStage a " +
                    "LEFT JOIN CACIDB..Allowance b " +
                    "ON a.Pj_Code = b.Pj_Code " +
                    "LEFT JOIN CACIDB..AowStage c " +
                    "ON a.Pj_Code = c.Pj_Code AND b.Aow_Code = c.Aow_Code " +
                    "LEFT JOIN CACIDB..Meeting d " +
                    "ON a.Metting_Code = d.Meeting_Code " +
                    "LEFT JOIN CACIDB..MtgTimes e " +
                    "ON d.Meeting_Code = e.Meeting_Code " +
                    "WHERE  a.Pj_Code = @Pj_Code AND b.Aow_Code = @Aow_Code AND a.Stage_Index = @Stage_Index AND c.Stage_Index = @Stage_Index";
        SqlCommand cmd = new SqlCommand(sql);
        cmd.Parameters.AddWithValue("@Pj_Code", Pj_Code);
        cmd.Parameters.AddWithValue("@Aow_Code", Aow_Code);
        cmd.Parameters.AddWithValue("@Stage_Index", Stage_Index);
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
        return to;
    }

    public void insertAowStageData(DataTO to)
    {
        new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..AowStage", to));   
    }


    public void updateAowStageData(DataTO to)
    {
        new SQLAgent(DataBase.CACIDB).execute(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..AowStage", to));
    }

    public DataTable  getEvaluateCommittee(DataTO to)
    {
        DataTable dt = new DataTable();
        string sql = "SELECT distinct a.Aow_Code,a.Meeting_Code,a.Meeting_Index ,a.Comm_Code,b.Comm_Name " +
                    "FROM CACIDB.dbo.Evaluations a " +
                    "LEFT JOIN CACIDB.dbo.Committee b " +
                    "ON a.Comm_Code = b.Comm_Code " +
                    "WHERE Aow_Code = @Aow_Code AND Meeting_Code = @Meeting_Code AND Meeting_Index = @Meeting_Index ";
        SqlCommand cmd = new SqlCommand(sql);
        cmd.Parameters.AddWithValue("@Aow_Code", to.getValue("Aow_Code").ToString());
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
                    "WHERE Aow_Code = @Aow_Code AND Meeting_Code = @Meeting_Code " +
                    "AND Meeting_Index = @Meeting_Index AND Comm_Code = @Comm_Code ";
        SqlCommand cmd = new SqlCommand(sql);
        cmd.Parameters.AddWithValue("@Aow_Code", to.getValue("Aow_Code").ToString());
        cmd.Parameters.AddWithValue("@Meeting_Code", to.getValue("Meeting_Code").ToString());
        cmd.Parameters.AddWithValue("@Meeting_Index", to.getValue("Meeting_Index").ToString());
        cmd.Parameters.AddWithValue("@Comm_Code", to.getValue("Comm_Code").ToString());
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        return dt;
    }


}