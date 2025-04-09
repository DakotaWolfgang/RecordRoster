using System.Collections.Generic;

namespace RecordRoster.Models
{
    public class AlbumDetails
    {
        public Album Album { get; set; }
        public List<Song> TrackList { get; set; }
    }
}