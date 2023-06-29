using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Interfaces.ITipo;
using HotelLogicaNegocio.DominioException;

namespace HotelLogicaDeApp.Implementaciones.Tipo
{
    public class RemoveT : IRemoveT
    {
        private IRepositorioTipo _repo;

        public RemoveT(IRepositorioTipo repo)
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
                throw new DominioException("Revisar datos", ex);
            }
        }

        public void Remove(Hotel.LogicaNegocio.Entidades.Tipo tipo)
        {
            try
            {
                _repo.Remove(tipo);
            }
            catch(Exception ex)
            {
                throw new DominioException("Revisar datos", ex);
            }
        }
    }
}
