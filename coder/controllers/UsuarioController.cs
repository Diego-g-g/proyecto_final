﻿using coder.models;
using coder.services;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussines.DTO;
using SistemaGestionBussines.services;

namespace SistemaGestionBussines.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioServices _services;

        public UsuarioController(UsuarioServices services)
        {
            _services = services;
        }

        [HttpPut]
        public IActionResult UpdateUsuario([FromBody] UsuarioDTO usuario)
        {
            if (_services.EditUsuario(usuario))
            {
                return Ok(usuario);
            }
            else
            {
                return BadRequest("No se pudo actualizar el usuario.");
            }
        }

        [HttpPost]
        public IActionResult AddUsuario([FromBody] UsuarioDTO usuario)
        {
            if (usuario is not null)
            {
                this._services.AddUsuario(usuario);

                return Ok(usuario);
            }
            return BadRequest("No se pudo agregar usuario.");
        }

        [HttpGet ("{nombreDeUsuario}")]
        public IActionResult GetFromNombre(string nombreDeUsuario) 
        {
            if (nombreDeUsuario == null) 
            {
                return BadRequest("Ingrese un usuario valido");
            }

            Usuario? usuario = this._services.GetFromNombre(nombreDeUsuario);
            if (usuario is not null)
            {
                return Ok(usuario);
            }
            return NotFound("No se encontro el usuario.");
        }

        [HttpGet ("{usuario}/{password}")]
        public IActionResult GetAcces(string usuario, string password)
        {
            if(usuario == null && password == null)
            {
                return base.NoContent();
            }
            UsuarioDTO user = this._services.GetFromUsuarioPass(usuario, password);
            
            if(user is not null) 
            {
                return Ok(user);
            }
            return BadRequest(new { mensaje = "Acceso denegado" });
        }
    }
}
