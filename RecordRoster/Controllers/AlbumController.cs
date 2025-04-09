using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using RecordRoster.Models;
using RecordRoster.DataAccessLayer;

namespace RecordRoster.Controllers
{
    public class AlbumController : Controller
    {
        private readonly RecordRosterDb _context = new RecordRosterDb();

        // GET: /Album/Library view
        public ActionResult Library()
        {
            if (TempData["Error"] != null)
            {
                ViewData["Error"] = TempData["Error"];
            }
            else if (TempData["Message"] != null) {
                ViewData["Message"] = TempData["Message"];
            }

            return View(_context.Albums.ToList());
        }

        // GET: /Album/Add form view
        public ActionResult Add()
        {
            return View();
        }

        // GET: /Album/Update form view
        public ActionResult Update(int id)
        {
            Album album = _context.Albums.Find(id);
            if (album == null)
            {
                // Album does not exist, send back to library
                return RedirectToAction("Library");
            }

            return View(album);
        }

        // GET: /Album/Details
        public ActionResult Details(int id)
        {
            Album album = _context.Albums
                .Include(a => a.Songs)
                .FirstOrDefault(a => a.Id == id);

            if (album == null)
            {
                // Album does not exist, send back to library
                return RedirectToAction("Library");
            }

            return View(album);
        }

        // POST: /Album/Add
        [HttpPost]
        public ActionResult Add(Album album)
        {
            try
            {
                _context.Albums.Add(album);
                _context.SaveChanges();
            }
            catch
            {
                TempData["Error"] = "Album could not be added. Check inputs and try again.";
                return RedirectToAction("Library");
            }

            TempData["Message"] = $"Album '{album.Title}' successfully added.";
            return RedirectToAction("Library");
        }

        // POST: /Album/Update
        [HttpPost]
        public ActionResult Update(Album album)
        {
            var existingAlbum = _context.Albums.Find(album.Id);
            if (existingAlbum != null)
            {
                existingAlbum.Title = album.Title;
                existingAlbum.Artist = album.Artist;
                existingAlbum.ReleaseYear = album.ReleaseYear;
                existingAlbum.Cover = album.Cover;
                _context.SaveChanges();

                TempData["Message"] = $"Album '{album.Title}' successfully updated.";
                return RedirectToAction("Library");
            }

            TempData["Error"] = "Album was not found.";
            return RedirectToAction("Library");
        }

        // GET: /Album/Delete
        public ActionResult Delete(int id)
        {
            var album = _context.Albums.Find(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
                _context.SaveChanges();

                TempData["Message"] = $"Album '{album.Title}' successfully deleted.";
                return RedirectToAction("Library");
            }

            TempData["Error"] = "Album was not found.";
            return RedirectToAction("Library");
        }
    }
}