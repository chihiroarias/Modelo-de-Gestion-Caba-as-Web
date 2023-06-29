using Hotel.LogicaAccessoDatos.EF.Hotel.LogicaAccessoDatos.EF;
using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaNegocio.DominioException;
using HotelLogicaNegocio.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAccessoDatos.EF
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private ObligatorioContext _db;
        public RepositorioUsuario(ObligatorioContext db)
        {
            _db = db;
        }
        public void PrecargaUsuariO(Usuario usr)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> FindAll()
        {
            throw new NotImplementedException();
        }

        public Usuario FindByEmail(string email)
        {
            try
            {
                var busc = _db.Usuarios.FirstOrDefault(u => u.Email == email);
                return busc;
            }
            catch (Exception ex)
            {
                throw new DominioException("No se encontró ", ex);
            }
        }
        public void Add(Usuario obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Error: El usuario no puede ser nulo");
            obj.Validar();
            try
            {
                _db.Usuarios.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo dar de alta el Usuario. {ex.Message}");
            }
        }


        public void Remove(Usuario obj)
        {
            try
            {
                _db.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DominioException("No se pudo realizar la operación ", ex);
            }
        }

        public void Update(Usuario obj)
        {
            try
            {
                Usuario orig = _db.Usuarios.FirstOrDefault(u => u.Email.Equals(obj.Email));
                orig.Password = obj.Password;
                _db.Entry(orig).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DominioException("Revisar datos ", ex);
            }
        }



        public bool CheckEsUsuario(string email, Contra password)
        {
            var buscado = _db.Usuarios.FirstOrDefault(u => u.Email == email && u.Password.Pass == password.Pass);
            if (buscado == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Usuario FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
