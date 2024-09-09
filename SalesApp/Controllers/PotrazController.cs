using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesApp.Models.ViewModel;
using System.Net.Http.Headers;

namespace SalesApp.Controllers
{
    public class PotrazController : Controller

    {
        

        private const string URL = "http://localhost:5009/api/Potrazivanjas";
        // GET: PotrazController
        public ActionResult ListaPotrazivanja()
        {
            //Potrazivanja potrazivanja = new Potrazivanja();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(URL).Result;

            var dataObjects = response.Content.ReadAsAsync<IEnumerable<Potrazivanja>>().Result;

            //foreach(var d in dataObjects)
            //{
            //    potrazivanja.Id = d.Id;
            //    potrazivanja.NameOfClient=d.NameOfClient;
            //    potrazivanja.Amount = d.Amount;
            //}


            return View("Potraz", dataObjects);
        }

        // GET: PotrazController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PotrazController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PotrazController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: PotrazController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PotrazController/Edit/5
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

        // GET: PotrazController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PotrazController/Delete/5
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
