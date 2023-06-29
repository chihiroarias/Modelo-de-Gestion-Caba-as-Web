using Hotel.LogicaNegocio.Entidades;
using HotelLogicaDeApp.Interfaces.ITipo;
using HotelServiciosWebApi.Dtos.Conversiones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HotelServiciosWebApi.Dtos.TiposDtos;
using Microsoft.Extensions.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelServiciosWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TiposController : ControllerBase
    {
        private IAddT cu_Add;
        private IFindT cu_Find;
        private IRemoveT cu_Remove;
        private IUpdateT cu_Update;
        public TiposController(IAddT cu_Add, IFindT cu_Find, IRemoveT cu_Remove, IUpdateT cu_Update)
        {
            this.cu_Add = cu_Add;
            this.cu_Find = cu_Find;
            this.cu_Remove = cu_Remove;
            this.cu_Update = cu_Update;
        }
        /// <summary>
        /// Listado de Tipos
        /// </summary>
        /// <returns>Retorna un listado de los tipos</returns>
        // GET: api/<TiposController> 
        [HttpGet(Name ="ListaTipos")]
        public ActionResult <IEnumerable<TipoDto>>Get()
        {
            try
            {
                List<Tipo> Tipos = new List<Tipo>();
                Tipos = (List<Tipo>)cu_Find.FindAll();
                if (Tipos == null || Tipos.Count() == 0)
                {
                    return NotFound();

                }
                IEnumerable<TipoDto> tiposDto = ConversionTipo.FindAllTipoToDto(Tipos);
                return Ok(tiposDto);
                
               
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        /// <summary>
        /// Buscar por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna Tipo según su id</returns>
        // GET api/<TiposController>/5
        [HttpGet("id/{id}" , Name ="GetById")]

        public ActionResult<TipoDto> GetId(int id)
        {
            try
            {
                if (id == null)
                    return BadRequest("El id no puede ser null");
                var tipo = cu_Find.FindById(id);
                if (tipo == null)
                    return NotFound($"No existe el tipo con id {id}");
                TipoDto tipoDto = ConversionTipo.ConvertTipoToDto(tipo);
                return Ok(tipoDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Buscar por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>Retorna el tipo con dicho nombre</returns>
        //Tipo Find by Nombre
        [HttpGet("{nombre}", Name = "GetByNombre")]
        public ActionResult<TipoDto> GetName(string nombre)
        {
            try
            {
                if (nombre == null)
                    return BadRequest("El name no puede ser null");
                var tipo = cu_Find.FindByName(nombre);
                if (tipo == null)
                    return NotFound($"No existe el tipo con name {nombre}");
                TipoDto tipoDto = ConversionTipo.ConvertTipoToDto(tipo);
                return Ok(tipoDto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Crear un nuevo tipo
        /// </summary>
        /// <param name="tipodto"></param>
        /// <returns>En caso de suceso arroja 200</returns>
        [HttpPost(Name ="Nuevo")]
        public ActionResult<TipoDto> Post([FromBody] TipoDto tipodto)
        {
            if (tipodto == null)
                return BadRequest("El tipo no puede ser null");
            try
            {
                Tipo tipo = ConversionTipo.ConvertDtoToTipo(tipodto);
                cu_Add.Add(tipo);
                return CreatedAtRoute("GetById", new { id = tipo.TipoId }, tipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Editar Tipo
        /// </summary>
        /// <param name="tipoDto"></param>
        /// <returns>Retorna el objeto editado</returns>

        // PUT api/<TiposController>/tipodto
        [HttpPut(Name ="Editar")]
        public ActionResult<TipoDto> Put([FromBody] TipoDto tipoDto)
        {
            if (tipoDto == null)
            {
                return BadRequest("El tipo que desea editar no puede ser nullo");
            }
            try
            {
                Tipo tipo = ConversionTipo.ConvertDtoToTipo(tipoDto);
                cu_Update.Update(tipo);
                return CreatedAtRoute("GetById", new { id = tipo.TipoId }, tipo);

            }
            catch (Exception ex)//to do cuando paso un costo x huesped no lo cambia pero 
                                 // no esta explicitando que es es el error
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Se encarga de eliminar el objeto y lo busca por el nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>En caso de exito retorna nocontent</returns>
        //DELETE api/<TiposController>/nombre
        [HttpDelete("{nombre}")]
        public IActionResult Delete(string nombre)
        {
            if (nombre == null)
            {
                return BadRequest("El tipo nombre no puede ser null");
            }
            try
            {
                Tipo tipo = cu_Find.FindByName(nombre);
                if (cu_Remove.EnUsoEnCabaña(tipo))
                {
                    return BadRequest("El tipo no puede estar en uso en ninguna cabaña");
                }
                else
                {
                    cu_Remove.Remove(tipo);
                    return NoContent();
                }
            }
             catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
