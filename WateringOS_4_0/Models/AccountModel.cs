
using System.ComponentModel.DataAnnotations;

namespace WateringOS_4_0.Models
{
    public class ApprovedUser
    {
        public string HashCode { get; set; }
        public string Fingerprint { get; set; }
    }

    public class PasswordInput
    {
        [Required]
        public string Password { get; set; }
    }
}
