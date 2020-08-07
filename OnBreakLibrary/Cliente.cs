using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakLibrary
{
    [Serializable]
    public class Cliente
    {
        private string _rut;
        private string _razon_social;
        private string _direccion;
        private string _nombrecontacto;
        private string _emailcontacto;
        private string _telefono;
        private ActividadEmpresa _actividadEmpresaCliente;
        private TipoEmpresa _tipoEmpresaCliente;

        public string Rut
        {
            get
            {
                return _rut;
            }

            set
            {
                _rut = value;
            }
        }

        public string Nombre_contacto
        {
            get
            {
                return _nombrecontacto;
            }
            set
            {
                _nombrecontacto = value;
            }
        }

        public string Email_contacto
        {
            get
            {
                return _emailcontacto;
            }
            set
            {
                _emailcontacto = value;
            }
        }

        public string Razon_social
        {
            get
            {
                return _razon_social;
            }

            set
            {
                _razon_social = value;
            }
        }

        public string Direccion
        {
            get
            {
                return _direccion;
            }

            set
            {
                _direccion = value;
            }
        }

        public void Show()
        {
            throw new NotImplementedException();
        }

        public string Telefono
        {
            get
            {
                return _telefono;
            }

            set
            {
                _telefono = value;
            }
        }

        public ActividadEmpresa ActividadEmpresaCliente { get => _actividadEmpresaCliente; set => _actividadEmpresaCliente = value; }
        public TipoEmpresa TipoEmpresaCliente { get => _tipoEmpresaCliente; set => _tipoEmpresaCliente = value; }
    }
}
