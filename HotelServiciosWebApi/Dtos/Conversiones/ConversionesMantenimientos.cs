using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.ValueObjects;
using HotelLogicaNegocio.ValueObjects;
using HotelServiciosWebApi.Dtos.CabaniasDtos;
using HotelServiciosWebApi.Dtos.MantenimientoDto;

namespace HotelServiciosWebApi.Dtos.Conversiones
{
    public class ConversionesMantenimientos
    {

        internal static MantenDto ConvertMTodto(Mantenimiento m)
        {
            return new MantenDto()
            {
                Fecha = m.Fecha,
                Descripcion = m.Descripcion,
                Costo = m.Costo,
                ResponsableInput = m.Responsable.NombreCompleto,
                CabaniaAsociada = m.CabaniaAsociada
            };
        }
        internal static Mantenimiento ConvertmtTomod(MantenDto dt)
        {
            return new Mantenimiento()
            {
                Fecha = dt.Fecha,
                Descripcion = dt.Descripcion,
                Costo = dt.Costo,
                Responsable = new NameManten(dt.ResponsableInput),
                CabaniaAsociada = dt.CabaniaAsociada
            };
        }
        internal static IEnumerable<MantenDto> ConvertAllToDto(IEnumerable<Mantenimiento> ms)
        {
            var list = ms
                .Select(c => ConvertMTodto(c)).ToList();
            return list;
        }
    }
}
