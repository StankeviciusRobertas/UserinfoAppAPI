﻿using System;
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
        public string? Name { get; set; } = null!;
        public string? Description { get; set; } 
        public byte[] ImageBytes { get; set; } = null!;

        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
    }
}
