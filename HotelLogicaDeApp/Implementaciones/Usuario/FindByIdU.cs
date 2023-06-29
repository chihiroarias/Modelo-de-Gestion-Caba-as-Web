using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Interfaces.IUsuario;
using HotelLogicaNegocio.DominioException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Implementaciones.Usuario
{
    public class FindByIdU : IFindU
    {
        private IRepositorioUsuario _repo;

        public FindByIdU(IRepositorioUsuario repo)
        {
            _repo = repo;

        }
        public Hotel.LogicaNegocio.Entidades.Usuario FindById(string Email)
        {
            try
            {
                return _repo.FindByEmail(Email);
            }
            catch (Exception ex)
            {

                throw new DominioException("No se encontro" + ex.Message, ex);
            }

        }
    }
}
