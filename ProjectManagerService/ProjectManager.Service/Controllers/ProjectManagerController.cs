using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectManager.BL;
using ProjectManager.BL.Model;
namespace ProjectManager.Service.Controllers
{
    [RoutePrefix("api")]
    public class ProjectManagerController : ApiController
    {
        internal IProjectManagerBL _manager;
        public ProjectManagerController(IProjectManagerBL manager)
        {
            _manager = manager;
        }
        [Route("getalltasks")]
        [HttpGet]
        public IHttpActionResult GetAllTasks()
        {
            return Json<IEnumerable<TaskModel>>(_manager.GetAllTasks());
        }

        [Route("gettaskbyprojectid/{pid}")]
        [HttpGet]
        public IHttpActionResult GetTaskByProjectID(int pid)
        {
            return Json<IEnumerable<TaskModel>>(_manager.GetAllTasks().Where(c => (c.IsParentTask==false) && (c.Project?.ProjectID == pid)));
        }

        [Route("getprnttaskbyname/{name?}")]
        [HttpGet]
        public IHttpActionResult GetParentTaskByName(string name="x")
        {
            if (name.Length > 1)
            {
                return Json<IEnumerable<TaskModel>>(_manager.GetAllTasks().Where(c => c.IsParentTask && c.TaskName.ToUpper().Contains(name.ToUpper())));
            }
            else
            {
                return Json<IEnumerable<TaskModel>>(_manager.GetAllTasks().Where(c => c.IsParentTask));
            }
        }

        [Route("getallprojects")]
        [HttpGet]
        public IHttpActionResult GetAllProjects()
        {
            return Json<IEnumerable<ProjectModel>>(_manager.GetAllProjects());
        }

        [Route("getprojectbyname/{name?}")]
        [HttpGet]
        public IHttpActionResult GetProjectByName(string name="x")
        {
            if (name.Length > 1)
            {
                return Json<IEnumerable<ProjectModel>>(_manager.GetAllProjects().Where(c => c.ProjectName.ToUpper().Contains(name.ToUpper())));
            }
            else
            {
                return Json<IEnumerable<ProjectModel>>(_manager.GetAllProjects());
            }
        }

        [Route("getallusers")]
        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            return Json<IEnumerable<UserModel>>(_manager.GetAllUsers());
        }

        [Route("getuserbyname/{name?}")]
        [HttpGet]
        public IHttpActionResult GetUserByName(string name="x")
        {
            if (name.Length > 1)
            {
                return Json<IEnumerable<UserModel>>(_manager.GetAllUsers().Where(c => c.FirstName.ToUpper().Contains(name.ToUpper())));
            }
            else
            {
                return Json<IEnumerable<UserModel>>(_manager.GetAllUsers());
            }
        }
        [Route("adduser")]
        [HttpPost]
        public IHttpActionResult AddUser(UserModel usr)
        {
            if (usr.UserId > 0)
            {
                _manager.AddUser(usr);
                return Ok("User updated successfully");
            }
            else
            {
                _manager.AddUser(usr);
                return Ok("User added successfully");
            }
        }

        [Route("deleteuser")]
        [HttpPost]
        public IHttpActionResult DeleteUser(UserModel usr)
        {
            _manager.DeleteUser(usr);
            return Ok("User Deleted successfully");
        }

        [Route("addproject")]
        [HttpPost]
        public IHttpActionResult AddProject(ProjectModel project)
        {
            if (project.ProjectID > 0)
            {
                _manager.AddProject(project);
                return Ok("Project updated succesfully");
            }
            else
            {
                _manager.AddProject(project);
                return Ok("Project added succesfully");
            }
        }

        [Route("addtask")]
        [HttpPost]
        public IHttpActionResult AddTask(TaskModel task)
        {
            if (task.TaskID > 0)
            {
                _manager.AddTask(task);
                return Ok("Task updated succesfully");
            }
            else
            {
                _manager.AddTask(task);
                return Ok("Task added successfully");
            }
        }

        

        [Route("gettaskbyid/{id}")]
        [HttpGet]
        public IHttpActionResult GetTaskByID(int id)
        {
            return Json<TaskModel>(_manager.GetTaskById(id));
        }

        [Route("addparenttask")]
        [HttpPost]
        public IHttpActionResult AddParentTask(TaskModel task)
        {
            if (task.TaskID > 0)
            {
                _manager.AddParentTask(task);
                return Ok("Task updated successfully");
            }
            else
            {
                _manager.AddParentTask(task);
                return Ok("Task Added successfully");
            }
        }
    }
}
