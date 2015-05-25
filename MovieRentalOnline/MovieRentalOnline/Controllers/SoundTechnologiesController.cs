using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieRentalOnline.DAL;
using MovieRentalOnline.Models;

namespace MovieRentalOnline.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SoundTechnologiesController : Controller
    {
        private RentalContext db = new RentalContext();

        // GET: SoundTechnologies
        public async Task<ActionResult> Index()
        {
            return View(await db.SoundTechnologys.ToListAsync());
        }

        // GET: SoundTechnologies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoundTechnology soundTechnology = await db.SoundTechnologys.FindAsync(id);
            if (soundTechnology == null)
            {
                return HttpNotFound();
            }
            return View(soundTechnology);
        }

        // GET: SoundTechnologies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SoundTechnologies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SoundTechnologyId,Name,Description")] SoundTechnology soundTechnology)
        {
            if (ModelState.IsValid)
            {
                db.SoundTechnologys.Add(soundTechnology);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(soundTechnology);
        }

        // GET: SoundTechnologies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoundTechnology soundTechnology = await db.SoundTechnologys.FindAsync(id);
            if (soundTechnology == null)
            {
                return HttpNotFound();
            }
            return View(soundTechnology);
        }

        // POST: SoundTechnologies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SoundTechnologyId,Name,Description")] SoundTechnology soundTechnology)
        {
            if (ModelState.IsValid)
            {
                db.Entry(soundTechnology).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(soundTechnology);
        }

        // GET: SoundTechnologies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoundTechnology soundTechnology = await db.SoundTechnologys.FindAsync(id);
            if (soundTechnology == null)
            {
                return HttpNotFound();
            }
            return View(soundTechnology);
        }

        // POST: SoundTechnologies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SoundTechnology soundTechnology = await db.SoundTechnologys.FindAsync(id);
            db.SoundTechnologys.Remove(soundTechnology);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
