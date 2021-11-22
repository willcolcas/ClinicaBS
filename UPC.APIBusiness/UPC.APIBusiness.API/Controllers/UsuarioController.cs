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
    [Route("api/usuario")]
    public class UsuarioController : Controller
    {
        /// <summary>
        /// _UsuarioRepository
        /// </summary>
        protected readonly IUsuarioRepository _UsuarioRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        public UsuarioController(IUsuarioRepository UsuarioRepository)
        {
            _UsuarioRepository = UsuarioRepository;
        }
        /// <summary>
        /// findAll
        /// </summary>
        [OpenApiOperation("findAll")]
        [HttpGet]
        public ActionResult findAll()
        {
            var res = _UsuarioRepository.findAll();
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
            var res = _UsuarioRepository.findById(id);
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
            var res = _UsuarioRepository.pagination(searchText, page);
            if (res == null)
                return StatusCode(401);
            return Json(res);
        }


        /// <summary>
        /// Save
        /// </summary>
        [OpenApiOperation("Save")]
        [HttpPost]
        public ActionResult save([FromBody] EntityUsuario usuario)
        {
            var res = _UsuarioRepository.save(usuario);
            if (res == null)
                return StatusCode(401);
            return Json(res);
        }


        /// <summary>
        /// delete
        /// </summary>
        [OpenApiOperation("login")]
        [HttpPost]
        [Route("login")]
        public ActionResult login([FromBody] Login login)
        {
            var res = _UsuarioRepository.login(login);
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
            _UsuarioRepository.delete(id);
        }


    }
}
