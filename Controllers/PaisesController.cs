
namespace _05_Tienda.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _05_Tienda.Models;
    internal class PaisesController
    {
        private PaisModel modeloPais = new PaisModel();

        public List<PaisModel> todos()
        {
            return modeloPais.todos();
        }
        public PaisModel uno(PaisModel pais)
        {
            return modeloPais.uno(pais);
        }
        public string insertar(PaisModel pais)
        {
            return modeloPais.insertar(pais);
        }
        public string actualziar(PaisModel pais)
        {
            return modeloPais.actualizar(pais);
        }
        public string eliminar(PaisModel pais)
        {
            return modeloPais.eliminar(pais);
        }

    }
}
