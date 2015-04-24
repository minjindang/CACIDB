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
public class SM1005BL : ICommonBL,IQueryBL,IMMDUIBL
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
        throw new NotImplementedException();
    }

    #endregion

    #region IMMDUIBL 成員

    void IMMDUIBL.InsertData(DataTO to, DataSet ds)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        to.setValue("Mcol_1", ICommonBL.getNewSerialNo(DataBase.TBQGDB, "NE"));

        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("Master", to));

        foreach (DataTable dt in ds.Tables)
        {
            switch (dt.TableName)
            {
                case "grvQuery":
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        DataTO dTO = new DataTO();

                        dTO.setValue("Mcol_1", to.getValue("Mcol_1"));

                        dTO.setValue("DDcol_1", dtRow["DDcol_1"].ToString());

                        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("MDDRela", dTO));
                    }

                    break;
                case "grvQuery2":

                    foreach (DataRow dtRow in dt.Rows)
                    {
                        DataTO dTO = new DataTO();

                        dTO.setValue("Mcol_1", to.getValue("Mcol_1"));
                        dTO.setValue("DDcol_21", dtRow["DDcol_21"].ToString());

                        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("MDDRela_2", dTO));
                    }

                    break;
                default:
                    break;
            }
        }

        new SQLAgent(DataBase.TBQGDB).execute(cmds.ToArray());
    }

    void IMMDUIBL.UpdateData(DataTO to, DataSet ds)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getUpdateCommand("Master", to));

        foreach (DataTable dt in ds.Tables)
        {
            if (dt.TableName == "grvQuery")
            {
                // 刪除不在清單內的資料
                string delStr = "DELETE FROM MDDRela " +
                                "WHERE Mcol_1=@Mcol_1 ";

                SqlCommand delCmd = new SqlCommand(delStr);

                delCmd.Parameters.AddWithValue("@Mcol_1", to.getValue("Mcol_1"));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    delCmd.CommandText += "AND DDcol_1 != @DDcol_1" + i.ToString() + " ";
                    delCmd.Parameters.AddWithValue("@DDcol_1" + i.ToString(), dt.Rows[i]["DDcol_1"].ToString());
                }

                cmds.Add(delCmd);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTO dto = new DataTO();

                    dto.setValue("Mcol_1", to.getValue("Mcol_1"));
                    dto.setValue("DDcol_1", dt.Rows[i]["DDcol_1"].ToString());

                    if (!new SQLCommandBuilder(DataBase.TBQGDB).isDataExistByPrimayKey("MDDRela", dto))
                    {
                        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("MDDRela", dto));
                    }
                }
            }
            else if (dt.TableName == "grvQuery2")
            {
                // 刪除不在清單內的資料
                string delStr = "DELETE FROM MDDRela_2 " +
                                "WHERE Mcol_1=@Mcol_1 ";

                SqlCommand delCmd = new SqlCommand(delStr);

                delCmd.Parameters.AddWithValue("@Mcol_1", to.getValue("Mcol_1"));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    delCmd.CommandText += "AND DDcol_21 != @DDcol_21" + i.ToString() + " ";
                    delCmd.Parameters.AddWithValue("@DDcol_21" + i.ToString(), dt.Rows[i]["DDcol_21"].ToString());
                }

                cmds.Add(delCmd);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTO dto = new DataTO();

                    dto.setValue("Mcol_1", to.getValue("Mcol_1"));
                    dto.setValue("DDcol_21", dt.Rows[i]["DDcol_21"].ToString());

                    if (!new SQLCommandBuilder(DataBase.TBQGDB).isDataExistByPrimayKey("MDDRela_2", dto))
                    {
                        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("MDDRela_2", dto));
                    }
                }
            }
        }

        new SQLAgent(DataBase.TBQGDB).execute(cmds.ToArray());

    }

    DataTable IMMDUIBL.QueryDetailData(string QueryGridViewID, DataTO to)
    {
        DataTable dt = new DataTable();
        
        switch (QueryGridViewID)
        {
            case "grvQueryData":
                new SQLAgent(DataBase.TBQGDB).select(new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("DDetail_1",to),dt);
                break;
            case "grvQueryData2":
                new SQLAgent(DataBase.TBQGDB).select(new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("DDetail_2", to), dt);
                break;
            default:
                break;
        }

        return dt;
    }

    void IMMDUIBL.LoadData(DataTO to, DataSet ds)
    {
        SqlDataReader sr = new SQLAgent(DataBase.TBQGDB).select(new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("Master", to));

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

        if (!ds.Tables.Contains("grvQuery"))
        {
            DataTable detaildt = new DataTable("grvQuery");

            string sqlstr = "SELECT b.* " +
                        "FROM MDDRela a JOIN DDetail_1 b ON a.DDcol_1=b.DDcol_1 " +
                        "WHERE a.Mcol_1=@Mcol_1 ";

            SqlCommand cmd = new SqlCommand(sqlstr);

            cmd.Parameters.AddWithValue("@Mcol_1", to.getValue("Mcol_1").ToString());

            new SQLAgent(DataBase.TBQGDB).select(cmd, detaildt);

            ds.Tables.Add(detaildt);
        }

        if (!ds.Tables.Contains("grvQuery2"))
        {
            DataTable detaildt = new DataTable("grvQuery2");

            string sqlstr = "SELECT b.* " +
                        "FROM MDDRela_2 a JOIN DDetail_2 b ON a.DDcol_21=b.DDcol_21 " +
                        "WHERE a.Mcol_1=@Mcol_1 ";

            SqlCommand cmd = new SqlCommand(sqlstr);

            cmd.Parameters.AddWithValue("@Mcol_1", to.getValue("Mcol_1").ToString());

            new SQLAgent(DataBase.TBQGDB).select(cmd, detaildt);

            ds.Tables.Add(detaildt);
        }
    }

    bool IMMDUIBL.IsDataExist(DataTO to)
    {
        if (!to.isColumnExist("Mcol_1"))
            return false;
        else
            return new SQLCommandBuilder(DataBase.TBQGDB).isDataExistByPrimayKey("Master", to);
    }

    #endregion

    
}