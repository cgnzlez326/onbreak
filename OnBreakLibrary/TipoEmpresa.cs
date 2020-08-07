using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakLibrary
{
    [Serializable]
    public class TipoEmpresa
    {
        private int _id;
        private string _descripcionTipoEmpresa;

        public int Id { get => _id; set => _id = value; }
        public string DescripcionTipoEmpresa { get => _descripcionTipoEmpresa; set => _descripcionTipoEmpresa = value; }
    }
}
