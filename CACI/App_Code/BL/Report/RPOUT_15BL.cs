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
public class RPOUT_15BL : ICommonBL, IQueryBL
{
    #region IQueryMarkBL 成員

        private string getDefaultSql()
    {
        DataTable dt = new DataTable();
        BaseFun bf = new BaseFun();
        string result = "select dbo.chgToChnDate(convert(nvarchar(10),getdate(),111)) as getdate, " +
                         "(select Record_Text from MtgRecord where Meeting_Code = '0000000011' and Meeting_Index = 1 )as MtgRecord, " +
                         "b.Com_Code,b.Com_Name,b.Com_CttName,b.Com_CttTel,a.Coach_Date,d.Pj_Name,e.Times_Bgn,f.ChKd_Name,g.Comm_Name, " +
                         "(select Sys_CdText from SysCode where a.Coach_Status = Sys_CdCode and Sys_CdKind = 'O' and Sys_CdType = 'S') as status, " +
                         "c.Meeting_Code, c.Meeting_Index  " +
                         "from Committee g,Coach a inner join Company b on a.Com_Code = b.Com_Code inner join MtgCom c on a.Com_Code = c.Com_Code " +
                         "inner join Project d on a.Pj_Code = d.Pj_Code inner join MtgTimes e on c.Meeting_Code = e.Meeting_Code   " +
                         "and c.Meeting_Index = e.Meeting_Index inner join CoachKind f on a.ChKd_Code = f.ChKd_Code ";
                                             
        return result;
    }
        private SqlCommand getFilter(string sqlstr, DataTO to)
    {

        SqlCommand cmd = new SqlCommand(sqlstr);

        for (int i = 0; i < to.getAllColumnName().Length; i++)
        {
            switch (to.getAllColumnName()[i])
            {

                case "Com_Tonum":
                    cmd.CommandText += " AND b.Com_Tonum = @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
             
                case "Com_Name":
                    cmd.CommandText += " AND b.Com_Name = @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;

                case "Coach_DateS":
                    cmd.CommandText += " AND dbo.chgToChnDate(a.Coach_Date) >= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Coach_DateE":
                    cmd.CommandText += " AND dbo.chgToChnDate(a.Coach_Date) <= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Times_BgnS":
                    cmd.CommandText += " AND dbo.chgToChnDate(e.Times_Bgn) >= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
                case "Times_BgnE":
                    cmd.CommandText += " AND dbo.chgToChnDate(e.Times_Bgn) <= @" + to.getAllColumnName()[i];
                    cmd.Parameters.AddWithValue("@" + to.getAllColumnName()[i], to.getValue(to.getAllColumnName()[i]));
                    break;
            }
        }
        return cmd;
    }
        DataTable IQueryBL.QueryDataForList(DataTO to)
        {
            DataTable dt = new DataTable();
            BaseFun bf = new BaseFun();
            string sqlstr = getDefaultSql();
            SqlCommand cmd = getFilter(sqlstr, to);

            cmd.CommandText += "group by b.Com_Code,b.Com_Name,b.Com_CttName,a.Coach_Date,b.Com_CttTel,a.Coach_Date,d.Pj_Name,e.Times_Bgn,f.ChKd_Name,a.Coach_Status,c.Meeting_Code, c.Meeting_Index, g.Comm_Name ";

            new SQLAgent(DataBase.CACIDB).select(cmd, dt);
            return dt;
        }

        DataTable IQueryBL.QueryDataForList(DataTO to, string sortStr)
        {
            DataTable dt = new DataTable();
            BaseFun bf = new BaseFun();

            string sqlstr = getDefaultSql();

            SqlCommand cmd = getFilter(sqlstr, to);

            cmd.CommandText += " order by " + sortStr;
            new SQLAgent(DataBase.CACIDB).select(cmd, dt);
            return dt;
        }
        public DataTable getPrintInfo(DataTO conds, String SelectData)
        {

            DataTable dt = new DataTable();
            BaseFun bf = new BaseFun();

            string sqlstr = getDefaultSql();

            SqlCommand cmd = getFilter(sqlstr, conds);

            if (!SelectData.Equals(""))
            {
                cmd.CommandText += " AND b.Com_Code in (" + SelectData + ")";

            }

            cmd.CommandText += "order by b.Com_Code ";
            new SQLAgent(DataBase.CACIDB).select(cmd, dt);
            return dt;

        }
    #endregion
        
    void IQueryBL.DeleteData(DataTO to)
        {
            throw new NotImplementedException();
        }

  
}