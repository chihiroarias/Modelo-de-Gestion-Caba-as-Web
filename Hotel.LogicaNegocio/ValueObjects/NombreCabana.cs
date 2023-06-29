using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hotel.LogicaNegocio.ValueObjects
{
    [Owned]
    public class NombreCabana : IEquatable<NombreCabana>
    {
        /// <summary>
        /// Nombre de Cabaña
        /// </summary>
        public string Name { get; private set; }
        public NombreCabana(string name)
        {
            Validar(name);
            Name = name;
        }
        protected NombreCabana() { }
        public bool ValidarSoloLetras(string texto)
        {
            // Recorrer cada caracter del texto
            for (int i = 0; i < texto.Length; i++)
            {
                // Comprobar si el caracter no es una letra
                if (!Char.IsLetter(texto[i]))
                {
                    return false;
                }
            }

            // Si todos los caracteres son letras, devolver true
            return true;
        }
        private  void Validar(string nom)
        {
            if (string.IsNullOrEmpty(nom) || nom.Length > 30 || nom.Length < 5)
                throw new Exception("No se puede realizar la operación");
            if (!ValidarSoloLetras(nom))
                throw new InvalidOperationException("El nombre solo puede tener letras");
            if (string.IsNullOrEmpty(nom.Trim()))
                throw new InvalidOperationException("El nombre no puede estar vacío");
            if (nom.Trim() != nom)
                throw new InvalidOperationException("El nombre no puede llevar espacios embebidos al principio o final");
        }
     
        #region Igualdad del VO
        /// <summary>
        /// La igualdad de los value object es si su estado es igual (es decir, todos sus atributos)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Equals(NombreCabana? other)
        {
            if (other.Equals(null))
                throw new NotImplementedException();
            return Name.Equals(other.Name);
        }
        public override bool Equals(object obj)
        {
            var other = obj as NombreCabana;
            if (other.Equals(null))
                throw new NotImplementedException();
            return Name.Equals(other.Name);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name);  //genera una variable numérica del lugar en memoria que tiende a ser única
        }
        public override string ToString()
        {
            return $"{Name}";
        }

     
        #endregion
    }
}
