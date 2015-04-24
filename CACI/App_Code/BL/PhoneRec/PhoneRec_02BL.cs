using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;
using System.Text.RegularExpressions;


/// <summary>
/// Coach_01BL 的摘要描述
/// </summary>
public class PhoneRec_02BL : ICommonBL, IMMDUIBL
{
    #region IMMDUIBL 成員

    void IMMDUIBL.InsertData(DataTO to, DataSet ds)
    {
        throw new NotImplementedException();
    }

    bool IMMDUIBL.IsDataExist(DataTO to)
    {
        return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("PhoneRec", to);
    }

    void IMMDUIBL.LoadData(DataTO to, DataSet ds)
    {
        string sqlstr = "SELECT  * FROM CACIDB..PhoneRec a JOIN CACIDB..Company b ON a.PhRec_ComCode=b.Com_Code WHERE a.PhRec_Code=@PhRec_Code ";

        SqlCommand cmd = new SqlCommand(sqlstr);


        cmd.Parameters.AddWithValue("@PhRec_Code", to.getValue("PhRec_Code"));


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

        DataTable PhoneDt = getPhoneRecList(to.getValue("PhRec_Code").ToString());
        ds.Tables.Add(PhoneDt);
    }

    DataTable IMMDUIBL.QueryDetailData(string QueryGridViewID, DataTO to)
    {
        throw new NotImplementedException();
    }

    void IMMDUIBL.UpdateData(DataTO to, DataSet ds)
    {
        throw new NotImplementedException();
    }

    #endregion    

    public DataTable getPhoneRecList(string PhRec_Code)
    {
        DataTable PhoneDt = new DataTable("grv_Phone");

        string phone_Sqlstr = "SELECT b.Case_Code,b.PhRec_Code,dbo.chgToChnDate(c.PRcRp_Date) PRcRp_Date,ISNULL(dbo.getSysCodeText('C','Y',a.CntClass_Code ) ,'無類別') as CntClass_Code,ISNULL(dbo.getSysCodeText('C','R',c.PRcRp_Handle ) ,'未處理') PRcRp_Handle " +
                              "FROM CACIDB..PhoneRec a JOIN CACIDB..PhRecCase b ON a.PhRec_Code=b.PhRec_Code " +
                                                        "JOIN CACIDB..PhRecRep c ON a.PhRec_Code=c.PhRec_Code " +
                              "WHERE a.PhRec_Code=@PhRec_Code ";

        SqlCommand phone_Cmd = new SqlCommand(phone_Sqlstr);

        phone_Cmd.Parameters.AddWithValue("@PhRec_Code", PhRec_Code);

        new SQLAgent(DataBase.CACIDB).select(phone_Cmd, PhoneDt);

        return PhoneDt;
    }
}

