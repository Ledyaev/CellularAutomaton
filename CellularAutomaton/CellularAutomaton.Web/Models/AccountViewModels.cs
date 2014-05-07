using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Resources;

namespace CellularAutomaton.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(ResourceType = typeof (Resource), Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [Display(ResourceType = typeof (Resource), Name = "BirthDay")]
        public DateTime BirthDay { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Resource), Name = "CurrentPassword")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof (Resource), ErrorMessageResourceName = "PasswordError", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Resource), Name = "NewPassword")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Resource), Name = "ConfirmPassword")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof (Resource), ErrorMessageResourceName = "NotMachedPasswords")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(ResourceType = typeof (Resource), Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Resource), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof (Resource), Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(ResourceType = typeof (Resource), Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof (Resource), ErrorMessageResourceName = "PasswordError", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Resource), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Resource), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof (Resource), ErrorMessageResourceName = "NotMachedPasswords")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(ResourceType = typeof (Resource), Name = "Email")]
        public string Email { get; set; }
    }
}
