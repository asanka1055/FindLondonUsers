using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LondonUsers.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IpAddress { get; set; }
        public double? Longitude { get; set; }
        public double? Laitude { get; set; }
    }
}