using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NSE.WebApp.MVC.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "The field {0} can not be empty.")]
        [EmailAddress(ErrorMessage = "Invalid format for field {0}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} can not be empty.")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The field {0} can not be empty.")]
        [Compare("Password", ErrorMessage = "Password and confirmation mismatching")]
        public string PasswordConfirmation { get; set; }
    }

    public class LoginUser
    {
        [Required(ErrorMessage = "The field {0} can not be empty.")]
        [EmailAddress(ErrorMessage = "Invalid format for field {0}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} can not be empty.")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }
    }

    public class UserLoginResponse
    {
        public string AccessToken { get; set; }
        public string ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }

    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }

    public class UserClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
