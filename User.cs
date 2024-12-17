using System;
using System.Collections.Generic;

#nullable disable

namespace WinFormsApp1
{
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
