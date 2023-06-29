using Hotel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioMantenimiento:IRepositorio<Mantenimiento>
    {
        int CantMantenimientosCabaniaxFecha(int cabaniaAsociada, DateTime fecha);
        IEnumerable<Mantenimiento> FindByIdCab(int cabId);

        IEnumerable<Mantenimiento> FiltrarbyFechas(int cabId, DateTime fechaIn, DateTime fechaFin);
        IEnumerable<Mantenimiento> FindByQPers(int q1, int q2);
    }
}
