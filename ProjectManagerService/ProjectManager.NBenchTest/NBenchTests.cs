using NBench;
using ProjectManager.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.NBenchTest
{
    public class NBenchTests
    {
        [PerfBenchmark(NumberOfIterations = 5, RunMode = RunMode.Throughput, RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.SixtyFourKb)]
        public void BenchMarkMethod_GetAllProjects(BenchmarkContext context)
        {
            ProjectDbService _dbSvc = new ProjectDbService();
            IEnumerable<Project> allProjects = _dbSvc.GetAllProjects().ToList();
        }

        [PerfBenchmark(NumberOfIterations = 5, RunMode = RunMode.Throughput, RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.SixtyFourKb)]
        public void BenchMarkMethod_GetAllTasks(BenchmarkContext context)
        {
            ProjectDbService _dbSvc = new ProjectDbService();
            IEnumerable<DL.Task> allTasks = _dbSvc.GetAllTasks().ToList();
        }

        [PerfBenchmark(NumberOfIterations = 5, RunMode = RunMode.Throughput, RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.SixtyFourKb)]
        public void BenchMarkMethod_GetTaskById(BenchmarkContext context)
        {
            ProjectDbService _dbSvc = new ProjectDbService();
            DL.Task tskObj = _dbSvc.GetTaskById(2315);
        }

        [PerfBenchmark(NumberOfIterations = 5, RunMode = RunMode.Throughput, RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.SixtyFourKb)]
        public void BenchMarkMethod_GetAllParentTasks(BenchmarkContext context)
        {
            ProjectDbService _dbSvc = new ProjectDbService();
            IEnumerable<ParentTask> lstParentTasks = _dbSvc.GetAllParentTasks();
        }

        [PerfBenchmark(NumberOfIterations = 5, RunMode = RunMode.Throughput, RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.SixtyFourKb)]
        public void BenchMarkMethod_GetAllUsers(BenchmarkContext context)
        {
            ProjectDbService _dbSvc = new ProjectDbService();
            IEnumerable<Usr> lstUsrs = _dbSvc.GetAllUsers();
        }

        #region Add commands
        [PerfBenchmark(NumberOfIterations = 5, RunMode = RunMode.Throughput, RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.SixtyFourKb)]
 
        public void BenchMarkMethod_AddUser(BenchmarkContext context)
        {
            ProjectDbService _dbSvc = new ProjectDbService();
            _dbSvc.AddUser(new Usr() { Employee_ID = 123456, FirstName = "ABC", LastName = "XYZ" });
        }

        [PerfBenchmark(NumberOfIterations = 5, RunMode = RunMode.Throughput, RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.SixtyFourKb)]
        public void BenchMarkMethod_AddProject(BenchmarkContext context)
        {
            ProjectDbService _dbSvc = new ProjectDbService();
            _dbSvc.AddProject(new Project()
            {
                Project_Name = "PP_TEST",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Priority = 10
            });
        }

        [PerfBenchmark(NumberOfIterations = 5, RunMode = RunMode.Throughput, RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.SixtyFourKb)]
        public void BenchMarkMethod_AddTask(BenchmarkContext context)
        {
            ProjectDbService _dbSvc = new ProjectDbService();
            _dbSvc.AddTask(new DL.Task()
            {
                Task_Name = "TSK_TEST",
                Parent_ID = 2108,
                ParentTask = new ParentTask() { Parent_ID = 2108, Parent_Task = "Integrated Receivables" },
                Project_ID = 206,
                Usrs = new List<Usr>() { new Usr() { Usr_ID = 2440, Employee_ID = 562951, FirstName = "Abhinav", LastName = "Joshi", Project_ID = 206 } },
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Priority = 15,
                Status = "Completed"
            });
        }

        [PerfBenchmark(NumberOfIterations = 5, RunMode = RunMode.Throughput, RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.SixtyFourKb)]
        public void BenchMarkMethod_AddParentTask(BenchmarkContext context)
        {
            ProjectDbService _dbSvc = new ProjectDbService();
            _dbSvc.AddParentTask(new ParentTask() { Parent_ID = 2109, Parent_Task = "Quote Screen Updated" });
        }
        #endregion
    }
}
