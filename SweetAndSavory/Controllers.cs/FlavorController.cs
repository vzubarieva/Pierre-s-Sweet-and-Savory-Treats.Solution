using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace SweetAndSavory.Controllers
{
    public class FlavorsController : Controller
    {
        private readonly SweetAndSavoryContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public FlavorsController(
            UserManager<ApplicationUser> userManager,
            SweetAndSavoryContext db,
            ILogger<FlavorsController> logger
        )
        {
            _userManager = userManager;
            _db = db;
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View(_db.Flavors.ToList());
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Flavor flavor, int TreatId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            flavor.User = currentUser;
            _db.Flavors.Add(flavor);
            _db.SaveChanges();
            if (TreatId != 0)
            {
                _db.FlavorTreat.Add(
                    new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId }
                );
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var thisFlavor = _db.Flavors
                .Include(treat => treat.User)
                .Include(flavor => flavor.JoinEntities)
                .ThenInclude(join => join.Treat)
                .FirstOrDefault(flavor => flavor.FlavorId == id);

            ViewBag.isOwner = thisFlavor.User.Id == currentUser?.Id;
            return View(thisFlavor);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var thisFlavor = _db.Flavors
                .Include(flavor => flavor.User)
                .FirstOrDefault(flavor => flavor.FlavorId == id);
            ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
            return View(thisFlavor);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(Flavor flavor, int TreatId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);

            if (flavor.UserId == currentUser.Id)
            {
                if (TreatId != 0)
                {
                    _db.FlavorTreat.Add(
                        new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId }
                    );
                }
                _db.Entry(flavor).State = EntityState.Modified;
                _db.SaveChanges();
            }
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

        public ActionResult Delete(int id)
        {
            var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
            return View(thisFlavor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
            _db.Flavors.Remove(thisFlavor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteTreat(int joinId)
        {
            var joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
            _db.FlavorTreat.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
