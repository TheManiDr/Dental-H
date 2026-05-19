using Dental_H.DAO;
using Dental_H.Model;

namespace Dental_H.Controller
{
    public class LoginController
    {
        private UsuarioDAO usuarioDAO;

        public LoginController()
        {
            usuarioDAO = new UsuarioDAO();
        }

        public Usuario IniciarSesion(string usuario,string contrasena)
        {
            // VALIDAR CAMPOS VACÍOS
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                return null;
            }

            // LLAMAR DAO
            Usuario usuarioEncontrado = usuarioDAO.Login(usuario, contrasena);

            return usuarioEncontrado;
        }
    }
}