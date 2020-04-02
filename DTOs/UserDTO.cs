using System.Collections.Generic;
using advancedwebapi.Models;

namespace advancedwebapi.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
         public List<Character> Characters { get; set; }
    }
}