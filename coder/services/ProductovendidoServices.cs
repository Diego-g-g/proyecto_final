using coder.database;
using coder.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionBussines.DTO;
using SistemaGestionBussines.Mapper;

namespace coder.services
{
    public class ProductovendidoServices
    {
        private DataContext _context;

        public ProductovendidoServices(DataContext context)
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


            if (vendido is not null)
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

        public List<ProductoVendidoDTO> FindProductoVendidoFromUsuario(int id)
        {
            List<Producto> productos = this._context.Productos
                .Include(p => p.ProductoVendidos)
                .Where(p => p.IdUsuario == id)
                .ToList();

            List<ProductoVendido?> vendido = productos
                .Select(p => p.ProductoVendidos
                                    .ToList()
                                    .Find(pv => pv.IdProducto == p.Id))
                .Where(p => !object.ReferenceEquals(p, null))
                .ToList();
           
            List<ProductoVendidoDTO> vendidoDTO = vendido
                .Select(pv => ProductoVendidoMapper.MapeaProductovendidoADto(pv)).ToList();

            return vendidoDTO;
        }

    }
}
