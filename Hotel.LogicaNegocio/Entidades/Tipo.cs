using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelLogicaNegocio.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace Hotel.LogicaNegocio.Entidades
{
    [Table("Tipo")]
    public class Tipo
    {
        public int TipoId { get; set; }
        public NameTipo Nombre { get; set; }
        public string Descripcion { get; set; }

        public double CostoxHuesped { get; set; }

        public void Validar()
        {
            ValidarDescripcion(this.Descripcion);
            ValidarCostoxHuesped(this.CostoxHuesped);
        }

        private void ValidarCostoxHuesped(double costoxHuesped)
        {
            if (costoxHuesped < 1)
            {
                throw new InvalidOperationException("El costo no puede ser negativo");
            };
        }
        private void ValidarDescripcion(string descripcion)
        {
            if (descripcion.Length < 10 || descripcion.Length > 200)
                throw new InvalidOperationException("La descripcion debe tener entre 10 y 200 caracteres");
        }
        public bool Equals([AllowNull] Tipo other)
        {
            if (other == null)
                return false;
            return  other.Nombre.NameT.Equals(this.Nombre.NameT);
        }
    }

}
