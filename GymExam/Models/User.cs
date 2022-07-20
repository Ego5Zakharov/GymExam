using Microsoft.AspNetCore.Identity;

namespace GymExam.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }

    }
}
