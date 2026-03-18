using System.ComponentModel.DataAnnotations;

namespace Gis_Project.ViewModels
{
    public class UserRegisterVM
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name Is Required")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Is Required")]

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Password and confirm Password do not match.")]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
