using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakLibrary
{
    [Serializable]
    public class ModalidadTest
    {
        private int _id;
        private string _nombreModalidad;

        public int Id { get => _id; set => _id = value; }
        public string NombreModalidad { get => _nombreModalidad; set => _nombreModalidad = value; }
    }
}
