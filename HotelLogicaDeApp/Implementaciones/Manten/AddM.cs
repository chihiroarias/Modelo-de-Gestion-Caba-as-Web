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
    public class AddM :IAddM
    {
        private IRepositorioMantenimiento _repo;

        public AddM(IRepositorioMantenimiento repo)
        {
            _repo = repo;
        }

        public void Add(Mantenimiento man)
        {
            try
            {
                man.Validar();
                _repo.Add(man);
            }
            catch (Exception ex) 
            {
                throw new DominioException("Revisar datos", ex);
            }
        }
    }
}
