using System.ComponentModel.DataAnnotations;

namespace diplom.ViewModel
{
    public class UserSettingsViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Location { get; set; }
    }
}
