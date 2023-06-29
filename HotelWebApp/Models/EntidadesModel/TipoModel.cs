using HotelLogicaNegocio.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelMVC.Models.EntidadesModel
{
    [NotMapped]
    public class TipoModel
    {
        [Display (Name= "Id")]
        public int TipoId { get; set; }

        [Display(Name = "Nombre")]

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, MinimumLength = 10)]
        public string NombreInput { get; set; }
        [Display(Name = "Descripcion")]

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(200, MinimumLength = 20)]
        public string Descripcion { get; set; }
        [Display(Name = "Coso por cada huesped")]

        [Required(ErrorMessage = "This field is required.")]
        [Range(0,9999)]
        public double CostoxHuesped { get; set; }
    }
}
