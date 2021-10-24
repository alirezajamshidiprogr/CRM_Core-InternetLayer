using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM_Core.Entities.Models.Regions
{
    public class Region
    {
        public class Province
        {
            public int Id { get; set; }
            [Required]
            public string ProvinceName { get; set; }
            [Required]
            public string ProvinceTitle { get; set; }
            [Required]
            public string ProvinceCode { get; set; }
        }

        public class City
        {
            public int Id { get; set; }
            [Required]
            public string CityCode { get; set; }
            [Required]
            public string CityName { get; set; }
            [Required]
            public int ProvinceId { get; set; }
            public Province Province { get; set; }
            [Required]
            public bool IsActive { get; set; }
            [Required]
            public DateTime StartActiveDate { get; set; }
            public DateTime EndActiveDate { get; set; }
        }

        public class UserRegion
        {
            public int Id { get; set; }
            [Required]
            public int CityId { get; set; }
            public City City { get; set; }
            [Required]
            public string IdentityUserId { get; set; }
            public IdentityUser IdentityUser { get; set; }
            [Required]
            public bool IsActive { get; set; }
            [Required]
            public DateTime StartActiveDate { get; set; }
            public DateTime EndActiveDate { get; set; }
        }
    }
}
