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
    public class BloodTest
    {
        [Key]
        [DataMember]
        public virtual int test_id { get; set; }

        [Required]
        [DataMember]
        public virtual string component_name_english { get; set; }

        [Required]
        [DataMember]
        public virtual string value { get; set; }

        [Required]
        [DataMember]
        public virtual int min_val { get; set; }

        [Required]
        [DataMember]
        public virtual int max_val { get; set; }


        [Required]
        [DataMember]
        public virtual string reference { get; set; }


        public class BloodMap : ClassMap<BloodTest>
        {
            public BloodMap()
            {
                Table("tb_blood_tests_decoder");

                Id(x => x.test_id).GeneratedBy.Identity();

                Map(x => x.component_name_english);
                Map(x => x.value);
                Map(x => x.min_val);
                Map(x => x.max_val);
                Map(x => x.reference);
            }
        }
    }
}