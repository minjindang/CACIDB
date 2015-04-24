using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using com.kangdainfo.online.WebBase.TO;
using com.kangdainfo.online.WebBase.DB;

/// <summary>
/// ProjectBase 的摘要描述
/// </summary>
public class ProjectBase
{
    public DataTO getScoreData(string Score_Code)
    {
        DataTO scoreTo = new DataTO();

        scoreTo.setValue("Score_Code", Score_Code);

        SqlDataReader sr = new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..Score", scoreTo));

        if (sr.Read())
        {
            for (int i = 0; i < sr.FieldCount; i++)
            {
                if (!scoreTo.isColumnExist(sr.GetName(i)))
                {
                    scoreTo.setValue(sr.GetName(i), sr[sr.GetName(i)].ToString());
                }
            }
        }

        sr.Close();

        return scoreTo;
    }
}