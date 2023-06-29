using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelServiciosWebApi.Dtos.TiposDtos
{
    public class TipoDto
    {

            public int TipoId { get; set; }
            [Required(ErrorMessage = "This field is required.")]
            [StringLength(50, MinimumLength = 10, ErrorMessage = "The field must be between 10 and 50 characters.")]
            public string NombreInput { get; set; }

            [Required(ErrorMessage = "This field is required.")]
            [StringLength(200, MinimumLength = 20)] 
            public string Descripcion { get; set; }
            [Range(0, 9999)]
            public double CostoxHuesped { get; set; }
        
    }

}
