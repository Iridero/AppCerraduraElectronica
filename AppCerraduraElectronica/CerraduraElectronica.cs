using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCerraduraElectronica
{
    public class CerraduraElectronica
    {
        private string codigo = "*1234";
        private byte intentos = 0;
        private bool bloqueada = false;

        public delegate void Notificacion();

        public event Notificacion IngresoExitoso;
        public event Notificacion IngresoFallido;
        public event Notificacion CerraduraBloqueada;

        public void IngresarClave(string clave)
        {
            if (bloqueada)
            {
                CerraduraBloqueada?.Invoke();
                return;
            }
            if (codigo==clave)
            {
                IngresoExitoso?.Invoke();
                intentos = 0;
            }
            else
            {
                IngresoFallido?.Invoke();
                intentos++;
                if (intentos==5)
                {
                    CerraduraBloqueada?.Invoke();
                    bloqueada = true;
                }
            }
        }
    }
}
