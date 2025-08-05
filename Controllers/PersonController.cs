using Microsoft.AspNetCore.Mvc;

namespace Smart_OrdreMission.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
