using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserinfoApp.DAL.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public byte[] ImageBytes { get; set; } = null!;

        [ForeignKey(nameof(UserInfo))]
        public int UserinfoId { get; set; }
        public UserInfo UserInfo { get; set; } = null!;
    }
}
