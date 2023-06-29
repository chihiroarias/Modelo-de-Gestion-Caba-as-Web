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
    public class UpdateU:IUpdateU
    {
        private IRepositorioUsuario _repo;

        public UpdateU(IRepositorioUsuario repo)
        {
            _repo = repo;

        }

        public void Update(Hotel.LogicaNegocio.Entidades.Usuario us)
        {
            try
            {
                _repo.Update(us);//to do falta implementar el update en repousuario
            }catch (Exception ex)
            {
                    throw new DominioException("Revisar datos", ex);
             }
           
        }
    }
}
