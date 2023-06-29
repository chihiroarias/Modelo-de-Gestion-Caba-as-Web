using System.ComponentModel.DataAnnotations;

namespace HotelServiciosWebApi.Dtos.CabaniasDtos
{
    public class CabaniaDto
    {
        public int CabaniaId { get; set; }//lo agregue al final no pude probar
        public int TipoAsociado { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "The field must be between 10 and 50 characters.")]
        public string NameInput { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(200, MinimumLength = 20)]
        public string Descripcion { get; set; }

        public bool Jacuzzi { get; set; }

        public bool Habilitada { get; set; }
        [Required]
        [Range(1, 50)]
        public int? MaxPersonas { get; set; }

        public string ImgNombreInput { get; set; }
    }
}
