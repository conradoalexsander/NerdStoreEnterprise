using System.ComponentModel.DataAnnotations;

namespace NSE.Identity.API.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage="The field {0} is obligatory")]
        [EmailAddress(ErrorMessage="The field {0} is in wrong format")]
        public string Email { get; set; }


        [Required(ErrorMessage = "The field {0} is obligatory")]
        [StringLength(100, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="Unmatching passwords")]
        public string PasswordConfirmation { get; set; }
    }    
    
    public class LoginUser
    {
        [Required(ErrorMessage="The field {0} is obligatory")]
        [EmailAddress(ErrorMessage="The field {0} is in wrong format")]
        public string Email { get; set; }


        [Required(ErrorMessage = "The field {0} is obligatory")]
        [StringLength(100, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

    }
}
