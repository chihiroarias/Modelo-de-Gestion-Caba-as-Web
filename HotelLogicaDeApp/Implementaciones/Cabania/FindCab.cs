using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Interfaces;
using HotelLogicaNegocio.DominioException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaDeApp.Implementaciones.Cabania
{
    public class FindCab : IFindC
    {
        private IRepositorioCabania _repoC;
        private IRepositorioTipo _repoT;
        public FindCab(IRepositorioCabania repo, IRepositorioTipo repot)
        {
            _repoC = repo;
            _repoT = repot;
        }

        public IEnumerable<Hotel.LogicaNegocio.Entidades.Cabania> GetAll()
        {
            try
            {
                return _repoC.FindAll();
            }
            catch (Exception ex)
            {
                throw new DominioException("No se pudo realizar la operacion", ex);
            }
        }
        public Hotel.LogicaNegocio.Entidades.Cabania FindById(int id)
        {
            try
            {
                return _repoC.FindById(id);
            }catch(Exception ex)
            {
                throw new DominioException("No se pudo realizar la operacion", ex);
            }
        }

        public IEnumerable<Hotel.LogicaNegocio.Entidades.Tipo> GetTyps()
        {
            try
            {
                return _repoT.FindAll();
            }
            catch(Exception ex)
            {
                throw new DominioException("No se pudo realizar la operacion", ex);

            }
        }

        IEnumerable<Hotel.LogicaNegocio.Entidades.Tipo> IFindC.GetTyps()
        {
            try
            {
                return _repoT.FindAll();
            }
            catch (Exception ex)
            {
                throw new DominioException("No se encontro nada ", ex);
            }
        }

        public IEnumerable<Hotel.LogicaNegocio.Entidades.Cabania> cabFind(int? idTipo, double? monto)
        {
            try
            {
                return _repoC.cabFind(idTipo, monto);

            }
            catch (Exception ex)
            {
                throw new DominioException(ex.Message);

            }
        }
    }
}
