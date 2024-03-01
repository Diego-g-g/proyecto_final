using coder.models;
using SistemaGestionBussines.DTO;

namespace SistemaGestionBussines.Mapper
{
    public static class ProductoVendidoMapper
    {
        public static ProductoVendidoDTO MapeaProductovendidoADto(ProductoVendido vendido)
        {
            ProductoVendidoDTO dto = new ProductoVendidoDTO();
            dto.Id = vendido.Id;
            dto.Stock = vendido.Stock;
            dto.IdProducto = vendido.IdProducto;
            dto.IdVenta = vendido.IdVenta;
            return dto;
        }

        public static ProductoVendido MapeaProductovendidoDTO(ProductoVendidoDTO dto)
        {
            ProductoVendido vendido = new ProductoVendido();
            vendido.Id = dto.Id;
            vendido.Stock = dto.Stock;
            vendido.IdProducto = dto.IdProducto;
            vendido.IdVenta = dto.IdVenta;
            return vendido;
        }
    }
}
