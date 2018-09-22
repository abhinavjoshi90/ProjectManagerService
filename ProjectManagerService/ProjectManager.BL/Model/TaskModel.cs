using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BL.Model
{
    public class TaskModel
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string ParentTaskName { get; set; }
        public int? ParentTaskID { get; set; }
        public int? Priority { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectModel Project { get; set; }
        public UserModel User { get; set; }
        public bool IsParentTask { get; set; }
    }
}
