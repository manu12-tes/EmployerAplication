using Microsoft.AspNetCore.Mvc;

namespace EmployerAplication.Controllers
{
    public class PruebaController : Controller
    {

        // GET: /HelloWorld/
        [HttpGet("/HelloWorld")]
        public IActionResult Index()
        {
            return View();
        }
        // 
        // GET: /HelloWorld/Welcome/ 
        [HttpGet("/HelloWorld/Welcome")]
       public IActionResult Welcome(String name , int numTimes=1)
        {
            ViewData["Message"] = "Hello" + name;
            ViewData["numTimes"] = numTimes;
            return View();
        }
    }
}
