using coder.models;
using SistemaGestionBussines.DTO;

namespace SistemaGestionBussines.Mapper
{
    public static class ProductoMapper
    {
        public static Producto MapperDTOAProducto(ProductoDTO producto)
        {
            Producto pro = new Producto();
            pro.Descripciones = producto.Descripciones;
            pro.Costo = producto.Costo;
            pro.PrecioVenta = producto.PrecioVenta;
            pro.Stock = producto.Stock;
            pro.IdUsuario = producto.IdUsuario;
            return pro;

        }
        public static ProductoDTO MapperProductoADTO(Producto producto)
        {
            ProductoDTO pro = new ProductoDTO();
            pro.Descripciones = producto.Descripciones;
            pro.Costo = producto.Costo;
            pro.PrecioVenta = producto.PrecioVenta;
            pro.Stock = producto.Stock;
            pro.IdUsuario = producto.IdUsuario;
            return pro;
        }

        public static Producto MapeoEditProductoDTO(ProductoDTO dto, Producto producto)
        {
            producto.Descripciones = dto.Descripciones;
            producto.Costo = dto.Costo;
            producto.PrecioVenta = dto.PrecioVenta;
            producto.Stock = dto.Stock;
            producto.IdUsuario = dto.IdUsuario;
            return producto;
        }
    }
}
