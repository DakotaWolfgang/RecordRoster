using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RecordRoster.Models;
using RecordRoster.DataAccessLayer;
using RecordRoster.Repositories;

namespace RecordRoster.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository _albumRepo = new AlbumRepository();
        private readonly ISongRepository _songRepo = new SongRepository();

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

            return View(_albumRepo.GetAllAlbums());
        }

        // GET: /Album/Add form view
        public ActionResult Add()
        {
            return View();
        }

        // GET: /Album/Update form view
        public ActionResult Update(int id)
        {
            var album = _albumRepo.GetAlbumById(id);
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
            var album = _albumRepo.GetAlbumById(id);
            var trackList = _songRepo.GetSongsByAlbum(id);

            if (album == null)
            {
                // Album does not exist, send back to library
                return RedirectToAction("Library");
            }

            return View(new AlbumDetails
            {
                Album = album,
                TrackList = trackList
            });
        }

        // POST: /Album/Add
        [HttpPost]
        public ActionResult Add(Album album)
        {
            try
            {
                _albumRepo.AddAlbum(album);
                TempData["Message"] = $"Album '{album.Title}' successfully added.";
            }
            catch
            {
                TempData["Error"] = "Album could not be added. Check inputs and try again.";
                return RedirectToAction("Library");
            }

  
            return RedirectToAction("Library");
        }

        // POST: /Album/Update
        [HttpPost]
        public ActionResult Update(Album album)
        {
            var existing = _albumRepo.GetAlbumById(album.Id);
            if (existing != null)
            {
                _albumRepo.UpdateAlbum(album);
                TempData["Message"] = $"Album '{album.Title}' successfully updated.";
            }
            else
            {
                TempData["Error"] = "Album was not found.";
            }

            return RedirectToAction("Library");
        }

        // GET: /Album/Delete
        public ActionResult Delete(int id)
        {
            var album = _albumRepo.GetAlbumById(id);
            if (album == null)
            {
                TempData["Error"] = "Album was not found.";
                return RedirectToAction("Library");
            }
                
            // Remove all songs associated with the album
            var songs = _songRepo.GetSongsByAlbum(id);
            foreach (var song in songs)
            {
                _songRepo.DeleteSong(song.AlbumId, song.TrackNumber);
            }

            _albumRepo.DeleteAlbum(id);
            TempData["Message"] = $"Album '{album.Title}' successfully deleted.";
            return RedirectToAction("Library");
        }

        // GET: /Album/PopulateWithSampleData
        public ActionResult PopulateWithSampleData()
        {
            // If any albums exist in the database, do not populate sample data
            if (_albumRepo.GetAllAlbums().Any())
            {
                TempData["Error"] = "Data already exists.";
                return RedirectToAction("Library");
            }

            List<Album> sampleAlbums = new List<Album>
            {
                new Album { Title = "Abbey Road", Artist = "The Beatles", ReleaseYear = 1969, Cover = "https://upload.wikimedia.org/wikipedia/en/4/42/Beatles_-_Abbey_Road.jpg" },
                new Album { Title = "Thriller", Artist = "Michael Jackson", ReleaseYear = 1982, Cover = "https://upload.wikimedia.org/wikipedia/en/5/55/Michael_Jackson_-_Thriller.png" },
                new Album { Title = "Dire Straits", Artist = "Dire Straits", ReleaseYear = 1978, Cover = "https://upload.wikimedia.org/wikipedia/en/1/15/DS_Dire_Straits.jpg" },
                new Album { Title = "Hotel California", Artist = "Eagles", ReleaseYear = 1976, Cover = "https://upload.wikimedia.org/wikipedia/en/4/49/Hotelcalifornia.jpg" },
                new Album { Title = "Led Zeppelin IV", Artist = "Led Zeppelin", ReleaseYear = 1971, Cover = "https://upload.wikimedia.org/wikipedia/en/2/26/Led_Zeppelin_-_Led_Zeppelin_IV.jpg" },
            };

            // Add sample albums to the database
            foreach (var album in sampleAlbums)
            {
                _albumRepo.AddAlbum(album);
            }
            


            // Get IDs of the added albums since increment/identity may not always be sequential
            var albumsInDb = _albumRepo.GetAllAlbums();
            var addedAlbumIds = albumsInDb
                .Where(a => sampleAlbums.Any(s => s.Title == a.Title && s.Artist == a.Artist))
                .OrderBy(a => a.Title)
                .Select(a => a.Id)
                .ToList();

            // Create sample songs for each album
            List<Song> sampleSongs = new List<Song>
            {
                // Abbey Road
                new Song { Title = "Come Together", AlbumId = addedAlbumIds[0], TrackNumber = 1 },
                new Song { Title = "Something", AlbumId = addedAlbumIds[0], TrackNumber = 2 },
                new Song { Title = "Maxwell's Silver Hammer", AlbumId = addedAlbumIds[0], TrackNumber = 3 },
                new Song { Title = "Oh! Darling", AlbumId = addedAlbumIds[0], TrackNumber = 4 },
                new Song { Title = "Octopus's Garden", AlbumId = addedAlbumIds[0], TrackNumber = 5 },
                new Song { Title = "I Want You (She's So Heavy)", AlbumId = addedAlbumIds[0], TrackNumber = 6 },
                new Song { Title = "Here Comes the Sun", AlbumId = addedAlbumIds[0], TrackNumber = 7 },
                // Thriller
                new Song { Title = "Wanna Be Startin' Somethin'", AlbumId = addedAlbumIds[1], TrackNumber = 1 },
                new Song { Title = "Baby Be Mine", AlbumId = addedAlbumIds[1], TrackNumber = 2 },
                new Song { Title = "The Girl Is Mine", AlbumId = addedAlbumIds[1], TrackNumber = 3 },
                new Song { Title = "Thriller", AlbumId = addedAlbumIds[1], TrackNumber = 4 },
                new Song { Title = "Beat It", AlbumId = addedAlbumIds[1], TrackNumber = 5 },
                new Song { Title = "Billie Jean", AlbumId = addedAlbumIds[1], TrackNumber = 6 },
                new Song { Title = "Human Nature", AlbumId = addedAlbumIds[1], TrackNumber = 7 },
                // Dire Straits
                new Song { Title = "Down to the Waterline", AlbumId = addedAlbumIds[2], TrackNumber = 1 },
                new Song { Title = "Water of Love", AlbumId = addedAlbumIds[2], TrackNumber = 2 },
                new Song { Title = "Setting Me Up", AlbumId = addedAlbumIds[2], TrackNumber = 3 },
                new Song { Title = "Six Blade Knife", AlbumId = addedAlbumIds[2], TrackNumber = 4 },
                new Song { Title = "Southbound Again", AlbumId = addedAlbumIds[2], TrackNumber = 5 },
                new Song { Title = "Sultans of Swing", AlbumId = addedAlbumIds[2], TrackNumber = 6 },
                new Song { Title = "In the Gallery", AlbumId = addedAlbumIds[2], TrackNumber = 7 },
                // Hotel California
                new Song { Title = "Hotel California", AlbumId = addedAlbumIds[3], TrackNumber = 1 },
                new Song { Title = "New Kid in Town", AlbumId = addedAlbumIds[3], TrackNumber = 2 },
                new Song { Title = "Life in the Fast Lane", AlbumId = addedAlbumIds[3], TrackNumber = 3 },
                new Song { Title = "Wasted Time", AlbumId = addedAlbumIds[3], TrackNumber = 4 },
                new Song { Title = "Wasted Time (Reprise)", AlbumId = addedAlbumIds[3], TrackNumber = 5 },
                new Song { Title = "Victim of Love", AlbumId = addedAlbumIds[3], TrackNumber = 6 },
                new Song { Title = "Pretty Maids All in a Row", AlbumId = addedAlbumIds[3], TrackNumber = 7 },
                // Led Zeppelin IV
                new Song { Title = "Black Dog", AlbumId = addedAlbumIds[4], TrackNumber = 1 },
                new Song { Title = "Rock and Roll", AlbumId = addedAlbumIds[4], TrackNumber = 2 },
                new Song { Title = "The Battle of Evermore", AlbumId = addedAlbumIds[4], TrackNumber = 3 },
                new Song { Title = "Stairway to Heaven", AlbumId = addedAlbumIds[4], TrackNumber = 4 },
                new Song { Title = "Misty Mountain Hop", AlbumId = addedAlbumIds[4], TrackNumber = 5 },
                new Song { Title = "Four Sticks", AlbumId = addedAlbumIds[4], TrackNumber = 6 },
                new Song { Title = "Going to California", AlbumId = addedAlbumIds[4], TrackNumber = 7 },
            };

            // Add sample songs to the database
            foreach (var song in sampleSongs)
            {
                _songRepo.AddSong(song);
            }

            TempData["Message"] = "Sample data successfully added.";

            return RedirectToAction("Library");
        }
    }
}