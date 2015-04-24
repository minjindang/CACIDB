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
public class SM1001_2BL : ICommonBL,IQueryMarkBL
{
    #region IQueryMarkBL 成員

    DataTable IQueryMarkBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();

        SqlCommand cmd = new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("Master", to);

        new SQLAgent(DataBase.TBQGDB).select(cmd, dt);

        return dt;
    }

    DataTable IQueryMarkBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        SqlCommand cmd = new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("Master", to);

        cmd.CommandText += " Order By " + sortStr;

        new SQLAgent(DataBase.TBQGDB).select(cmd, dt);

        return dt;
    }

    void IQueryMarkBL.MarkData(DataTO[] tos, DataTO mto)
    {
        foreach (DataTO to in tos)
        {
            

        }
    }

    #endregion

}