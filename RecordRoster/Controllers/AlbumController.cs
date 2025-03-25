using System;
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

        // GET: Library view
        public ActionResult Library()
        {
            return View(_context.Albums.ToList());
        }

        // GET: Add album form view
        public ActionResult Add()
        {
            return View();
        }

        // GET: Album detail view
        public ActionResult Details()
        {
            // Assume ID in route
            var val = RouteData.Values["id"];
            if (val == null)
            {
                // Broken route, send back to library
                return RedirectToAction("Library");
            }

            int albumId = int.Parse(val.ToString());
            Album album = _context.Albums.Find(albumId);
            if (album == null)
            {
                // Album does not exist, send back to library
                return RedirectToAction("Library");
            }

            return View(album);
        }


        // Edit Album Placeholder
        [HttpPost]
        public ActionResult EditAlbum(Album album)
        {
            var existingAlbum = _context.Albums.Find(album.Id);
            if (existingAlbum != null)
            {
                existingAlbum.Title = album.Title;
                existingAlbum.Artist = album.Artist;
                existingAlbum.ReleaseYear = album.ReleaseYear;
                existingAlbum.Cover = album.Cover;
                _context.SaveChanges();
                return RedirectToAction("Library");
            }
            return RedirectToAction("Library");
        }


        // Delete Album
        // TODO: Use TempData to send success/fail message to library page
        [HttpPost]
        public ActionResult DeleteAlbum(int id)
        {
            var album = _context.Albums.Find(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
                _context.SaveChanges();
                return RedirectToAction("Library");
            }

            return RedirectToAction("Library");
        }


        // Add Album
        [HttpPost]
        public ActionResult AddAlbum(Album album)
        {
            _context.Albums.Add(album);
            _context.SaveChanges();
            return RedirectToAction("Library");
        }
    }
}