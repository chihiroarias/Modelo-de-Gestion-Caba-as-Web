using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLogicaNegocio.ValueObjects
{
    [Owned]
    public class NameTipo: IEquatable<NameTipo>
    {
        public string NameT { get; private set; }
        public NameTipo(string name) 
        {
            Validar(name);
            NameT= name;
        }
        protected NameTipo() { }
        private void Validar(string nombre)
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
        public bool Equals(NameTipo? other)
        {
            if (other == null)
                return false;
            return NameT.Equals(other.NameT);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(NameTipo))
                return false;
            return Equals((NameTipo)obj);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(NameT);  //genera una variable numérica del lugar en memoria que tiende a ser única
        }
        public override string ToString()
        {
            return $"{NameT}";
        }


        #endregion
    }
}
