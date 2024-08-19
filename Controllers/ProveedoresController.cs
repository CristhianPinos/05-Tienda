
namespace _05_Tienda.Controllers
{
    using _05_Tienda.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ProveedoresController
    {
        private ProveedoresModel modeloProveedor = new ProveedoresModel();

        public List<ProveedoresModel> todos()
        {
            return modeloProveedor.todos();
        }
        public ProveedoresModel uno(ProveedoresModel proveedor)
        {
            return modeloProveedor.uno(proveedor);
        }
        public string insertar(ProveedoresModel proveedor)
        {
            return modeloProveedor.insertar(proveedor);
        }
        public string actualizar(ProveedoresModel proveedor)
        {
            return modeloProveedor.actualizar(proveedor);
        }
        public string eliminar(ProveedoresModel proveedor)
        {
            return modeloProveedor.eliminar(proveedor);
        }
    }
}
