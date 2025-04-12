using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecordRoster.Repositories;
using RecordRoster.Models;
using RecordRoster.DataAccessLayer;
using System.Security.Cryptography.X509Certificates;

namespace RecordRoster.Repositories
{
	public class AlbumRepository : IAlbumRepository
	{
		private readonly RecordRosterDb _context = new RecordRosterDb();

		public List<Album> GetAllAlbums()
		{
			return _context.Albums.ToList();
		}

		public Album GetAlbumById(int id)
		{
			return _context.Albums
				.Include("Songs")
				.FirstOrDefault(a => a.Id == id);
		}

		public void AddAlbum(Album album)
		{
			_context.Albums.Add(album);
			_context.SaveChanges();
		}

		public void UpdateAlbum(Album album)
		{
			var existing = _context.Albums.Find(album.Id);
			if (existing != null)
			{
				existing.Title = album.Title;
				existing.Artist = album.Artist;
				existing.ReleaseYear = album.ReleaseYear;
				existing.Cover = album.Cover;
				_context.SaveChanges();
			}

		}

		public void DeleteAlbum(int id)
		{
			var album = _context.Albums.Find(id);
			if (album != null)
			{
				_context.Albums.Remove(album);
				_context.SaveChanges();
			}
		}
	}
}