using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using Dapper;
using System.Data.SqlClient;
using Pizzas.API.Utils;

namespace Pizzas.API.Services {
    public class PizzaServices {
        public static List<Pizza> GetAll() {
            string sqlQuery;
            List<Pizza> returnList;

            returnList = new List<Pizza>();
            using (SqlConnection db = BD.GetConnection()) {
                sqlQuery = "SELECT *";
                sqlQuery += "FROM Pizzas";
                returnList = db.Query<Pizza>(sqlQuery).ToList();
            }

            return returnList;
        } 

        
        public static Pizza ConsultaPizzas (int IdPizza)
        {
            string sqlQuery;
            Pizza PizzaBuscada;

            using (SqlConnection db = BD.GetConnection()) {
                sqlQuery = "SELECT *";
                sqlQuery += "FROM Pizzas WHERE Id = @pId";
                PizzaBuscada = db.QueryFirstOrDefault<Pizza>(sqlQuery, new {pId = IdPizza});
            }

            return PizzaBuscada;
        }

            public static void AgregarPizza (Pizza Pizza){
                string sqlQuery;

                using (SqlConnection db = BD.GetConnection()) {
                    sqlQuery = "INSERT";
                    sqlQuery += "INTO Pizzas( Id, Nombre, LibreGluten, Importe, Descripcion) VALUES ( @pId, @pNombre, @pLibreGluten, @pImporte, @pDescripcion)";
                    db.Execute(sqlQuery, new{ pId = Pizza.Id, pNombre = Pizza.Nombre, pLibreGluten = Pizza.LibreGluten, pImporte = Pizza.Importe, pDescripcion = Pizza.Descripcion});
                }
            }

            /* public static Pizza ModificarPizza (int id, Pizza Pizza){
               string sql = "UPDATE Pizzas SET (Nombre = @pNombre, LibreGluten = @pLibreGluten, Importe = @pImporte, Descripcion = @pDescripcion) WHERE id = @pId ";
               using(SqlConnection db =  new SqlConnection(_connectionString)){
                  db.QueryFirstOrDefault(sql, new {pId = Pizza.Id, pNombre = Pizza.Nombre, pLibreGluten = Pizza.LibreGluten, pImporte = Pizza.Importe, pDescripcion = Pizza.Descripcion});
               }
               return Pizza;
            }

             public static int EliminarPizza (int Id){
               int Registro = 0;
               string sql = "DELETE * FROM Pizza WHERE Id = @p ";
               using(SqlConnection db =  new SqlConnection(_connectionString)){
                   Registro = db.Execute(sql, new {p = Id});
               }
               return Registro;
            } */
        }
}