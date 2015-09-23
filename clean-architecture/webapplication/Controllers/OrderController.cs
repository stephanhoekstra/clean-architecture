using System;
using System.Web.Mvc;
using core.interactors.core.interactors;

namespace webapplication.Controllers
{

    

    public class OrderController : Controller
    {
        private readonly ICreateOrderInteractor _createOrderInteractor;

        public OrderController(ICreateOrderInteractor createOrderInteractor)
        {
            _createOrderInteractor = createOrderInteractor;
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(CreateOrderRequest createOrderRequest)
        {
            var result = _createOrderInteractor.Handle(createOrderRequest);
            return RedirectToAction("OrderConfirmation",new {result.OrderId});
        }

        public ActionResult OrderConfirmation(Guid orderid)
        {
            return View();
        }
    }
}
