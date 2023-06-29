using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Interfaces;
using HotelLogicaNegocio.DominioException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Implementaciones.Cabania
{
    public class Buscar:IBuscar
    {
        private IRepositorioCabania _repoC;

        public Buscar(IRepositorioCabania repo)
        {
            _repoC = repo;
        }

        IEnumerable<Hotel.LogicaNegocio.Entidades.Cabania> IBuscar.Buscar(string? nombre, int? maxPersonas, bool? habilitado, int? tipoAsociado)
        {
            try
            {
               return _repoC.Buscar(nombre, maxPersonas, habilitado, tipoAsociado);
            }
            catch (Exception ex)
            {
                throw new DominioException("Invalid Operation", ex);
            }
        }
    }
}
