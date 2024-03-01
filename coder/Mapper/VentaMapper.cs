using coder.models;
using SistemaGestionBussines.DTO;

namespace SistemaGestionBussines.Mapper
{
    public static class VentaMapper
    {
        public static VentaDTO MapeoEditVentaDTO(VentaDTO dto, Venta venta)
        {
            dto.Id = venta.Id;
            dto.Comentarios = venta.Comentarios;
            dto.IdUsuario = venta.IdUsuario;
            return dto;
        }

        public static Venta MapeoVentaDtoAVenta(Venta venta, VentaDTO dto)
        {
            venta.Id = dto.Id;
            venta.Comentarios = dto.Comentarios;
            venta.IdUsuario = dto.IdUsuario;
            return venta;
        }

        public static VentaDTO MapeoReturnDTO(Venta venta)
        {
            VentaDTO dto = new VentaDTO();
            dto.Id = venta.Id;
            dto.Comentarios = venta.Comentarios;
            dto.IdUsuario = venta.IdUsuario;
            return dto;
        }
    }
}
