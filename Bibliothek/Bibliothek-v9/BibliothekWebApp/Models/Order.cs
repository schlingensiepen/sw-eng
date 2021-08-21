using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BibliothekLib;

namespace BibliothekWebApp.Models
{
    public class Order
    {
        [Key]
        [Column(Order = 0)]
        public int? UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int? WorkId { get; set; }

        public User User { get; set; }
        public Work Work { get; set; }
    }
}