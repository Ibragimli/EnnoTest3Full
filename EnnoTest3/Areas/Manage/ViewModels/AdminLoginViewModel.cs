using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnnoTest3.Areas.Manage.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]
        [StringLength(maximumLength:25)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 25)]
        public string Password { get; set; }
    }
}
