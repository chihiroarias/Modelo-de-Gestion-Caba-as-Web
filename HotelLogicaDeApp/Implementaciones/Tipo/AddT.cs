using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Interfaces.ITipo;
using HotelLogicaNegocio.DominioException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Implementaciones.Tipo
{
    public class AddT:IAddT
    {
        private IRepositorioTipo _repo;

        public AddT(IRepositorioTipo repo)
        {
            _repo = repo;
        }   
        public void Add(Hotel.LogicaNegocio.Entidades.Tipo tipo)
        {
            if (tipo == null)
            {
                throw new DominioException("No puede ser nulo");
            }
            try
            {
                tipo.Validar();
                _repo.Add(tipo);
            }
            catch (Exception ex)
            {

                throw new DominioException("No se agregar el tipo." + ex.Message, ex);
            }
        }
    }
}
