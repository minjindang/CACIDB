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
public class Allowance_01BL : ICommonBL, IQueryBL, IMDUIBL, IMMDUIBL
{
    public Allowance_01BL()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }


    void IQueryBL.DeleteData(DataTO to)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..Allowance", to));

        //DataTO comTo = new DataTO();
        //comTo.setValue("Com_Code", to.getValue("Com_Code").ToString());
        //cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..Company", comTo));

        DataTO apPjTo = new DataTO();
        apPjTo.setValue("Aow_Code", to.getValue("Aow_Code").ToString());
        cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getDeleteCommand("CACIDB..ApPjContext", apPjTo));

        string deleteSql = string.Format("DELETE FROM CACIDB..ApplyAsis WHERE Aow_Code = '{0}'", to.getValue("Aow_Code").ToString());
        cmds.Add(new SqlCommand(deleteSql));

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    public SqlCommand getQuerySqlCommand(DataTO to)
    {
        string sqlstr = "SELECT Rank() OVER(ORDER BY a.Aow_Code) as SerialNo,a.* " +
                        "FROM( " +
                       "SELECT DISTINCT  f.ApPj_Name,c.Pj_Name , " +
                       "b.Com_Name,dbo.chgToChnDate(c.Pj_StartDate) as Pj_StartDate,a.Aow_Code,c.Pj_PjFill,b.Com_Code ,c.Pj_Code FROM CACIDB..Allowance a " +
                       "LEFT JOIN CACIDB..Company b ON a.Com_Code = b.Com_Code " +
                       "LEFT JOIN CACIDB..Project c ON a.Pj_Code = c.Pj_Code " +
                       "LEFT JOIN CACIDB..AowStage d ON a.Pj_Code = d.Pj_Code AND a.Aow_Code = d.Aow_Code " +
                       "LEFT JOIN CACIDB..PjStage e ON d.Pj_Code = e.Pj_Code AND d.Stage_Index = e.Stage_Index " +
                       "LEFT JOIN CACIDB..ApPjContext f ON f.Aow_Code = a.Aow_Code " +
                       "WHERE 1=1 AND b.Com_Code IS NOT NULL ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            if ("Pj_Name".Equals(to.getAllColumnName()[i]) || "Com_Name".Equals(to.getAllColumnName()[i]) || "Stage_Name".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + " like '%" + to.getValue(to.getAllColumnName()[i]) + "%'";
            else if ("Pj_StartDate".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND Pj_StartDate >= '" + Allowance_01BL.chgChnDateToEnDate(to.getValue(to.getAllColumnName()[i]).ToString()) + "'";
            else if ("Pj_EndDate".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND Pj_StartDate <= '" + Allowance_01BL.chgChnDateToEnDate(to.getValue(to.getAllColumnName()[i]).ToString()) + "'";
            else if ("Aow_Code".Equals(to.getAllColumnName()[i]))
                cmd.CommandText += " AND a.Aow_Code like '%" + to.getValue(to.getAllColumnName()[i]) + "%'";
            else
            {
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i];
                cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
            }
            System.Diagnostics.Debug.WriteLine("@" + to.getAllColumnName()[i] + ":" + to.getValue(to.getAllColumnName()[i]));
        }
        return cmd;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = getQuerySqlCommand(to);
        cmd.CommandText += " ) a Order By a." + sortStr;
        System.Diagnostics.Debug.WriteLine(cmd.CommandText);
        try
        {
            new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = getQuerySqlCommand(to);
        cmd.CommandText += " ) a ";
        System.Diagnostics.Debug.WriteLine(cmd.CommandText);
        try
        {
            new SQLAgent(DataBase.CACIDB).select(cmd, dt);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
        return dt;
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
            //apPjTo.updateValue("Com_Code", newCom_Code);
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
            DataTO comDTo = new DataTO();
            DataTO apPjDto = new DataTO();
            string detailComCode = string.Empty;
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName == "IsExists")
                    continue;
                comDTo.setValue(col.ColumnName, dt.Rows[i][col.ColumnName].ToString());
            }
            if (dt.Rows[i]["IsExists"].ToString() == "N")
            {
                detailComCode = getNewSerialNo(DataBase.CACIDB, "CA");
                comDTo.updateValue("Com_Code", detailComCode);
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Company", comDTo));
            }
            else
            {
                detailComCode = dt.Rows[i]["Com_Code"].ToString();
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Company", comDTo));
            }
            string newAas_Code = getNewSerialNo(DataBase.CACIDB, "AA");
            apPjDto.setValue("Aas_Code", newAas_Code);
            apPjDto.setValue("Aow_Code", newAow_Code);
            apPjDto.setValue("Aas_Type", "B");
            apPjDto.setValue("Com_Code", detailComCode);
            apPjDto.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
            apPjDto.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("ApplyAsis", apPjDto));
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
        string sqlstr = "SELECT b.Com_Name,b.Com_Tonum,b.Com_OrgForm,b.Com_Fax, b.Com_BsCell,b.Com_BsGender,b.Com_BsTel,b.Com_BsNightTel, " +
                        "b.Com_Boss,b.Com_BsTel,b.Com_Email,b.Com_OPAddr, b.Com_CttName,b.Com_CttCell,b.Com_BsIDNO, " +
                        "b.Com_Url,b.Com_MnProduct,b.Com_MnSectors,c.ApPj_Name, " +
                        "dbo.chgToChnDate(c.ApPj_BgnDate) as ApPj_BgnDate, " +
                        "dbo.chgToChnDate(c.ApPj_EndDate) as ApPj_EndDate, " +
                        "c.ApPj_ProdCls,c.ApPj_ApGroup,c.ApPj_TotAmt,c.ApPj_AowAmt, a.Aow_FMan, a.Aow_FMIDNO, a.Aow_PJPM,a.Aow_PMTel, " +
                        "c.ApPj_OthAmt,c.ApPj_Goal ,c.ApPj_Policies,c.ApPj_Profit ,c.ApPj_Solution ,a.Aow_Code,b.Com_Code , b.Com_Account , b.Com_Pass " +
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

        string dsqlstr = "SELECT *,'Y' as IsExists FROM CACIDB..Company " +
                        "WHERE Com_Code in " +
                        "( " +
                        "SELECT Com_Code " +
                        "FROM CACIDB..ApplyAsis " +
                        "WHERE Aow_Code = @Aow_Code"+
                        ")";

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
            DataTO comDTo = new DataTO();
            DataTO apPjDto = new DataTO();
            string detailComCode = string.Empty;
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName == "IsExists")
                    continue;
                comDTo.setValue(col.ColumnName, dt.Rows[i][col.ColumnName].ToString());
            }
            if (dt.Rows[i]["IsExists"].ToString() == "N")
            {
                detailComCode = getNewSerialNo(DataBase.CACIDB, "CA");
                comDTo.updateValue("Com_Code", detailComCode);
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("Company", comDTo));
            }
            else
            {
                detailComCode = dt.Rows[i]["Com_Code"].ToString();
                cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("Company", comDTo));
            }
            string newAas_Code = getNewSerialNo(DataBase.CACIDB, "AA");
            apPjDto.setValue("Aas_Code", newAas_Code);
            apPjDto.setValue("Aow_Code", allowanceTo.getValue("Aow_Code"));
            apPjDto.setValue("Aas_Type", "B");
            apPjDto.setValue("Com_Code", detailComCode);
            apPjDto.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());
            apPjDto.setValue("Rec_Info", to.getValue("Rec_Info").ToString());
            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("ApplyAsis", apPjDto));
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
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Allowance", to) && new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("Project", to);
        else
            return false;
    }

    void IMMDUIBL.LoadData(DataTO to, DataSet ds)
    {
        string sqlstr = "SELECT b.Com_Name,b.Com_Tonum,b.Com_OrgForm,b.Com_Fax,  b.Com_BsCell,b.Com_BsGender,b.Com_BsTel,b.Com_BsNightTel, " +
                        "b.Com_Boss,b.Com_BsTel,b.Com_Email,b.Com_OPAddr, b.Com_CttName,b.Com_CttCell,b.Com_BsIDNO, " +
                        "b.Com_Url,b.Com_MnProduct,b.Com_MnSectors,c.ApPj_Name, " +
                        "dbo.chgToChnDate(c.ApPj_BgnDate) as ApPj_BgnDate, " +
                        "dbo.chgToChnDate(c.ApPj_EndDate) as ApPj_EndDate, " +
                        "c.ApPj_ProdCls,c.ApPj_ApGroup,c.ApPj_TotAmt,c.ApPj_AowAmt , a.Aow_FMan, a.Aow_FMIDNO, a.Aow_PJPM,a.Aow_PMTel, " +
                        "c.ApPj_OthAmt,c.ApPj_Goal ,c.ApPj_Policies,c.ApPj_Profit ,c.ApPj_Solution ,a.Aow_Code,b.Com_Code , b.Com_Account , b.Com_Pass " +
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
        string dsqlstr = "SELECT DISTINCT a.Pj_Code,b.Aow_Code,a.Stage_Index,a.Stage_Name ,ISNULL(dbo.chgToChnDate(a.Stage_Date),'N/A') as Stage_Date, " +
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