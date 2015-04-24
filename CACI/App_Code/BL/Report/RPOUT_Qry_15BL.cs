using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using com.kangdainfo.online.WebBase.BL;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;

/// <summary>
/// SAMPLEBL 的摘要描述
/// </summary>
public class RPOUT_Qry_15BL : ICommonBL, IQueryBL
{
    #region IQueryMarkBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        string sqlstr = "select b.Com_Name, b.Com_CttName, b.Com_CttTel, b.Com_CttCell, a.Coach_Date, c.Pj_Name, f.Times_Bgn, d.ChKd_Name,  " +
                        "(select SysCode.Sys_CdText from SysCode where d.ChKd_Order = SysCode.Sys_CdCode " +
                        ") as CoachKind, i.Comm_Name, g.Record_Text, (select SysCode.Sys_CdText " +
                        "from Coach inner join SysCode on Coach.Coach_Status = SysCode.Sys_CdCode " +
                        "where SysCode.Sys_CdKind = 'O' and SysCode.Sys_CdType = 'S') as CoachStatus " +
                        "from Coach a inner join Company b on a.Com_Code = b.Com_Code " +
                        "inner join Project c on a.Pj_Code = c.Pj_Code " +
                        "inner join CoachKind d on a.ChKd_Code = d.ChKd_Code " +
                        "left join CoachStage e on a.Coach_Code = e.Coach_Code " +
                        "left join CoachMeeting j on e.Coach_Code = j.Coach_Code " +
                        "inner join MtgTimes f on j.Meeting_Code = f.Meeting_Code " +
                        "inner join MtgRecord g on j.Meeting_Code = g.Meeting_Code " +
                        "inner join MtgCrew h on j.Meeting_Code = h.Meeting_Code " +
                        "inner join Committee i on h.Comm_Code = i.Comm_Code " +
                        "order by b.Com_Name,f.Times_Bgn ";
                       
        
        
        DataTable dt = new DataTable();

        SqlCommand cmd = new SqlCommand(sqlstr);

        //SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Company", to);

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        SqlCommand cmd = new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("Master", to);

        cmd.CommandText += " Order By " + sortStr;

        new SQLAgent(DataBase.TBQGDB).select(cmd, dt);

        return dt;
    }

 

    #endregion


    void IQueryBL.DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }
}