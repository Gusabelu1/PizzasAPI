using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using Pizzas.API.Services;
using Usuarios.API.Models;

namespace Usuarios.API.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Usuario usuario) {
            bool UserFound = false;

            if(UserFound){
                return Ok(entity);
            } else {
                return NotFound();
            }
        }
    }
}