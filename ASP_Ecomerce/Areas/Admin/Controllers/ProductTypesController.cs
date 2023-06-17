using ASP_Ecomerce.Data;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Ecomerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDBContext dBContext;
        public ProductTypesController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public IActionResult Index()
        {
            var data = dBContext.ProductTypes.ToList();
            return View(data);
        }
    }
}
