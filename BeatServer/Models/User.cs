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
    public class User
    {
        [Key]
        [DataMember]
        public virtual int user_id { get; set; }

        [Required]
        [DataMember]
        public virtual string email { get; set; }

        [Required]
        [DataMember]
        public virtual string name { get; set; }

        [Required]
        [DataMember]
        public virtual string password { get; set; }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("tb_users");

            Id(x => x.user_id).GeneratedBy.Identity();

            Map(x => x.email);
            Map(x => x.name);
            Map(x => x.password);

        }
    }

}