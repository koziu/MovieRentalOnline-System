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
    public class PhysicalProductsController : Controller
    {
        private RentalContext db = new RentalContext();

        // GET: PhysicalProducts
        public async Task<ActionResult> Index()
        {
            var physicalProducts = db.PhysicalProducts.Include(p => p.Product);
            return View(await physicalProducts.ToListAsync());
        }

        // GET: PhysicalProducts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicalProduct physicalProduct = await db.PhysicalProducts.FindAsync(id);
            if (physicalProduct == null)
            {
                return HttpNotFound();
            }
            return View(physicalProduct);
        }

        // GET: PhysicalProducts/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: PhysicalProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PhysicalProductId,ProductId,Description,IsHidden")] PhysicalProduct physicalProduct)
        {
            if (ModelState.IsValid)
            {
                db.PhysicalProducts.Add(physicalProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductId", physicalProduct.ProductId);
            return View(physicalProduct);
        }

        // GET: PhysicalProducts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicalProduct physicalProduct = await db.PhysicalProducts.FindAsync(id);
            if (physicalProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductId", physicalProduct.ProductId);
            return View(physicalProduct);
        }

        // POST: PhysicalProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PhysicalProductId,ProductId,Description,IsHidden")] PhysicalProduct physicalProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(physicalProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductId", physicalProduct.ProductId);
            return View(physicalProduct);
        }

        // GET: PhysicalProducts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicalProduct physicalProduct = await db.PhysicalProducts.FindAsync(id);
            if (physicalProduct == null)
            {
                return HttpNotFound();
            }
            return View(physicalProduct);
        }

        // POST: PhysicalProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PhysicalProduct physicalProduct = await db.PhysicalProducts.FindAsync(id);
            db.PhysicalProducts.Remove(physicalProduct);
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
