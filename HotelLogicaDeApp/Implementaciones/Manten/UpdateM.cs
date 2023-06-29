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
    public class UpdateM : IUpdateM
    {
        private IRepositorioMantenimiento _repo;

        public UpdateM(IRepositorioMantenimiento repo)
        {
            _repo = repo;
        }

        public void Update(Mantenimiento obj)
        {
            try
            {
                _repo.Update(obj);
            }
            catch (Exception ex)
            {
                throw new DominioException("Revisar datos", ex);
            }
        }
    }
}
