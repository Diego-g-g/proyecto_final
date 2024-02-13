using coder.database;
using coder.DTO;
using coder.models;
using Microsoft.EntityFrameworkCore;

namespace coder.services
{
    public class VentaBussiness
    {
        private DataContext _context;

        public VentaBussiness(DataContext context)
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

        public void EditVenta(int id, Venta venta)
        {
            Venta? ventaEditar = this._context.Venta.Find(id);

            if (ventaEditar == null)
            {
                throw new InvalidOperationException("Venta no encontrada");
            }


            if (venta.Comentarios != null)
            {
                ventaEditar.Comentarios = venta.Comentarios;
            }

            this._context.Update(ventaEditar);
            this._context.SaveChanges();
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
    }
}
