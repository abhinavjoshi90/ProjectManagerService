using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DL
{
    public class ProjectDbService : IProjectDbService
    {
        ProjectDbContext _context = new ProjectDbContext();

        public IQueryable<Task> GetAllTasks()
        {
            return _context.Tasks;
        }

        public Task GetTaskById(int taskId)
        {
            return _context.Tasks.Where(t => t.Task_ID == taskId).FirstOrDefault();
        }
        public IQueryable<ParentTask> GetAllParentTasks()
        {
            return _context.ParentTasks;
        }
        public IQueryable<Usr> GetAllUsers()
        {
            return _context.Usrs;
        }

        public IQueryable<Project> GetAllProjects()
        {
            return _context.Projects;
        }
        public void AddUser(Usr obj)
        {
            _context.Usrs.Add(obj);
            _context.SaveChanges();
        }
        public void AddProject(Project prj)
        {
            if (prj.Project_ID > 0)
            {
                _context.Entry(prj).State = System.Data.Entity.EntityState.Modified;

                var users = GetAllUsers().Where(c => c.Project_ID == prj.Project_ID && c.Task_ID == null).ToList();
                foreach (var user in users)
                {
                    user.Project_ID = null;
                    _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                }
            }
            else
            {
                _context.Projects.Add(prj);
            }
            if (prj.Usrs?.Count > 0)
            {
                foreach (var user in prj.Usrs)
                {
                    _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                }

               
            }
            _context.SaveChanges();
        }

        public void AddTask(Task task)
        {
            if (task.Task_ID > 0)
            {
                var users = GetAllUsers().Where(c => c.Project_ID == task.Project_ID && c.Task_ID == task.Task_ID).ToList();
                foreach (var user in users)
                {
                    user.Project_ID = null;
                    user.Task_ID = null;
                    _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                }

                //var parents = GetAllParentTasks().Where(c => c.Parent_ID == task.ParentTask.Parent_ID).ToList();
                //foreach(var prnt in parents)
                //{
                //    prnt.Tasks 
                //}
                _context.Entry(task).State= System.Data.Entity.EntityState.Modified; 
            }
            else
            {
                _context.Tasks.Add(task);
            }
                  
            if (task.Usrs?.Count > 0)
            {
                foreach(var user in task.Usrs)
                {
                    _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                }
            }
            if (task.ParentTask.Parent_ID > 0)
            {
                _context.Entry(task.ParentTask).State = System.Data.Entity.EntityState.Modified;
            }
            _context.SaveChanges();
        }
    }
}
