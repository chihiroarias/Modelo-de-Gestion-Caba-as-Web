using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Implementaciones.Cabania
{
    public class CreateCab : ICreateCab
    {
        private IRepositorioCabania _repoC;

        public CreateCab(IRepositorioCabania repo)
        {
            _repoC = repo;
        }
        public void Add(Hotel.LogicaNegocio.Entidades.Cabania cab)
        {
            if(cab.Equals(null))
            {
                throw new ArgumentException();
            }
            try
            {
                cab.Validar();
                _repoC.Add(cab);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

      
    }
}
