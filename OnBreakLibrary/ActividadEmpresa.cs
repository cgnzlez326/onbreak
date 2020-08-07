using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakLibrary
{
    [Serializable]
    public class ActividadEmpresa
    {
        private int _id;
        private string _descripcionActividad;

        public int Id { get => _id; set => _id = value; }
        public string DescripcionActividad { get => _descripcionActividad; set => _descripcionActividad = value; }
    }
}
