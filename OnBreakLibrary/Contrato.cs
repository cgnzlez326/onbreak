using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakLibrary
{
    [Serializable]
    public class Contrato
    {
        private string _rut;
        private string _numeroContrato;
        private string _nombreEvento;
        private ModalidadServicio _modalidadServicioContrato;
        private string _direccion;
        private int _cantAsist;
        private int _personalAdicional;
        private double _total;
        private DateTime _inicioEvento;
        private DateTime _terminoEvento;
        private string _observaciones;
        private DateTime _creacionContrato;
        private DateTime _terminoContrato;
        private bool _vigente;

        public string VigenteTexto
        {
            get
            {
                if(_vigente == true)
                {
                    return "Si";
                }

                return "No";
            }
        }

        public string FechaCreacionContrato
        {
            get
            {
                return this._creacionContrato.ToString("dd-MM-yyyy HH:mm");
            }
        }

        public string FechaTerminoContrato
        {
            get
            {
                return this._terminoContrato.ToString("dd-MM-yyyy HH:mm");
            }
        }

        public string FechaInicioEvento
        {
            get
            {
                return this._inicioEvento.ToString("dd-MM-yyyy HH:mm");
            }
        }

        public string FechaTerminoEvento
        {
            get
            {
                return this._terminoEvento.ToString("dd-MM-yyyy HH:mm");
            }
        }


        public string Rut { get => _rut; set => _rut = value; }
        public string NumeroContrato { get => _numeroContrato; set => _numeroContrato = value; }
        public string NombreEvento { get => _nombreEvento; set => _nombreEvento = value; }
        public string Direccion { get => _direccion; set => _direccion = value; }
        public int CantAsist { get => _cantAsist; set => _cantAsist = value; }
        public int PersonalAdicional { get => _personalAdicional; set => _personalAdicional = value; }
        public DateTime InicioEvento { get => _inicioEvento; set => _inicioEvento = value; }
        public DateTime TerminoEvento { get => _terminoEvento; set => _terminoEvento = value; }
        public string Observaciones { get => _observaciones; set => _observaciones = value; }
        public DateTime CreacionContrato { get => _creacionContrato; set => _creacionContrato = value; }
        public DateTime TerminoContrato { get => _terminoContrato; set => _terminoContrato = value; }
        public bool Vigente { get => _vigente; set => _vigente = value; }
        public ModalidadServicio ModalidadServicioContrato { get => _modalidadServicioContrato; set => _modalidadServicioContrato = value; }
        public double Total { get => _total; set => _total = value; }
    }   
}
