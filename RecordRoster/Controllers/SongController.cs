using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecordRoster.DataAccessLayer;
using RecordRoster.Models;
using System.Data.Entity;

namespace RecordRoster.Controllers
{
    public class SongController : Controller
    {
        // GET: Song
        private readonly RecordRosterDb _context = new RecordRosterDb();

        public ActionResult Library()
        {
            var songs = _context.Songs.ToList();
            return View(songs);
        }
        
        public ActionResult Add(int albumId)
        {
            var song = new Song { AlbumId = albumId };
            return View(song);
        }


        // Create Song
        [HttpPost]
        public ActionResult Add(Song song)
        {
            if (!ModelState.IsValid)
            {
                return View(song);
            }

            // Add new track to DB
            _context.Songs.Add(song);
            _context.SaveChanges();

            // Redirect to the album's details page so you can see the updated track list
            return RedirectToAction("Details", "Album", new { id = song.AlbumId});
        }

        public ActionResult Update(int albumId, int trackNumber)
        {
            var song = _context.Songs.Find(albumId, trackNumber);
            if (song == null)
            {
                return RedirectToAction("Library", "Album");
            }
            return View(song);
        }

        // Edit Song
        [HttpPost]
        public ActionResult Update(Song updatedSong)
        {
            if(!ModelState.IsValid)
            {
                return View(updatedSong);
            }

            // EF can track changes if we retrieve the existing record
            var existingSong = _context.Songs.Find(updatedSong.AlbumId, updatedSong.TrackNumber);
            if(existingSong == null)
            {
                return RedirectToAction("Library", "Album");
            }

            // Update only the fields that can change
            existingSong.Title = updatedSong.Title;
            _context.SaveChanges();

            // After updating a song, redirect to album details
            return RedirectToAction("Details", "Album", new { id = existingSong.AlbumId});
        }

        // Delete Song
        [HttpPost]
        public ActionResult Delete(int albumId, int trackNumber)
        {
            var song = _context.Songs.Find(albumId, trackNumber);
            if(song != null )
            {
                _context.Songs.Remove(song);
                _context.SaveChanges();
            }

            return RedirectToAction("Details", "Album", new { id = albumId });
        }
    }
}