using Hotel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioTipo: IRepositorio<Tipo>
    {
        Tipo FindByName(string name);

        bool EnUsoEnCabaña(Tipo tipo);
    }
}
