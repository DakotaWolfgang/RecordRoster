using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordRoster.Models
{
    public class Song
    {
        // Primary key should be composite of AlbumId and TrackNumber
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Album ID is required")]
        public int AlbumId { get; set; }

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Track number is required")]
        public int TrackNumber { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
    }
}