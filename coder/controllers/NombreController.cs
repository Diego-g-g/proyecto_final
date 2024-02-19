using Microsoft.AspNetCore.Mvc;

namespace SistemaGestionBussines.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NombreController : Controller
    {
        [HttpGet]
        public string GetNombre()
        {
            return "Diego Gauna";
        }
    }
}
