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
    public class Menu
    {
        [Key]
        [DataMember]
        public virtual int MenuId { get; set; }

        [Required]
        [DataMember]
        public virtual User User { get; set; }

        [Required]
        [DataMember]
        public virtual string Breakfast { get; set; }

        [Required]
        [DataMember]
        public virtual string MidMorning { get; set; }

        [Required]
        [DataMember]
        public virtual string Lunch { get; set; }

        [Required]
        [DataMember]
        public virtual string Afternoon { get; set; }

        [Required]
        [DataMember]
        public virtual string Dinner { get; set; }
    }

    public class MenuMap : ClassMap<Menu>
    {
        public MenuMap()
        {
            Table("menues");

            Id(x => x.MenuId).GeneratedBy.Identity();

            References<User>(x => x.User, "UserId");

            Map(x => x.Breakfast);
            Map(x => x.MidMorning);
            Map(x => x.Lunch);
            Map(x => x.Afternoon);
            Map(x => x.Dinner);
        }
    }

}