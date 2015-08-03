using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using core.interactors;
using core.interactors.core.interactors;
using MediatR;

namespace webapplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMediator _mediatr;

        public OrderController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: Order/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(CreateOrderRequest createOrderRequest)
        {
            var result = _mediatr.Send(createOrderRequest);
            return RedirectToAction("OrderConfirmation",new {result.OrderId});
        }

        //// GET: Order/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Order/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Order/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Order/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public ActionResult OrderConfirmation(Guid orderid)
        {
            return View();
        }
    }
}
