using System;

using System.Collections;
using System.Collections.Generic;
using ProjectManager.DL;
using Moq;
using NUnit.Framework;
using ProjectManager.BL;
using System.Linq;
using ProjectManager.BL.Model;

namespace ProjectManager.UnitTest
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1_GetAllTasks()
        {
            IEnumerable<DL.Task> lstTask = new List<DL.Task>()
            {
                new DL.Task(){ Task_ID=100, Task_Name="Task1", Project_ID =201, StartDate =DateTime .Now , EndDate =DateTime .Now , ParentTask =new DL.ParentTask(){ Parent_ID =100, Parent_Task ="Parent1"} , Parent_ID=100, Priority =10, Usrs =
                new List<Usr>{ new Usr(){ Usr_ID =100, FirstName ="Abhinav", LastName ="joshi",Project_ID =201,Task_ID=100,Employee_ID=562951 } }
                ,Project=new Project(){ Project_ID=201, Project_Name="IR", Priority=10 } },
                new DL.Task(){ Task_ID=101, Task_Name="Task2", Project_ID =202, StartDate =DateTime .Now , EndDate =DateTime .Now , ParentTask =new DL.ParentTask(){ Parent_ID =100, Parent_Task ="Parent1"} , Parent_ID=100, Priority =10, Usrs =
                new List<Usr>{ new Usr(){ Usr_ID =101, FirstName ="Manish", LastName ="Singh",Project_ID =202,Task_ID=101,Employee_ID=562952 } }
                ,Project=new Project(){ Project_ID=202, Project_Name="ACL", Priority=10 } },
                new DL.Task(){ Task_ID=102, Task_Name="Task3", Project_ID =202, StartDate =DateTime .Now , EndDate =DateTime .Now , ParentTask =new DL.ParentTask(){ Parent_ID =100, Parent_Task ="Parent1"} , Parent_ID=100, Priority =10, Usrs =null
                ,Project=new Project(){ Project_ID=202, Project_Name="ACL", Priority=10 } },
                new DL.Task(){ Task_ID=103, Task_Name="Task4", Project_ID =202, StartDate =DateTime .Now , EndDate =DateTime .Now , ParentTask =new DL.ParentTask(){ Parent_ID =100, Parent_Task ="Parent1"} , Parent_ID=100, Priority =10, Usrs =null
                ,Project=new Project(){ Project_ID=202, Project_Name="ACL", Priority=10 } },
                new DL.Task(){ Task_ID=104, Task_Name="Task5", Project_ID =202, StartDate =DateTime .Now , EndDate =DateTime .Now , ParentTask =new DL.ParentTask(){ Parent_ID =100, Parent_Task ="Parent1"} , Parent_ID=100, Priority =10, Usrs =null
                ,Project=new Project(){ Project_ID=202, Project_Name="ACL", Priority=10 } }
            };

            IEnumerable<Usr> lstUsers = new List<Usr>()
            {
                new Usr(){Usr_ID =100, FirstName ="Abhinav", LastName ="joshi",Project_ID =201,Task_ID=100,Employee_ID=562951  },
                new Usr(){ Usr_ID =101, FirstName ="Manish", LastName ="Singh",Project_ID =202,Task_ID=101,Employee_ID=562952 }
            };

            IEnumerable<ParentTask> lstParent = new List<ParentTask>()
            {
                new ParentTask(){ Parent_ID=100, Parent_Task ="Parent1"},
                new ParentTask(){ Parent_ID=101,Parent_Task="Parent2"}
            };
            Mock<IProjectDbService> mock = new Mock<IProjectDbService>();
            ProjectManagerBL projBL = new ProjectManagerBL(mock.Object);
            mock.Setup(c => c.GetAllTasks()).Returns(lstTask.AsQueryable());
            mock.Setup(c => c.GetAllUsers()).Returns(lstUsers.AsQueryable());
            mock.Setup(c => c.GetAllParentTasks()).Returns(lstParent.AsQueryable());

            int expectedcount = 7;
            var actual = projBL.GetAllTasks();
            Assert.AreEqual(expectedcount, actual.Count());
        }

        [Test]
        public void TestMethod2_GetAllProjects()
        {
            IEnumerable<Project> lstProjects = new List<Project>
            {
                new Project(){ Project_ID=201, Project_Name="IR", Priority=10,  StartDate=DateTime.Now ,EndDate =DateTime.Now,
                    Tasks =new List<Task>{ new Task(){ Task_ID=100, Status="Completed"},new Task(){ Task_ID=101,Status="In Progress"} }
                    ,Usrs =new List<Usr>(){ new Usr() { Usr_ID = 100, FirstName = "Abhinav", LastName = "joshi", Project_ID = 201, Task_ID = null, Employee_ID = 562951 },
                    new Usr(){ Usr_ID =101, FirstName ="Manish", LastName ="Singh",Project_ID =201,Task_ID=100,Employee_ID=562952 }} },
                 new Project(){ Project_ID=202, Project_Name="ACL", Priority=15,  StartDate=DateTime.Now ,EndDate =DateTime.Now,
                    Tasks =new List<Task>{ new Task(){ Task_ID=102, Status="Completed"},new Task(){ Task_ID=103,Status="In Progress"} } }
            };

            IEnumerable<Usr> lstUsers = new List<Usr>()
            {
                new Usr(){ Usr_ID = 100, FirstName = "Abhinav", LastName = "joshi", Project_ID = 201, Task_ID = null, Employee_ID = 562951 },
                new Usr(){ Usr_ID =101, FirstName ="Manish", LastName ="Singh",Project_ID =201,Task_ID=100,Employee_ID=562952 }
            };

            Mock<IProjectDbService> mock = new Mock<IProjectDbService>();
            ProjectManagerBL projBL = new ProjectManagerBL(mock.Object);
            mock.Setup(c => c.GetAllProjects()).Returns(lstProjects.AsQueryable());
            mock.Setup(c => c.GetAllUsers()).Returns(lstUsers.AsQueryable());

            int expectedcount = 2;
            var actual = projBL.GetAllProjects();
            Assert.AreEqual(expectedcount, actual.Count());
        }

        [Test]
        public void TestMethod3_GetAllUsers()
        {
            IEnumerable<Usr> lstUsers = new List<Usr>()
            {
                new Usr(){ Usr_ID = 100, FirstName = "Abhinav", LastName = "joshi", Project_ID = 201, Task_ID = null, Employee_ID = 562951 },
                new Usr(){ Usr_ID =101, FirstName ="Manish", LastName ="Singh",Project_ID =201,Task_ID=100,Employee_ID=562952 }
            };

            Mock<IProjectDbService> mock = new Mock<IProjectDbService>();
            ProjectManagerBL projBL = new ProjectManagerBL(mock.Object);
            mock.Setup(c => c.GetAllUsers()).Returns(lstUsers.AsQueryable());
            int expectedcount = 2;
            var actual = projBL.GetAllUsers();
            Assert.AreEqual(expectedcount, actual.Count());
        }

        [Test]
        public void TestMethod4_AddUser()
        {
            UserModel usr = new UserModel() { EmployeeId = 100, FirstName = "Abhinav", LastName = "Joshi", UserId = 100 };
            Mock<IProjectDbService> mock = new Mock<IProjectDbService>();
            ProjectManagerBL projBL = new ProjectManagerBL(mock.Object);
            projBL.AddUser(usr);
            mock.Verify(c => c.AddUser(It.IsAny<Usr>()), Times.Once);
        }
        [Test]
        public void TestMethod5_DeleteUser()
        {

            UserModel usr = new UserModel() { EmployeeId = 100, FirstName = "Abhinav", LastName = "Joshi", UserId = 100 };
            Mock<IProjectDbService> mock = new Mock<IProjectDbService>();
            ProjectManagerBL projBL = new ProjectManagerBL(mock.Object);
            projBL.DeleteUser(usr);
            mock.Verify(c => c.DeleteUser(It.IsAny<Usr>()), Times.Once);
        }
        [Test]
        public void TestMethod6_AddProject()
        {
            ProjectModel projM = new ProjectModel() { ProjectName = "IR", ProjectID = 101, NoOfTasks = 5, CompletedTasks = 1, StartDate = DateTime.Now, EndDate = DateTime.Now, Priority = 20 };
            Mock<IProjectDbService> mock = new Mock<IProjectDbService>();
            ProjectManagerBL projBL = new ProjectManagerBL(mock.Object);
            projBL.AddProject(projM);
            mock.Verify(c => c.AddProject(It.IsAny<Project>()), Times.Once);
        }
        [Test]
        public void TestMethod7_AddTask()
        {
            TaskModel obj = new TaskModel() { TaskID = 100, IsParentTask = true, Project = new ProjectModel() { ProjectID = 100 }, TaskName = "Task1", StartDate = DateTime.Now, EndDate = DateTime.Now, Priority = 10 };
            Mock<IProjectDbService> mock = new Mock<IProjectDbService>();
            ProjectManagerBL projBL = new ProjectManagerBL(mock.Object);
            projBL.AddTask(obj);
            mock.Verify(c => c.AddTask(It.IsAny<Task>()), Times.Once);
        }
        [Test]
        public void TestMethod8_GetTaskById()
        {
            DL.Task taskObj = new DL.Task()
            {
                Task_ID = 100,
                Task_Name = "Task1",
                Project_ID = 201,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                ParentTask = new DL.ParentTask() { Parent_ID = 100, Parent_Task = "Parent1" },
                Parent_ID = 100,
                Priority = 10,
                Usrs = new List<Usr> {new Usr()
                { Usr_ID = 100,
                  FirstName = "Abhinav",
                  LastName = "joshi",
                  Project_ID = 201,
                  Task_ID = 100,
                  Employee_ID = 562951
                }
                },
                Project = new Project() { Project_ID = 201, Project_Name = "IR", Priority = 10 }
            };
            Mock<IProjectDbService> mock = new Mock<IProjectDbService>();
            ProjectManagerBL projBL = new ProjectManagerBL(mock.Object);
            mock.Setup(c => c.GetTaskById(It.IsAny<int>())).Returns(taskObj);

            // int expectedcount = 1;
            var actual = projBL.GetTaskById(100);
            Assert.AreEqual(100, actual.TaskID);
        }
        [Test]
        public void TestMethod9_AddParentTask()
        {
            TaskModel obj = new TaskModel() { TaskID = 100, TaskName = "Task1" };
            Mock<IProjectDbService> mock = new Mock<IProjectDbService>();
            ProjectManagerBL projBL = new ProjectManagerBL(mock.Object);
            projBL.AddParentTask(obj);
            mock.Verify(c => c.AddParentTask(It.IsAny<ParentTask>()), Times.Once);
        }
    }
}
