
using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Interfaces.IManten;
using HotelLogicaDeApp.Interfaces.IUsuario;
using HotelLogicaNegocio.DominioException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.LogicaNegocio.Entidades;

namespace HotelLogicaDeApp.Implementaciones.Usuario
{
    public class AddU : IAddU
    {
        private IRepositorioUsuario _repo;

        public AddU(IRepositorioUsuario repo)
        {
            _repo = repo;

        }
        public void Add(Hotel.LogicaNegocio.Entidades.Usuario us) 
        {
            if (us == null)
            {
                throw new DominioException("No puede ser nulo");
            }
            try
            {
                us.Validar();
                _repo.Add(us);
            }
            catch (Exception ex)
            {

                throw new DominioException("No se agregar el usuario." + ex.Message, ex);
            }

        }
    }
}
