using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace EVJ.Models
{
    public class ContactUsViewModel
    {
        [Required]
        public string Name { get; set; }
		[Required]
		public string Organization { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Comment { get; set; }
    }
}