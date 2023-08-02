using Microsoft.AspNetCore.Mvc;
using RDMS.Interface.Service;
using RDMS.Payment;

namespace RDMS.Controllers
{
    public class PaymentController : Controller
    {

        private readonly ICartItemServices _cartItemService;
        private readonly IPaystackServices _paystackServices;

        public PaymentController(ICartItemServices cartItemService, IPaystackServices paystackServices)
        {
            _cartItemService = cartItemService;
            _paystackServices = paystackServices;
        }


        public IActionResult Index()
        {
            return View();
        }

        [ActionName("Payments")]
        public IActionResult Payments()
        {
            return View();
            // return RedirectToAction("Orders", "Reciept");
        }



        [ActionName("MakeOrderPayMent")]
        public async Task<IActionResult> MakeOrderPayMent()
        {
            var payment = await _cartItemService.GetUnpaidCartItemByUserIds();

            return Redirect(payment.data.authorization_url);
            //return View(payment);
        }

    }
}
