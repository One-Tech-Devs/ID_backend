﻿namespace ID_model.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public string? Role { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? AddressId { get; set; }
        public AddressModel? Address { get; set; }
        public byte[] Key { get; set; }
        public byte[] Iv { get; set; }
    }
}
