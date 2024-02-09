using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserinfoApp.DAL.Entities
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PersonalCode { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
        //public UserAdress UserAdress { get; set; } = null!;
        public ICollection<Image> Images { get; set; } = null!;
    }
}
