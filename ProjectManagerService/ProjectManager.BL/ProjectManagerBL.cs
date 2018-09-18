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

        public IEnumerable<ProjectModel> GetAllProjects()
        {
            var allProjects = from item in _dbService.GetAllProjects()
                              select new ProjectModel
                              {
                                  ProjectID = item.Project_ID,
                                  ProjectName = item.Project_Name,
                                  NoOfTasks = item.Tasks.Count,
                                  CompletedTasks = item.Tasks.Where(c => c.Status == "Completed").Count(),
                                  StartDate = item.StartDate,
                                  EndDate = item.EndDate,
                                  Priority = item.Priority
                              };
            return allProjects.ToList();
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var allUsers = from item in _dbService.GetAllUsers()
                           select new UserModel
                           {
                               FirstName = item.FirstName,
                               LastName = item.LastName,
                               EmployeeId = item.Employee_ID
                           };
            return allUsers.Distinct().ToList();
        }

        public void AddUser(UserModel usr)
        {
            _dbService.AddUser(new Usr() { Employee_ID = usr.EmployeeId, FirstName = usr.FirstName, LastName = usr.LastName });
        }
    }
}
