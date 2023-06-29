using Hotel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioCabania: IRepositorio<Cabania>
    {
        string CrearNomImagen(Cabania c);

        IEnumerable<Cabania> Buscar(string? nombre, int? maxPersonas , bool? habilitado , int? tipoAsociado);
        IEnumerable<Cabania> cabFind(int? idTipo, double? monto);
    }
}
