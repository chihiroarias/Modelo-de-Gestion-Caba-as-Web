using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaNegocio.ValueObjects
{
    [Owned]

    public class Imagen : IEquatable<Imagen>
    {
        public string? Nombre { get; set; }



        public Imagen(string nombre)
        {
            ValidarNombre(nombre);
            Nombre = nombre;
        }
        protected Imagen()
        {

        }   
        public bool Equals(Imagen? other)
        {
            if(other == null)
                throw new ArgumentException("El nombre de la img no se puede comparar con nulo"); 
            return Nombre.Equals(other.Nombre);
        }

        public void Validar()
        {
            ValidarNombre(Nombre);
        }
        private void ValidarNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre.Trim()))
                throw new InvalidOperationException("El nombre no puede estar vacío");
        }
    }

}
