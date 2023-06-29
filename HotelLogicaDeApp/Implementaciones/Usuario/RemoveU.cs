using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Interfaces.IUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.LogicaNegocio.Entidades;
using HotelLogicaNegocio.DominioException;

namespace HotelLogicaDeApp.Implementaciones.Usuario
{
    public class RemoveU : IRemoveU
    {
        private IRepositorioUsuario _repo;

        public RemoveU(IRepositorioUsuario repo)
        {
            _repo = repo;
        }
        public void Remove(Hotel.LogicaNegocio.Entidades.Usuario us)
        {
            try
            {
                _repo.Remove(us); 
            }
            catch (Exception ex)
            {
                throw new DominioException("No fue posible", ex);
            }
        }
    }
}
