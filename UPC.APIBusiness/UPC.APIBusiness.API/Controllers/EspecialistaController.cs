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
        protected readonly IEspecialistaRepository _especialistaRepository;
        protected readonly ISucursalRepository _sucursalRepository;
        protected readonly IEspecialidadRepository _especialidadRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        public EspecialistaController(IEspecialistaRepository especialistaRepository, ISucursalRepository sucursalRepository, IEspecialidadRepository especialidadRepository)
        {
            this._especialistaRepository = especialistaRepository;
            this._sucursalRepository = sucursalRepository;
            this._especialidadRepository = especialidadRepository;
        }
        /// <summary>
        /// findAll
        /// </summary>
        [OpenApiOperation("findAll")]
        [HttpGet]
        public ActionResult findAll()
        {
            var res = _especialistaRepository.findAll();
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
            var res = _especialistaRepository.findById(id);
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
            especialistaLoad.especialista = _especialistaRepository.findById(id);
            especialistaLoad.pagination = _especialistaRepository.pagination(page: page);
            return Json(especialistaLoad);
        }



        /// <summary>
        /// loadHorarios
        /// </summary>
        [OpenApiOperation("loadHorarios")]
        [HttpGet]
        [Route("loadHorarios/{id}/{id_sucursal}/{id_especialidad}/{dia}")]
        public ActionResult loadHorarios(int id, int id_sucursal, int id_especialidad, int dia)
        {
            var horarioLoad = new HorarioLoad();
            horarioLoad.horarios = _especialistaRepository.loadHorarios(id, id_sucursal, id_especialidad, dia);
            horarioLoad.sucursales = _sucursalRepository.findAll();
            horarioLoad.especialidades = _especialidadRepository.findAll();
            return Json(horarioLoad);
            // return Json("hola");
        }

        /// <summary>
        /// pagination
        /// </summary>
        [OpenApiOperation("pagination")]
        [HttpGet]
        [Route("pagination/{searchText}/{page}")]
        public ActionResult pagination(string searchText = "", int page = 0)
        {
            var res = _especialistaRepository.pagination(searchText, page);
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
            var res = _especialistaRepository.filter(filter);
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
            var res = _especialistaRepository.save(especialista);
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
            // return Json("hola");
            _especialistaRepository.saveHorarios(horarios);
        }

        /// <summary>
        /// delete
        /// </summary>
        [OpenApiOperation("delete")]
        [HttpDelete]
        [Route("{id}")]
        public void delete(int id)
        {
            _especialistaRepository.delete(id);
        }
    }
}
