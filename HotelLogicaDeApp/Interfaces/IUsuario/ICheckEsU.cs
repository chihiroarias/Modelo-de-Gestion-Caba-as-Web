using Hotel.LogicaNegocio.Entidades;
using HotelLogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Interfaces.IUsuario
{
    public interface ICheckEsU
    {
        bool CheckEs(string email, Contra password);
    }
}
