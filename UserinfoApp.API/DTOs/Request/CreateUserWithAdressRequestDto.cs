namespace UserinfoApp.API.DTOs.Request
{
    public class CreateUserWithAdressRequestDto
    {
        public UserInfoRequestDto UserInfo { get; set; }
        public UserAdressRequestDto UserAdress { get; set; }
    }
}
