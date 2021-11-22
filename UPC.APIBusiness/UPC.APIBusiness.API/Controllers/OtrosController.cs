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
    [Route("api/otros")]
    public class OtrosController : Controller
    {

        [HttpGet]
        public ActionResult hola()
        {
            return Json("hola mundo");
        }

    }
}
