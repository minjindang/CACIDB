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
public class PjClass_01BL : ICommonBL, IMDUIBL
{
    #region IMDUIBL 成員

    void IMDUIBL.InsertData(DataTO to, DataTable dt)
    {
        throw new NotImplementedException();
    }

    bool IMDUIBL.IsDataExist(DataTO to)
    {
        return true;
    }

    void IMDUIBL.LoadData(DataTO to, DataTable dt)
    {
        string masterSqlstr = "SELECT * " +
                              "FROM CACIDB..PjClass ";
        SqlCommand masCmd = new SqlCommand(masterSqlstr);
        new SQLAgent(DataBase.CACIDB).select(masCmd, dt);
    }

    void IMDUIBL.UpdateData(DataTO to, DataTable dt)
    {
        List<SqlCommand> cmds = new List<SqlCommand>();
        
        string delsqlstr = "DELETE FROM CACIDB..PjClass ";

        cmds.Add(new SqlCommand(delsqlstr));

        foreach (DataRow row in dt.Rows)
        {
            DataTO dto = new DataTO();

            foreach(DataColumn col in dt.Columns )
            {
                dto.setValue(col.ColumnName, row[col.ColumnName]);
            }

            dto.setValue("Rec_InfoID", to.getValue("Rec_InfoID").ToString());

            cmds.Add(new SQLCommandBuilder(DataBase.CACIDB).getInsertCommand("CACIDB..PjClass", dto));
        }

        new SQLAgent(DataBase.CACIDB).execute(cmds.ToArray());
    }

    #endregion

    #region 自訂功能

    #endregion
}