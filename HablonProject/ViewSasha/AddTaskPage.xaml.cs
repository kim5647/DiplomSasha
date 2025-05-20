// File: ViewSasha/AddTaskPage.xaml.cs

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Core.Core.ModelsSasha;
using HablonProject.ServicesSasha;

namespace HablonProject.ViewSasha
{
    public partial class AddTaskPage : Page
    {
        private List<Statuse> Statuses;
        private List<Employee> Employees;
        private List<Project> Projects;
        private List<TaskAssignment> Assignments = new();
        private readonly Frame _frame;
        private readonly Employee _employee;
        private readonly AddTaskPageServices _addTaskPageServices;

        public AddTaskPage(Employee employee)
        {
            InitializeComponent();
            _frame = MainWindowServices.MainContentFrame;
            _employee = employee;
            _addTaskPageServices = new AddTaskPageServices();
            LoadData();
        }

        private void LoadData()
        {
            Statuses = _addTaskPageServices.GetStatuse();
            Employees = _addTaskPageServices.GetEmployee();
            Projects = _addTaskPageServices.GetProject();

            StatusComboBox.ItemsSource = Statuses;
            StatusComboBox.DisplayMemberPath = "StatusName";
            StatusComboBox.SelectedValuePath = "StatusID";

            EmployeeComboBox.ItemsSource = Employees;
            EmployeeComboBox.DisplayMemberPath = "FullName";
            EmployeeComboBox.SelectedValuePath = "EmployeeID";

            ProjectComboBox.ItemsSource = Projects;
            ProjectComboBox.DisplayMemberPath = "ProjectName";
            ProjectComboBox.SelectedValuePath = "ProjectID";
        }

        private void AddEmployeeToTask_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeComboBox.SelectedItem is Employee selectedEmployee)
            {
                var assignment = new TaskAssignment
                {
                    TaskID = 0,
                    EmployeeID = selectedEmployee.EmployeeID.Value,
                    AssignedDate = DateTime.Now
                };

                Assignments.Add(assignment);
                AssignedEmployeesListBox.Items.Add($"{selectedEmployee.FirstName} {selectedEmployee.LastName}");
            }
        }

        private void RemoveEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (AssignedEmployeesListBox.SelectedIndex >= 0)
            {
                int indexToRemove = AssignedEmployeesListBox.SelectedIndex;

                if (indexToRemove < Assignments.Count)
                {
                    Assignments.RemoveAt(indexToRemove);
                    AssignedEmployeesListBox.Items.RemoveAt(indexToRemove);
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

        private void SaveTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskNameTextBox.Text) || StatusComboBox.SelectedItem == null || ProjectComboBox.SelectedItem == null)
            {
                MessageBox.Show("Заполните обязательные поля: Название задачи, Статус и Проект");
                return;
            }

            var task = new Tasks(
                taskName: TaskNameTextBox.Text,
                dueDate: DueDatePicker.SelectedDate ?? DateTime.Now,
                priority: (PriorityComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "",
                statusId: (int)StatusComboBox.SelectedValue,
                projectId: (int)ProjectComboBox.SelectedValue,
                description: DescriptionTextBox.Text
            );


            _addTaskPageServices.AddTask(task);

            task = _addTaskPageServices.GetTask(task);

            foreach (var assignment in Assignments)
            {
                if (task.TaskID != null)
                {
                    assignment.TaskID = (int)task.TaskID;
                }
            }

            _addTaskPageServices.AddTaskAssignment(Assignments);

            _frame.Content = new BoardPage(_employee);
        }
    }
}