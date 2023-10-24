﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_model.Models
{
    internal class UserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid AddressId { get; set; }
        public AddressModel? Address { get; set; }
    }
}
