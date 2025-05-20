using Core.Core.ModelsSasha;
using HablonProject.ServicesSasha.BaseServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HablonProject.ServicesSasha;

public class AddTaskPageServices : BaseServices
{
    public List<Project> GetProject() => _projectRepository.GetAllProjects();
    //AddTask
    public void AddTask(Tasks tasks) => _tasksRepository.AddTask(tasks);
    //GetTask
    public Tasks GetTask(Tasks tasks) => _tasksRepository.GetOneTasks(tasks);
    //AddTaskAssignment
    public void AddTaskAssignment(List<TaskAssignment> taskAssignments) => _taskAssignmentRepository.AddTaskAssignment(taskAssignments);
}

