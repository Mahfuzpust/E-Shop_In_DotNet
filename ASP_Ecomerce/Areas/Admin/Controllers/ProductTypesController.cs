using ASP_Ecomerce.Data;
using ASP_Ecomerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Ecomerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDBContext dbContext;
        public ProductTypesController(ApplicationDBContext dBContext)
        {
                this.dbContext = dBContext;
        }
        public IActionResult Index()
        {
            var data = dbContext.ProductTypes.ToList();
            return View(data);
        }

        //Get ProductTpes
        public IActionResult Create()
        {
            return View();
        }
        //Post ProductTypes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes model)
        {
            if (ModelState.IsValid)
            {
                dbContext.ProductTypes.Add(model);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        //Get Method Product Types Edit
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ProductType = dbContext.ProductTypes.FirstOrDefault(e => e.Id == id);
            if (ProductType == null)
            {
                return NotFound();
            }
            return View(ProductType);
        }
        //Post Method Edit 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes model)
        {
            if (ModelState.IsValid)
            {
                dbContext.ProductTypes.Update(model);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }
        //Get Method Product Types Details
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ProductType = dbContext.ProductTypes.FirstOrDefault(e => e.Id == id);
            if (ProductType == null)
            {
                return NotFound();
            }
            return View(ProductType);
        }
        //Post Method Details 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes model)
        {
            return RedirectToAction(nameof(Index));
        }
        //Delete
        public IActionResult Delete(int? id, ProductTypes model)
        {

            if (id == null)
            {
                return NotFound();
            }
            if (id != model.Id)
            {

            }
            var ProductType = dbContext.ProductTypes.FirstOrDefault(e => e.Id == id);
            if (ProductType == null)
            {
                return NotFound();
            }
            return View(ProductType);
        }

        //Post Method Delete 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProductTypes model)
        {
            if (ModelState.IsValid)
            {
                dbContext.ProductTypes.Remove(model);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }
    }
}
