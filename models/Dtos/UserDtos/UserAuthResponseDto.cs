using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.Dtos.UserDtos
{
    // The single DTO the client will receive
    public class UserAuthResponseDto
    {
        public string? Token { get; set; } // The generated JWT
        public bool IsNewUser { get; set; } // Indicates if the user was newly created
    }
}
