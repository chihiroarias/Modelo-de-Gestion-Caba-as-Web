using Hotel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Interfaces.ITipo
{
    public interface IRemoveT
    {
        public void Remove(Tipo tipo);
        public bool EnUsoEnCabaña(Tipo tipo);
    }
}
