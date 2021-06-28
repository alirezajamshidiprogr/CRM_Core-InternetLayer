using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM_Core.DomainLayer
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(200)]
        public string Password { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public string Mobile { get; set; }
        public DateTime CreateOn { get; set; }
        public string Address { get; set; }
    }

    public class UserActions
    {
        public int Id { get; set; }
        public string P_LoginDate { get; set; }
        public string M_LoginDate { get; set; }

    }

}
