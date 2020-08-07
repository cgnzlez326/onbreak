using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakLibrary
{
    [Serializable]
    public class ModalidadServicio
    {
        private string _id;
        private TipoEventoEmpresa _idEventoEmpresa;
        private string _nombreModalidad;
        private double _valorBase;
        private int _personalBase;

        public string Id { get => _id; set => _id = value; }
        public string NombreModalidad { get => _nombreModalidad; set => _nombreModalidad = value; }
        public double ValorBase { get => _valorBase; set => _valorBase = value; }
        public int PersonalBase { get => _personalBase; set => _personalBase = value; }
        public TipoEventoEmpresa IdEventoEmpresa { get => _idEventoEmpresa; set => _idEventoEmpresa = value; }
    }
}
