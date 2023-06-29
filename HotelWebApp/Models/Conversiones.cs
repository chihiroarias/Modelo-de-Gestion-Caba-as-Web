using Hotel.LogicaNegocio.Entidades;
using HotelMVC.Models.EntidadesModel;
using Microsoft.Extensions.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Hotel.LogicaNegocio.ValueObjects;
using HotelLogicaNegocio.ValueObjects;

namespace HotelMVC.Models
{
    public class Conversiones
    {
        internal static Models.EntidadesModel.CabaniaModel ConvertCabToModel(Cabania cm)
        {
            return new Models.EntidadesModel.CabaniaModel()
            {
                
                TipoAsociado = cm.TipoAsociado,
            
                NameInput = cm.Nombre.Name,
                Descripcion = cm.Descripcion,
                Jacuzzi = cm.Jacuzzi,
                Habilitada = cm.Habilitada,
                MaxPersonas = cm.MaxPersonas,
                ImgNombreInput = cm.Foto.Nombre
            };
        }

        internal static Cabania ConvertModelToCab(CabaniaModel cm)
        {
            return new Cabania()
            {
                
                TipoAsociado = cm.TipoAsociado,
                Nombre = new NombreCabana(cm.NameInput),
                Descripcion = cm.Descripcion,
                Jacuzzi = cm.Jacuzzi,
                Habilitada = cm.Habilitada,
                MaxPersonas = cm.MaxPersonas
                 
            };
        }

        internal static IEnumerable<CabaniaModel> FindAllCabToModel(IEnumerable<Cabania> Cab)
        {
            var list = Cab
                .Select(c => ConvertCabToModel(c)).ToList();
            return list;
        }
        internal static Models.EntidadesModel.MantenimientoModel ConvertMantenToModel(Mantenimiento mt)
        {
            return new MantenimientoModel()
            {
                
                Fecha = mt.Fecha,
                Descripcion = mt.Descripcion,
                Costo = mt.Costo,
                ResponsableInput = mt.Responsable.NombreCompleto,
                CabaniaAsociada = mt.CabaniaAsociada
            };
        }
        internal static Mantenimiento ConvertModelToManten(MantenimientoModel mm)
        {
            return new Mantenimiento()
            {
                
                Fecha = mm.Fecha,
                Descripcion = mm.Descripcion,
                Costo = mm.Costo,
                Responsable = new NameManten(mm.ResponsableInput),
                CabaniaAsociada = mm.CabaniaAsociada,
            };
        }
        internal static IEnumerable<MantenimientoModel> FindAllManteToModel(IEnumerable<Mantenimiento> Mant)
        {
            var list = Mant
                .Select(c => ConvertMantenToModel(c)).ToList();
            return list;
        }
        internal static Models.EntidadesModel.TipoModel ConvertTipoToModel(Tipo tp)
        {
            return new TipoModel()
            {
                CostoxHuesped = tp.CostoxHuesped,
                NombreInput = tp.Nombre.NameT,
                Descripcion = tp.Descripcion,
                TipoId= tp.TipoId
            };
        }
        internal static Tipo ConvertModelToTipo(TipoModel tm)
        {
            return new Tipo()
            {
                CostoxHuesped=tm.CostoxHuesped,
                Nombre = new NameTipo(tm.NombreInput),
                Descripcion = tm.Descripcion,
                TipoId=tm.TipoId
            };
        }
        public static IEnumerable<TipoModel> FindAllTipoToModel(IEnumerable<Tipo> Tipo)
        {
            var list = Tipo
                .Select(c => ConvertTipoToModel(c)).ToList();
            return list;
        }
        internal static UsuarioModel ConverModelToUsu(Usuario usu)
        {
            return new UsuarioModel()
            {
                Email = usu.Email,
                PassInput = usu.Password.Pass
            };
        }
        internal static Usuario ConvertUsuToModel(UsuarioModel usu)
        {
            return new Usuario()
            {
                Email = usu.Email,
                Password = new Contra(usu.PassInput)
            };
        }
        internal static IEnumerable<UsuarioModel> FindAllUsuToModel(IEnumerable<Usuario> usu)
        {
            return usu
                .Select(c => ConverModelToUsu(c)).ToList();
            
        }
    }
}
