using System.Data.SqlClient;
using System;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.IO;
using Pizzas.API.Utils;
using Usuarios.API.Models;

namespace Pizzas.API.Helpers {
    public class ConfigurationHelper {
        public static IConfiguration GetConfiguration() {
            IConfiguration config;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json", 
                    optional: true, 
                    reloadOnChange: true);
            config = builder.Build();
            return config;
        }

        public static bool IsValidToken (string Token) {
            string sqlQuery;
            string validToken;
            DateTime validDate;

            sqlQuery = "SELECT Token ";
            sqlQuery += "FROM Usuarios WHERE Token = @pToken";
            using (SqlConnection db = BD.GetConnection()) {
                validToken = db.QueryFirstOrDefault<string>(sqlQuery, new {pToken = Token});
            }

            sqlQuery = "SELECT TokenExpirationDate ";
            sqlQuery += "FROM Usuarios WHERE Token = @pToken";
            using (SqlConnection db = BD.GetConnection()) {
                validDate = db.QueryFirstOrDefault<DateTime>(sqlQuery, new {pToken = Token});
            }

            if (validToken != null && DateTime.UtcNow < validDate) {
                return true;
            } else {
                return false;
            }
        }
    }
}