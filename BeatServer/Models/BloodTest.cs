using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using FluentNHibernate.Mapping;

namespace BeatServer.Models
{

    public class BloodTest
    {
        public virtual string component_name_english { get; set; }

        public virtual string value { get; set; }

        public virtual int min_val { get; set; }

        public virtual int max_val { get; set; }

    }
}