using System;
using System.Data.SqlClient;
using Pizzas.API.Helpers;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Pizzas.API.Utils
{
    public class BD
    {
        public static SqlConnection GetConnection() {
            SqlConnection db;
            string connectionString = @"server=A-AMI-12;DataBase=DAI-Pizzas;Trusted_Connection=True;";

            /* connectionString = 
                ConfigurationHelper.GetConfiguration()
                .GetValue<string>("DatabaseSettings:ConnectionString"); */
            db = new SqlConnection(connectionString);

            Console.WriteLine(connectionString);
            return db;
        }
    }
}






