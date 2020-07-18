using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Tinderro.API.Controllers
{
    // klasa ktora pozwala na odpalanie angulara bez odpalania angulara hoho
    public class NaszaNazwa_FallbackController : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}