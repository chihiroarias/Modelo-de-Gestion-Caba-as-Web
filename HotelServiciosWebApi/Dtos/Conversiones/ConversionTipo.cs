using Hotel.LogicaNegocio.Entidades;
using HotelLogicaNegocio.ValueObjects;
using HotelServiciosWebApi.Dtos.TiposDtos;

namespace HotelServiciosWebApi.Dtos.Conversiones
{
    public class ConversionTipo
    {
        internal static Tipo ConvertDtoToTipo(TipoDto tm)
        {
            return new Tipo()
            {
                CostoxHuesped = tm.CostoxHuesped,
                Nombre = new NameTipo(tm.NombreInput),
                Descripcion = tm.Descripcion,
                TipoId = tm.TipoId
            };
        }
        internal static TipoDto ConvertTipoToDto(Tipo tp)
        {
            return new TipoDto()
            {
                CostoxHuesped = tp.CostoxHuesped,
                NombreInput = tp.Nombre.NameT,
                Descripcion = tp.Descripcion,
                TipoId = tp.TipoId
            };
        }
        public static IEnumerable<TipoDto> FindAllTipoToDto(IEnumerable<Tipo> Tipo)
        {
            var list = Tipo
                .Select(c => ConvertTipoToDto(c)).ToList();
            return list;
        }

    }
}
