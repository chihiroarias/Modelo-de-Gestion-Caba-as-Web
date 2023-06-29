using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.ValueObjects;
using HotelLogicaNegocio.ValueObjects;
using HotelServiciosWebApi.Dtos.CabaniasDtos;

namespace HotelServiciosWebApi.Dtos.Conversiones

{
    public class ConversionesCabania
    {
        internal static CabaniaDto ConvertCabToDto(Cabania cm)
        {
            return new CabaniaDto()
            {
                CabaniaId = cm.CabaniaId,
                TipoAsociado = cm.TipoAsociado,
                NameInput = cm.Nombre.Name,
                Descripcion = cm.Descripcion,
                Jacuzzi = cm.Jacuzzi,
                Habilitada = cm.Habilitada,
                MaxPersonas = cm.MaxPersonas,
                ImgNombreInput = cm.Foto.Nombre
            };
        }
        internal static Cabania ConvertDtoToCab(CabaniaDto cm)
        {
            return new Cabania()
            {
                CabaniaId = cm.CabaniaId,
                TipoAsociado = cm.TipoAsociado,
                Nombre = new NombreCabana(cm.NameInput),
                Descripcion = cm.Descripcion,
                Jacuzzi = cm.Jacuzzi,
                Habilitada = cm.Habilitada,
                MaxPersonas = cm.MaxPersonas,
                Foto = new Imagen(cm.ImgNombreInput)

            };
        }

        internal static IEnumerable<CabaniaDto> FindAllCabToDto(IEnumerable<Cabania> Cab)
        {
            var list = Cab
                .Select(c => ConvertCabToDto(c)).ToList();
            return list;
        }
    }
}
