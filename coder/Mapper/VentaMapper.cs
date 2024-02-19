using coder.models;
using SistemaGestionBussines.DTO;

namespace SistemaGestionBussines.Mapper
{
    public static class VentaMapper
    {
        public static Venta MapeoEditVentaDTO(VentaDTO dto, Venta venta)
        {
            venta.Comentarios = dto.Comentarios;
            venta.IdUsuario = dto.IdUsuario;
            return venta;
        }
    }
}
