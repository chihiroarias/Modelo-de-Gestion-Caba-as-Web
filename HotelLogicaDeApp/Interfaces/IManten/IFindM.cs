using Hotel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Interfaces.IManten
{
    public interface IFindM
    {
        public Mantenimiento FindById(int id);
        public IEnumerable<Mantenimiento> FindAll();
        public int CantMantenimientosCabaniaxFecha(int cabaniaAsociada, DateTime fecha);
        public IEnumerable<Mantenimiento> FindByIdCab(int cabId);
        public IEnumerable<Mantenimiento> FiltrarbyFechas(int cabId, DateTime fechaIn, DateTime fechaFin);
        public IEnumerable<Mantenimiento> FindByQPers(int q1, int q2);
    }
}
