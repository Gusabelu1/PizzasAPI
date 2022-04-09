using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using Pizzas.API.Services;
using Pizzas.API.Helpers;

namespace Pizzas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class PizzaController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Pizza> Pizzas = PizzaServices.GetAll();
            return Ok(Pizzas);
        }
        
        [HttpGet("{id}")]
        public Pizza GetById(int id){
            Pizza PBuscada = PizzaServices.ConsultaPizzas(id);
            return PBuscada;
        }

        
        [HttpPost]
        public IActionResult Create(Pizza Pizza) {
            string Token = Request.Headers["Token"];
            bool tokenValido = ConfigurationHelper.IsValidToken(Token);
            if (tokenValido) {
                PizzaServices.AgregarPizza(Pizza);
                return Ok();
            } else {
                return Unauthorized("No se encontro el token ingresado o tu token esta vencido");
            }
        }

        
        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza Pizza) {
            string Token = Request.Headers["Token"];
            bool tokenValido = ConfigurationHelper.IsValidToken(Token);
            if (tokenValido) {
                PizzaServices.ModificarPizza(id, Pizza);
                return Ok();
            } else {
                return Unauthorized("No se encontro el token ingresado o tu token esta vencido");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id) {
            string Token = Request.Headers["Token"];
            bool tokenValido = ConfigurationHelper.IsValidToken(Token);
            if (tokenValido) {
                int Registro = PizzaServices.EliminarPizza(id);
                return Ok(Registro);
            } else {
                return Unauthorized("No se encontro el token ingresado o tu token esta vencido");
            }
        }
    }
}

