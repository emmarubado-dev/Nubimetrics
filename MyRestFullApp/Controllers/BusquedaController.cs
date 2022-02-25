using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyRestFullApp.Core.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestFullApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BusquedaController : ControllerBase
    {
        private readonly ILogger<BusquedaController> _logger;
        private readonly IBusquedaServices _busquedaServices;

        public BusquedaController(ILogger<BusquedaController> logger, IBusquedaServices busquedaServices)
        {
            _logger = logger;
            _busquedaServices = busquedaServices;
        }

        [HttpGet]
        [Route("{articulo}")]
        [SwaggerOperation(Summary = "Devuelve la busqueda de un articulo de ML", Description = "Devuelve el articulo de ML")]
        public async Task<IActionResult> Get(string articulo)
        {
            var result = await _busquedaServices.GetArticulos(articulo);
            return Ok(result);
        }
    }
}
