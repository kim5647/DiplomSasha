using Core.Core.ModelsSasha;
using System.Collections.Generic;
using HablonProject.ServicesSasha.BaseServise;
using System.Windows.Media;

namespace HablonProject.ServicesSasha;


public class ProjectsPageServices : BaseServices
{
    public List<Project> GetProject(Employee employee)
    {
        List<ProjectAssignment> projectAssignment = GetProjectAssignment(employee);
        
        return GetProject(projectAssignment);
    }
    public string GetStatusName(int? statusId)
    {
        return statusId switch
        {
            1 => "Не начато",
            2 => "В работе",
            3 => "На удерживании",
            4 => "Завершенный",
            5 => "Отмененный",
            _ => "Неизвестно"
        };
    }

    public Color GetStatusColor(int? statusId)
    {
        return statusId switch
        {
            1 => Colors.Gray,
            2 => (Color)ColorConverter.ConvertFromString("#42A5F5"),
            3 => (Color)ColorConverter.ConvertFromString("#FFA726"),
            4 => (Color)ColorConverter.ConvertFromString("#66BB6A"),
            5 => (Color)ColorConverter.ConvertFromString("#EF5350"),
            _ => Colors.Black
        };
    }
}