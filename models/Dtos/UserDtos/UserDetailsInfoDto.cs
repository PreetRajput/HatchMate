using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.Dtos.UserDtos
{
    public class UserDetailsInfoDto
    {
        public string? Username { get; set; }
        public string? Pet_Type { get; set; }
        public List<string> Tasks { get; set; } = new();
        public string? PetName { get; set; }
        public int Pet_Level { get; set; }
    }
}
