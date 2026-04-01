using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.Dtos.TaskDtos
{
    public class TasksIdDto
    {
        public List<Guid> TaskIds { get; set; } = new();
        public bool IsCompleted { get; set; }
    }
}
