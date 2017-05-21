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
    public class Foodgroup
    {
        [Key]
        [DataMember]
        public virtual int Id { get; set; }

        [Required]
        [DataMember]
        public virtual string FoodGroup { get; set; }
    }

    public class FoodgroupMap : ClassMap<Foodgroup>
    {
        public FoodgroupMap()
        {
            Table("foodgroups");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.FoodGroup);

        }
    }

}