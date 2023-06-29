using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Hotel.LogicaNegocio.ValueObjects;
using HotelLogicaNegocio.ValueObjects;

namespace Hotel.LogicaNegocio.Entidades
{
    [Table("Cabania")]
    public class Cabania
    {


        public int CabaniaId { get; set; }
        [ForeignKey("MiTipo")] public int TipoAsociado { get; set; }
        public Tipo MiTipo { get; set; }
        public NombreCabana? Nombre { get; set; }
        public string Descripcion { get; set; }

        public bool Jacuzzi { get; set; }

        public bool Habilitada { get; set; }

        public int? MaxPersonas { get; set; }

        public Imagen Foto { get; set; }



        public void Validar()
        {
            ValidarDescripcion(this.Descripcion);
        }
     
     
        private void ValidarDescripcion(string descripcion)
        {
            if (descripcion.Length < 10 || descripcion.Length > 500)
                throw new InvalidOperationException("La descripcion debe tener entre 10 y 200 caracteres");
        }
        private void ValidarMaxPersonas(int num)
        {
            if(num < 1)
            {
                throw new InvalidOperationException("El maximo de personas debe ser mayor a 1");
            }
        }
    }
   
}
