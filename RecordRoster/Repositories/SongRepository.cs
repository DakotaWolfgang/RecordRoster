using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecordRoster.DataAccessLayer;
using RecordRoster.Models;

namespace RecordRoster.Repositories
{
	public class SongRepository : ISongRepository
	{
		private readonly RecordRosterDb _context = new RecordRosterDb();

		public List<Song> GetSongsByAlbum(int albumId)
		{
			return _context.Songs
				.Where(s => s.AlbumId == albumId)
				.OrderBy(s => s.TrackNumber)
				.ToList();
		}

		public Song GetSong(int albumId, int trackNumber)
		{
			return _context.Songs.Find(albumId, trackNumber);
		}

		public void AddSong(Song song)
		{
			_context.Songs.Add(song);
			_context.SaveChanges();
		}

		public void UpdateSong(Song song)
		{
			var existing = _context.Songs.Find(song.AlbumId, song.TrackNumber);
			if (existing != null)
			{
				existing.Title = song.Title;
				_context.SaveChanges();
			}
		}

		public void DeleteSong(int albumId, int trackNumber)
		{
			var song = _context.Songs.Find(albumId, trackNumber);
			if (song != null)
			{
				_context.Songs.Remove(song);
				_context.SaveChanges();
			}
		}

		public bool IsDuplicateTrack(int albumId, int trackNumber)
		{
			return _context.Songs
				.Any(s => s.AlbumId == albumId && s.TrackNumber == trackNumber);
		}

		public List<Song> GetAllSongs()
		{
			return _context.Songs.ToList();
		}
	}
}