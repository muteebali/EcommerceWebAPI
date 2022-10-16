using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models
{
    public partial class User
    {
        public User()
        {
            Address = new HashSet<Address>();
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public string PasswordHash { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
