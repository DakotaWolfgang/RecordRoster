using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordRoster.Models
{
	public class Album
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required(ErrorMessage = "Title is required")]
		public string Title { get; set; }

        [Required(ErrorMessage = "Artist is required")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "Release year is required")]
        public int ReleaseYear { get; set; }

        [Display(Name = "Album artwork URL")]
        public string Cover { get; set; }
    }
}