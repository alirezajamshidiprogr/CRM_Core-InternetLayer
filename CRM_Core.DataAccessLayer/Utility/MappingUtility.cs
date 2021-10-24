using CRM_Core.DataAccessLayer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace CRM_Core.Infrastructure
{
    public static class MappingUtility
    {

        //public static DataTable LoadStoreProcedureDT()
        //{
        //    using (var context = new CRM_CoreDB())
        //    {
        //        var cmd = context.Database.GetDbConnection().CreateCommand();
        //        var param = cmd.CreateParameter();
        //        cmd.CommandText = "[dbo].[GetPeopleProperty_SearchMobiles]";
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        param.ParameterName = "@PeopleId";
        //        param.Value = 3;
        //        cmd.Parameters.Add(param);
        //        context.Database.OpenConnection();
        //        var dataReader = cmd.ExecuteReader();
        //        var dataTable = new DataTable();
        //        dataTable.Load(dataReader);
        //        return dataTable;
        //    }
        //}
        public static DataSet LoadStoreProcedureDS(int peopleId)
        {
            SqlConnection conn = new SqlConnection("Server=.; initial Catalog=MoshattehDB; integrated security=true;");
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "[dbo].[GetPeopleProperty_SearchMobiles]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PeopleId", peopleId);
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();

            ///conn.Open();
            da.Fill(ds);
            ///conn.Close();

            return ds;
        }

        public static List<T> DataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        var value = dr[column.ColumnName];
                        pro.SetValue(obj, value.ToString() == string.Empty ? string.Empty : dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}

