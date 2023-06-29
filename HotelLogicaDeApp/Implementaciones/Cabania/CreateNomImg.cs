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
    public class CreateNomImg : ICreateNameImg
    {
        private IRepositorioCabania _repoC;

        public CreateNomImg(IRepositorioCabania repo)
        {
            _repoC = repo;
        }
        public string CrearNomImagen(Hotel.LogicaNegocio.Entidades.Cabania c)
        {
            try
            {
                return _repoC.CrearNomImagen(c);
            }
            catch (Exception ex)
            {
                throw new DominioException("Falló la operación: ", ex);
            }
        }
        
    }
}
