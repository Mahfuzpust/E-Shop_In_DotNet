using ASP_Ecomerce.Data;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Ecomerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class PlaceOrderController : Controller
    {
        private readonly ApplicationDBContext dBContext;
        public PlaceOrderController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Get Place Order
        public IActionResult PlaceOrder()
        {
            return View();
        }
    }
}
