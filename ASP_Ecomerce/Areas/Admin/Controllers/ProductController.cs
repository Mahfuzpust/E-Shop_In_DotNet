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
        //Get Create Method
        public IActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(dbContext.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(dbContext.SpecialTags.ToList(), "Id", "SpecialTag");

            return View();
        }
        //Post Create Method
        [HttpPost]
        public async Task<ActionResult> Create(Products products,IFormFile image)
        {
            if(ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(env.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }

                if(image == null)
                {
                    products.Image = "Images/Noimg.jpg";
                }

                dbContext.Products.Add(products);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        //Edit Get Product
        public IActionResult Edit(int id)
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
        public IActionResult Details(int id)
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

    }
}
