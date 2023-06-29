using Hotel.LogicaAccessoDatos.EF;
using Hotel.LogicaAccessoDatos.EF.Hotel.LogicaAccessoDatos.EF;
using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Implementaciones.Cabania;
using HotelLogicaDeApp.Implementaciones.Manten;
using HotelLogicaDeApp.Implementaciones.Tipo;
using HotelLogicaDeApp.Implementaciones.Usuario;
using HotelLogicaDeApp.Interfaces;
using HotelLogicaDeApp.Interfaces.IManten;
using HotelLogicaDeApp.Interfaces.ITipo;
using HotelLogicaDeApp.Interfaces.IUsuario;
using Microsoft.EntityFrameworkCore;

namespace HotelServiciosWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt => opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "HotelServiciosWebApi.xml")));

            builder.Services.AddDbContext<ObligatorioContext>
                (opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("OblP3")));
            //le hacemos la llamada al OblContext y le pasamos lo que deberá utilizar con una expresión lambda    


            builder.Services.AddScoped<IRepositorioCabania, RepositorioCabania>();
            builder.Services.AddScoped<IRepositorioMantenimiento, RepositorioMantenimiento>();
            builder.Services.AddScoped<IRepositorioTipo, RepositorioTipo>();
            builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
            builder.Services.AddScoped<ICreateCab, CreateCab>();
            builder.Services.AddScoped<IBuscar, Buscar>();
            builder.Services.AddScoped<IFindC, FindCab>();
            builder.Services.AddScoped<IRemoveC, Remove>();
            builder.Services.AddScoped<IUpdateC, UpdateCab>();
            builder.Services.AddScoped<ICreateNameImg, CreateNomImg>();
            builder.Services.AddScoped<IAddM, AddM>();
            builder.Services.AddScoped<IUpdateM, UpdateM>();
            builder.Services.AddScoped<IFindM, FindM>();
            builder.Services.AddScoped<IRemoveM, RemoveM>();
            builder.Services.AddScoped<IRemoveU, RemoveU>();
            builder.Services.AddScoped<ICheckEsU, CheckEsU>();
            builder.Services.AddScoped<IAddU, AddU>();
            builder.Services.AddScoped<IUpdateU, UpdateU>();
            builder.Services.AddScoped<IRemoveT, RemoveT>();
            builder.Services.AddScoped<IFindT, FindT>();
            builder.Services.AddScoped<IAddT, AddT>();
            builder.Services.AddScoped<IUpdateT, UpdateT>();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}