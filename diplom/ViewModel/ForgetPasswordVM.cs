using System.ComponentModel.DataAnnotations;

namespace diplom.ViewModel
{
    public class ForgetPasswordVM
    {
        [Required]
        [EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
