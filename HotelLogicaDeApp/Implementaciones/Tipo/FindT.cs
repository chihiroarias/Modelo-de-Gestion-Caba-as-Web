using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Interfaces.ITipo;
using HotelLogicaNegocio.DominioException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Implementaciones.Tipo
{
    public class FindT : IFindT
    {
        private IRepositorioTipo _repo;

        public FindT(IRepositorioTipo repo)
        {
            _repo = repo;
        }

        public bool EnUsoEnCabaña(Hotel.LogicaNegocio.Entidades.Tipo tipo)
        {
            try
            {
                return _repo.EnUsoEnCabaña(tipo);
            }catch (Exception ex)
            {
                throw new DominioException("No se pudo .-.", ex);
            }
        }

        public  IEnumerable<Hotel.LogicaNegocio.Entidades.Tipo> FindAll()
        {
            try
            {
                return _repo.FindAll();
            }catch(Exception ex)
            {
                throw new DominioException("No se pudo .-.", ex);
            }
        }

        public Hotel.LogicaNegocio.Entidades.Tipo FindById(int id)
        {
            try
            {
                return _repo.FindById(id);
            }
            catch(Exception ex)
            {
                throw new DominioException("Revisar datos", ex);
            }
        }

        public Hotel.LogicaNegocio.Entidades.Tipo FindByName(string name)
        {
            try
            {
                return _repo.FindByName(name);
            }catch(Exception ex)
            {
                throw new DominioException("Error con este nombre", ex);
            }
        }
    }
}
