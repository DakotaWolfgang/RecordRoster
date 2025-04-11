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
            try
            {

                if (!ModelState.IsValid)
                {
                    return View(song);
                }

                // Checking for Duplicates
                var duplicate = _context.Songs.FirstOrDefault(s =>
                s.AlbumId == song.AlbumId && s.TrackNumber == song.TrackNumber);

                if (duplicate != null)
                {
                    TempData["Error"] = $"Track #{song.TrackNumber} already exists for this album.";
                    return RedirectToAction("Details", "Album", new { id = song.AlbumId });
                }

                // Add new track to DB if there are no duplicates
                _context.Songs.Add(song);
                _context.SaveChanges();

                TempData["Message"] = $"Track '{song.Title}' (#{song.TrackNumber}) added successfully!";

                // Redirect to the album's details page so you can see the updated track list
                return RedirectToAction("Details", "Album", new { id = song.AlbumId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error adding track: " + ex.Message;
                return RedirectToAction("Details", "Album", new { id = song.AlbumId });
            }
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(updatedSong);
                }

                // EF can track changes if we retrieve the existing record
                var existingSong = _context.Songs.Find(updatedSong.AlbumId, updatedSong.TrackNumber);
                if (existingSong == null)
                {
                    return RedirectToAction("Library", "Album");
                }

                // Update only the fields that can change
                existingSong.Title = updatedSong.Title;
                _context.SaveChanges();

                TempData["Message"] = $"Track #{existingSong.TrackNumber} updated successfully!";

                // After updating a song, redirect to album details
                return RedirectToAction("Details", "Album", new { id = existingSong.AlbumId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error updating track: " + ex.Message;
                return RedirectToAction("Detail", "Album", new { id = updatedSong.AlbumId });
            }
        }

        // Delete Song
        [HttpPost]
        public ActionResult Delete(int albumId, int trackNumber)
        {
            try
            {
                var song = _context.Songs.Find(albumId, trackNumber);
                if (song == null)
                {
                    TempData["Error"] = "Track to delete not found.";
                    return RedirectToAction("Details", "Album", new { id = albumId });
                }

                _context.Songs.Remove(song);
                _context.SaveChanges();

                TempData["Message"] = $"Track #{trackNumber} deleted successfully!";
                return RedirectToAction("Details", "Album", new { id = albumId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error deleting track: " + ex.Message;
                return RedirectToAction("Detail", "Album", new { id = albumId });
            }
        }
    }
}