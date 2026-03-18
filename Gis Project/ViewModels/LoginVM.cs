using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gis_Project.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "User Name Or Email")]
        [Required(ErrorMessage = "User Name Or Email Is Required")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }

        [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
