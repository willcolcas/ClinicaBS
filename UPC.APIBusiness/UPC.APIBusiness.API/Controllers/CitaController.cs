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
    [Route("api/cita")]
    [Produces("application/json")]
    public class CitaController : Controller
    {
        /// <summary>
        /// _CitaRepository
        /// </summary>
        protected readonly ICitaRepository citaRepository;
        protected readonly ISucursalRepository sucursalRepository;
        protected readonly IEspecialidadRepository especialidadRepository;
        /// <summary>
        /// Constructor
        /// </summary>

        public CitaController(ICitaRepository citaRepository, ISucursalRepository sucursalRepository, IEspecialidadRepository especialidadRepository)
        {
            this.citaRepository = citaRepository;
            this.sucursalRepository = sucursalRepository;
            this.especialidadRepository = especialidadRepository;
        }
        /// <summary>
        /// findAll
        /// </summary>
        [OpenApiOperation("findAll")]
        [HttpGet]
        public ActionResult findAll()
        {
            var res = citaRepository.findAll();
            if (res == null)
                return StatusCode(401);

            return Json(res);
        }

        /// <summary>
        /// load
        /// </summary>
        [OpenApiOperation("load")]
        [HttpGet]
        [Route("load")]
        public ActionResult load()
        {
            var citaLoad = new CitaLoad();
            citaLoad.especialidades = especialidadRepository.findAll();
            citaLoad.sucursales = sucursalRepository.findAll();
            return Json(citaLoad);
        }


        /// <summary>
        /// findById
        /// </summary>
        [OpenApiOperation("findById")]
        [HttpGet]
        [Route("{id}")]
        public ActionResult findById(int id)
        {
            var res = citaRepository.findById(id);
            if (res == null)
                return StatusCode(401);
            return Json(res);
        }

        /// <summary>
        /// pagination
        /// </summary>
        [OpenApiOperation("pagination")]
        [HttpGet]
        [Route("pagination/{id_usuario}/{searchText}/{page}")]
        public ActionResult paginationByIdUsuario(int id_usuario, string searchText = "", int page = 0)
        {
            var res = citaRepository.paginationByIdUsuario(id_usuario, searchText, page);
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
        public ActionResult save([FromBody] EntityCita cita)
        {
            var res = citaRepository.save(cita);
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
            citaRepository.delete(id);
        }
    }
}
