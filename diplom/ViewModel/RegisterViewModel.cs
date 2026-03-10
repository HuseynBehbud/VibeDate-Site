using System.ComponentModel.DataAnnotations;

namespace diplom.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password),Compare(nameof(Password),ErrorMessage ="Password does not match")]
        public string ConfirmPassword { get; set; }
        public string Location { get; set; }
    }
}
