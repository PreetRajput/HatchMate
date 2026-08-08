using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.Dtos.TaskDtos
{
    public class TaskElementDto
    {
        public List<TaskItemDto> Tasks { get; set; }
        public bool HasMoreTask { get; set; }
    }
}
