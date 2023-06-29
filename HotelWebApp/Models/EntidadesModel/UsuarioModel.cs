using HotelLogicaNegocio.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelMVC.Models.EntidadesModel
{
    [NotMapped]
    public class UsuarioModel
    {
        public string Email { get; set; }
        public string PassInput { get; set; }
      

    }
}
