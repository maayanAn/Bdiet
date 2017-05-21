using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using FluentNHibernate.Mapping;

namespace BeatServer.Models
{
    [DataContract]
    public class Allergy
    {
        [Key]
        [DataMember]
        public virtual int Id { get; set; }

        [Required]
        [DataMember]
        public virtual string Name { get; set; }
    }

    public class AllergyMap : ClassMap<Allergy>
    {
        public AllergyMap()
        {
            Table("allergies");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);

        }
    }

}