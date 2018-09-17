using System.Collections.Generic;
using ProjectManager.BL.Model;

namespace ProjectManager.BL
{
    public interface IProjectManagerBL
    {
        IEnumerable<TaskModel> GetAllTasks();
    }
}