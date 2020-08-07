using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreakLibrary
{
    [Serializable]
    public class TipoEventoEmpresa
    {
        private int _id;
        private string _descripcionEvento;

        public int Id { get => _id; set => _id = value; }
        public string DescripcionEvento { get => _descripcionEvento; set => _descripcionEvento = value; }
    }
}
