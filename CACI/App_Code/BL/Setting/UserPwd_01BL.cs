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
public class UserPwd_01BL : ICommonBL, IQueryBL, IMasterUIBL
{
    #region IQueryBL 成員

    DataTable IQueryBL.QueryDataForList(DataTO to)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT Comm_Code, Comm_ComName, Comm_Name, Comm_Account, CASE Comm_AcStatus WHEN 'L' THEN '鎖定' ELSE '正常' END Comm_AcStatus, Type FROM " +
                        "(SELECT Comm_Code, Comm_ComName, Comm_Name, Comm_Account, Comm_AcStatus, Type='Committee' FROM CACIDB..Committee " +
                        "UNION " +
                        "SELECT Com_Code, Com_Name, Com_Boss, Com_Account, Com_AcStatus, Type='Company' FROM CACIDB..Company) AS ab  " +
                        "WHERE 1=1";
        
        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            if (to.getAllColumnName()[i] == "Comm_AcStatus")
            {
                if (to.getValue(to.getAllColumnName()[i]).ToString()=="L")
                {
                    //cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i] + " ";
                    cmd.CommandText += " AND Comm_AcStatus='L' ";
                }
                else if (to.getValue(to.getAllColumnName()[i]).ToString() == "UnL")//正常
                {
                    cmd.CommandText += " AND Comm_AcStatus='' ";
                }
            }
            else
            {
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + " Like '%' + @" + to.getAllColumnName()[i] + " + '%' ";
            }

            cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
        }


        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
    {
        DataTable dt = new DataTable();

        string sqlstr = "SELECT Comm_Code, Comm_ComName, Comm_Name, Comm_Account, CASE Comm_AcStatus WHEN 'L' THEN '鎖定' ELSE '正常' END Comm_AcStatus, Type FROM " +
                        "(SELECT Comm_Code, Comm_ComName, Comm_Name, Comm_Account, Comm_AcStatus, Type='Committee' FROM CACIDB..Committee " +
                        "UNION " +
                        "SELECT Com_Code, Com_Name, Com_Boss, Com_Account, Com_AcStatus, Type='Company' FROM CACIDB..Company) AS ab  " +
                        "WHERE 1=1";

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            if (to.getAllColumnName()[i] == "Comm_AcStatus")
            {
                if (to.getValue(to.getAllColumnName()[i]).ToString() == "L")
                {
                    //cmd.CommandText += " AND " + to.getAllColumnName()[i] + "=@" + to.getAllColumnName()[i] + " ";
                    cmd.CommandText += " AND Comm_AcStatus='L' ";
                }
                else if (to.getValue(to.getAllColumnName()[i]).ToString() == "UnL")//正常
                {
                    cmd.CommandText += " AND Comm_AcStatus='' ";
                }
            }
            else
            {
                cmd.CommandText += " AND " + to.getAllColumnName()[i] + " Like '%' + @" + to.getAllColumnName()[i] + " + '%' ";
            }

            cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
        }

        cmd.CommandText += " Order By " + sortStr;

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }

    void IQueryBL.DeleteData(DataTO to)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region IMasterUIBL 成員

    void IMasterUIBL.InsertData(DataTO to)
    {
        throw new NotImplementedException();
    }

    void IMasterUIBL.UpdateData(DataTO to)
    {
        SqlCommand cmd = null;

        string type = to.getValue("Type").ToString();

        to.removeValue("Type");

        if (type == "Committee")
            cmd = new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..Committee", to);
        else
        {
            DataTO compTo = new DataTO();

            for(int i = 0 ; i < to.getAllColumnName().Length ; i++ )
            {
                compTo.setValue(to.getAllColumnName()[i].Replace("Comm", "Com"), to.getValue(to.getAllColumnName()[i]));
            }

            cmd = new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB..Company", compTo);
        }

        new SQLAgent(DataBase.CACIDB).execute(cmd);
    }

    void IMasterUIBL.LoadData(DataTO to)
    {
        if (to.getValue("Comm_Code").ToString().StartsWith("CO"))
        {
            DataTO committeeTo = new DataTO();

            committeeTo.setValue("Comm_Code", to.getValue("Comm_Code"));

            SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..Committee", committeeTo);

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

                if (!to.isColumnExist("Type"))
                    to.setValue("Type", "Committee");
            }
            sr.Close();
        }
        else
        {
            DataTO companyTo = new DataTO();

            companyTo.setValue("Com_Code", to.getValue("Comm_Code"));

            SqlCommand cmd = new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("CACIDB..Company", companyTo);

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
                
                if (!to.isColumnExist("Type")) 
                    to.setValue("Type", "Company");
            }
            sr.Close();

        }
    }

    bool IMasterUIBL.IsDataExist(DataTO to)
    {
        if (to.getValue("Comm_Code").ToString().StartsWith("CA"))
        {
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("CACIDB..Company", to);
        }
        else if (to.getValue("Comm_Code").ToString().StartsWith("CO"))
        {
            return new SQLCommandBuilder(DataBase.CACIDB).isDataExistByPrimayKey("CACIDB..Committee", to);
        }

        return false;
    }

    void IMasterUIBL.MarkData(DataTO to)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    #region 自訂動作

    public void UpdatePassWord(string _code, string _type, string newPwd)
    {
        SqlCommand cmd = null;

        if (_type == "Committee")
        {
            DataTO to = new DataTO();
            to.setValue("Comm_Code", _code);
            to.setValue("Comm_Pass", newPwd);

            cmd = new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB.." + _type, to);
        }
        else
        {
            DataTO to = new DataTO();
            to.setValue("Com_Code", _code);
            to.setValue("Com_Pass", newPwd);

            cmd = new SQLCommandBuilder(DataBase.CACIDB).getUpdateCommand("CACIDB.." + _type, to);
        }

        new SQLAgent(DataBase.CACIDB).execute(cmd);
    }

    public DataTable getDetailData(DataTO to)
    {
        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(new SQLCommandBuilder(DataBase.CACIDB).getSelectCommand("Detail_1", to), dt);

        return dt;
    }

    public DataTable getPj_Kind_List()
    {
        string sqlstr = "SELECT * FROM CACIDB..SysCode WHERE Sys_CdKind='P' AND Sys_CdType='K' ";

        SqlCommand cmd = new SqlCommand(sqlstr);

        DataTable dt = new DataTable();

        new SQLAgent(DataBase.CACIDB).select(cmd, dt);

        return dt;
    }
    #endregion
}