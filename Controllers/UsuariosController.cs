using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using Pizzas.API.Services;
using Usuarios.API.Models;
using Usuarios.API.Services;

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
            string entity = null;

            usuario = UsuarioServices.Login(usuario.UserName, usuario.Password);
            
            if (usuario != null) {
                UserFound = true;
                entity = usuario.Token;
            }

            if (UserFound){
                return Ok(entity);
            } else {
                return NotFound("No se encontro el usuario ingresado");
            }
        }
    }
}