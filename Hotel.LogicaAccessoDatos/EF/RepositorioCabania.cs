using Hotel.LogicaAccessoDatos.EF.Hotel.LogicaAccessoDatos.EF;
using Hotel.LogicaAccessoDatos.EF.Hotel.LogicaAccessoDatos.EF;
using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.InterfacesRepositorio;
using Hotel.LogicaNegocio.ValueObjects;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelLogicaNegocio.DominioException;

namespace Hotel.LogicaAccessoDatos.EF
{
    public class RepositorioCabania : IRepositorioCabania
    {
        private ObligatorioContext _db;
        public RepositorioCabania(ObligatorioContext db)
        {
            _db = db;
        }

        public void Add(Cabania cab)
        {
            bool existe = _db.Cabanias.Any(e => e.Nombre.Name== cab.Nombre.Name);
            if (cab == null || existe)
            {
                throw new ArgumentNullException("Error: La cabaña no puede ser nula o ya existe");
            }
          
            //algo para validar nombre de foto
            try
            {
                cab.Validar();
                _db.Cabanias.Add(cab);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo dar de alta la cabaña. {ex.Message}");
            }
        }

        public IEnumerable<Cabania> Buscar(string? nombre, int? maxPersonas, bool? habilitado, int? tipoAsociado)
        {
            // La manera de filtrar es que devuelve 1 de las cosas que se pone en los filtros
            IEnumerable<Cabania> filtrado = Enumerable.Empty<Cabania>();
            if (nombre != null || maxPersonas != 0 || habilitado == true || tipoAsociado != 9999999){
                if(nombre != null)
                    {
                    var nomfiltrados = _db.Cabanias.Where(c=>c.Nombre.Name.Equals(nombre));
                    if (nomfiltrados.Any())
                    {
                        filtrado = filtrado.Union(nomfiltrados);
                    }
                    }
                if(maxPersonas != 0)
                {
                    var maxfiltrados = _db.Cabanias.Where(c => c.MaxPersonas >= maxPersonas)
                                                    .Include(c => c.MiTipo)
                                                    .ToList(); ;
                    if (maxfiltrados.Any())
                    {
                        filtrado = filtrado.Union(maxfiltrados);
                    }

                }
                if (habilitado == true)
                {
                    var habilitadosfiltrados = _db.Cabanias.Where(c => c.Habilitada == habilitado)
                                                    .Include(c => c.MiTipo)
                                                    .ToList(); ;
                    if (habilitadosfiltrados.Any())
                    {
                        filtrado = filtrado.Union(habilitadosfiltrados);
                    }
                }if(tipoAsociado != 9999999)
                {
                    var tipofiltrados = _db.Cabanias.Where(c => c.TipoAsociado == tipoAsociado)
                                                    .Include(c => c.MiTipo)
                                                    .ToList(); ;
                    if (tipofiltrados.Any())
                    {
                        filtrado = filtrado.Union(tipofiltrados);
                    }
                }
                return filtrado.Distinct();
            
            }
            else
            {
                 var todos = FindAll();
                return todos ;
            }
                
        }

        public string CrearNomImagen(Cabania c)
        {
            if(c == null || c.Nombre == null || string.IsNullOrEmpty(c.Nombre.Name))
            {
                throw new DominioException("La cabania debe tener un nombre");
            }
    
            string name = c.Nombre.Name.Replace(" ", "_");
            int secuenciador = 1;
            string nomArchivo = $"{name}_{secuenciador.ToString("D3")}";
            return nomArchivo;
        }


        public IEnumerable<Cabania> FindAll()
        {
            var cabanias = _db.Cabanias
                .Include(c => c.MiTipo)
                .Include(c => c.Foto)
                .ToList();

            return cabanias  ;

        }

        public Cabania FindById(int id)
        {
            try
            {
                return _db.Cabanias.FirstOrDefault(m => m.CabaniaId == id);
            }
            catch (Exception ex)
            {
                throw new DominioException("No se pudo eliminar ", ex);
            }
        }

        public void Remove(Cabania obj)
        {
            try
            {
                
                _db.Cabanias.Remove(obj);
            }
            catch (Exception ex)
            {
                throw new DominioException("No se pudo eliminar ", ex);
            }
        }

        public void Update(Cabania obj)
        {
            try
            {
                obj.Validar();
                var objetoOriginal = FindById(obj.CabaniaId);
                if (objetoOriginal != null)
                {
                    objetoOriginal.Jacuzzi = obj.Jacuzzi;
                    objetoOriginal.TipoAsociado=obj.TipoAsociado;
                    objetoOriginal.Descripcion = obj.Descripcion;
                    objetoOriginal.MiTipo=obj.MiTipo;
                    objetoOriginal.Foto=obj.Foto;
                    objetoOriginal.MaxPersonas=obj.MaxPersonas;
                    objetoOriginal.Nombre=obj.Nombre;
                    objetoOriginal.Habilitada=obj.Habilitada;
                    _db.Entry(objetoOriginal).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new DominioException("No se pudo editar", ex);
            }
        }
        public IEnumerable<Cabania> cabFind(int? idTipo, double? monto)
        {
            try
            {
                IQueryable<Cabania> cabs = _db.Cabanias.AsQueryable();
                if (cabs.Count() == 0)
                {
                    throw new DominioException("Lista Vqacía");
                }
                else if (idTipo != null)
                {
                    cabs = cabs.Where(c => c.TipoAsociado == idTipo);
                }
                else if (monto != null && monto != 0)
                {
                    cabs = cabs.Where(c => c.MiTipo.CostoxHuesped <= monto && c.Habilitada == true && c.Jacuzzi == true);
                }
                return cabs;

            }
            catch (Exception ex)
            {
                throw new DominioException("Error", ex);
            }

        }
    }
}
