using Hotel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Interfaces
{
    public interface IFindC
    {
        Cabania FindById(int id);
        IEnumerable<Cabania> GetAll();
        IEnumerable<Tipo> GetTyps();

        IEnumerable<Cabania> cabFind(int? idTipo, double? monto);

    }
}
