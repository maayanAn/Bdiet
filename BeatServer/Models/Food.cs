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
    public class Food
    {
        [Key]
        [DataMember]
        public virtual int FoodId { get; set; }


        [Required]
        [DataMember]
        public virtual string Name { get; set; }

        [Required]
        [DataMember]
        public virtual Foodgroup FoodGroup { get; set; }

        [Required]
        [DataMember]
        public virtual Mealtype MealType { get; set; }


        [Required]
        [DataMember]
        public virtual string Amount { get; set; }

        [Required]
        [DataMember]
        public virtual string Allergens { get; set; }

        public virtual IList<int> AllergessIdList
        {
            get
            {
                return Utils.CommaSeparatedStringToIntList(Allergens); ;
            }
            set
            {
                Allergens = string.Join(",", value);
            }
        }

        [Required]
        [DataMember]
        public virtual string Preferences { get; set; }

        public virtual IList<int> PreferencesIdList
        {
            get
            {
                return Utils.CommaSeparatedStringToIntList(Preferences);
            }
            set
            {
                Preferences = string.Join(",", value);
            }
        }

        [Required]
        [DataMember]
        public virtual string Nutrients { get; set; }

        public virtual IList<int> NutrientsIdList
        {
            get
            {
                return Utils.CommaSeparatedStringToIntList(Nutrients);
            }
            set
            {
                Nutrients = string.Join(",", value);
            }
        }
    }

    public class FoodMap : ClassMap<Food>
    {
        public FoodMap()
        {
            Table("food");

            Id(x => x.FoodId).GeneratedBy.Identity();

            References<Foodgroup>(x => x.FoodGroup, "FoodGroup");
            References<Mealtype>(x => x.MealType, "MealType");

            Map(x => x.Name);
            Map(x => x.Amount);
            Map(x => x.Preferences);
            Map(x => x.Allergens);
            Map(x => x.Nutrients);
        }
    }

}