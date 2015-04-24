using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;

/// <summary>
/// RPOUT_07BL 的摘要描述
/// </summary>
public class RPOUT_07BL : ICommonBL, IQueryBL
{
    #region IQueryBL 成員

    private string getDefaultSql()
    {
        string result = "select distinct b.Aow_Code, b.Reg_Code, e.Com_Name, b.Aow_PJPM, c.ApPj_Name, f.Reg_Code, " +
                        "(select Com_Name from Company where Com_Code=f.Com_Code) as Com_Name, c.ApPj_ApGroup, c.ApPj_ProdCls, " +
                        "c.ApPj_Msectors, e.Com_CttName, e.Com_CttTel, e.Com_CttTel2, e.Com_CttMail, e.Com_OPAddr, c.ApPj_AowAmt, " +
                        "c.ApPj_FundAmt, c.ApPj_TotAmt " +
                        "from Project a left join Allowance b     on a.Pj_Code = b.Pj_Code " +
                                       "left join ApPjContext c  on b.Aow_Code = c.Aow_Code " +
                                       "left join AowStage d	  on b.Aow_Code = c.Aow_Code and a.Pj_Code = d.Pj_Code " +
                                       "left join Company e      on b.Com_Code = e.Com_Code " +
                                       "left join ApplyAsis f	  on b.Aow_Code = f.Aow_Code and f.Aas_Type = 'C' " +
                                       "left join CompanyPrice g on e.Com_Code = g.Com_Code " +
                                       "WHERE a.Pj_Name = @Pj_Name and d.Stage_Index = @Stage_Index";
                                             //"b.Aow_Date between dbo.chgToEnDate(@Aow_DateS) and dbo.chgToEnDate(@Aow_DateE) and 1=1";		
        return result;
    }

    public DataTable getPj_Name() 
    {
        DataTable dt = new DataTable();
        string sqlstr = "select Pj_Name, Stage_Name, Stage_Index from PjStage, Project where PjStage.Pj_Code = Project.Pj_Code and Pj_PjFill = '3'";
        SqlCommand cmd = new SqlCommand(sqlstr);
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    public DataTable getStage_Name(DataTO to, String Pj_Name) 
    {
        DataTable dt = new DataTable();
        string sqlstr = "select Pj_Name, Stage_Name, Stage_Index from PjStage, Project where PjStage.Pj_Code = Project.Pj_Code and Pj_PjFill = '3' and Pj_Name = @Pj_Name";
        SqlCommand cmd = new SqlCommand(sqlstr);
        cmd.Parameters.AddWithValue("@Pj_Name", Pj_Name);
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    private SqlCommand getFilter(string sqlstr, DataTO to)
    {
        SqlCommand cmd = new SqlCommand(sqlstr);
        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {
                case "txt_Aow_DateS":
                    cmd.CommandText += " AND dbo.chgToChnDate(Allowance.Aow_Date) >= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "txt_Aow_DateE":
                    cmd.CommandText += " AND dbo.chgToChnDate(Allowance.Aow_Date) <= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "ddl_ApPj_ApGroup":
                    cmd.CommandText += " AND ApPjContext.ApPj_ApGroup = @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "sel_AwSg_Verify":
                    cmd.CommandText += " AND AowStage.AwSg_Verify = @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "txt_Com_Tonum":
                    cmd.CommandText += " AND Company.Com_Tonum = @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "txt_Com_Name":
                    cmd.CommandText += " AND Company.Com_Name like '%" + to.getValue(to.getAllColumnName()[i]) + "%'";
                    break;
                case "dll_ApPj_Msectors":
                    cmd.CommandText += " AND ApPjContext.ApPj_Msectors = @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "txt_Com_CapitalS":
                    cmd.CommandText += " AND Company.Com_Capital >= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "txt_Com_CapitalE":
                    cmd.CommandText += " AND Company.Com_Capital <= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "sel_CompanyPrice":
                    string companyPrice = (string)to.getValue(to.getAllColumnName()[i]);
                    if (companyPrice.Equals("Y"))
                    {
                        cmd.CommandText += " AND Company.Com_Code in (select distinct Com_Code from CompanyPrice)";
                    }
                    else if (companyPrice.Equals("N"))
                    {
                        cmd.CommandText += " AND Company.Com_Code not in (select distinct Com_Code from CompanyPrice)";
                    }
                    break;
            }
        }
        return cmd;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        throw new NotImplementedException();
    }

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = getFilter(getDefaultSql(), to);
        cmd.Parameters.AddWithValue("@Pj_Name", to.getValue("Pj_Name").ToString());
        cmd.Parameters.AddWithValue("@Stage_Index", to.getValue("Pj_Stage").ToString());
        cmd.Parameters.AddWithValue("@Aow_DateS", to.getValue("Aow_DateS").ToString());
        cmd.Parameters.AddWithValue("@Aow_DateE", to.getValue("Aow_DateE").ToString());
        //cmd.CommandText += " order by ApPj_ApGroup ";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    public DataTable getPrintDatas(DataTO conds, String SelectData)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = getFilter(getDefaultSql(), conds);
        cmd.Parameters.AddWithValue("@Pj_Name", conds.getValue("Pj_Name").ToString());
        cmd.Parameters.AddWithValue("@Stage_Index", conds.getValue("Pj_Stage").ToString());
        cmd.Parameters.AddWithValue("@Aow_DateS", conds.getValue("Aow_DateS").ToString());
        cmd.Parameters.AddWithValue("@Aow_DateE", conds.getValue("Aow_DateE").ToString());
        if (!SelectData.Equals(""))
        {
            cmd.CommandText += " AND e.Com_Name in (" + SelectData + ")";
        }
       // cmd.CommandText += " order by ApPj_ApGroup ";
        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    #endregion

    void IQueryBL.DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }
}
