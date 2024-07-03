using Domain.Common;

namespace Domain.Entities.Users
{
    public class Address:BaseEntity,IDatedModification
    {
        public string Title { get; set; }
        public string Address_Line_1 { get; set; }
        public string Address_Line_2 { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumberCode { get; set; }
        public string CityCode { get; set; }
        public string RegionCode { get; set; }
        public Guid UserId {  get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
