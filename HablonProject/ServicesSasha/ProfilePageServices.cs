using Core.Core.ModelsSasha;
using HablonProject.ServicesSasha.BaseServise;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Documents;

namespace HablonProject.ServicesSasha;

public class ProfilePageServices : BaseServices
{
    public int GetStatuseProject(Employee employee)
    {
        List<ProjectAssignment> projectAssignment = GetProjectAssignment(employee);
        //foreach (ProjectAssignment projectAssignmentItem in projectAssignment)
        //{
        //    Debug.WriteLine("ProfilePageServices:    " + projectAssignmentItem.AssignmentID + " " + projectAssignmentItem.ProjectID + " " + projectAssignmentItem.EmployeeID);
        //}
        List<Project> project = GetProject(projectAssignment);
        
        List<Statuse> status = GetStatuse(project);
        int ProgectTask = 0;
        foreach (Statuse s in status)
        {
            if ("Не начато" == s.StatusName || "В работе" == s.StatusName || "На удерживании" == s.StatusName)
            {
                ProgectTask++;
            }
        }
        return ProgectTask;
    }
    public int GetStatuseTasks(Employee employee)
    {
        List<TaskAssignment> taskAssignments = GetTaskAssignment(employee);
        List<Tasks> tasks = GetTasks(taskAssignments);
        List<Statuse> statuses = GetStatuse(tasks);

        int taskIncriment = 0;

        foreach (Statuse s in statuses)
        {
            if ("В работе" == s.StatusName)
            {
                taskIncriment++;
            }
        }

        return taskIncriment;
    }
    public int GetStatuseComplit(Employee employee)
    {
        List<TaskAssignment> taskAssignments = GetTaskAssignment(employee);
        List<Tasks> tasks = GetTasks(taskAssignments);
        List<Statuse> statuses = GetStatuse(tasks);

        int complitIncriment = 0;

        foreach (Statuse s in statuses)
        {
            if ("Завершенный" == s.StatusName)
            {
                complitIncriment++;
            }
        }

        return complitIncriment;
    }
}