﻿using coder.models;
using coder.services;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussines.DTO;
using System.Security.Cryptography.X509Certificates;

namespace SistemaGestionBussines.controllers
{
    [ApiController]
    [Route ("api/[controller]")]
    public class VentaController : Controller
    {
        VentaServices _services;
        public VentaController(VentaServices services)
        {
            this._services = services;
        }

        [HttpGet("{id}")]
        public IActionResult VentasfromIdUsuario(int id) 
        {
            if(id <= 0) 
            {
                return Conflict("Id incorrecto");
            }
            
            List<VentaDTO> ventas = this._services.FindVentafromIdUsuario(id);
            
            if(ventas is not null)
            {
                return Ok(ventas);
            }
            return BadRequest("No se encontraron ventas");
        }

        [HttpPut]
        public IActionResult UpdateVenta(int id, [FromBody] VentaDTO venta)
        {
            if (id <= 0)
            {
                return BadRequest("ID no válido.");
            }

            var existeVenta = _services.GetVentaId(id);

            if (existeVenta == null)
            {
                return NotFound("Venta no encontrada.");
            }

            if (_services.EditVenta(id, venta))
            {
                return Ok(new {Mensaje = "Venta modificada", Venta = existeVenta});
            }
            else
            {
                return BadRequest("No se pudo actualizar la venta.");
            } 
        }

        [HttpPost("{idUsuario}")]
        public IActionResult CrearVenta(int idusuario, [FromBody] List<ProductoDTO> productos)
        {
            if (productos.Count == 0)
            {
                return BadRequest();
            }
            try
            {
                this._services.AgregarNuevaVenta(idusuario, productos);
                return Ok();
            }
            catch
            {
                return Conflict();
            }
        }
    }
}
