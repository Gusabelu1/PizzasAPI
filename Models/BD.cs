using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using Dapper;
using System.Data.SqlClient;

namespace Pizzas.API.Controllers
{
    public class BD
    {
        private static string _connectionString = @"server=A-CEO-41\SQLEXPRESS;
        DataBase=DAI-Pizzas;Trusted_Connection=True;";

        public static Pizza ConsultaPizzas (int IdPizza){
           Pizza PizzaBuscada = null;
           string sql = "SELECT * FROM Pizzas WHERE Id = @p";
           using(SqlConnection db =  new SqlConnection(_connectionString)){
               PizzaBuscada = db.QueryFirstOrDefault<Pizza>(sql, new {p = IdPizza});
           }
           return PizzaBuscada;
        }

        public static void AgregarPizza (Pizza Pizza){
            string sql = "INSERT INTO Pizzas( Id, Nombre, LibreGluten, Importe, Descripcion) VALUES ( @pId, @pNombre, @pLibreGluten, @pImporte, @pDescripcion)";
            using(SqlConnection db =  new SqlConnection(_connectionString)){
                db.Execute(sql, new{ pId = Pizza.Id, pNombre = Pizza.Nombre, pLibreGluten = Pizza.LibreGluten, pImporte = Pizza.Importe, pDescripcion = Pizza.Descripcion});
            }
        }
        public static Pizza ModificarPizza (int id, Pizza Pizza){
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
        }
    }
}






