using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using FluentNHibernate.Mapping;
using BeatServer.Managers;

namespace BeatServer.Models
{
    [DataContract]
    public class User
    {
        [Key]
        [DataMember]
        public virtual int UserId { get; set; }

        [Required]
        [DataMember]
        public virtual string Email { get; set; }

        [Required]
        [DataMember]
        public virtual string Name { get; set; }

        [Required]
        [DataMember]
        public virtual string Password { get; set; }


        [Required]
        [DataMember]
        public virtual string Allergies { get; set; }

        public virtual IList<int> AllergiesIdList
        {
            get
            {
                if (Allergies == null)
                {
                    return new List<int>();
                }
                else
                {
                    return Utils.CommaSeparatedStringToIntList(Allergies);
                }
            }
            set
            {
                Allergies = string.Join(",", value);
            }
        }

        [Required]
        [DataMember]
        public virtual string Preferences { get; set; }

        public virtual IList<int> PreferencesIdList
        {
            get
            {
                if (Preferences == null)
                {
                    return new List<int>();
                }
                else
                {
                    return Utils.CommaSeparatedStringToIntList(Preferences);
                }
            }
            set
            {
                Preferences = string.Join(",", value);
            }
        }

        [Required]
        [DataMember]
        public virtual string NutrientLacks { get; set; }

        public virtual IList<int> NutrientLacksList
        {
            get
            {
                if (NutrientLacks == null)
                {
                    return new List<int>();
                }
                else
                {
                    return Utils.CommaSeparatedStringToIntList(NutrientLacks);
                }
            }
            set
            {
                NutrientLacks = string.Join(",", value);
            }
        }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("users");

            Id(x => x.UserId).GeneratedBy.Identity();

            Map(x => x.Email);
            Map(x => x.Name);
            Map(x => x.Password);
            Map(x => x.Allergies);
            Map(x => x.Preferences);
            
        }
    }

}