using System;
using System.Linq;
using System.Web.Mvc;
using RecordRoster.DataAccessLayer;
using RecordRoster.Models;
using RecordRoster.Repositories;


namespace RecordRoster.Controllers
{
    public class SongController : Controller
    {
        // GET: Song
        private readonly ISongRepository _songRepo = new SongRepository();

        public ActionResult Library()
        {
            var songs = _songRepo.GetAllSongs();
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

                if (_songRepo.IsDuplicateTrack(song.AlbumId, song.TrackNumber))
                {
                    TempData["Error"] = $"Track #{song.TrackNumber} already exists for this album.";
                    return RedirectToAction("Details", "Album", new { id = song.AlbumId });
                }

                // Add new track to DB if there are no duplicates
                _songRepo.AddSong(song);
                TempData["Message"] = $"Track '{song.Title}' (#{song.TrackNumber}) added successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error adding track: " + ex.Message;
                return RedirectToAction("Details", "Album", new { id = song.AlbumId });
            }

            return RedirectToAction("Details", "Album", new { id = song.AlbumId });
        }

        public ActionResult Update(int albumId, int trackNumber)
        {
            var song = _songRepo.GetSong(albumId, trackNumber);
            if (song == null)
            {
                TempData["Error"] = "Track not found.";
                return RedirectToAction("Library", "Album", new { id = albumId});
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
                var existingSong = _songRepo.GetSong(updatedSong.AlbumId, updatedSong.TrackNumber);
                if (existingSong == null)
                {
                    TempData["Error"] = "Track not found.";
                    return RedirectToAction("Library", "Album", new { id = updatedSong.AlbumId });
                }

                // Update only the fields that can change
                _songRepo.UpdateSong(updatedSong);
                TempData["Message"] = $"Track #{existingSong.TrackNumber} updated successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error updating track: " + ex.Message;
            }

            return RedirectToAction("Details", "Album", new { id = updatedSong.AlbumId });
        }

        // Delete Song
        [HttpPost]
        public ActionResult Delete(int albumId, int trackNumber)
        {
            try
            {
                var song = _songRepo.GetSong(albumId, trackNumber);
                if (song == null)
                {
                    TempData["Error"] = "Track to delete not found.";
                    return RedirectToAction("Details", "Album", new { id = albumId });
                }

                _songRepo.DeleteSong(albumId, trackNumber);
                TempData["Message"] = $"Track #{trackNumber} deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error deleting track: " + ex.Message;
            }

            return RedirectToAction("Details", "Album", new { id = albumId });
        }
    }
}