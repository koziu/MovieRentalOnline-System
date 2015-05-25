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
    public class StorageMediumsController : Controller
    {
        private RentalContext db = new RentalContext();

        // GET: StorageMediums
        public async Task<ActionResult> Index()
        {
            return View(await db.StorageMediums.ToListAsync());
        }

        // GET: StorageMediums/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageMedium storageMedium = await db.StorageMediums.FindAsync(id);
            if (storageMedium == null)
            {
                return HttpNotFound();
            }
            return View(storageMedium);
        }

        // GET: StorageMediums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StorageMediums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StorageMediumId,Name,Description")] StorageMedium storageMedium)
        {
            if (ModelState.IsValid)
            {
                db.StorageMediums.Add(storageMedium);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(storageMedium);
        }

        // GET: StorageMediums/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageMedium storageMedium = await db.StorageMediums.FindAsync(id);
            if (storageMedium == null)
            {
                return HttpNotFound();
            }
            return View(storageMedium);
        }

        // POST: StorageMediums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StorageMediumId,Name,Description")] StorageMedium storageMedium)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storageMedium).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(storageMedium);
        }

        // GET: StorageMediums/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageMedium storageMedium = await db.StorageMediums.FindAsync(id);
            if (storageMedium == null)
            {
                return HttpNotFound();
            }
            return View(storageMedium);
        }

        // POST: StorageMediums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StorageMedium storageMedium = await db.StorageMediums.FindAsync(id);
            db.StorageMediums.Remove(storageMedium);
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
