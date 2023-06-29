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
    public class UpdateT:IUpdateT
    {
        private IRepositorioTipo _repo;

        public UpdateT(IRepositorioTipo repo)
        {
            _repo = repo;
        }

        public void Update(Hotel.LogicaNegocio.Entidades.Tipo tipo)
        {
            try
            {
                _repo.Update(tipo);
            }catch (Exception ex)
            {
                throw new DominioException("Error - ", ex);
            }
        }

    }
    
}
