using Dapper;
using DataAccessModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DataAccess;


namespace DataAccess
{
    public class UserManager
    {
        public static int CreateUser(User data)
        {           

            string sql = @"insert into dbo.[User] (firstName, lastName, Password, Admin)
                            values(@firstName, @lastName, @Password, @Admin);";

            return SQLDataAccess.SaveData(sql, data);
        }

        //Prepare SQL to be sent to DAL
        public static bool IsValid(string firstName, string Password)
        {
            string sql = @"select count(1) from dbo.[User] where firstName = '" + firstName + "' and Password = '" + Password + "';"; //SQl injection

            return SQLDataAccess.ValidateUser(sql);            
        }

        public List<User> LoadUser(User data)
        {
            string sql = @"select from dbo.[User] where firstName = " + data.firstName; //Should probably laod based on Id

            return SQLDataAccess.LoadSearchData<User>(sql);
        }
    }
}
