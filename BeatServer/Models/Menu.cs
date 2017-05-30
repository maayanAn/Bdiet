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
        public Menu()
        {

        }

        public Menu(Meal Breakfast, Meal MidMorning, Meal Lunch, Meal Afternoon, Meal Dinner)
        {
            this.Breakfast = Breakfast.ToString();
            this.MidMorning = MidMorning.ToString();
            this.Lunch = Lunch.ToString();
            this.Afternoon = Afternoon.ToString();
            this.Dinner = Dinner.ToString();

            MealList = new List<Meal>();
            MealList.Add(Breakfast);
            MealList.Add(MidMorning);
            MealList.Add(Lunch);
            MealList.Add(Afternoon);
            MealList.Add(Dinner);
        }

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

        public virtual List<Meal> MealList { get; set; }
        //{
        //    get
        //    {
        //        return this.MealList;
        //    }
        //    set
        //    {
        //        MealList = value;
        //        Breakfast = MealList[0].ToString();
        //        MidMorning = MealList[1].ToString();
        //        Lunch = MealList[2].ToString();
        //        Afternoon = MealList[3].ToString();
        //        Dinner = MealList[4].ToString();
        //    }
        //}

        public virtual int Score { get; set; }
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