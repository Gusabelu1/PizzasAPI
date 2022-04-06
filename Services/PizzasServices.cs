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

            sqlQuery = "SELECT * ";
            sqlQuery += "FROM Pizzas";
            using (SqlConnection db = BD.GetConnection()) {
                returnList = db.Query<Pizza>(sqlQuery).ToList();
            }

            return returnList;
        } 

        
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
            }
        }
}