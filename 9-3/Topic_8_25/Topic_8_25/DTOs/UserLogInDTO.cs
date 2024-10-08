﻿namespace WepAPICore.DTOs
{
    public class UserLogInDTO
    {
        public string? Email { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

    }
}
