using MessageRoomSolution.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Common
{
    public class CommonUtils
    {
        public static string Md5(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            var textBytes = Encoding.Default.GetBytes(password);
            var cryptHandler = new MD5CryptoServiceProvider();
            var hash = cryptHandler.ComputeHash(textBytes);
            var ret = "";
            foreach (var a in hash)
            {
                if (a < 16)
                    ret += "0" + a.ToString("x");
                else
                    ret += a.ToString("x");
            }
            return ret;
        }

        public static string GeneratePassword(string password)
        {
            var hasher = new PasswordHasher<AppUser>();
            return hasher.HashPassword(null, password);
        }

        public static bool VerifyPassword(AppUser user, string password)
        {
            if (user == null || string.IsNullOrEmpty(password))
            {
                return false;
            }
            var hasher = new PasswordHasher<AppUser>();
            return hasher.VerifyHashedPassword(user, user.Password, password) > 0;
        }

        public static string GenerateToken(AppUser user, IConfiguration mCongfiguration)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.MobilePhone, user.Phone),
                new Claim(ClaimTypes.Gender, user.Gender),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(mCongfiguration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var baseToken = new JwtSecurityToken(mCongfiguration["Tokens:Issuer"],
                mCongfiguration["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(baseToken);
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || email.Trim().EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone) || phone.Length != 10 || !phone.StartsWith("0"))
            {
                return false;
            }
            return true;
        }

        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                return false;
            }
            return true;
        }
    }
}