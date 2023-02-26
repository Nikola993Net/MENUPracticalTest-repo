using System.ComponentModel.DataAnnotations;

namespace Payment_Simulator.Models
{
    public class RegistrationDTO
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
