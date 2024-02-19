using coder.models;
using SistemaGestionBussines.DTO;

namespace SistemaGestionBussines.Mapper
{
    public static class UsuarioMapper
    {
        public static Usuario MapeaAUsuario(UsuarioDTO usuario)
        {
            Usuario user = new Usuario();
            user.Nombre = usuario.Nombre;
            user.Apellido = usuario.Apellido;
            user.NombreUsuario = usuario.NombreUsuario;
            user.Contraseña = usuario.Contraseña;
            user.Mail = usuario.Mail;

            return user;
        }

        public static UsuarioDTO MapeaADTO(Usuario usuario)
        {
            UsuarioDTO user = new UsuarioDTO();
            user.Nombre = usuario.Nombre;
            user.Apellido = usuario.Apellido;
            user.NombreUsuario = usuario.NombreUsuario;
            user.Contraseña = usuario.Contraseña;
            user.Mail = usuario.Mail;

            return user;
        }

        public static Usuario MapeoEditUsuarioDTO(UsuarioDTO DTO, Usuario usuario)
        {
            usuario.Nombre = DTO.Nombre;
            usuario.Apellido = DTO.Apellido;
            usuario.NombreUsuario = DTO.NombreUsuario;
            usuario.Contraseña = DTO.Contraseña;
            usuario.Mail = DTO.Mail;

            return usuario;

        }

    }
}
