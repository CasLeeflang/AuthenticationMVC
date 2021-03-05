using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DataAccessModel;


namespace DataAccess
{
    public class SQLDataAccess
    {

        public static string GetConnectionString()
        {
            return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBase;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);
            }
        }

        public static List<T> LoadSearchData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public static bool ValidateUser(string sql)
        {         
            using (IDbConnection conn = new SqlConnection(SQLDataAccess.GetConnectionString()))
            {
                return conn.ExecuteScalar<bool>(sql);
            }
        }

    }
}
