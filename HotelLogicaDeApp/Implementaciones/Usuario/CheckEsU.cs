using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Interfaces.IUsuario;
using HotelLogicaNegocio.DominioException;
using HotelLogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Implementaciones.Usuario
{
    public class CheckEsU:ICheckEsU
    {
        private IRepositorioUsuario _repo;

        public CheckEsU(IRepositorioUsuario repo)
        {
            _repo = repo;
        }

        public bool CheckEs(string email, Contra password)
        {
            try
            {
                return _repo.CheckEsUsuario(email,password);
            }catch (Exception ex)
            {
                throw new DominioException("No fue posible", ex);
            }
        }
    }
}
