using ProjectManager.BL.Model;
using ProjectManager.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BL
{
    public class ProjectManagerBL : IProjectManagerBL
    {
        IProjectDbService _dbService;
        public ProjectManagerBL(IProjectDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<TaskModel> GetAllTasks()
        {
            var allTasks = from item in _dbService.GetAllTasks()
                           select new TaskModel
                           {
                               TaskID = item.Task_ID,
                               TaskName = item.Task_Name,
                               ParentTaskName = item.ParentTask.Parent_Task,
                               ParentTaskID = item.ParentTask.Parent_ID,
                               StartDate = item.StartDate,
                               EndDate = item.EndDate,
                               Priority = item.Priority
                           };

            return allTasks.ToList();
        }
    }
}
