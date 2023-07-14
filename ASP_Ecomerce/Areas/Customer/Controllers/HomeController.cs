using ASP_Ecomerce.Data;
using ASP_Ecomerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASP_Ecomerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
       private readonly ApplicationDBContext dBContext;

        public HomeController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public IActionResult Index()
        {
            var products = this.dBContext.Products.Include(c=>c.ProductTypes).Include(f=>f.SpecialTags).ToList();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}