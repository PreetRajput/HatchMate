using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.Dtos
{
    public class TaskItemDto
    {
        public Guid Id { get; set; }
        public string? Task { get; set; }
        public bool  IsCompleted { get; set; }
    }
}
