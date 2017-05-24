using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BeatServer.Models
{
    public class PersonalZone
    {
        [Required]
        public virtual List<string> userAllergies { get; set; }

        [Required]
        public virtual List<string> userPreferences { get; set; }

        [Required]
        public virtual int userId { get; set; }
    }
}