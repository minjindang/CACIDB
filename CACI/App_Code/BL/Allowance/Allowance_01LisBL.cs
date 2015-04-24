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
public class Allowance_01LisBL : ICommonBL, IMDUIBL
{
    public Allowance_01LisBL()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
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
        string sqlstr = "SELECT b.Com_Name,b.Com_Tonum,b.Com_OrgForm,b.Com_Fax, " +
                        "b.Com_Boss,b.Com_BsTel,b.Com_Email,b.Com_OPAddr, " +
                        "b.Com_Url,b.Com_MnProduct,b.Com_MnSectors,c.ApPj_Name, " +
                        "dbo.chgToChnDate(c.ApPj_BgnDate) as ApPj_BgnDate, " +
                        "dbo.chgToChnDate(c.ApPj_EndDate) as ApPj_EndDate, " +
                        "c.ApPj_ProdCls,c.ApPj_ApGroup,c.ApPj_TotAmt,c.ApPj_AowAmt, " +
                        "c.ApPj_OthAmt,c.ApPj_Agenda ,a.Aow_Code,b.Com_Code " +
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


    void IMDUIBL.InsertData(DataTO to, DataTable dt)
    {
        throw new NotImplementedException();
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        throw new NotImplementedException();
    }
}