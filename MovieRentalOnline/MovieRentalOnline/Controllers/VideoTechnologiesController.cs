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
    public class VideoTechnologiesController : Controller
    {
        private RentalContext db = new RentalContext();

        // GET: VideoTechnologies
        public async Task<ActionResult> Index()
        {
            return View(await db.VideoTechnologys.ToListAsync());
        }

        // GET: VideoTechnologies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoTechnology videoTechnology = await db.VideoTechnologys.FindAsync(id);
            if (videoTechnology == null)
            {
                return HttpNotFound();
            }
            return View(videoTechnology);
        }

        // GET: VideoTechnologies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VideoTechnologies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VideoTechnologyId,Name,Description")] VideoTechnology videoTechnology)
        {
            if (ModelState.IsValid)
            {
                db.VideoTechnologys.Add(videoTechnology);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(videoTechnology);
        }

        // GET: VideoTechnologies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoTechnology videoTechnology = await db.VideoTechnologys.FindAsync(id);
            if (videoTechnology == null)
            {
                return HttpNotFound();
            }
            return View(videoTechnology);
        }

        // POST: VideoTechnologies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VideoTechnologyId,Name,Description")] VideoTechnology videoTechnology)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videoTechnology).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(videoTechnology);
        }

        // GET: VideoTechnologies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoTechnology videoTechnology = await db.VideoTechnologys.FindAsync(id);
            if (videoTechnology == null)
            {
                return HttpNotFound();
            }
            return View(videoTechnology);
        }

        // POST: VideoTechnologies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VideoTechnology videoTechnology = await db.VideoTechnologys.FindAsync(id);
            db.VideoTechnologys.Remove(videoTechnology);
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
