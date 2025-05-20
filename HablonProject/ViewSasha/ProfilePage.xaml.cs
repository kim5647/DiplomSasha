using Core.Core.ModelsSasha;
using HablonProject.ServicesSasha;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;


namespace HablonProject.ViewSasha
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private readonly ProfilePageServices _profilePageServices;
        public ProfilePage(Employee employee)
        {
            InitializeComponent();

            _profilePageServices = new ProfilePageServices();

            Position position = new Position();
            Department department = new Department();

            position = _profilePageServices.GetPostion(employee);

            department = _profilePageServices.GetDepartment(position);

            FullNameLable.Text = employee.FirstName + " " + employee.LastName;

            DepartmentLable.Text += department.DepartmentName;

            PostionNameLable.Text = position.PositionName;

            EmailLable.Text += employee.Email;

            PrefixLable.Text = employee.FirstName[0].ToString() + employee.LastName[0].ToString();

            ComplitLable.Text = _profilePageServices.GetStatuseComplit(employee).ToString();

            TasksLable.Text = _profilePageServices.GetStatuseTasks(employee).ToString();

            TuringProgectLable.Text = _profilePageServices.GetStatuseProject(employee).ToString();
        }
    }
}
