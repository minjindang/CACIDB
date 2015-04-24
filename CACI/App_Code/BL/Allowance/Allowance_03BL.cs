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
public class Allowance_03BL : ICommonBL, IMasterUIBL, IMDUIBL
{
    public Allowance_03BL()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    void IMasterUIBL.InsertData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();
        DataTO comTo = (DataTO)to.getValue("Company");
        DataTO apPjTo = (DataTO)to.getValue("ApPjContext");
        DataTO allowanceTo = (DataTO)to.getValue("Allowance");
        string isExists = to.getValue("IsExists").ToString();
        string newAow_Code = getNewSerialNo(DataBase.CACIDB, "AP");
        string newCom_Code = getNewSerialNo(DataBase.CACIDB, "CA");
        //單位(公司)基本資料
        comTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
        comTo.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
        if (isExists == "N")
        {
            comTo.updateValue("Com_Code", newCom_Code);
            allowanceTo.updateValue("Com_Code", newCom_Code);
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Company", comTo));
        }
        else
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Company", comTo));
        //獎補助
        allowanceTo.setValue("Aow_Code", newAow_Code);
        allowanceTo.setValue("Aow_Date", "\\getDate()");
        allowanceTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
        allowanceTo.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Allowance", allowanceTo));
        //計劃資料
        apPjTo.setValue("Aow_Code", newAow_Code);
        apPjTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
        apPjTo.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("ApPjContext", apPjTo));
        try
        {
            new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }

    bool IMasterUIBL.IsDataExist(DataTO to)
    {
        if (to.isColumnExist("Aow_Code"))
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Allowance", to);
        else if (((DataTO)to.getValue("ApPjContext")).isColumnExist("Aow_Code"))
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("ApPjContext", to);
        //else if (to.isColumnExist("Com_Code"))
        //return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Company", to);
        //else if (to.isColumnExist("Aas_Code"))
        // return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("ApplyAsis", to);
        else
            return false;
    }

    void IMasterUIBL.LoadData(DataTO to)
    {
        string sqlstr = "SELECT b.Com_Name,b.Com_BsGender,b.Com_Tonum,b.Com_BsTel,b.Com_BsNightTel,b.Com_BsCell, " +
                        "b.Com_Fax,b.Com_Email,b.Com_OPAddr,b.Com_MnSectors, " +
                        "c.ApPj_Name, " +
                        "c.ApPj_Goal ,c.ApPj_Policies,c.ApPj_Profit ,c.ApPj_Solution,c.ApPj_TotAmt,c.ApPj_AowAmt, " +
                        "c.ApPj_OthAmt,c.ApPj_Goal ,a.Aow_Code,b.Com_Code, " +
                        "b.Com_Account , b.Com_Pass " + 
                        "FROM CACIDB..Allowance a LEFT JOIN CACIDB..Company b " +
                        "ON a.Com_Code = b.Com_Code LEFT JOIN CACIDB..ApPjContext c " +
                        "ON a.Aow_Code = c.Aow_Code  " +
                        "WHERE a.Aow_Code=@Aow_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        cmd.Parameters.AddWithValue("@Aow_Code", to.getValue("Aow_Code"));

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
    }

    void IMasterUIBL.MarkData(DataTO to)
    {
        throw new NotImplementedException();
    }

    void IMasterUIBL.UpdateData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();
        DataTO comTo = (DataTO)to.getValue("Company");
        DataTO apPjTo = (DataTO)to.getValue("ApPjContext");
        //DataTO allowanceTo = (DataTO)to.getValue("Allowance");
        //單位(公司)基本資料
        comTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
        comTo.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Company", comTo));
        //獎補助
        //allowanceTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
        //allowanceTo.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
        //cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Allowance", allowanceTo));
        //計劃資料
        apPjTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
        apPjTo.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("ApPjContext", apPjTo));
        try
        {
            new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }

    void IMDUIBL.InsertData(DataTO to, DataTable dt)
    {
        throw new NotImplementedException();
    }

    bool IMDUIBL.IsDataExist(DataTO to)
    {
        if (to.isColumnExist("Aow_Code") && to.isColumnExist("Pj_Code"))
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Allowance", to) && new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("PjStage", to);
        else
            return false;
    }

    void IMDUIBL.LoadData(DataTO to, DataTable dt)
    {
        string sqlstr = "SELECT b.Com_Name,b.Com_BsGender,b.Com_Tonum,b.Com_BsTel,b.Com_BsNightTel,b.Com_BsCell, " +
                        "b.Com_Fax,b.Com_Email,b.Com_OPAddr,b.Com_MnSectors, " +
                        "c.ApPj_Name, " +
                        "c.ApPj_Goal ,c.ApPj_Policies,c.ApPj_Profit ,c.ApPj_Solution,c.ApPj_TotAmt,c.ApPj_AowAmt, " +
                        "c.ApPj_OthAmt,c.ApPj_Goal ,a.Aow_Code,b.Com_Code, " +
                        "b.Com_Account , b.Com_Pass " + 
                        "FROM CACIDB..Allowance a LEFT JOIN CACIDB..Company b " +
                        "ON a.Com_Code = b.Com_Code LEFT JOIN CACIDB..ApPjContext c " +
                        "ON a.Aow_Code = c.Aow_Code  " +
                        "WHERE a.Aow_Code=@Aow_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);
        cmd.Parameters.AddWithValue("@Aow_Code", to.getValue("Aow_Code"));
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
        string dsqlstr = "SELECT a.Pj_Code,b.Aow_Code,a.Stage_Index,a.Stage_Name ,a.Stage_Date, " +
                        "a.Stage_Text,CASE c.AwSg_Verify WHEN 'Y' THEN '通過' ELSE '未通過' END  AwSg_Verify " +
                        "FROM CACIDB.dbo.PjStage a " +
                        "LEFT JOIN CACIDB.dbo.Allowance b " +
                        "ON a.Pj_Code = b.Pj_Code " +
                        "LEFT JOIN CACIDB.dbo.AowStage c " +
                        "ON a.Pj_Code = c.Pj_Code and b.Aow_Code = c.Aow_Code " +
                        "WHERE a.Pj_Code = @Pj_Code AND b.Aow_Code = @Aow_Code ";

        cmd = new SqlCommand(dsqlstr);
        cmd.Parameters.AddWithValue("@Pj_Code", to.getValue("Pj_Code"));
        cmd.Parameters.AddWithValue("@Aow_Code", to.getValue("Aow_Code"));
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        throw new NotImplementedException();
    }
}