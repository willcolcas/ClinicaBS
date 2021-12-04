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
    [Route("api/horario")]
    [AllowAnonymous]

    public class HorarioController : Controller
    {
        /// <summary>
        /// _HorarioRepository
        /// </summary>
        protected readonly IHorarioRepository _HorarioRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        public HorarioController(IHorarioRepository HorarioRepository)
        {
            _HorarioRepository = HorarioRepository;
        }

        /// <summary>
        /// delete
        /// </summary>
        [OpenApiOperation("delete")]
        [HttpDelete]
        [Route("{id}")]
        public void delete(int id)
        {
            _HorarioRepository.delete(id);
        }
    }
}
