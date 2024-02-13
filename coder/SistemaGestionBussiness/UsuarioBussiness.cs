using coder.database;
using coder.DTO;
using coder.models;
using Microsoft.EntityFrameworkCore;

namespace coder.services
{
    public class UsuarioBussiness
    {
        private DataContext _context;

        public UsuarioBussiness(DataContext context)
        {
            this._context = context;
        }

        public Usuario GetUsuarioId(int id)
        {
            var usuario = this._context.Usuarios.Find(id);

            if (usuario == null)
            {
                throw new InvalidOperationException("Usuario no encontrado");
            }
            return usuario;
        }

        public List<Usuario> GetUsuarios()
        {
            List<Usuario> list = new List<Usuario>();

            list = this._context.Usuarios.ToList();

            if (list == null)
            {
                throw new InvalidOperationException("No se encontraron usuarios");
            }

            return list;

        }

        public void AddUsuario(Usuario usuario) 
        {
            this._context.Usuarios.Add(usuario);
            this._context.SaveChanges();

        }

        public void EditUsuario(int id, Usuario usuario)
        {
            Usuario? usuarioEditar = this._context.Usuarios.Find(id);

            if (usuarioEditar == null)
            {
                throw new InvalidOperationException("Usuario no encontrado");
            }


            if (usuario.Nombre != null)
            {
                usuarioEditar.Nombre = usuario.Nombre;
            }

            if (usuario.Apellido != null)
            {
                usuarioEditar.Apellido = usuario.Apellido;
            }

            if (usuario.NombreUsuario != null)
            {
                usuarioEditar.NombreUsuario = usuario.NombreUsuario;
            }

            if (usuario.Mail != null)
            {
                usuarioEditar.Mail = usuario.Mail;
            }
    
            this._context.Update(usuarioEditar);
            this._context.SaveChanges();
        }

        public void DeleteUsuario(int id)
        {
            Usuario? usuarioDelet = this._context.Usuarios
                .Include(u => u.Venta)
                .Include(u => u.Productos)
                .Where(u => u.Id == id)
                .FirstOrDefault();

            if (usuarioDelet == null)
            {
                throw new InvalidOperationException("Usuario no encontrado");
            }
            this._context.Remove(usuarioDelet);
            this._context.SaveChanges(); 
        }


    }
}
