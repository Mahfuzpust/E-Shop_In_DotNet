using ASP_Ecomerce.Data;
using ASP_Ecomerce.Models;
using ASP_Ecomerce.Utility;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Ecomerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly ApplicationDBContext dBContext;
        public OrderController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Get Checkout action Method
        public IActionResult Checkout()
        {
            return View();
        }

        //Post checkout Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Checkout(Order anOrder)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.ProductId = product.Id;
                    anOrder.OrderDetails.Add(orderDetails);
                }
            }

            anOrder.OrderNo = GetOrderNo();
            dBContext.Orders.Add(anOrder);
            await dBContext.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<Products>());
            return View();
        }


        public string GetOrderNo()
        {
            int rowCount = dBContext.Orders.ToList().Count() + 1;
            return rowCount.ToString("000");
        }

    }
}
