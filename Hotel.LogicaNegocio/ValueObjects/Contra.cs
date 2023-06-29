using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HotelLogicaNegocio.ValueObjects
{
    [Owned]

    public class Contra : IEquatable<Contra>
    {
   
        public string Pass { get; private set; }

        public Contra(string pass)
        {
            ValidarPassword(pass);
            this.Pass = pass;
        }
        protected Contra() { }

        public void ValidarPassword(string contra)
        {
            if (contra.Length < 6)
            {
                throw new InvalidOperationException("La contraseña debe tener al menos 6 caracteres");
            }
            bool tieneMayusculas = contra.Any(c => char.IsUpper(c));
            if (!tieneMayusculas)
            {
                throw new InvalidOperationException("La contraseña debe tener al menos 1 Mayuscula");
            }
            bool tieneMinusculas = contra.Any(c => char.IsLower(c));
            if (!tieneMinusculas)
            {
                throw new InvalidOperationException("La contraseña debe tener al menos 1 Minuscula");
            }
            bool tieneNumeros = contra.Any(c => char.IsDigit(c));
            if (!tieneNumeros)
            {
                throw new InvalidOperationException("La contraseña debe tener al menos 1 Mayuscula");
            }
        }
        #region Igualdad del VO
        /// <summary>
        /// La igualdad de los value object es si su estado es igual (es decir, todos sus atributos)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Equals(Contra? other)
        {
            if (other.Equals(null))
                throw new NotImplementedException();
            return Pass.Equals(other.Pass);
        }
        public override bool Equals(object obj)
        {
            var other = obj as Contra;
            if (other.Equals(null))
                return Pass.Equals(other.Pass);
            throw new NotImplementedException();
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Pass);  //genera una variable numérica del lugar en memoria que tiende a ser única
        }
        public override string ToString()
        {
            return $"{Pass}";
        }


        #endregion
    }
}
