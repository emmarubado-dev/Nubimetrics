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

    public class PaisesController : Controller
    {
        private readonly ILogger<PaisesController> _logger;
        private readonly IPaisesServices _paisesServices;

        public PaisesController(ILogger<PaisesController>logger, IPaisesServices paisesServices)
        {
            _logger = logger;
            _paisesServices = paisesServices;
        }

        [HttpGet]
        [Route("{pais}")]
        [SwaggerOperation(Summary = "Devuelve el pais de ML", Description = "Devuelve el pais de ML")]
        public async Task<IActionResult> Get(string pais)
        {
            var result = await _paisesServices.GetPaisML(pais);
            return Ok(result);
        }
    }
}
