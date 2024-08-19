
namespace _05_Tienda.Controllers
{
    using _05_Tienda.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    internal class CiudadesController
    {
        private CiudadModel modeloCiudad = new CiudadModel();

        public List<CiudadModel> todos()
        {
            return modeloCiudad.todos();
        }
        public CiudadModel uno(CiudadModel ciudad)
        {
            return modeloCiudad.uno(ciudad);
        }
        public string insertar(CiudadModel ciudad)
        {
            return modeloCiudad.insertar(ciudad);
        }
        public string actualizar(CiudadModel ciudad)
        {
            return modeloCiudad.actualizar(ciudad);
        }
        public string eliminar(CiudadModel ciudad)
        {
            return modeloCiudad.eliminar(ciudad);
        }
    }
}
