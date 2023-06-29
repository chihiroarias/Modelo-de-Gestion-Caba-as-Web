using HotelLogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaNegocio.Entidades
{
    public class Usuario
    {
        [Key] public string Email { get; set; }

        public Contra Password { get; set; }

        public Usuario(string email, Contra pass)
        {
            Email = email;
            Validar();
            Password = pass;
        }
        public Usuario() { }
        public void Validar()
        {
            ValidarEmail(Email);
        }
        public void ValidarEmail(string mail)
        {
             if (string.IsNullOrEmpty(mail))
            {
                throw new InvalidOperationException("El mail no puede ser null");
            }
            bool tieneArroba = mail.Contains("@");
            if (!tieneArroba)
            {
                throw new InvalidOperationException("El mail tiene que tener @");
            }
        }
       
       
   

    }
}
