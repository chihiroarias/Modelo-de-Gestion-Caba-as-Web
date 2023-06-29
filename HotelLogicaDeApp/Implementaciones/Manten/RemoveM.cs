using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Interfaces.IManten;
using HotelLogicaNegocio.DominioException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Implementaciones.Manten
{
    public class RemoveM : IRemoveM
    {
        private IRepositorioMantenimiento _repo;

        public RemoveM(IRepositorioMantenimiento repo)
        {
            _repo = repo;
        }

        public void Remove(Mantenimiento obj)
        {
            try
            {
                _repo.Remove(obj);
            }
            catch (Exception ex)
            {
                throw new DominioException("No fue posible", ex);
            }
        }
    }
}
