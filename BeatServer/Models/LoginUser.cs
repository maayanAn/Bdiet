using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeatServer.Models
{
    public class LoginUser
    {
        [Required]
        public virtual string email { get; set; }

        [Required]
        public virtual string password { get; set; }
    }
}