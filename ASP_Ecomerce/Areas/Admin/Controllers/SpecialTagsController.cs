using ASP_Ecomerce.Data;
using ASP_Ecomerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Ecomerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagsController : Controller
    {
        private readonly ApplicationDBContext dBContext;
        public SpecialTagsController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public IActionResult Index()
        {
            var data = dBContext.SpecialTags.ToList();
            return View(data);
        }

        //Create GET
        public IActionResult Create()
        {
            return View();
        }

        //Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTags model)
        {
            if (ModelState.IsValid)
            {
                dBContext.SpecialTags.Add(model);
                await dBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        //Edit GET
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var SpecialTag = dBContext.SpecialTags.FirstOrDefault(e => e.Id == id);
            if (SpecialTag == null)
            {
                return NotFound();
            }
            return View(SpecialTag);
        }

        //Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialTags model)
        {
            if (ModelState.IsValid)
            {
                dBContext.SpecialTags.Update(model);
                await dBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        //Get Method Product Types Details
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ProductType = dBContext.SpecialTags.FirstOrDefault(e => e.Id == id);
            if (ProductType == null)
            {
                return NotFound();
            }
            return View(ProductType);
        }
        //Post Method Details 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(SpecialTags model)
        {
            return RedirectToAction(nameof(Index));
        }

        //Delete Product Types
        public IActionResult Delete(int? id, SpecialTags model)
        {

            if (id == null)
            {
                return NotFound();
            }
            if (id != model.Id)
            {

            }
            var ProductType = dBContext.SpecialTags.FirstOrDefault(e => e.Id == id);
            if (ProductType == null)
            {
                return NotFound();
            }
            return View(ProductType);
        }

        //Post Method Delete 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SpecialTags model)
        {
            if (ModelState.IsValid)
            {
                dBContext.SpecialTags.Remove(model);
                await dBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }
    }
}
