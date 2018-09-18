using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BL.Model
{
   public class ProjectModel
    {
        public int ProjectID { get; set; }
        public string  ProjectName { get; set; }
        public int NoOfTasks { get; set; }
        public int CompletedTasks { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Priority { get; set; }
    }
}
