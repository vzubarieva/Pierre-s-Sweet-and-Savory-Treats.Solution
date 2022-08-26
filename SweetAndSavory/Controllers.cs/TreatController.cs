using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;

namespace SweetAndSavory.Controllers
{
    public class TreatsController : Controller
    {
        private readonly SweetAndSavoryContext _db;

        public TreatsController(SweetAndSavoryContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Treat> model = _db.Treats.ToList();
            return View(model);
        }

        //         //         public ActionResult Create()
        //         //         {
        //         //             ViewBag.RestaurantId = new SelectList(_db.Restaurants, "RestaurantId", "Name");
        //         //             return View();
        //         //         }

        //         //         [HttpPost]
        //         //         public ActionResult Create(Cuisine cuisine)
        //         //         {
        //         //             _db.Cuisines.Add(cuisine);
        //         //             _db.SaveChanges();
        //         //             return RedirectToAction("Index");
        //         //         }

        public ActionResult Details(int id)
        {
            var thisTreat = _db.Treats
                .Include(treat => treat.JoinEntities)
                .ThenInclude(join => join.Flavor)
                .FirstOrDefault(treat => treat.TreatId == id);
            return View(thisTreat);
        }
        //         //         public ActionResult Edit(int id)
        //         //         {
        //         //             var thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
        //         //             ViewBag.RestaurantId = new SelectList(_db.Restaurants, "RestaurantId", "Name");
        //         //             return View(thisCuisine);
        //         //         }

        //         //         [HttpPost]
        //         //         public ActionResult Edit(Cuisine cuisine)
        //         //         {
        //         //             _db.Entry(cuisine).State = EntityState.Modified;
        //         //             _db.SaveChanges();
        //         //             return RedirectToAction("Index");
        //         //         }

        //         //         public ActionResult Delete(int id)
        //         //         {
        //         //             var thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
        //         //             return View(thisCuisine);
        //         //         }

        //         //         [HttpPost, ActionName("Delete")]
        //         //         public ActionResult DeleteConfirmed(int id)
        //         //         {
        //         //             var thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
        //         //             _db.Cuisines.Remove(thisCuisine);
        //         //             _db.SaveChanges();
        //         //             return RedirectToAction("Index");
        //         //         }
    }
}
