using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BibliothekLib;

namespace BibliothekWebApp.Models
{
    public class WorkUserOrderSelectViewModel
    {
        public int? WorkId { get; set; }
        public Work Work { get; set; }
        public bool IsSelected { get; set; }
        public string DisplayName { get; set; }
    }

    public class UserOrderSelectViewModel
    {
        public List<WorkUserOrderSelectViewModel> WorkUserOrderSelects { get; set; } =
            new List<WorkUserOrderSelectViewModel>();
        public int? UserId { get; set; }
        public User User { get; set; }
        public string DisplayName { get; set; }
    }

    public class OrderSelectViewModel
    {
        public List<UserOrderSelectViewModel> UserOrderSelects { get; set; } =
            new List<UserOrderSelectViewModel>();
    }
}