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
public class Allowance_04BL : ICommonBL, IMDUIBL, IMMDUIBL
{
    public Allowance_04BL()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }


    void IMDUIBL.InsertData(DataTO to, DataTable dt)
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
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Company", comTo));
        }
        else
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..Company", comTo));
        //獎補助
        allowanceTo.setValue("Aow_Code", newAow_Code);
        allowanceTo.setValue("Aow_Date", "\\getDate()");
        allowanceTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
        allowanceTo.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Allowance", allowanceTo));
        //計劃資料
        apPjTo.setValue("Aow_Code", newAow_Code);
        apPjTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
        apPjTo.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..ApPjContext", apPjTo));
        //Detail
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTO aasTo = new DataTO();
            DataTO comDetailTo = new DataTO();
            string newAas_Code = getNewSerialNo(DataBase.CACIDB, "AA");
            newCom_Code = getNewSerialNo(DataBase.CACIDB, "CA");
            comDetailTo.setValue("Com_Code", newCom_Code);
            aasTo.setValue("Aas_Code", newAas_Code);
            aasTo.setValue("Com_Code", newCom_Code);
            aasTo.setValue("Aow_Code", newAow_Code);
            aasTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName.IndexOf("Aas_") == 0)
                    aasTo.setValue(col.ColumnName, dt.Rows[i][col.ColumnName].ToString());
                else if (col.ColumnName.IndexOf("Com_") == 0)
                    comDetailTo.setValue(col.ColumnName, dt.Rows[i][col.ColumnName].ToString());
            }
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..ApplyAsis", aasTo));
            if (comTo.getValue("Com_Code").ToString() == comDetailTo.getValue("Com_Code").ToString())
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..Company", comDetailTo));
            else
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Company", comDetailTo));
        }
        try
        {
            new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }

    bool IMDUIBL.IsDataExist(DataTO to)
    {
        if (to.isColumnExist("Aow_Code"))
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Allowance", to);
        else if (((DataTO)to.getValue("Allowance")).isColumnExist("Aow_Code"))
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Allowance", to);
        //else if (to.isColumnExist("Com_Code"))
        //return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Company", to);
        //else if (to.isColumnExist("Aas_Code"))
        // return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("ApplyAsis", to);
        else
            return false;
    }

    void IMDUIBL.LoadData(DataTO to, DataTable dt)
    {
        string sqlstr = "SELECT b.Com_Name,b.Com_Tonum,a.Aow_GPName,a.Aow_RegNum, " +
                        "a.Aow_FMan,a.Aow_FMIDNO,b.Com_MnSectors,a.Aow_PJPM, " +
                        "a.Aow_PMTel,b.Com_CttName,b.Com_CttTel,b.Com_Fax, " +
                        "b.Com_CttMail,b.Com_OPAddr,b.Com_Url,c.ApPj_Name,c.ApPj_AowAmt, " +
                        "c.ApPj_OthAmt,c.ApPj_Goal ,c.ApPj_Policies,c.ApPj_Profit ,c.ApPj_Solution ,a.Aow_Code,b.Com_Code ,c.ApPj_TotAmt, " +
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

        string dsqlstr = "SELECT * " +
                        "FROM CACIDB..ApplyAsis a " +
                        "LEFT JOIN CACIDB..Company b " +
                        "ON a.Com_Code = b.Com_Code " +
                        "WHERE a.Aow_Code = @Aow_Code";

        cmd = new SqlCommand(dsqlstr);

        cmd.Parameters.AddWithValue("@Aow_Code", to.getValue("Aow_Code"));

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();
        DataTO comTo = (DataTO)to.getValue("Company");
        DataTO apPjTo = (DataTO)to.getValue("ApPjContext");
        DataTO allowanceTo = (DataTO)to.getValue("Allowance");
        string deleteSql = string.Format("DELETE FROM CACIDB..ApplyAsis WHERE Aow_Code = '{0}'",allowanceTo.getValue("Aow_Code").ToString());
        cmds.Add(new SqlCommand(deleteSql));//Detail
        //單位(公司)基本資料
        comTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
        comTo.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..Company", comTo));
        //獎補助
        allowanceTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
        allowanceTo.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..Allowance", allowanceTo));
        //計劃資料
        apPjTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
        apPjTo.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..ApPjContext", apPjTo));
        //Detail
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTO aasTo = new DataTO();
            comTo = new DataTO();
            aasTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName.IndexOf("Aas_") == 0)
                    aasTo.setValue(col.ColumnName, dt.Rows[i][col.ColumnName].ToString());
                else if (col.ColumnName.IndexOf("Com_") == 0)
                    comTo.setValue(col.ColumnName, dt.Rows[i][col.ColumnName].ToString());
            }
            string newCom_Code = getNewSerialNo(DataBase.CACIDB, "CA");
            aasTo.setValue("Aas_Code", getNewSerialNo(DataBase.CACIDB, "AA"));
            aasTo.setValue("Aow_Code", allowanceTo.getValue("Aow_Code").ToString());
            if (string.IsNullOrEmpty(comTo.getValue("Com_Code").ToString()))
            {
                aasTo.setValue("Com_Code", newCom_Code);
                comTo.updateValue("Com_Code", newCom_Code);
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..Company", comTo));
            }
            else
            {
                aasTo.setValue("Com_Code", comTo.getValue("Com_Code").ToString());
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..Company", comTo));
            }
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..ApplyAsis", aasTo));
            
        }
        try
        {
            new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }

    void IMMDUIBL.InsertData(DataTO to, DataSet ds)
    {
        throw new NotImplementedException();
    }

    bool IMMDUIBL.IsDataExist(DataTO to)
    {
        if (to.isColumnExist("Aow_Code") && to.isColumnExist("Pj_Code"))
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Allowance", to) && new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("PjStage", to);
        else
            return false;
    }

    void IMMDUIBL.LoadData(DataTO to, DataSet ds)
    {
        string sqlstr = "SELECT b.Com_Name,b.Com_Tonum,a.Aow_GPName,a.Aow_RegNum, " +
                        "a.Aow_FMan,a.Aow_FMIDNO,b.Com_MnSectors,a.Aow_PJPM, " +
                        "a.Aow_PMTel,b.Com_CttName,b.Com_CttTel,b.Com_Fax, " +
                        "b.Com_CttMail,b.Com_OPAddr,b.Com_Url,c.ApPj_Name,c.ApPj_AowAmt, " +
                        "c.ApPj_OthAmt,c.ApPj_Goal ,c.ApPj_Policies,c.ApPj_Profit ,c.ApPj_Solution ,a.Aow_Code,b.Com_Code ,c.ApPj_TotAmt, " +
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
        DataTable dtAowStage = new DataTable("grv_AowStage");
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
        new SQLAgent(DataBase.CACIDB).select(cmd, dtAowStage);
        ds.Tables.Add(dtAowStage);
        DataTable dtApplyAsis = new DataTable("grvQuery");
        string dsqlstr2 = "SELECT * " +
                        "FROM CACIDB..ApplyAsis a " +
                        "LEFT JOIN CACIDB..Company b " +
                        "ON a.Com_Code = b.Com_Code " +
                        "WHERE a.Aow_Code = @Aow_Code";
        cmd = new SqlCommand(dsqlstr2);
        cmd.Parameters.AddWithValue("@Aow_Code", to.getValue("Aow_Code"));
        new SQLAgent(DataBase.CACIDB).select(cmd, dtApplyAsis);
        ds.Tables.Add(dtApplyAsis);
    }

    DataTable IMMDUIBL.QueryDetailData(string QueryGridViewID, DataTO to)
    {
        throw new NotImplementedException();
    }

    void IMMDUIBL.UpdateData(DataTO to, DataSet ds)
    {
        throw new NotImplementedException();
    }
}