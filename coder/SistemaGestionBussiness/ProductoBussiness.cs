using coder.database;
using coder.models;
using Microsoft.EntityFrameworkCore;

namespace coder.services
{
    public class ProductoBussiness
    {
        private DataContext _context;

        public ProductoBussiness(DataContext dataContext)
        {
            _context = dataContext;
        }

        public Producto GetProductoId(int id) 
        {
            Producto producto = _context.Productos.Find(id);

            
            if (producto == null)
            {
              throw new InvalidOperationException("Producto no encontrado");
            }
            return producto;
            
        }

        public List<Producto> GetProductos()
        {
            List<Producto> list = new List<Producto>();

            list = this._context.Productos.ToList();

            if (list == null)
            {
                throw new InvalidOperationException("No se encontraron productos");
            }

            return list;

        }

        public void AddProducto(Producto producto)
        {
            this._context.Productos.Add(producto);
            this._context.SaveChanges();    

        }

        public void EditProducto(int id, Producto producto)
        {
            Producto? productoEditar = this._context.Productos.Find(id);

            if (productoEditar == null)
            {
                throw new InvalidOperationException("Producto no encontrado");
            }


            if (producto.Descripciones != null)
            {
                productoEditar.Descripciones = producto.Descripciones;
            }

            if (producto.Costo != null)
            {
                productoEditar.Costo = producto.Costo;
            }
            
            if (producto.PrecioVenta != null)
            {
                productoEditar.PrecioVenta = producto.PrecioVenta;
            }

            if (producto.Stock != null)
            {
                productoEditar.Stock = producto.Stock;
            }


            this._context.Update(productoEditar);
            this._context.SaveChanges();
        }

        public void DeleteProducto(int id)
        {
            Producto? productoDelet = this._context.Productos.Include(p => p.ProductoVendidos).Where(p => p.Id == id).FirstOrDefault();

            if (productoDelet == null)
            {
                throw new InvalidOperationException("Usuario no encontrado");
            }

            this._context.Remove(productoDelet);
            this._context.SaveChanges();
        }
    }
}
