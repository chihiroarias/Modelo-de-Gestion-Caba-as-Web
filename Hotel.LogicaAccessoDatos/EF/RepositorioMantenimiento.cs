using Hotel.LogicaAccessoDatos.EF.Hotel.LogicaAccessoDatos.EF;
using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaNegocio.DominioException;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAccessoDatos.EF
{
    public class RepositorioMantenimiento :IRepositorioMantenimiento
    {
        private ObligatorioContext _db;
        public RepositorioMantenimiento(ObligatorioContext db)
        {
            _db = db;
        }

        public void Add(Mantenimiento man)
        {
            if(man == null)
            {
                throw new ArgumentNullException("Error: Mantenimiento no puede ser nulo");
            }
            if (CantMantenimientosCabaniaxFecha(man.CabaniaAsociada, man.Fecha) < 3)
            {
                man.Validar();
                try
                {
                    _db.Mantenimientos.Add(man);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"No se pudo dar de alta la cabaña. {ex.Message}");
                }
            }
            else
            {
                throw new Exception("solo puede haber 3 mantenimientos x cabaña x dia");
            }
          
        }

        public int CantMantenimientosCabaniaxFecha(int cabaniaAsociada, DateTime fecha)
        {
            try
            {
                var buscado = _db.Mantenimientos.Where(m => m.CabaniaAsociada == cabaniaAsociada && m.Fecha == fecha);
                var cant = buscado.Count();
                return cant;
            }
            catch (Exception ex)
            {
                throw new DominioException("No fue posible", ex);
            }
        }

        public IEnumerable<Mantenimiento> FiltrarbyFechas(int cabId, DateTime fechaIn, DateTime fechaFin)
        {
            try
            {
                var mantcabania = FindByIdCab(cabId);
                var filtrado = mantcabania.Where(m =>m.Fecha >= fechaIn && m.Fecha <= fechaFin)
                                          .OrderByDescending(m => m.Costo)
                                          .ToList();
                return filtrado;
            }
            catch (Exception ex)
            {
                throw new DominioException("No fue posible", ex);
            }
        }

        public IEnumerable<Mantenimiento> FindAll()
        {
            try
            {
                var ms = _db.Mantenimientos
                    .Include(m => m.Responsable).ToList();
                return ms;
                
            }
            catch (Exception ex)
            {
                throw new DominioException("No fue posible", ex);
            }
        }

        public Mantenimiento FindById(int id)
        {
            try
            {
                return _db.Mantenimientos.FirstOrDefault(m => m.MantenimientoId == id);
            }catch(Exception ex)
            {
                throw new DominioException("No se pudo realizar la operación ", ex);
            }
        }

        public IEnumerable<Mantenimiento> FindByIdCab(int cabId)
        {
            try
            {
                var buscado = _db.Mantenimientos.Where(m =>m.CabaniaAsociada == cabId)
                                                .OrderByDescending(m=> m.Costo)
                                                .ToList();
                return buscado;
            }
            catch (Exception ex)
            {
                throw new DominioException("No fue posible", ex);
            }
        }

        public void Remove(Mantenimiento obj)
        {
            try
            {
                _db.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DominioException("No fue posible", ex);
            }
        }

        public void Update(Mantenimiento obj)
        {
            try
            {
                obj.Validar();
                var original = FindById(obj.MantenimientoId);
                if (original != null)
                {
                    original.Descripcion= obj.Descripcion;
                    original.MiCabania=obj.MiCabania;
                    original.Costo=obj.Costo;
                    original.CabaniaAsociada=obj.CabaniaAsociada;
                    original.Fecha=obj.Fecha;
                    original.Responsable=obj.Responsable;
                    _db.Entry(original).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DominioException("No fue posible", ex);
            }
        }
        public IEnumerable<Mantenimiento> FindByQPers(int q1, int q2)
        {
            try
            {
                IQueryable<Mantenimiento> ms = _db.Mantenimientos
                .Include(m => m.Responsable) // Incluir la entidad propietaria
                .Where(m => m.MiCabania.MaxPersonas >= q1 && m.MiCabania.MaxPersonas <= q2)
                .GroupBy(m => m.Responsable.NombreM)
                .AsNoTracking() // Deshabilitar el rastreo
                .Select(g => new Mantenimiento
                {
                    Responsable = g.First().Responsable,
                    Costo = g.Sum(m => m.Costo)
                });

                List<Mantenimiento> mants = ms.ToList();

                return mants;

                //IQueryable<Mantenimiento> ms = (IQueryable<Mantenimiento>)_db.Mantenimientos
                //    .Where(m => m.MiCabania.MaxPersonas >= q1 && m.MiCabania.MaxPersonas <= q2)
                //    .GroupBy(m => m.Responsable.NombreM)
                //    .Select(g => new Mantenimiento
                //    {
                //        Responsable = g.First().Responsable,
                //        Costo = g.Sum(m => m.Costo)
                //    }).Include(m => m.Responsable).ToList();
                //return ms;
            }
            catch (Exception ex)
            {
                throw new DominioException("No fue posible", ex);
            }
        }
    }
}
