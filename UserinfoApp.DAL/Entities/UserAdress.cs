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

        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
    }
}