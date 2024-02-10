namespace ButaAPI.Database.Model
{
    public class Location
    {
        public string Country { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? House { get; set; }
        public string? PostalCode { get; set; }
    }
}
