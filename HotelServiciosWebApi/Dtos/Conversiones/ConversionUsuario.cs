using Hotel.LogicaNegocio.Entidades;
using HotelLogicaNegocio.ValueObjects;
using HotelServiciosWebApi.Dtos.UsuarioDtos;


namespace HotelServiciosWebApi.Dtos.Conversiones
{

    public class ConversionUsuario
    {
        internal static UsuarioDto ConverDtoToUsu(Usuario usu)
        {
            return new UsuarioDto()
            {
                Email = usu.Email,
                PassInput = usu.Password.Pass
            };
        }
        internal static Usuario ConvertDtoToTipo(UsuarioDto usu)
        {
            return new Usuario()
            {
                Email = usu.Email,
                Password = new Contra(usu.PassInput)
            };
        }

    }
}
