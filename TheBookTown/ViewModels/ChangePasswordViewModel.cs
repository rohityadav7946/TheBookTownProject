using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBookTown.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Current Password")]
        public string CurrentPassword { get; set; }


        [Required]
        [Display(Name ="New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Display(Name ="Confirm New Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="New password and confirm new password do not match")]

        public string ConfirmPassword { get; set; }

    }
}
