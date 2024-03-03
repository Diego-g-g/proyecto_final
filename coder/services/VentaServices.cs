using coder.database;
using coder.models;
using Microsoft.EntityFrameworkCore;
using SistemaGestionBussines.DTO;
using SistemaGestionBussines.Mapper;

namespace coder.services
{
    public class VentaServices
    {
        private DataContext _context;
        private readonly ProductovendidoServices productoVendidoServices;
        private readonly ProductoServices productoService;

        public VentaServices(DataContext context, ProductovendidoServices productovendidoServices, ProductoServices productoService)
        {
            _context = context;
            this.productoVendidoServices = productovendidoServices;
            this.productoService = productoService;
        }

        public VentaDTO GetVentaId(int id)
        {
            var ven = this._context.Venta.Find(id);

            if (ven == null)
            {
                throw new InvalidOperationException("Venta no encontrado");
            }

            VentaDTO? newVenta = new VentaDTO();
            newVenta = VentaMapper.MapeoEditVentaDTO(newVenta, ven);

            return newVenta;
        }

        public List<Venta> GetVentas()
        {
            List<Venta> list = new List<Venta>();

            list = this._context.Venta.ToList();

            if (list == null)
            {
                throw new InvalidOperationException("No se encontraron ventas");
            }

            return list;

        }

        public void AddVenta(Venta venta)
        {
            this._context.Venta.Add(venta);
            this._context.SaveChanges();

        }

        public bool EditVenta(int id, VentaDTO venta)
        {
            Venta? ventaEditar = this._context.Venta.Find(id);

            if (ventaEditar == null || venta == null)
            {
                return false;
            }
            Venta? editado = VentaMapper.MapeoVentaDtoAVenta(ventaEditar, venta);
            if (editado is not null)
            {
                this._context.Update(editado);
                this._context.SaveChanges();
                return true;
            }
            return false;
        }

        public void DeleteVenta(int id)
        {
            Venta? ventaDelet = this._context.Venta
                .Include(u => u.ProductoVendidos)
                .Where(u => u.Id == id)
                .FirstOrDefault();

            if (ventaDelet == null)
            {
                throw new InvalidOperationException("Venta no encontrada");
            }
            this._context.Remove(ventaDelet);
            this._context.SaveChanges();
        }

        public List<VentaDTO> FindVentafromIdUsuario(int id)
        {
            List<Venta> ventas = new List<Venta>();
            ventas = this._context.Venta.Where(v => v.IdUsuario == id).ToList();

            List<VentaDTO> dtoVenta = new List<VentaDTO>();

            foreach (Venta v in ventas)
            {
                dtoVenta.Add(VentaMapper.MapeoReturnDTO(v));
            }

            return dtoVenta;
        }

        public bool AgregarNuevaVenta(int id, List<ProductoDTO> productosDTO)
        {
            Venta venta = new Venta();
            List<String> nombreProductos = productosDTO.Select(p => p.Descripciones).ToList();
            string comentario = string.Join(",", nombreProductos);
            venta.IdUsuario = id;
            venta.Comentarios = comentario;

            this._context.Venta.Add(venta);
            this._context.SaveChanges();

            this.ActualizarProductosVendidos(productosDTO, venta.Id);
            this.ActualizarStockPV(productosDTO);

            return true;

        }

        public void ActualizarProductosVendidos(List<ProductoDTO> productosDTO, int idVenta) 
        {
            foreach (ProductoDTO p in productosDTO)
            {
                ProductoVendidoDTO productosVendidos = new ProductoVendidoDTO();
                productosVendidos.IdProducto = p.Id;
                productosVendidos.IdVenta = idVenta;
                productosVendidos.Stock = p.Stock;
                this.productoVendidoServices.AddProductoVendido(productosVendidos);
            }
        }

        private void ActualizarStockPV(List<ProductoDTO> productosDTO)
        {
            foreach(ProductoDTO p in productosDTO)
            {
                ProductoDTO productoAModificar = this.productoService.GetProductoId(p.Id);
                productoAModificar.Stock -= p.Stock;
                this.productoService.EditProducto(productoAModificar);
            }
        }

    }
}
