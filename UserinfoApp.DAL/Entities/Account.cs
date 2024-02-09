using System.ComponentModel.DataAnnotations;

namespace UserinfoApp.DAL.Entities
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string Role { get; set; } = "user";
        public UserInfo UserInfo { get; set; } = null!;
        public UserAdress UserAdress { get; set; }
        public ICollection<Image> Images { get; set; } = null!;
    }
}
