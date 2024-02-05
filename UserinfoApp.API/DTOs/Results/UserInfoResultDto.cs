namespace UserinfoApp.API.DTOs.Results
{
    public class UserInfoResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PersonalCode { get; set; } 
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IEnumerable<int> ImageIds { get; set; }
    }
}
    