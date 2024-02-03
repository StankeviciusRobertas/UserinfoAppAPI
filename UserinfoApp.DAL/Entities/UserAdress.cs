using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserinfoApp.DAL.Entities
{
    public class UserAdress
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;
        public string FlatNumber { get; set; } = null!;

        [ForeignKey(nameof(UserInfo))]
        public int UserInfoId { get; set; }
        public UserInfo UserInfo { get; set; } = null!;
    }
}