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
    [Route("api/especialidad")]
    [Produces("application/json")]
    public class EspecialidadController : Controller
    {
        /// <summary>
        /// _EspecialidadRepository
        /// </summary>
        protected readonly IEspecialidadRepository _EspecialidadRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        public EspecialidadController(IEspecialidadRepository EspecialidadRepository)
        {
            _EspecialidadRepository = EspecialidadRepository;
        }
        /// <summary>
        /// findAll
        /// </summary>
        [OpenApiOperation("findAll")]
        [HttpGet]
        public ActionResult findAll()
        {
            var res = _EspecialidadRepository.findAll();
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
            var res = _EspecialidadRepository.findById(id);
            if (res == null)
                return StatusCode(401);
            return Json(res);
        }

        /// <summary>
        /// pagination
        /// </summary>
        [OpenApiOperation("pagination")]
        [HttpGet]
        [Route("pagination/{searchText}/{page}")]
        public ActionResult pagination(string searchText = "", int page = 0)
        {
            var res = _EspecialidadRepository.pagination(searchText, page);
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
        public ActionResult save([FromBody]EntityEspecialidad especialidad)
        {
            var res = _EspecialidadRepository.save(especialidad);
            if (res == null)
                return StatusCode(401);
            return Json(res);
        }
        /// <summary>
        /// delete
        /// </summary>
        [OpenApiOperation("delete")]
        [HttpDelete]
        [Route("{id}")]
        public void delete(int id)
        {
            _EspecialidadRepository.delete(id);
        }
    }
}
