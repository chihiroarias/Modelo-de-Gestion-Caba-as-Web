using Hotel.LogicaNegocio.Entidades;
using HotelLogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaNegocio.InterfacesRepositorio
{
    public  interface IRepositorioUsuario:IRepositorio<Usuario>
    {
        bool CheckEsUsuario(string email, Contra password);
        public Usuario FindByEmail(string email);
    }
}
