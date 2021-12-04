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
    [Authorize]
    [Route("api/especialidad")]
    [Produces("application/json")]

    public class EspecialidadController : Controller
    {
        /// <summary>
        /// especialidadRepository
        /// </summary>
        protected readonly IEspecialidadRepository _especialidadRepository;
        /// <summary>
        /// sucursalRepository
        /// </summary>
        protected readonly ISucursalRepository _sucursalRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        public EspecialidadController(IEspecialidadRepository EspecialidadRepository, ISucursalRepository sucursalRepository)
        {
            this._especialidadRepository = EspecialidadRepository;
            this._sucursalRepository = sucursalRepository;
        }
        /// <summary>
        /// findAll
        /// </summary>
        [OpenApiOperation("findAll")]
        [HttpGet]
        public ActionResult findAll()
        {
            var res = _especialidadRepository.findAll();
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
            var especialidadLoad = new EspecialidadLoad();
            especialidadLoad.especialidad = _especialidadRepository.findById(id);
            especialidadLoad.pagination = _especialidadRepository.pagination(page: page);
            especialidadLoad.sucursales = _sucursalRepository.findAll();
            return Json(especialidadLoad);
        }


        /// <summary>
        /// findById
        /// </summary>
        [OpenApiOperation("findById")]
        [HttpGet]
        [Route("{id}")]
        public ActionResult findById(int id)
        {
            var res = _especialidadRepository.findById(id);
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
            var res = _especialidadRepository.pagination(searchText, page);
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
        public ActionResult save([FromBody] EntityEspecialidad especialidad)
        {
            var res = _especialidadRepository.save(especialidad);
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
            _especialidadRepository.delete(id);
        }
    }
}
