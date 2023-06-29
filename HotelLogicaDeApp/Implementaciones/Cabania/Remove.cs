using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Implementaciones.Cabania
{
    public class Remove: IRemoveC
    {
        private IRepositorioCabania _repoC;

        public Remove(IRepositorioCabania repo)
        {
            _repoC = repo;
        }

        void IRemoveC.Remove(Hotel.LogicaNegocio.Entidades.Cabania obj)
        {
            try
            {
                _repoC.Remove(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
