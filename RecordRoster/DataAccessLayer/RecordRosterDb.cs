using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecordRoster.Models;
using System.Data.Entity;

namespace RecordRoster.DataAccessLayer
{
	public class RecordRosterDb : DbContext
	{
		public RecordRosterDb() : base("name=RecordRosterDb") {
			Database.SetInitializer(new CreateDatabaseIfNotExists<RecordRosterDb>());
		}

		public DbSet<Album> Albums { get; set; }
		public DbSet<Song> Songs { get; set; }
	}
}