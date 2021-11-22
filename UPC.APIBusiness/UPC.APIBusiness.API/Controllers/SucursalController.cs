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
    [Produces("application/json")]
    [Route("api/sucursal")]
    [AllowAnonymous]
    public class SucursalController : Controller
    {
        /// <summary>
        /// _SucursalRepository
        /// </summary>
        protected readonly ISucursalRepository _SucursalRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        public SucursalController(ISucursalRepository SucursalRepository)
        {
            _SucursalRepository = SucursalRepository;
        }
        /// <summary>
        /// findAll
        /// </summary>
        [OpenApiOperation("findAll")]
        [HttpGet]
        public ActionResult findAll()
        {
            var res = _SucursalRepository.findAll();
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
            var res = _SucursalRepository.findById(id);
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
            var res = _SucursalRepository.pagination(searchText, page);
            if (res == null)
                return StatusCode(401);
            return Json(res);
        }


        /// <summary>
        /// Save
        /// </summary>
        [OpenApiOperation("Save")]
        [HttpPost]
        public ActionResult save([FromBody] EntitySucursal sucursal)
        {
            var res = _SucursalRepository.save(sucursal);
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
            _SucursalRepository.delete(id);
        }
    }
}
