using System;
using System.Windows.Forms;

namespace Dental_H.Util
{
    public static class AppNavigator
    {
        private static bool navegando;

        public static void AbrirDesdeLogin(Form login, Form destino)
        {
            RegistrarCierreDeAplicacion(destino);
            navegando = true;
            destino.Show();
            login.Hide();
            navegando = false;
        }

        public static void IrA(Form actual, Form destino)
        {
            if (actual != null && actual.GetType() == destino.GetType())
            {
                destino.Dispose();
                return;
            }

            RegistrarCierreDeAplicacion(destino);
            navegando = true;
            destino.Show();

            if (actual != null)
            {
                actual.Hide();
                actual.Close();
            }

            navegando = false;
        }

        public static void AbrirSecundaria(Form actual, Form destino)
        {
            RegistrarCierreDeAplicacion(destino);
            navegando = true;
            destino.Show();
            actual?.Hide();
            navegando = false;
        }

        public static void Regresar(Form actual, Form anterior)
        {
            navegando = true;
            anterior?.Show();
            actual?.Close();
            navegando = false;
        }

        private static void RegistrarCierreDeAplicacion(Form form)
        {
            form.FormClosed += (sender, e) =>
            {
                if (!navegando)
                {
                    Application.Exit();
                }
            };
        }
    }
}
