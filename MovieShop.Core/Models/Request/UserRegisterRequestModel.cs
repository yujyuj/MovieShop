using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace MovieShop.Core.Models.Request
{
    public class UserRegisterRequestModel
    {
        [Required(ErrorMessage = "Email cannot be empty")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Email should be in valid format")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password cannot be empty!")]
        [StringLength(100, ErrorMessage = "The {0} muct be at least {2} chanracters long", MinimumLength = 8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Must contain: upper case, lower case, special character, 8 characrters")]
        // 1 capitol, smll, number and special chaarcter, 8 length, max 100
        public string Password { get; set; }


        // must be same as password
        [Compare("Password", ErrorMessage = "Passwords must be matched!")]
        public string RePassword { get; set; }


        [Required(ErrorMessage = "First Name cannot be empty")]
        [StringLength(50)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last Name cannot be empty")]
        [StringLength(50)]
        public string LastName { get; set; }


        public DateTime DateOfBirth { get; set; }
    }
}
