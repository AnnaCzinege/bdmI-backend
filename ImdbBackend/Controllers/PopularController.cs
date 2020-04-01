using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImdbBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PopularController : ControllerBase
    {
        private readonly ILogger<PopularController> _logger;

        public PopularController(ILogger<PopularController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "Cinci egy szar!";
        }
    }
}