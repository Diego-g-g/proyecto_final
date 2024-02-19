using coder.services;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussines.DTO;

namespace SistemaGestionBussines.controllers
{
    [ApiController]
    [Route ("api/[controller]")]
    public class ProductoVendidoController : Controller
    {
        ProductovendidoServices _services;

        public ProductoVendidoController(ProductovendidoServices services)
        {
            _services = services;
        }


        [HttpGet("{idUsuario}")]
        public IActionResult ProductoVendidoFromUsuario(int idUsuario)
        {
            if (idUsuario <= 0) 
            {
                return BadRequest ("El ID ingresado no es válido.");
            }

            List<ProductoVendidoDTO> list = this._services.FindProductoVendidoFromUsuario(idUsuario);

            if (list.Count == 0 )
            {
                 return NotFound("no se encontraron productos vendidos.");
            }
            return Ok(list);
        }
    }
}
