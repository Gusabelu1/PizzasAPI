using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;

namespace Pizzas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaController : ControllerBase
    {
        public IActionResult GetAll()
        {
            var rng = new Random();
            return Ok(Enumerable.Range(1, 5).Select(index => new Pizza
            {
                Descripcion = "Con salsa de tomate y queso",
                Id = 1,
                Importe = 300,
                LibreGluten = false,
                Nombre = "Muzza Individual"
            })
            .ToArray());
        }
        [HttpGet("{id}")]
        public Pizza GetById(int id){
            Pizza PBuscada = BD.ConsultaPizzas(id);
            return PBuscada;
        }

        [HttpPost]
        public IActionResult Create(Pizza pizza) {
            BD.AgregarPizza(pizza);
            return Ok();
            
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza) {
            BD.ModificarPizza(id, pizza);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id) {
            int Registro = BD.EliminarPizza(id);
            return Ok();
        }
    }
}

