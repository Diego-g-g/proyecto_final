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

        public VentaServices(DataContext context)
        {
            _context = context;
        }

        public Venta GetVentaId(int id)
        {
            var venta = this._context.Venta.Find(id);

            if (venta == null)
            {
                throw new InvalidOperationException("Venta no encontrado");
            }
            return venta;
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
            Venta? editado = VentaMapper.MapeoEditVentaDTO(venta, ventaEditar);
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

        public List<Venta> FindVentafromIdUsuario(int id)
        {
            List<Venta> ventas = new List<Venta>();
            ventas = this._context.Venta.Where(v => v.IdUsuario == id).ToList();
            return ventas;
        }
    }
}
