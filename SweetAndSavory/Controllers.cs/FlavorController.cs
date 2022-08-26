using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;

namespace SweetAndSavory.Controllers
{
    public class FlavorsController : Controller
    {
        private readonly SweetAndSavoryContext _db;

        public FlavorsController(SweetAndSavoryContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Flavor> model = _db.Flavors.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Flavor flavor, int TreatId)
        {
            _db.Flavors.Add(flavor);
            _db.SaveChanges();
            if (TreatId != 0)
            {
                _db.FlavorTreat.Add(
                    new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId }
                );
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisFlavor = _db.Flavors
                .Include(flavor => flavor.JoinEntities)
                .ThenInclude(join => join.Treat)
                .FirstOrDefault(flavor => flavor.FlavorId == id);
            return View(thisFlavor);
        }

        public ActionResult Edit(int id)
        {
            var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
            ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
            return View(thisFlavor);
        }

        [HttpPost]
        public ActionResult Edit(Flavor flavor, int TreatId)
        {
            if (TreatId != 0)
            {
                _db.FlavorTreat.Add(
                    new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId }
                );
            }
            _db.Entry(flavor).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddTreat(int id)
        {
            var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
            ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
            return View(thisFlavor);
        }

        [HttpPost]
        public ActionResult AddTreat(Flavor flavor, int TreatId)
        {
            if (TreatId != 0)
            {
                _db.FlavorTreat.Add(
                    new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId }
                );
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //         public ActionResult Delete(int id)
        //         {
        //             var thisRestaurant = _db.Restaurants.FirstOrDefault(
        //                 restaurant => restaurant.RestaurantId == id
        //             );
        //             return View(thisRestaurant);
        //         }

        //         [HttpPost, ActionName("Delete")]
        //         public ActionResult DeleteConfirmed(int id)
        //         {
        //             var thisRestaurant = _db.Restaurants.FirstOrDefault(
        //                 restaurant => restaurant.RestaurantId == id
        //             );
        //             _db.Restaurants.Remove(thisRestaurant);
        //             _db.SaveChanges();
        //             return RedirectToAction("Index");
        //         }
    }
}
