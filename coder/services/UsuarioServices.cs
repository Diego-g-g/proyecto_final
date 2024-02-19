using coder.database;
using coder.models;
using Microsoft.EntityFrameworkCore;
using SistemaGestionBussines.DTO;
using SistemaGestionBussines.Mapper;

namespace SistemaGestionBussines.services
{
    public class UsuarioServices
    {
        private DataContext _context;

        public UsuarioServices(DataContext context)
        {
            this._context = context;
        }

        public Usuario? GetUsuarioId(int id)
        {
            return this._context.Usuarios.Find(id);
        }

        public List<Usuario> GetUsuarios()
        {
            List<Usuario> list = this._context.Usuarios.ToList();

            if (list == null)
            {
                throw new InvalidOperationException("No se encontraron usuarios");
            }

            return list;

        }

        public bool AddUsuario(UsuarioDTO usuario) 
        {
            if (usuario is not null)
            {
                this._context.Usuarios.Add(UsuarioMapper.MapeaAUsuario(usuario));
                this._context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool EditUsuario(int id, UsuarioDTO usuario)
        {
            Usuario? usuarioEditar = this._context.Usuarios.Find(id);

            if (usuarioEditar is not null)
            {

                this._context.Update(UsuarioMapper.MapeoEditUsuarioDTO(usuario, usuarioEditar));
                this._context.SaveChanges();

                return true;
            }
            return false;
        }

        public Usuario? GetFromNombre(string nombre)
        {
            Usuario? usuario = this._context.Usuarios.Where(n => n.NombreUsuario == nombre).FirstOrDefault();
            return usuario;
        }

        public Usuario? GetFromUsuarioPass(string usuario, string pass)
        {
            Usuario? user = this._context.Usuarios.Where(u => u.NombreUsuario == usuario && u.Contraseña ==  pass).FirstOrDefault();

            return user;
        }
    }
}
