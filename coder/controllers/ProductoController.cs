using coder.models;
using coder.services;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussines.DTO;

namespace SistemaGestionBussines.controllers
{
    [ApiController]
    [Route ("api/[controller]")]
    public class ProductoController : Controller
    {
        private ProductoServices _services;

        public ProductoController(ProductoServices services) 
        {
            this._services = services;
        }

        [HttpPut]
        public IActionResult UpdateProducto([FromBody] ProductoDTO producto)
        {
            if (this._services.EditProducto(producto))
            {
                return Ok(producto);
            }
            return BadRequest("No se pudo actualizar el producto.");
        }


        [HttpDelete ("{idProducto}")]
        public IActionResult DeleteProducto(int idProducto)
        {
            if (idProducto <= 0)
            {
                return BadRequest("Ingrese un ID valido.");
            }
            if (this._services.DeleteProducto(idProducto))
            {
                return Ok(new { Mensaje = "Eliminacion con exito." });
            }
            return BadRequest("No se pudo actualizar el usuario.");
        }

        [HttpPost]
        public IActionResult AddProducto(ProductoDTO producto) 
        {
            if(producto is not null)
            {
                this._services.AddProducto(producto);
                return Ok(producto);
            }
            return BadRequest("No se pudo agregar el producto.");
        }

        [HttpGet ("{idUsuario}")]
        public IActionResult GetProductoFromIdUsuario(int idUsuario)
        {
            if (idUsuario <= 0) 
            {
                return BadRequest("Ingrese id valido.");
            }

            List<Producto> lista = this._services.GetProductoFromIdUsuario(idUsuario);

            if (lista.Count == 0)
            {
                return NotFound("No hay Productos con ese usuario.");
            }
            else
            {
                return Ok(lista);
            }
        }

    }
}
