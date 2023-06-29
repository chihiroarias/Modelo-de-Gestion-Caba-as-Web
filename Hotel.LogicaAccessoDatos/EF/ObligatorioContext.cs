using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hotel.LogicaAccessoDatos.EF
{
    namespace Hotel.LogicaAccessoDatos.EF
    {
        public class ObligatorioContext : DbContext
        {
            public DbSet<Cabania> Cabanias { get; set; }

            public DbSet<Tipo> Tipos { get; set; }

            public DbSet<Mantenimiento> Mantenimientos { get; set; }
            public DbSet<Usuario> Usuarios { get; set; }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Cabania>(entity =>
                {
                    entity.OwnsOne(c => c.Nombre, nombre =>
                    {
                        nombre.Property(n => n.Name).HasColumnName("Nombre");
                    });
                });
                modelBuilder.Entity<Tipo>(entity =>
                {
                    entity.OwnsOne(c => c.Nombre, nombre =>
                    {
                        nombre.Property(n => n.NameT).HasColumnName("Nombre");
                        nombre.HasIndex(n => n.NameT).IsUnique();
                    });
                });
                modelBuilder.Entity<Usuario>(entity =>
                {
                    entity.OwnsOne(c => c.Password, pass =>
                    {
                        pass.Property(n => n.Pass).HasColumnName("Nombre");
                    });
                });
                modelBuilder.Entity<Mantenimiento>(entity =>
                {
                    entity.OwnsOne(c => c.Responsable, respon =>
                    {
                        respon.Property(n => n.NombreM).HasColumnName("Nombre");
                    });
                });
            }

            //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            //{
            //    string cadenaConexion =//VERIFICAR LA CADENA DE CONEXION, AUN NO CREE DATABASE
            //        @"SERVER=(localdb)\MSsqlLocaldb;
            //    DATABASE=ObligatorioP3PrimerParte;
            //    INTEGRATED SECURITY=TRUE;
            //    ENCRYPT=False";
            //    optionsBuilder.UseSqlServer(cadenaConexion)
            //        .EnableDetailedErrors();
            //} YA NO SE UTILIZARÁ

            //Por tanto agregamos un constructor que nos permita
            //utilizar la cadena de conexión en appsettings
            public ObligatorioContext(DbContextOptions options) : base(options)
            {

            }


        }
    }

}
