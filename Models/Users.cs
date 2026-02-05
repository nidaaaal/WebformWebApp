using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyWebApp.Models
{
    public class Users
    {
        [Column("id")]

        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("display_name")]

        public string DisplayName { get; set; } = null;

        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        [Column("gender")]
        public bool Gender { get; set; }
        [Column("age")]
        public int Age { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("city")]
        public string City { get; set; } = null;
        [Column("state")]
        public string State { get; set; } = null;
        [Column("zipcode")]

        public int ZipCode { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("mobile")]
        public string Mobile { get; set; } = null;
    }




}