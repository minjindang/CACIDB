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
public class SM1001BL : ICommonBL,IQueryBL,IMasterUIBL
{
    #region IQueryBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();

        SqlCommand cmd = new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("Master", to);

        new SQLAgent(DataBase.TBQGDB).select(cmd, dt);

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

    void IQueryBL.DeleteData(DataTO to)
    {
        SqlCommand cmd = new SQLCommandBuilder(DataBase.TBQGDB).getDeleteCommand("Master", to);

        new SQLAgent(DataBase.TBQGDB).execute(cmd);
    }

    #endregion

    #region IMasterUIBL 成員

    void IMasterUIBL.InsertData(DataTO to)
    {
        to.setValue("Mcol_1", ICommonBL.getNewSerialNo(DataBase.TBQGDB, "NE"));
        
        SqlCommand cmd = new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("Master", to);

        new SQLAgent(DataBase.TBQGDB).execute(cmd);
    }

    void IMasterUIBL.UpdateData(DataTO to)
    {
        SqlCommand cmd = new SQLCommandBuilder(DataBase.TBQGDB).getUpdateCommand("Master", to);

        new SQLAgent(DataBase.TBQGDB).execute(cmd);
    }

    void IMasterUIBL.LoadData(DataTO to)
    {
        SqlCommand cmd = new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("Master", to);

        SqlDataReader sr = new SQLAgent(DataBase.TBQGDB).select(cmd);

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
    }

    bool IMasterUIBL.IsDataExist(DataTO to)
    {
        if (!to.isColumnExist("Mcol_1"))
            return false;
        return new SQLCommandBuilder(DataBase.TBQGDB).isDataExistByPrimayKey("Master", to);
    }

    void IMasterUIBL.MarkData(DataTO to)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    #region 自訂動作

    public DataTable getDetailData(DataTO to)
    {
        DataTable dt = new DataTable();

        new SQLAgent(DataBase.TBQGDB).select(new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("Detail_1", to), dt);

        return dt;
    }
    #endregion
}