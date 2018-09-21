using System.Linq;

namespace ProjectManager.DL
{
    public interface IProjectDbService
    {
        IQueryable<Task> GetAllTasks();
        IQueryable<Usr> GetAllUsers();
        IQueryable<Project> GetAllProjects();
         void AddUser(Usr obj);
        void AddProject(Project prj);
        IQueryable<ParentTask> GetAllParentTasks();
        void AddTask(Task task);
    }
}