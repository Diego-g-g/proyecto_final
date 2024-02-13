using coder.database;
using coder.models;
using Microsoft.EntityFrameworkCore;

namespace coder.services
{
    public class ProductoVendidoServices
    {
        private DataContext _context;

        public ProductoVendidoServices(DataContext context)
        {
            this._context = context;
        }

        public ProductoVendido GetProductoVendidoId(int id)
        {
            var vendido = this._context.ProductoVendidos.Find(id);

            if (vendido == null)
            {
                throw new InvalidOperationException("Producto vendido no encontrado");
            }
            return vendido;
        }

        public List<ProductoVendido> GetProductoVendidos()
        {
            List<ProductoVendido> list = new List<ProductoVendido>();

            list = this._context.ProductoVendidos.ToList();

            if (list == null)
            {
                throw new InvalidOperationException("No se encontraron Productos vendidos");
            }

            return list;

        }

        public void AddProductoVendido(ProductoVendido vendido)
        {
            this._context.ProductoVendidos.Add(vendido);
            this._context.SaveChanges();

        }

        public void EditProductoVendido(int id, ProductoVendido vendido)
        {
            ProductoVendido? vendidoEditar = this._context.ProductoVendidos.Find(id);

            if (vendidoEditar == null)
            {
                throw new InvalidOperationException("Producto vendido no encontrado");
            }


            if (vendido.Stock != null)
            {
                vendidoEditar.Stock = vendido.Stock;
            }


            this._context.Update(vendidoEditar);
            this._context.SaveChanges();
        }

        public void DeleteProductoVendido(int id)
        {
            ProductoVendido? vendidoDelet = this._context.ProductoVendidos.Where(p => p.Id == id).FirstOrDefault();

            if (vendidoDelet == null)
            {
                throw new InvalidOperationException("Usuario no encontrado");
            }
            this._context.Remove(vendidoDelet);
            this._context.SaveChanges();
        }
    }
}
