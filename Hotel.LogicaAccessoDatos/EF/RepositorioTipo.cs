using Hotel.LogicaAccessoDatos.EF.Hotel.LogicaAccessoDatos.EF;
using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaNegocio.DominioException;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAccessoDatos.EF
{
    public class RepositorioTipo : IRepositorioTipo
    {
        private ObligatorioContext _db;
        public RepositorioTipo(ObligatorioContext db)
        {
            _db = db;
        }
        public void Add(Tipo tipo)
        {
            bool existe = _db.Tipos.Any(e => e.Nombre.NameT == tipo.Nombre.NameT);
            if (tipo == null || existe) throw new ArgumentNullException("Error: El tipo no puede ser nulo o ya existe");
            tipo.Validar();
            try
            {
                _db.Tipos.Add(tipo);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo dar de alta el Tipo. {ex.Message}");
            }
        }

        public bool EnUsoEnCabaña(Tipo tipo)
        {
            var esta = _db.Cabanias.FirstOrDefault((T => T.TipoAsociado == tipo.TipoId));
            if (esta == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<Tipo> FindAll()
        {
            try
            {
                var ms = _db.Tipos
                    .Include(m => m.Nombre).ToList();
                return ms;
            }
            catch (Exception ex)
            {
                throw new DominioException("No se encontró ", ex);
            }
           
        }

        public Tipo FindById(int id)
        {
            try
            {
                var pesq = _db.Tipos.FirstOrDefault((t => t.TipoId == id));
                return pesq;
            }
            catch (Exception ex)
            {
                throw new DominioException("No se encontró ", ex);
            }
        }

        public Tipo FindByName(string name)
        {
            
            try
            {
                var buscado = _db.Tipos.FirstOrDefault((Tipo => Tipo.Nombre.NameT == name));
                return buscado;
            }
            catch (Exception ex)
            {
                throw new DominioException("No se encontró ", ex);
            }
        }

        public void Remove(Tipo tipo)
        {
            bool existe = _db.Cabanias.Any(e => e.TipoAsociado.Equals(tipo.TipoId));
            if (tipo == null || existe) throw new ArgumentNullException("Error: El tipo no puede ser nulo");
         //   if(EnUsoEnCabaña(tipo))
            try
            {
                _db.Tipos.Remove(tipo);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo Eliminar el Tipo. {ex.Message}");
            }
        }

        public void Update(Tipo obj)
        {
            var original = FindByName(obj.Nombre.NameT);
            if (original == null) {
                throw new ArgumentNullException("Error: No existe ese tipo");
            }
            else
            {
                obj.Validar();
                try
                {
                    original.Descripcion = obj.Descripcion;
                    original.CostoxHuesped = obj.CostoxHuesped;
                    _db.Entry(original).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw new Exception($"No se pudo editar el Tipo. {ex.Message}");
                }
            }
        }


    }
}
