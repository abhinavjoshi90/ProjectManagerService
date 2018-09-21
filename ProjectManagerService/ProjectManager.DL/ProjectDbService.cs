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
            _context.Projects.Add(prj);
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
            _context.Tasks.Add(task);
           
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
