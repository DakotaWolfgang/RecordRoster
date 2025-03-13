using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RecordRoster.Models
{
	public class Album
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Title is required")]
		public string Title { get; set; }

        [Required(ErrorMessage = "Artist is required")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "Year Released is required")]
        public string Year { get; set; }

        [Display(Name = "Album artwork URL")]
        public string ImageUrl { get; set; }
    }
}