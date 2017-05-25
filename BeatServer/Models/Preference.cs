using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using FluentNHibernate.Mapping;

namespace BeatServer.Models
{
    //[DataContract]
    //public class Preference : PresonalZoneListItem
    //{
    //    public override int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    //    public override string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //}

    [DataContract]
    public class Preference
    {

        [Key]
        [DataMember]
        public virtual int Id { get; set; }

        [Required]
        [DataMember]
        public virtual string Name { get; set; }

    }

    public class PreferenceMap : ClassMap<Preference>
    {
        public PreferenceMap()
        {
            Table("preferences");

            Id(x => x.Id).GeneratedBy.Identity();
            
            Map(x => x.Name);

        }
    }

}