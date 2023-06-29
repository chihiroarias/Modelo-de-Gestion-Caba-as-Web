using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelLogicaNegocio.ValueObjects;

namespace Hotel.LogicaNegocio.Entidades
{
    [Table("Mantenimientos")]
    public class Mantenimiento

    {
        public int MantenimientoId { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }

        public double Costo { get; set; }
        public NameManten Responsable { get; set; }
        [ForeignKey("MiCabania")] public int CabaniaAsociada { get; set; }
        public Cabania MiCabania { get; set; }
        
        public Mantenimiento() { }

    public void Validar()
    {
        ValidarDescripcion(this.Descripcion);
        ValidarCosto(this.Costo);
        
    }
    private void ValidarDescripcion(string descripcion)
    {
        if (descripcion.Length < 10 || descripcion.Length > 200)
            throw new InvalidOperationException("La descripcion debe tener entre 10 y 200 caracteres");
    }
    private void ValidarCosto(double costo)
        {
            if (costo < 1)
            {
                throw new InvalidOperationException("El costo debe ser mayor a 0");
            }
        }

    }
}
