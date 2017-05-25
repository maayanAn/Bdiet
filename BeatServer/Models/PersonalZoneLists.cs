using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using FluentNHibernate.Mapping;

namespace BeatServer.Models
{
    
     public class PersonalZoneLists
    {        
    
        
         public List<Allergy> allergiesList { get; set; }

      
       
        public List<Preference> preferencesList { get; set; }
        
    }
}