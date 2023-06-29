using Hotel.LogicaNegocio.Entidades;
using HotelLogicaDeApp.Interfaces;
using HotelServiciosWebApi.Dtos.CabaniasDtos;
using HotelServiciosWebApi.Dtos.Conversiones;
using HotelServiciosWebApi.Dtos.TiposDtos;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelServiciosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabaniasController : ControllerBase
    {
        private ICreateCab cu_create;
        private IFindC cu_Find;
        private IUpdateC cu_Update;
        private ICreateNameImg cu_Nimg;
        private IBuscar cu_Busq;
        private IRemoveC cu_Remove;

        public CabaniasController(ICreateCab cu_create, IFindC cu_Find, IUpdateC cu_Update, ICreateNameImg cu_Nimg, IBuscar cu_Busq, IRemoveC cu_Remove)
        {
            this.cu_create = cu_create;
            this.cu_Find = cu_Find;
            this.cu_Update = cu_Update;
            this.cu_Nimg = cu_Nimg;
            this.cu_Busq = cu_Busq;
            this.cu_Remove = cu_Remove;
        }

        /// <summary>
        /// Te da una lista con las cabañas existentes
        /// </summary>
        /// <returns>IEnumerable</returns>

        // GET: api/<CabaniasController> 
        [HttpGet(Name = "Listado")]

        public ActionResult<IEnumerable<CabaniaDto>> Get()
        {
            try
            {
                List<Cabania> Cabs = new List<Cabania>();
                Cabs = (List<Cabania>)cu_Find.GetAll();
                if (Cabs == null || Cabs.Count == 0)
                {
                    return NotFound();
                }
                IEnumerable<CabaniaDto> cabaniasDto = ConversionesCabania.FindAllCabToDto(Cabs);
                return Ok(cabaniasDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        /// <summary>
        /// Recibe los siguientes parámetros y en base a tu consulta retornará un listado
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="maxPersonas"></param>
        /// <param name="habilitado"></param>
        /// <param name="tipoAsociado"></param>
        /// <returns>IEnumerable</returns>
        //GET api/<CabaniasController>/
        [HttpGet("GetByFiltros")]
        public ActionResult<Cabania> GetByFiltros(string? nombre, int maxPersonas, bool habilitado, int tipoAsociado)
        {
            try
            {//puedo ingresar nulls, ver q filtros tengo que poner

                var cab = cu_Busq.Buscar(nombre, maxPersonas, habilitado, tipoAsociado);
                IEnumerable<CabaniaDto> cabaniasDto = ConversionesCabania.FindAllCabToDto(cab);
                return Ok(cabaniasDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Crear Cabaña
        /// </summary>
        /// <param name="cabaniaDto"></param>
        /// <returns>En caso de exito retorna la cabaña creada</returns>
        // POST api/<CabaniasController> 
        [HttpPost(Name = "NuevaCabania")] 
        public ActionResult<Cabania> Post([FromBody] CabaniaDto cabaniaDto)
        {
            if (cabaniaDto == null)
                return BadRequest("La cabania no puede ser null");
            try
            {
                Cabania cab = ConversionesCabania.ConvertDtoToCab(cabaniaDto);
                var Nimg = cu_Nimg.CrearNomImagen(cab);
                cab.Foto.Nombre = Nimg;
                cu_create.Add(cab);
                return CreatedAtRoute("GetCabById", new { id = cab.CabaniaId }, cab);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene una cabania por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>En caso de exito retornará el objeto buscado</returns>
        [HttpGet("id/{id}", Name = "GetCabById")]
        public ActionResult<TipoDto> GetCabById(int id)
        {
            try
            {
                if (id == null)
                    return BadRequest("El id no puede ser null");
                var cab = cu_Find.FindById(id);
                if (cab == null)
                    return NotFound($"No existe la  cabaña con id {id}");
                CabaniaDto cabaniaDto = ConversionesCabania.ConvertCabToDto(cab);
                return Ok(cabaniaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Delete de cabaña
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna 200 en caso de haber sido eliminado</returns>
        // DELETE api/<CabaniasController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null)
                return BadRequest("El id no puede ser null");
            try
            {
                Cabania cab = cu_Find.FindById(id);
                cu_Remove.Remove(cab);
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene un listado en base al id del tipo y el monto
        /// </summary>
        /// <param name="idTipo"></param>
        /// <param name="monto"></param>
        /// <returns>IEnumerable</returns>
        [HttpGet("GetByIdTipo")]
        public ActionResult<Cabania> GetByIdTipo(int? idTipo, double? monto)
        {
            try
            {
                var cab = cu_Find.cabFind(idTipo, monto);
                IEnumerable<CabaniaDto> cabaniasDto = ConversionesCabania.FindAllCabToDto(cab);
                return Ok(cabaniasDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
