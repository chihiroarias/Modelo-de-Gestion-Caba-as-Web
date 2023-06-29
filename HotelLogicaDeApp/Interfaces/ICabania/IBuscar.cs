using Hotel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Interfaces
{
    public interface IBuscar
    {
        IEnumerable<Cabania> Buscar(string? nombre, int? maxPersonas, bool? habilitado, int? tipoAsociado);
    }
}
