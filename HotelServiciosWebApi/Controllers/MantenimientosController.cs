using Hotel.LogicaNegocio.Entidades;
using HotelLogicaDeApp.Interfaces.IManten;
using HotelServiciosWebApi.Dtos.Conversiones;
using HotelServiciosWebApi.Dtos.MantenimientoDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace HotelServiciosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientosController : ControllerBase
    {

        private IFindM cu_find;
        private IAddM cu_add;
        private IUpdateM cu_update;
        private IRemoveM cu_remove;

        public MantenimientosController(IFindM fd, IAddM ad, IUpdateM up, IRemoveM rm)
        {
            cu_find = fd;
            cu_add = ad;
            cu_update = up;
            cu_remove = rm;
        }
      
        
        [HttpGet("{id}")]
        
        public ActionResult Get(int id)
        {
            try
            {
                if (id == null)
                    return BadRequest("El id no puede ser null");
                var man = cu_find.FindById(id);
                if (man == null)
                    return NotFound($"No existe el tipo con id {id}");
                return Ok(man);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Crea un mantenimiento
        /// </summary>
        /// <param name="md"></param>
        /// <returns>retorna el objeto creado</returns>
        // POST api/<MantenimientosController>
        [HttpPost(Name ="newMantenimiento")]
        public ActionResult Post([FromBody] MantenDto md)
        {
            if (md == null)
                return BadRequest("El tipo no puede ser null");
            try
            {
                Mantenimiento m = ConversionesMantenimientos.ConvertmtTomod(md);
                cu_add.Add(m);
                return CreatedAtRoute("GetById", new { id = m.MantenimientoId }, m);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Edita el mantenimiento
        /// </summary>
        /// <param name="md"></param>
        /// <returns>retorna el objeto editado</returns>
        // PUT api/<MantenimientosController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] MantenDto md)
        {

            if (md == null) { BadRequest(); }
            try
            {
                Mantenimiento m = ConversionesMantenimientos.ConvertmtTomod(md);
                cu_update.Update(m);
                return Ok(m);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Genera una lista de mantenimientos en base al identificador de una cabaña y un rango de fechas
        /// </summary>
        /// <param name="id"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns>Retorna el listado</returns>
        [HttpGet("GetByDate")]
        public ActionResult GetByDate(
            [FromQuery]int id,
            [FromQuery] DateTime d1, 
            [FromQuery] DateTime d2)
        {
            if(id == null || d1 == null || d2 == null) { return BadRequest(); }
            try
            {
                List<Mantenimiento> ms = new List<Mantenimiento>();
                ms = (List<Mantenimiento>)cu_find.FiltrarbyFechas(id, d1, d2);
                if(ms.Count == 0) { return NotFound(); }
                return Ok(ms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Genera una lista en base a un rango de cantidad de personas que tiene de espacio la cabaña
        /// </summary>
        /// <param name="q1"></param>
        /// <param name="q2"></param>
        /// <returns>Retorna un listado</returns>
        [HttpGet("GetByQpersonas")]
        public ActionResult<IEnumerable<string>> GetByQpersonas(int q1, int q2)
        {
            try
            {
                List<Mantenimiento> mants = (List<Mantenimiento>)cu_find.FindByQPers(q1, q2);
                if (mants.Count == 0) { return NotFound(); }
                IEnumerable<MantenDto> md = ConversionesMantenimientos.ConvertAllToDto(mants);
                return Ok(md);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Devuelve todos los Mantenimientos de la cabaña 
        /// </summary>
        /// <param name="idCab"></param>
        /// <returns>Retorna un listado de mantenimientos</returns>

        [HttpGet("GetMantxIdCab/{idCab}")]
        public ActionResult<IEnumerable<MantenDto>>GetMantxIdCab(int idCab)
        {
            try
            {
                List<Mantenimiento> mantscab = new List<Mantenimiento>();
                mantscab = (List<Mantenimiento>)cu_find.FindByIdCab(idCab);
                if (mantscab.Count == 0) 
                { 
                    return NotFound();
                }
                return Ok(mantscab);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}



