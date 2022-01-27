using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnnoTest3.Models
{
    public class Setting:BaseEntity
    {
        [StringLength(maximumLength:50)]
        public string Key { get; set; }
        [StringLength(maximumLength:250)]
        public string Value { get; set; }
    }
}
