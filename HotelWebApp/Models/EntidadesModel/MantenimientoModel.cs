using Hotel.LogicaNegocio.Entidades;
using HotelLogicaNegocio.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelMVC.Models.EntidadesModel
{
    [NotMapped]
    public class MantenimientoModel
    {
        public int MantenimientoId { get; set; }
        public DateTime Fecha { get; set; }


        [Required(ErrorMessage = "This field is required.")]
        [StringLength(200, MinimumLength = 20)]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "This field is required.")]
        [Range(1,10000)]
        public double Costo { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, MinimumLength = 10)]
        public string ResponsableInput { get; set; }
        public int CabaniaAsociada { get; set; }
      
    }
}
