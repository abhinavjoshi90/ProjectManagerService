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

        //public ParentTask GetParentTaskById(int taskId)
        //{
        //    return _context.ParentTasks.Where(t => t.Parent_ID == taskId).FirstOrDefault();
        //}
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
            if (obj.Usr_ID > 0)
            {
                _context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                _context.Usrs.Add(obj);
            }
            _context.SaveChanges();
        }

        public void DeleteUser(Usr obj)
        {
            _context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
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
            if (task.Usrs?.Count > 0)
            {
                foreach (var user in task.Usrs)
                {
                    _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                }
            }
            else
            {
                task.Usrs = null;
            }
            if (task.ParentTask?.Parent_ID > 0)
            {
                _context.Entry(task.ParentTask).State = System.Data.Entity.EntityState.Modified;
            }
            if (task.ParentTask?.Parent_ID == 0 && string.IsNullOrEmpty(task.ParentTask?.Parent_Task))
            {
                task.ParentTask = null;
                task.Parent_ID = null;
            }
            if (task.Task_ID > 0)
            {   
                var users = GetAllUsers().Where(c => c.Project_ID == task.Project_ID && c.Task_ID == task.Task_ID).ToList();
                foreach (var user in users)
                {
                    var taskusr = task.Usrs.FirstOrDefault();
                    if (taskusr.Usr_ID != user.Usr_ID)
                    {
                        user.Project_ID = null;
                        user.Task_ID = null;
                        _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                
                _context.Entry(task).State= System.Data.Entity.EntityState.Modified; 
            }
            else
            {
                _context.Tasks.Add(task);
            }
          
            _context.SaveChanges();
        }

        public void AddParentTask( ParentTask task)
        {
            if (task.Parent_ID > 0)
            {
                _context.Entry(task).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                _context.ParentTasks.Add(task);
            }
            _context.SaveChanges();
        }

        
    }
}
