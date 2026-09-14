namespace BelanjaYuk.Web.Models
{
    public class ProfileViewModel
    {
        public string IdUser { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? DateOfBirth { get; set; }

        public string? Gender { get; set; }
    }
}