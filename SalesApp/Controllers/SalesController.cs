using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesApp.Models;

namespace SalesApp.Controllers
{
    [Authorize]

    public class SalesController : Controller
    {
        private readonly FieldSalesContext _salesContext;
        public SalesController(FieldSalesContext salesContext) { 
            _salesContext = salesContext;
        }

        // GET: SalesController
        public ActionResult Index(int orderId)
        {
            if (orderId == 0)
            {
                ViewBag.Clients = new SelectList(_salesContext.Clients.ToList(), "ClientId", "ClientName");
              
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                foreach (var product in _salesContext.Products.ToList())
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.ProductId = product.ProductId;
                    orderDetail.Quantity = 0;
                    orderDetail.OrderId = -1;
                    orderDetail.Product = product;
                    orderDetails.Add(orderDetail);
                }
                ViewBag.OrderDetails = orderDetails;
            }
            else
            {
                Order order=_salesContext.Orders.Single(x=>x.OrderId==orderId);

                ViewBag.Clients = new SelectList(_salesContext.Clients.ToList(), "ClientId", "ClientName",order.ClientId);
                //ViewBag.Regions = new SelectList(_salesContext.Regions.ToList(), "RegionName", "RegionName");

                List<OrderDetail> orderDetails = new List<OrderDetail>();
                foreach (var product in _salesContext.Products.ToList())
                {
                    OrderDetail tmpOd= _salesContext.OrderDetails.SingleOrDefault(x => x.OrderId == orderId && x.ProductId == product.ProductId);
                    int tmpQ = 0;
                    if (tmpOd != null)
                    {
                        tmpQ = tmpOd.Quantity??0;
                    }
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.ProductId = product.ProductId;
                    orderDetail.Quantity = tmpQ;
                    orderDetail.OrderId = -1;
                    orderDetail.Product = product;
                    orderDetails.Add(orderDetail);
                }
                ViewBag.OrderDetails = orderDetails;
            }
            
            return View();
        }

        // GET: SalesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int Clients, int[] orderDetails)
        {

            Order order = new Order();
            

            order.ClientId= Clients;
            order.RegionName = "";
            order.OrderDate = DateTime.Now;

            _salesContext.Orders.Add(order);

            _salesContext.SaveChanges();

            List<Product> products = _salesContext.Products.ToList();

            for(int i = 0; i < orderDetails.Length; i++)
            {
                if (orderDetails[i] == 0)
                {
                    continue;
                }
                OrderDetail orderDetail= new OrderDetail();
                orderDetail.OrderId=order.OrderId;
                orderDetail.ProductId = products[i].ProductId;
                orderDetail.Quantity = orderDetails[i];

                _salesContext.OrderDetails.Add(orderDetail);
            }
            _salesContext.SaveChanges();
            try
            {
                ////return RedirectToAction(nameof(Index),new { orderId=order.OrderId });
                ///
                return Redirect("http://localhost:5133/Sales");

            }
            catch
            {
                return View();
            }
        }

        // GET: SalesController/Edit/5
        public ActionResult Edit(int id)
        {
            Order order = _salesContext.Orders.Single(x => x.OrderId == id);

            ViewBag.Clients = new SelectList(_salesContext.Clients.ToList(), "ClientId", "ClientName", order.ClientId);
            //ViewBag.Regions = new SelectList(_salesContext.Regions.ToList(), "RegionName", "RegionName");

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var product in _salesContext.Products.ToList())
            {
                OrderDetail tmpOd = _salesContext.OrderDetails.SingleOrDefault(x => x.OrderId == id && x.ProductId == product.ProductId);
                int tmpQ = 0;
                if (tmpOd != null)
                {
                    tmpQ = tmpOd.Quantity ?? 0;
                }
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.ProductId = product.ProductId;
                orderDetail.Quantity = tmpQ;
                orderDetail.OrderId = -1;
                orderDetail.Product = product;
                orderDetails.Add(orderDetail);
            }
            ViewBag.OrderDetails = orderDetails;


            return View("Index");
        }

        // POST: SalesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
