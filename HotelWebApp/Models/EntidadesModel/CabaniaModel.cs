using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.ValueObjects;
using HotelLogicaNegocio.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelMVC.Models.EntidadesModel
{
    [NotMapped]
    public class CabaniaModel
    {
        public int CabaniaId { get; set; }

        [Required(ErrorMessage = "This field is required.")]
    
        public int TipoAsociado { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, MinimumLength = 10)]
        public string NameInput { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(200, MinimumLength = 20)]
        public string Descripcion { get; set; }

        public bool Jacuzzi { get; set; }

        public bool Habilitada { get; set; }
        [Range(1, 50)]
        [Required]
        public int? MaxPersonas { get; set; }

        public string ImgNombreInput { get; set; }
    }
}
