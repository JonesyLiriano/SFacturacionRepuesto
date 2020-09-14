using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocios
{
    public class ClientesNegocio
    {
        ClientesDatos clientesDatos = new ClientesDatos();
        public Tuple<bool, int> AgregarCliente(Cliente cliente)
        {            

            return clientesDatos.AgregarCliente(cliente);
        }

        public ObjectResult<proc_CargarTodosClientes_Result> CargarTodosClientes(int indicePagina, int tamanoPagina, string filtro, string columna)
        {
            return clientesDatos.CargarTodosClientes(indicePagina, tamanoPagina, filtro, columna);
        }
      
        public bool EditarCliente(Cliente cliente)
        {
            return clientesDatos.EditarCliente(cliente);
        }

        public bool BorrarCliente(int clienteID)
        {
            return clientesDatos.BorrarCliente(clienteID);
        }
        public bool VerificarCredito(int clienteID, decimal montoFactura)
        {
            return clientesDatos.VerificarCredito(clienteID, montoFactura);
        }

        public ObjectResult<proc_BuscarClientePID_Result> BuscarClientePID(int clienteID)
        {
            return clientesDatos.BuscarClientePID(clienteID);
        }
    }
}
