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
public class Allowance_02BL : ICommonBL, IMDUIBL, IMMDUIBL
{
    public Allowance_02BL()
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
        //Detail
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTO dto = new DataTO();
            string newAas_Code = getNewSerialNo(DataBase.CACIDB, "AA");
            dto.setValue("Aas_Code", newAas_Code);
            dto.setValue("Aow_Code", newAow_Code);
            dto.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
            foreach (DataColumn col in dt.Columns)
            {
                if(col.ColumnName == "Aas_Amount")
                    dto.setValue(col.ColumnName, Convert.ToInt32(dt.Rows[i][col.ColumnName].ToString().Replace("千元", string.Empty).Replace(",",string.Empty))*1000);
                else
                    dto.setValue(col.ColumnName, dt.Rows[i][col.ColumnName].ToString());
            }
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("ApplyAsis", dto));
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
        string sqlstr = "SELECT b.Com_Name,b.Com_Tonum,b.Com_Capital,b.Com_LTover,b.Com_OrgForm,b.Com_Fax, " +
                        "b.Com_Boss,b.Com_BsTel,b.Com_CttName,b.Com_CttTel,b.Com_Email,b.Com_OPAddr, " +
                        "b.Com_Url,b.Com_MnProduct,b.Com_MnSectors,c.ApPj_Name, " +
                        "dbo.chgToChnDate(c.ApPj_BgnDate) as ApPj_BgnDate, " +
                        "dbo.chgToChnDate(c.ApPj_EndDate) as ApPj_EndDate, " +
                        "c.ApPj_ProdCls,c.ApPj_ApGroup,c.ApPj_TotAmt,c.ApPj_AowAmt, " +
                        "c.ApPj_OthAmt,c.ApPj_Goal,c.ApPj_Policies,c.ApPj_Profit,c.ApPj_Solution ,a.Aow_Code,b.Com_Code, " +
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

        string dsqlstr = "SELECT Aas_PjName, Aas_Year, Aas_PjUnit , 'Y' as IsExists " +
                        ", Cast(Cast(ISNULL(Aas_Amount ,0) / 1000 as DECIMAL(10,0)) as nvarchar) + '千元' as Aas_Amount " + 
                        "FROM CACIDB..ApplyAsis " +
                        "WHERE Aow_Code = @Aow_Code";

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
        //string deleteSql = string.Format("DELETE FROM CACIDB..ApplyAsis WHERE Aow_Code = '{0}'", allowanceTo.getValue("Aow_Code").ToString());
        //cmds.Add(new SqlCommand(deleteSql));//Detail
        //單位(公司)基本資料
        comTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
        comTo.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Company", comTo));
        //獎補助
        allowanceTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
        allowanceTo.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Allowance", allowanceTo));
        //計劃資料
        apPjTo.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
        apPjTo.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("ApPjContext", apPjTo));
        //Detail
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTO dto = new DataTO();
            string newAas_Code = getNewSerialNo(DataBase.CACIDB, "AA");
            dto.setValue("Aas_Code", newAas_Code);
            dto.setValue("Aas_Type", "A");
            dto.setValue("Aow_Code", allowanceTo.getValue("Aow_Code"));
            dto.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName == "IsExists")
                    continue;
                if (col.ColumnName == "Aas_Amount")
                    dto.setValue(col.ColumnName, Convert.ToInt32(dt.Rows[i][col.ColumnName].ToString().Replace("千元", string.Empty).Replace(",", string.Empty)) * 1000);
                else
                    dto.setValue(col.ColumnName, dt.Rows[i][col.ColumnName].ToString());
            }
            if (dt.Rows[i]["IsExists"].ToString() == "N")
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("ApplyAsis", dto));
            else
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("ApplyAsis", dto));
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
        string sqlstr = "SELECT b.Com_Name,b.Com_Tonum,b.Com_Capital,b.Com_LTover,b.Com_OrgForm,b.Com_Fax, " +
                        "b.Com_Boss,b.Com_BsTel,b.Com_CttName,b.Com_CttTel,b.Com_Email,b.Com_OPAddr, " +
                        "b.Com_Url,b.Com_MnProduct,b.Com_MnSectors,c.ApPj_Name, " +
                        "dbo.chgToChnDate(c.ApPj_BgnDate) as ApPj_BgnDate, " +
                        "dbo.chgToChnDate(c.ApPj_EndDate) as ApPj_EndDate, " +
                        "c.ApPj_ProdCls,c.ApPj_ApGroup,c.ApPj_TotAmt,c.ApPj_AowAmt, " +
                        "c.ApPj_OthAmt,c.ApPj_Goal,c.ApPj_Policies,c.ApPj_Profit,c.ApPj_Solution ,a.Aow_Code,b.Com_Code, " +
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
        string dsqlstr2 = "SELECT Aas_PjName, Aas_Year, Aas_PjUnit , 'Y' as IsExists " +
                        ", Cast(Cast(ISNULL(Aas_Amount ,0) / 1000 as DECIMAL(10,0)) as nvarchar) + '千元' as Aas_Amount " +
                        "FROM CACIDB..ApplyAsis " +
                        "WHERE Aow_Code = @Aow_Code";
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