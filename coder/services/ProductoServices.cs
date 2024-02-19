using coder.database;
using coder.models;
using Microsoft.EntityFrameworkCore;
using SistemaGestionBussines.DTO;
using SistemaGestionBussines.Mapper;

namespace coder.services
{
    public class ProductoServices
    {
        private DataContext _context;

        public ProductoServices(DataContext dataContext)
        {
            _context = dataContext;
        }

        public Producto? GetProductoId(int id) 
        {
            return this._context.Productos.Find(id);
            
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

        public bool AddProducto(ProductoDTO producto)
        {
            Producto nuevo = ProductoMapper.MapperDTOAProducto(producto);
            this._context.Productos.Add(nuevo);
            this._context.SaveChanges();    
            return true;            
        }

        public bool EditProducto(int id, ProductoDTO producto)
        {
            Producto? productoEditar = this._context.Productos.Find(id);

            if (productoEditar == null || producto == null)
            {
                return false;
            }
            Producto? editado = ProductoMapper.MapeoEditProductoDTO(producto, productoEditar);

            if (editado is not null)
            {
                this._context.Update(editado);
                this._context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteProducto(int id)
        {
            Producto? productoDelet = this._context.Productos.Include(p => p.ProductoVendidos).Where(p => p.Id == id).FirstOrDefault();

            if (productoDelet is not null)
            {
                this._context.Remove(productoDelet);
                this._context.SaveChanges();
                return true;
            }
            return false;

        }

        public List<Producto> GetProductoFromIdUsuario(int id)
        {
            List<Producto> lista = new List<Producto>(); 
            lista = this._context.Productos.Where(u => u.IdUsuario == id).ToList();
            return lista;
        }
    }
}
