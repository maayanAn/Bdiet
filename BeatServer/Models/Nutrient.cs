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
    public class Nutrient
    {
        [Key]
        [DataMember]
        public virtual int Id { get; set; }


        [Required]
        [DataMember]
        public virtual string Name { get; set; }

        [Required]
        [DataMember]
        public virtual double MinVal { get; set; }

        [Required]
        [DataMember]
        public virtual double MaxVal { get; set; }
    }

    public class NutrientMap : ClassMap<Nutrient>
    {
        public NutrientMap()
        {
            Table("nutrients");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);
            Map(x => x.MinVal);
            Map(x => x.MaxVal);

        }
    }
}