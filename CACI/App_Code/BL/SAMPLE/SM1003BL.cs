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
public class SM1003BL : ICommonBL,IQueryBL,IMMDUIBL
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

        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getDeleteCommand("Detail_1", to));

        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getDeleteCommand("Detail_2", to));

        new SQLAgent(DataBase.TBQGDB).execute(cmds.ToArray());
    }

    #endregion

    #region IMDDUIBL 成員

    void IMMDUIBL.InsertData(DataTO to, DataSet ds)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();

        to.setValue("Mcol_1", ICommonBL.getNewSerialNo(DataBase.TBQGDB, "NE"));

        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("Master", to));

        foreach (DataTable dt in ds.Tables)
        {
            switch (dt.TableName)
            {
                case "grvQuery" :

                    foreach(DataRow dtRow in dt.Rows)
                    {

                        DataTO d1TO = new DataTO();

                        d1TO.setValue("Mcol_1", to.getValue("Mcol_1"));

                        foreach (DataColumn col in dt.Columns)
                        {
                            d1TO.setValue(col.ColumnName, dtRow[col.ColumnName].ToString());
                        }

                        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("Detail_1",d1TO));
                    }
                    break;
                case "grvQuery2":

                    foreach (DataRow dtRow in dt.Rows)
                    {
                        DataTO d1TO = new DataTO();

                        d1TO.setValue("Mcol_1", to.getValue("Mcol_1"));
                        
                        foreach (DataColumn col in dt.Columns)
                        {
                            d1TO.setValue(col.ColumnName, dtRow[col.ColumnName].ToString());
                        }

                        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("Detail_2", d1TO));
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

        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getUpdateCommand("Master",to));

        foreach (DataTable dt in ds.Tables)
        {
            if (dt.TableName == "grvQuery")
            {
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

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTO dto = new DataTO();

                    dto.setValue("Mcol_1", to.getValue("Mcol_1").ToString());

                    for (int t = 0; t < dt.Columns.Count; t++)
                    {
                        dto.setValue(dt.Columns[t].ColumnName, dt.Rows[i][dt.Columns[t].ColumnName].ToString());
                    }

                    if (new SQLCommandBuilder(DataBase.TBQGDB).isDataExistByPrimayKey("Detail_1",dto))
                    {
                        cmds.Add( new SQLCommandBuilder(DataBase.TBQGDB).getUpdateCommand("Detail_1",dto));
                    }
                    else
                    {
                        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("Detail_1", dto));
                    }
                }
            }
            else if (dt.TableName == "grvQuery2")
            {
                // 刪除不在清單內的資料
                string delStr = "DELETE FROM Detail_2 " +
                                "WHERE Mcol_1=@Mcol_1 ";

                SqlCommand delCmd = new SqlCommand(delStr);

                delCmd.Parameters.AddWithValue("@Mcol_1", to.getValue("Mcol_1"));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    delCmd.CommandText += "AND Dcol_21 != @Dcol_21" + i.ToString() + " ";
                    delCmd.Parameters.AddWithValue("@Dcol_21" + i.ToString(), dt.Rows[i]["Dcol_21"].ToString());
                }

                cmds.Add(delCmd);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTO dto = new DataTO();

                    dto.setValue("Mcol_1", to.getValue("Mcol_1").ToString());

                    for (int t = 0; t < dt.Columns.Count; t++)
                    {
                        dto.setValue(dt.Columns[t].ColumnName, dt.Rows[i][dt.Columns[t].ColumnName].ToString());
                    }

                    if (new SQLCommandBuilder(DataBase.TBQGDB).isDataExistByPrimayKey("Detail_2",dto))
                    {
                        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getUpdateCommand("Detail_2", dto));
                    }
                    else
                    {
                        cmds.Add(new SQLCommandBuilder(DataBase.TBQGDB).getInsertCommand("Detail_2", dto));
                    }
                }
            }
        }

        new SQLAgent(DataBase.TBQGDB).execute(cmds.ToArray());
    }

    DataTable IMMDUIBL.QueryDetailData(string QueryGridViewID, DataTO to)
    {
        throw new NotImplementedException();
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

        DataTO Detail1TO = new DataTO();

        Detail1TO.setValue("Mcol_1", to.getValue("Mcol_1"));

        if (!ds.Tables.Contains("grvQuery"))
        {
            DataTable detail1dt = new DataTable("grvQuery");

            new SQLAgent(DataBase.TBQGDB).select(new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("Detail_1", Detail1TO), detail1dt);

            ds.Tables.Add(detail1dt);
        }

        if (!ds.Tables.Contains("grvQuery2"))
        {
            DataTable detail2dt = new DataTable("grvQuery2");

            new SQLAgent(DataBase.TBQGDB).select(new SQLCommandBuilder(DataBase.TBQGDB).getSelectCommand("Detail_2", Detail1TO), detail2dt);
            ds.Tables.Add(detail2dt);
        }
    }

    bool IMMDUIBL.IsDataExist(DataTO to)
    {
        return new SQLCommandBuilder(DataBase.TBQGDB).isDataExistByPrimayKey("Master", to);
    }

    #endregion
}