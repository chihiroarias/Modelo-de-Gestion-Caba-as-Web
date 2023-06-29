using Hotel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Interfaces.ITipo
{
    public interface IFindT
    {
        public IEnumerable<Tipo> FindAll();

        public Tipo FindById(int id);
        public bool EnUsoEnCabaña(Tipo tipo);

        public Tipo FindByName(string name);
    }
}
