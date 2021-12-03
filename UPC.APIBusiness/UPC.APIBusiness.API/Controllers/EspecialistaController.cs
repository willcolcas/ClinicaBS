using System.Collections.Generic;
using DBContext;
using DBEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace UPC.APIBusiness.API.Controllers
{


    /// <summary>
    /// Controller
    /// </summary>
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/especialista")]
    public class EspecialistaController : Controller
    {
        /// <summary>
        /// _EspecialistaRepository
        /// </summary>
        protected readonly IEspecialistaRepository especialistaRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        public EspecialistaController(IEspecialistaRepository especialistaRepository)
        {
            this.especialistaRepository = especialistaRepository;
        }
        /// <summary>
        /// findAll
        /// </summary>
        [OpenApiOperation("findAll")]
        [HttpGet]
        public ActionResult findAll()
        {
            var res = especialistaRepository.findAll();
            if (res == null)
                return StatusCode(401);

            return Json(res);
        }

        /// <summary>
        /// findById
        /// </summary>
        [OpenApiOperation("findById")]
        [HttpGet]
        [Route("{id}")]
        public ActionResult findById(int id)
        {
            var res = especialistaRepository.findById(id);
            if (res == null)
                return StatusCode(401);
            return Json(res);
        }

        /// <summary>
        /// load
        /// </summary>
        [OpenApiOperation("load")]
        [HttpGet]
        [Route("load/{id}/{page}")]
        public ActionResult load(int id, int page)
        {
            var especialistaLoad = new EspecialistaLoad();
            especialistaLoad.especialista = especialistaRepository.findById(id);
            especialistaLoad.pagination = especialistaRepository.pagination(page: page);
            return Json(especialistaLoad);
        }

        /// <summary>
        /// pagination
        /// </summary>
        [OpenApiOperation("pagination")]
        [HttpGet]
        [Route("pagination/{searchText}/{page}")]
        public ActionResult pagination(string searchText = "", int page = 0)
        {
            var res = especialistaRepository.pagination(searchText, page);
            if (res == null)
                return StatusCode(401);
            return Json(res);
        }

        /// <summary>
        /// pagination
        /// </summary>
        [OpenApiOperation("Filter")]
        [HttpPost]
        [Route("filter")]
        public ActionResult filter([FromBody] Filter filter)
        {
            var res = especialistaRepository.filter(filter);
            if (res == null)
                return StatusCode(401);
            return Json(res);
        }
        /// <summary>
        /// Save
        /// </summary>
        [OpenApiOperation("Save")]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult save([FromBody] EspecialistaExtend especialista)
        {
            var res = especialistaRepository.save(especialista);
            if (res == null)
                return StatusCode(401);
            return Json(res);
        }

        /// <summary>
        /// SaveHorarios
        /// </summary>
        [OpenApiOperation("SaveHorarios")]
        [Consumes("application/json")]
        [HttpPost]
        [Route("saveHorarios")]
        public void saveHorarios([FromBody] List<EntityHorario> horarios)
        {
            especialistaRepository.saveHorarios(horarios);
        }

        /// <summary>
        /// delete
        /// </summary>
        [OpenApiOperation("delete")]
        [HttpDelete]
        [Route("{id}")]
        public void delete(int id)
        {
            especialistaRepository.delete(id);
        }
    }
}
