using Core.Core.ModelsSasha;
using HablonProject.ServicesSasha.BaseServise;
using System.Collections.Generic;

namespace HablonProject.ServicesSasha;

public class AddProjectPageServices : BaseServices
{
    public void AddProject(Project project) => _projectRepository.AddProject(project);
    public void AddProjectAssignment(List<ProjectAssignment> projectAssignment) => _projectAssignmentRepository.AddProjectAssignment(projectAssignment);
}

