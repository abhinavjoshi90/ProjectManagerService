using ProjectManager.BL;
using ProjectManager.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectManagerService.Controllers
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
    }
}
