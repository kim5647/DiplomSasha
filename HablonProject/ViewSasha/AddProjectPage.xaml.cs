using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Core.Core.ModelsSasha;
using HablonProject.ServicesSasha;

namespace HablonProject.ViewSasha
{
    public partial class AddProjectPage : Page
    {
        private List<Statuse> Statuses;
        private List<Employee> Employees;
        private List<Role> Roles;
        private List<ProjectAssignment> Assignments = new();
        private readonly Frame _frame;
        private readonly Employee _employee;
        private readonly AddProjectPageServices _addProjectPageServices;

        public AddProjectPage(Employee employee)
        {
            InitializeComponent();
            _frame = MainWindowServices.MainContentFrame;
            _employee = employee;
            _addProjectPageServices = new AddProjectPageServices();
            LoadData();
        }

        private void LoadData()
        {
            Statuses = _addProjectPageServices.GetStatuse();
            Employees = _addProjectPageServices.GetEmployee();
            Roles = _addProjectPageServices.GetRole();

            StatusComboBox.ItemsSource = Statuses;
            StatusComboBox.DisplayMemberPath = "StatusName";
            StatusComboBox.SelectedValuePath = "StatusID";

            EmployeeComboBox.ItemsSource = Employees;
            EmployeeComboBox.DisplayMemberPath = "FullName"; // или FirstName + LastName если нужно
            EmployeeComboBox.SelectedValuePath = "EmployeeID";

            RoleComboBox.ItemsSource = Roles;
            RoleComboBox.DisplayMemberPath = "RoleName";
            RoleComboBox.SelectedValuePath = "RoleID";
        }

        private void AddEmployeeToProject_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeComboBox.SelectedItem is Employee selectedEmployee && RoleComboBox.SelectedItem is Role selectedRole)
            {
                var assignment = new ProjectAssignment
                {
                    ProjectID = 0, // временно 0, при сохранении проекта обновим
                    EmployeeID = selectedEmployee.EmployeeID.Value,
                    RoleID = selectedRole.RoleID.Value,
                    AssignedDate = DateTime.Now
                };

                Assignments.Add(assignment);
                AssignedEmployeesListBox.Items.Add($"{selectedEmployee.FirstName} - {selectedRole.RoleName}");
            }
        }
        private void RemoveEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (AssignedEmployeesListBox.SelectedIndex >= 0)
            {
                int indexToRemove = AssignedEmployeesListBox.SelectedIndex;

                if (indexToRemove < Assignments.Count)
                {
                    Assignments.RemoveAt(indexToRemove); // удаляем из Assignments
                    AssignedEmployeesListBox.Items.RemoveAt(indexToRemove); // удаляем из визуального списка
                }
            }
        }
        private void AssignedEmployeesListBox_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                var item = listBox.InputHitTest(e.GetPosition(listBox)) as FrameworkElement;
                if (item?.DataContext != null)
                {
                    listBox.SelectedItem = item.DataContext;
                }
            }
        }

        private void SaveProject_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProjectNameTextBox.Text) || StatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Заполните обязательные поля: Название проекта и Статус");
                return;
            }

            var project = new Project
            {
                ProjectName = ProjectNameTextBox.Text,
                StartDate = StartDatePicker.SelectedDate ?? DateTime.Now,
                EndDate = EndDatePicker.SelectedDate,
                Budget = decimal.TryParse(BudgetTextBox.Text, out decimal budget) ? budget : 0,
                ClientName = ClientTextBox.Text,
                StatusID = (int)StatusComboBox.SelectedValue,
                Description = DescriptionTextBox.Text
            };

            _addProjectPageServices.AddProject(project);

            project = _addProjectPageServices.GetProject(project);

            foreach (var item in Assignments)
            {
                if (project.ProjectID != null)
                {
                    item.ProjectID = (int)project.ProjectID;
                }
            }

            _addProjectPageServices.AddProjectAssignment(Assignments);

            // Переход обратно на страницу проектов
            _frame.Content = new ProjectsPage(_employee);
        }
    }
}
