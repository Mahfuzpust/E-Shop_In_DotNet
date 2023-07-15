using ASP_Ecomerce.Data;
using ASP_Ecomerce.Models;
using ASP_Ecomerce.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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

        //Get Product details 
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var product = dBContext.Products.Include(c=>c.ProductTypes).Include(c => c.SpecialTags).FirstOrDefault(c=>c.Id==id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST Product details 
        [HttpPost]
        [ActionName("Detail")]
        public ActionResult ProductDetail(int? id)
        {
            List<Products> products = new List<Products>();
            if (id == null)
            {
                return NotFound();
            }

            var product = dBContext.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return RedirectToAction(nameof(Index));
        }

        //Remove cart POST Method
        [HttpPost]
        public IActionResult Remove(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if(products != null)
            {
                var product = products.FirstOrDefault(c=>c.Id==id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}