using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Interfaces;
using HotelLogicaNegocio.DominioException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Implementaciones.Cabania
{
    public class UpdateCab : IUpdateC
    {
        private IRepositorioCabania _repoC;

        public UpdateCab(IRepositorioCabania repo)
        {
            _repoC = repo;
        }
        public void Update(Hotel.LogicaNegocio.Entidades.Cabania obj)
        {
            if (obj == null)
            {
                throw new NotImplementedException();
            }
            try
            {
                _repoC.Update(obj);
            }
            catch(Exception ex)
            {
                throw new DominioException("No se pudo realizar la operación ", ex);
            }
        }
    }
}
