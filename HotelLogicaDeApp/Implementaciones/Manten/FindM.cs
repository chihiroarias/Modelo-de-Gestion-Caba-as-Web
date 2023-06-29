using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Interfaces.IManten;
using HotelLogicaNegocio.DominioException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Implementaciones.Manten
{
    public class FindM: IFindM
    {
        private IRepositorioMantenimiento _repo;

        public FindM(IRepositorioMantenimiento repo)
        {
            _repo = repo;
        }

        public int CantMantenimientosCabaniaxFecha(int cabaniaAsociada, DateTime fecha)
        {
            try
            {
                return _repo.CantMantenimientosCabaniaxFecha(cabaniaAsociada, fecha);
            }
            catch (Exception ex)
            {
                throw new DominioException("Revisar datos", ex);
            }
        }

        public IEnumerable<Mantenimiento> FiltrarbyFechas(int cabId, DateTime fechaIn, DateTime fechaFin)
        {
            try
            {
                return _repo.FiltrarbyFechas(cabId, fechaIn, fechaFin);
            }
            catch (Exception ex)
            {
                throw new DominioException("Revisar datos", ex);
            }
        }

        public IEnumerable<Mantenimiento> FindAll()
        {
            try
            {
                return _repo.FindAll();
            }
            catch (Exception ex)
            {
                throw new DominioException("No se pudo .-.", ex);
            }
        }

        public Mantenimiento FindById(int id)
        {
            try
            {
                return _repo.FindById(id);
            }
            catch (Exception ex)
            {
                throw new DominioException("Revisar datos", ex);
            }
        }

        public IEnumerable<Mantenimiento> FindByIdCab(int cabId)
        {
            try
            {
                return _repo.FindByIdCab(cabId);
            }
            catch (Exception ex)
            {
                throw new DominioException("Revisar datos", ex);
            }
        }
        public IEnumerable<Mantenimiento> FindByQPers(int q1, int q2)
        {
            try
            {
                return _repo.FindByQPers(q1, q2);
            }
            catch (Exception ex)
            {
                throw new DominioException("Revisar datos", ex);
            }
        }
    }
}
