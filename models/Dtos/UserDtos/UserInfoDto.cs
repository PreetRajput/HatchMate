using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.Dtos.UserDtos
    {
        public class UserInfoDto
        {
            public string? Login { get; set; }
            public long? Id { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
        }
    }
