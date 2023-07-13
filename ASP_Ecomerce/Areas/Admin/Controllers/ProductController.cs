using ASP_Ecomerce.Data;
using ASP_Ecomerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ASP_Ecomerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDBContext dbContext;
        private readonly IHostingEnvironment env;
        public ProductController(ApplicationDBContext dBContext, IHostingEnvironment env)
        {
            this.dbContext = dBContext;
            this.env = env;
        }
        public IActionResult Index()
        {
            return View(dbContext.Products.Include(c => c.ProductTypes).Include(f => f.SpecialTags).ToList());
        }

        //POST Index Action Method
        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var products = dbContext.Products.Include(c=>c.ProductTypes).Include(c=>c.SpecialTags)
                .Where(c=>c.Price >= lowAmount && c.Price <= largeAmount).ToList();

            if (lowAmount==null || largeAmount == null)
            {
                products = dbContext.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTags)
                .ToList();
            }
            return View(products);
        }

        //Get Create Method
        public IActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(dbContext.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(dbContext.SpecialTags.ToList(), "Id", "SpecialTag");

            return View();
        }
        //Post Create Method
        [HttpPost]
        public async Task<IActionResult> Create(Products product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
               var searchProduct = dbContext.Products.FirstOrDefault(f=>f.Name == product.Name);
                if(searchProduct != null)
                {
                    ViewBag.message = "This product is already exist";
                    ViewData["productTypeId"] = new SelectList(dbContext.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["TagId"] = new SelectList(dbContext.SpecialTags.ToList(), "Id", "SpecialTag");
                    return View(product);
                }

                if (image != null)
                {
                    var name = Path.Combine(env.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    product.Image = "Images/Noimg.jpg";
                }
                dbContext.Products.Add(product);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }


        //Edit Get Product
        public IActionResult Edit(int? id)
        {
            ViewData["productTypeId"] = new SelectList(dbContext.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(dbContext.SpecialTags.ToList(), "Id", "SpecialTag");
            if (id == null)
            {
                return NotFound();
            }

            var product = dbContext.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTags)
                .FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST Edit Method
        [HttpPost]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(env.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    products.Image = "Images/NoIma.jpg";
                }

                dbContext.Products.Update(products);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        //GET Details 
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = dbContext.Products.Include(c=>c.ProductTypes).Include(c=>c.SpecialTags).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //Get Delete Method
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = dbContext.Products.Include(c => c.ProductTypes).Include(f => f.SpecialTags).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //POST Delete Method
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult>DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = dbContext.Products.FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
