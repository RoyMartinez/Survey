using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Survey.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Root Route
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return Ok("Please login or sign up to the api");
        }
    }
}
