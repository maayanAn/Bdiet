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
    public class Mealtype
    {
        [Key]
        [DataMember]
        public virtual int Id { get; set; }

        [Required]
        [DataMember]
        public virtual string MealType { get; set; }
    }

    public class MealtypeMap : ClassMap<Mealtype>
    {
        public MealtypeMap()
        {
            Table("mealtypes");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.MealType);

        }
    }
}