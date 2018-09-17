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
        public IQueryable<Usr> GetAllUsers()
        {
            return _context.Usrs;
        }
    }
}
