
namespace Application.DTOs.Responses.Users
{
    public record UserDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarImage { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    }
}
