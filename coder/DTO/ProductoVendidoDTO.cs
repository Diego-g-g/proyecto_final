﻿
namespace SistemaGestionBussines.DTO
{
    public class ProductoVendidoDTO
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }

        public static implicit operator ProductoVendidoDTO(ProductoDTO v)
        {
            throw new NotImplementedException();
        }
    }
}
