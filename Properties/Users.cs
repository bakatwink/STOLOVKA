using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Properties
{
    public static class Users
    {
        public static (string, string) loged;
        public static readonly Dictionary<string, string> users = new Dictionary<string, string>()
    {
        {"Student1", "student"},
        {"Student2", "student"},
        {"Employee1", "employee"}
    };

        public static readonly Dictionary<string, string> studentCrypt = new Dictionary<string, string>()
        {
            { "Student1", GetCrypted("Student1"+"student") },
            { "Student2", GetCrypted("Student2"+"student") }
        };

        public static string GetCrypted(string str)
        {
            byte[] data = new UTF8Encoding().GetBytes(str);
            SHA256 sha = SHA256.Create();
            var result = "";
            foreach(var item in sha.ComputeHash(data))
            {
                result += item;
            }
            return result;
        }
    }
}
