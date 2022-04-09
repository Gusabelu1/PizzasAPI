using System;
using System.Collections.Generic;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using Dapper;
using System.Data.SqlClient;
using Pizzas.API.Utils;
using Usuarios.API.Models;

namespace Usuarios.API.Services {
    public class UsuarioServices {
        public static Usuario Login (string UserName, string Password) {
            string sqlQuery;
            Usuario UsuarioBuscado;

            sqlQuery = "SELECT * ";
            sqlQuery += "FROM Usuarios WHERE UserName = @pUserName and Password = @pPassword";
            using (SqlConnection db = BD.GetConnection()) {
                UsuarioBuscado = db.QueryFirstOrDefault<Usuario>(sqlQuery, new {pUserName = UserName, pPassword = Password});
            }

            if (UsuarioBuscado != null) {
                RefreshToken(UsuarioBuscado.Id);
                return UsuarioBuscado;
            } else {
                return null;
            }
        }

        public static Usuario GetByUserNamePassword (string UserName, string Password) {
            string sqlQuery;
            Usuario UsuarioBuscado;

            sqlQuery = "SELECT * ";
            sqlQuery += "FROM Usuarios WHERE UserName = @pUserName and Password = @pPassword";
            using (SqlConnection db = BD.GetConnection()) {
                UsuarioBuscado = db.QueryFirstOrDefault<Usuario>(sqlQuery, new {pUserName = UserName, pPassword = Password});
            }

            if (UsuarioBuscado != null) {
                return UsuarioBuscado;
            } else {
                return null;
            }
        }

        public static Usuario GetByToken (string Token) {
            string sqlQuery;
            Usuario UsuarioBuscado;

            sqlQuery = "SELECT * ";
            sqlQuery += "FROM Usuarios WHERE Token = @pToken";
            using (SqlConnection db = BD.GetConnection()) {
                UsuarioBuscado = db.QueryFirstOrDefault<Usuario>(sqlQuery, new {pToken = Token});
            }

            if (UsuarioBuscado != null) {
                return UsuarioBuscado;
            } else {
                return null;
            } 
        }

        private static string RefreshToken (int Id) {
            string Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            DateTime TokenExpirationDate = DateTime.UtcNow.AddMinutes(15);
            string sqlQuery;

            sqlQuery = "UPDATE ";
            sqlQuery += "Usuarios SET Token = @pToken, TokenExpirationDate = @pTokenExpirationDate WHERE Id = @pId ";
            using (SqlConnection db = BD.GetConnection()) {
                db.Execute(sqlQuery, new{ pToken = Token, pTokenExpirationDate = TokenExpirationDate, pId = Id});
            }

            return Token;
        }
        /*
        public static Pizza ConsultaPizzas (int IdPizza)
        {
            string sqlQuery;
            Pizza PizzaBuscada;

            sqlQuery = "SELECT * "; 
            sqlQuery += "FROM Pizzas WHERE Id = @pId";
            using (SqlConnection db = BD.GetConnection()) {
                PizzaBuscada = db.QueryFirstOrDefault<Pizza>(sqlQuery, new {pId = IdPizza});
            }

            return PizzaBuscada;
        }

            public static void AgregarPizza (Pizza Pizza){
                string sqlQuery;

                sqlQuery = "INSERT ";
                sqlQuery += "INTO Pizzas( Nombre, LibreGluten, Importe, Descripcion) VALUES ( @pNombre, @pLibreGluten, @pImporte, @pDescripcion)";
                using (SqlConnection db = BD.GetConnection()) {
                    db.Execute(sqlQuery, new{ pNombre = Pizza.Nombre, pLibreGluten = Pizza.LibreGluten, pImporte = Pizza.Importe, pDescripcion = Pizza.Descripcion});
                }
            }

            public static void ModificarPizza (int id, Pizza Pizza){
               string sqlQuery;

               sqlQuery = "UPDATE ";
               sqlQuery += "Pizzas SET Nombre = @pNombre, LibreGluten = @pLibreGluten, Importe = @pImporte, Descripcion = @pDescripcion WHERE Id = @pId ";
               using(SqlConnection db = BD.GetConnection()) {
                  db.Execute(sqlQuery, new{ pId = id, pNombre = Pizza.Nombre, pLibreGluten = Pizza.LibreGluten, pImporte = Pizza.Importe, pDescripcion = Pizza.Descripcion});
               }
            }

            public static int EliminarPizza (int id){
               string sqlQuery;
               int Registro = 0;

               sqlQuery = "DELETE ";
               sqlQuery += "FROM Pizzas WHERE Id = @pId";               
               using(SqlConnection db =  BD.GetConnection()) {
                   Registro = db.Execute(sqlQuery, new {pId = id});
               }
               return Registro;
            } */
        }
}