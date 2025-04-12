using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecordRoster.Models;
using RecordRoster.Repositories;

namespace RecordRoster.Repositories
{
	public interface ISongRepository
	{
		List<Song> GetAllSongs();
		List<Song> GetSongsByAlbum(int albumId);
		Song GetSong(int albumId, int trackNumber);
		void AddSong(Song song);
		void UpdateSong(Song song);
		void DeleteSong(int albumId, int trackNumber);
		bool IsDuplicateTrack(int albumId, int trackNumber);
	}
}