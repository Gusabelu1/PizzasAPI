using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using Pizzas.API.Services;

namespace Pizzas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaController : ControllerBase
    {
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
        public IActionResult Create(Pizza pizza) {
            PizzaServices.AgregarPizza(pizza);
            return Ok();
        }

        
        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza) {
            PizzaServices.ModificarPizza(id, pizza);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id) {
            int Registro = PizzaServices.EliminarPizza(id);
            return Ok(Registro);
        }
    }
}

