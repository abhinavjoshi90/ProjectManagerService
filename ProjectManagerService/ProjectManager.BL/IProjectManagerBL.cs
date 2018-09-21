using System.Collections.Generic;
using ProjectManager.BL.Model;

namespace ProjectManager.BL
{
    public interface IProjectManagerBL
    {
        IEnumerable<TaskModel> GetAllTasks();
        IEnumerable<ProjectModel> GetAllProjects();
        IEnumerable<UserModel> GetAllUsers();
        void AddUser(UserModel usr);
        void AddProject(ProjectModel prj);
        void AddTask(TaskModel task);
        TaskModel GetTaskById(int taskId);
    }
}