using Capa.Datos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class ClienteBL
    {
        private ClienteDAL dalc = new ClienteDAL();

        public List<Cliente> ListarCl()
        {
            return dalc.Listarc();
        }

        public void AgregarCl(Cliente cliente)
        {
            if (cliente.id_cliente == 0)
                dalc.insertarc(cliente);
            else
                dalc.Actualizarc(cliente);
        }

        public void EliminarCl(Cliente cliente)
        {
            dalc.Eliminarc(cliente);
        }
         
    }
}

