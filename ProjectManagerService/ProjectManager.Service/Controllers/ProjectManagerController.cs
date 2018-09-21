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
        [Route("getallprojects")]
        [HttpGet]
        public IHttpActionResult GetAllProjects()
        {
            return Json<IEnumerable<ProjectModel>>(_manager.GetAllProjects());
        }
        [Route("getallusers")]
        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            return Json<IEnumerable<UserModel>>(_manager.GetAllUsers());
        }
        [Route("adduser")]
        [HttpPost]
        public IHttpActionResult AddUser(UserModel usr)
        {
            _manager.AddUser(usr);
            return Ok("User added successfully");
        }

        [Route("addproject")]
        [HttpPost]
        public IHttpActionResult AddProject(ProjectModel project)
        {
            _manager.AddProject(project);
            return Ok("Project added succesfully");
        }

        [Route("addtask")]
        [HttpPost]
        public IHttpActionResult AddTask(TaskModel task)
        {
            _manager.AddTask(task);
            return Ok("Task added succesfully");
        }

    }
}
