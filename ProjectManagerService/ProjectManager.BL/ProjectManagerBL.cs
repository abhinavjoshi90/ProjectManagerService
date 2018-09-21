﻿using ProjectManager.BL.Model;
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

            return GetAllParentTasks().Union(allTasks).ToList();
        }

        public IEnumerable<TaskModel> GetAllParentTasks()
        {
            var allTasks = from item in _dbService.GetAllParentTasks()
                           select new TaskModel
                           {
                               ParentTaskName = item.Parent_Task,
                               ParentTaskID = item.Parent_ID
                           };

            return allTasks.ToList();
        }

        public IEnumerable<ProjectModel> GetAllProjects()
        {
            var allProjects = from item in _dbService.GetAllProjects()
                              join usr in _dbService.GetAllUsers()
                              on item.Project_ID equals usr.Project_ID into ps
                              from usritem in ps.Where(c => c.Task_ID == null).DefaultIfEmpty()
                              select new ProjectModel
                              {
                                  ProjectID = item.Project_ID,
                                  ProjectName = item.Project_Name,
                                  NoOfTasks = item.Tasks.Count,
                                  CompletedTasks = item.Tasks.Where(c => c.Status == "Completed").Count(),
                                  StartDate = item.StartDate,
                                  EndDate = item.EndDate,
                                  Priority = item.Priority,
                                  Manager = usritem == null ? null : new UserModel()
                                  {
                                      EmployeeId = usritem.Employee_ID,
                                      FirstName = usritem.FirstName,
                                      LastName = usritem.LastName,
                                      UserId = usritem.Usr_ID
                                  }
                              };
            return allProjects.ToList();
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var allUsers = from item in _dbService.GetAllUsers()
                           select new UserModel
                           {
                               UserId = item.Usr_ID,
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

        public void AddProject(ProjectModel prj)
        {
            _dbService.AddProject(new Project() { Project_ID = prj.ProjectID, Project_Name = prj.ProjectName, StartDate = prj.StartDate, EndDate = prj.EndDate, Priority = prj.Priority, Usrs = new List<Usr> { new Usr() { Project_ID=prj.ProjectID, Usr_ID=prj.Manager.UserId, FirstName = prj.Manager.FirstName, LastName = prj.Manager.LastName, Employee_ID = prj.Manager.EmployeeId } } });
        }

        public void AddTask(TaskModel task)
        {
            _dbService.AddTask(new DL.Task()
            {
                Task_ID = task.TaskID,
                Task_Name = task.TaskName,
                Parent_ID = task.ParentTaskID,
                ParentTask = new ParentTask() { Parent_ID = task.ParentTaskID, Parent_Task = task.ParentTaskName },
                Project_ID = task.Project.ProjectID,
                Usrs = new List<Usr>() { new Usr() { Employee_ID = task.User.EmployeeId, FirstName = task.User.FirstName, LastName = task.User.LastName, Usr_ID = task.User.UserId, Task_ID = task.TaskID, Project_ID = task.Project.ProjectID } },
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                Priority = task.Priority,
                Status = task.StartDate > DateTime.Now ? "In Progress" : "Not Started"
            });
        }

        public TaskModel GetTaskById(int taskId)
        {
            DL.Task tsk = _dbService.GetTaskById(taskId);
            TaskModel tskModel = new TaskModel()
            {
                TaskID = tsk.Task_ID,
                TaskName = tsk.Task_Name,
                ParentTaskID = tsk.ParentTask.Parent_ID,
                ParentTaskName = tsk.ParentTask.Parent_Task,
                StartDate = tsk.StartDate,
                EndDate = tsk.EndDate,
                Priority = tsk.Priority,
                Project = new ProjectModel()
                {
                    ProjectID = tsk.Project.Project_ID,
                    ProjectName = tsk.Project.Project_Name,
                    Priority = tsk.Project.Priority
                }
            };

             foreach(var user in tsk.Usrs)
            {
                tskModel.User = new UserModel()
                {
                    EmployeeId = user.Employee_ID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserId = user.Usr_ID
                };
            }
            return tskModel;
        }
    }
}
