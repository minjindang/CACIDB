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
public class SM1002BL : ICommonBL,IQueryBL,IMDUIBL
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
        List<SqlCommand> cmds = new List<SqlCommand>();

        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getDeleteCommand("Master", to));

        DataTO dTo = new DataTO();

        dTo.setValue("Mcol_1", to.getValue("Mcol_1").ToString());

        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getDeleteCommand("Detail_1", dTo));

        new SQLAgent(DataBase.TBQGDB).execute(cmds.ToArray());

    }

    #endregion

    #region IMDUIBL 成員

    void IMDUIBL.InsertData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        string newMemberID = getNewSerialNo(DataBase.TBQGDB, "ME");

        to.setValue("Mcol_1", newMemberID);

        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("Master",to));

        for(int i = 0 ; i < dt.Rows.Count ; i++ )
        {
            DataTO dto = new DataTO();

            dto.setValue("Mcol_1", newMemberID);

            foreach (DataColumn col in dt.Columns)
            {
                dto.setValue(col.ColumnName, dt.Rows[i][col.ColumnName].ToString());
            }
           
            cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("Detail_1", dto));
        }

        new SQLAgent(DataBase.TBQGDB).execute(cmds.ToArray());
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        // 刪除不在清單內的資料
        string delStr = "DELETE FROM Detail_1 " +
                        "WHERE Mcol_1=@Mcol_1 ";

        SqlCommand delCmd = new SqlCommand(delStr);

        delCmd.Parameters.AddWithValue("@Mcol_1", to.getValue("Mcol_1"));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            delCmd.CommandText += "AND Dcol_1 != @Dcol_1" + i.ToString() + " ";
            delCmd.Parameters.AddWithValue("@Dcol_1" + i.ToString(), dt.Rows[i]["Dcol_1"].ToString());
        }

        cmds.Add(delCmd);

        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getUpdateCommand("Master",to));

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTO ddto = new DataTO();

            ddto.setValue("Mcol_1", to.getValue("Mcol_1").ToString());
            
            for (int t = 0; t < dt.Columns.Count; t++)
            {
                ddto.setValue(dt.Columns[t].ColumnName, dt.Rows[i][dt.Columns[t].ColumnName].ToString());
            }

            if (new SQLCommandBuilder(DataBase.TBQGDB).isDataExistByPrimayKey("Detail_1", ddto))
            {
                cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getUpdateCommand("Detail_1", ddto));
            }
            else
            {
                cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("Detail_1", ddto));
            }
        }

        new SQLAgent(DataBase.TBQGDB).execute(cmds.ToArray());
    }

    void IMDUIBL.LoadData(DataTO to, DataTable dt)
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

        DataTO dto = new DataTO();

        dto.setValue("Mcol_1", to.getValue("Mcol_1"));

        new SQLAgent(DataBase.TBQGDB).select(new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("Detail_1", dto), dt);

    }

    bool IMDUIBL.IsDataExist(DataTO to)
    {
        if (!to.isColumnExist("Mcol_1"))
            return false;
        else
            return new SQLCommandBuilder(DataBase.TBQGDB).isDataExistByPrimayKey("Master", to);
    }

    #endregion
}