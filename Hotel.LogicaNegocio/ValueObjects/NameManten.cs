using Hotel.LogicaNegocio.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace HotelLogicaNegocio.ValueObjects
{
    [Owned]
    public class NameManten : IEquatable<NameManten>
    {
        public string NombreM { get; private set; }
        public NameManten(string nombreM)
        {
            ValidarResponsable(nombreM);
            NombreM = nombreM;
        }
        protected NameManten() { }


        private void ValidarResponsable(string nombre)
        {
            if (string.IsNullOrEmpty(nombre.Trim()))
                throw new InvalidOperationException("El nombre no puede estar vacío");
            if (!ValidarSoloLetras(nombre))
                throw new InvalidOperationException("El nombre solo puede tener letras");
            if (nombre.Trim() != nombre)
                throw new InvalidOperationException("El nombre no puede llevar espacios embebidos al principio o final");
        }
        private bool ValidarSoloLetras(string texto)
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


        #region Igualdad del VO
        /// <summary>
        /// La igualdad de los value object es si su estado es igual (es decir, todos sus atributos)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Equals(NameManten? other)
        {
            if (other.Equals(null))
                throw new NotImplementedException();
            return NombreM.Equals(other.NombreM);
        }
        public override bool Equals(object obj)
        {
            var other = obj as NameManten;
            if (other.Equals(null))
                return NombreM.Equals(other.NombreM); 
            throw new NotImplementedException();
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(NombreM);  //genera una variable numérica del lugar en memoria que tiende a ser única
        }
        public override string ToString()
        {
            return NombreCompleto;
        }
        public string NombreCompleto
        {
            get
            {
                return $"{NombreM}";
            }
        }


        #endregion
    }
}
