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
        // GET: Album
        public ActionResult Index()
        {
            var albums = _context.Albums.ToList();
            ViewBag.RandomAlbum = albums.OrderBy(a => Guid.NewGuid()).FirstOrDefault();
            return View();
        }

        public ActionResult Library()
        {
            ViewBag.Albums = _context.Albums.ToList();
            return View();
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
                existingAlbum.Year = album.Year;
                existingAlbum.ImageUrl = album.ImageUrl;
                _context.SaveChanges();
                return Json(new { message = "Album updated Successfully" });
            }
            return Json(new { message = "Album not found" });
        }


        // Delete Album Placeholder
        [HttpPost]
        public ActionResult DeleteAlbum(int id)
        {
            var album = _context.Albums.Find(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
                _context.SaveChanges();
                return Json(new { message = "Album deleted Successfully" });
            }
            return Json(new { message = "Album not found" });
        }


        // Add Album Placeholder
        [HttpPost]
        public ActionResult AddAlbum(Album album)
        {
            _context.Albums.Add(album);
            _context.SaveChanges();
            return Json(new { message = "Album added Successfully" });
        }
    }
}