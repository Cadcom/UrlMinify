using Microsoft.AspNetCore.Mvc;
using UrlMinify.Business.Abstract;

namespace UrlMinify.Api.Controllers
{
    /// <summary>
    /// Url Minify API Base Controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BaseController : Controller
    {      

        private readonly ILogger<BaseController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUrlService service;

        public BaseController(ILogger<BaseController> logger, IConfiguration configuration, IUrlService service)
        {
            _logger = logger;
            _configuration = configuration;
            this.service = service;
        }

        /// <summary>
        /// Url Minify API Endpoint. Endpoint using Responsecahe for same requests
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("minifyUrl")]
        [ResponseCache(Duration = 60 * 60 * 24, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "url" })]

        public IActionResult minifyUrl([FromBody] string url)
        {
            

            var result = service.insertUrlAsync(url);
            return Content(result.Result.Minified);
        }


        /// <summary>
        /// dencrypt url-key endpoint. Endpoint using Responsecahe for same requests
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("realUrl")]
        [ResponseCache(Duration = 60 * 60 * 24, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "key" })]

        public IActionResult realUrl([FromBody] string key)
        {
            var result = service.getUrl(key);
            return Content(result.Url);
        }
    }
}