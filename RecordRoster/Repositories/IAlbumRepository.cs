using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecordRoster.Models;

namespace RecordRoster.Repositories
{
	public interface IAlbumRepository
	{
		List<Album> GetAllAlbums();
		Album GetAlbumById(int id);
		void AddAlbum(Album album);
		void UpdateAlbum(Album album);
		void DeleteAlbum(int id);
	}
}