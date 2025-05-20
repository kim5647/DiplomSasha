using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Core.Core.ModelsSasha;
using HablonProject.ServicesSasha;

namespace HablonProject.ViewSasha
{
    public partial class ProjectsPage : Page
    {
        private readonly ProjectsPageServices _projectsPageServices;
        private readonly Frame _frame;
        private readonly Employee _employee;
        public ProjectsPage(Employee employee)
        {
            InitializeComponent();

            _employee = employee;

            _frame = MainWindowServices.MainContentFrame;

            _projectsPageServices = new ProjectsPageServices();

            List<Project> projects = _projectsPageServices.GetProject(employee);

            LoadProjects(projects);
        }

        private void LoadProjects(List<Project> projects)
        {
            var uniqueProjects = new Dictionary<int, Project>();
            foreach (var project in projects)
            {
                if (!uniqueProjects.ContainsKey((int)project.ProjectID)) // предположим, что ProjectId — уникальный идентификатор проекта
                {
                    uniqueProjects.Add((int)project.ProjectID, project);
                }
            }

            foreach (var project in uniqueProjects.Values)
            {
                var border = new Border
                {
                    Background = new SolidColorBrush(Colors.White),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(225, 228, 232)),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(4),
                    Padding = new Thickness(10),
                    Margin = new Thickness(0, 0, 0, 10)
                };

                var stackPanel = new StackPanel();

                var projectName = new TextBlock
                {
                    Text = project.ProjectName,
                    FontSize = 16,
                    FontWeight = FontWeights.SemiBold,
                    Margin = new Thickness(0, 0, 0, 5)
                };

                var clientName = new TextBlock
                {
                    Text = $"Клиент: {project.ClientName}",
                    Margin = new Thickness(0, 2, 0, 2)
                };

                var budget = new TextBlock
                {
                    Text = $"Бюджет: {project.Budget:C}",
                    Margin = new Thickness(0, 2, 0, 2)
                };

                var startDate = new TextBlock
                {
                    Text = $"Начало: {project.StartDate:dd.MM.yyyy}",
                    Margin = new Thickness(0, 2, 0, 2)
                };

                var endDate = new TextBlock
                {
                    Text = $"Дедлайн: {(project.EndDate.HasValue ? project.EndDate.Value.ToString("dd.MM.yyyy") : "Не указан")}",
                    Margin = new Thickness(0, 2, 0, 2)
                };

                var status = new TextBlock
                {
                    Text = $"Статус: {_projectsPageServices.GetStatusName(project.StatusID)}",
                    Foreground = new SolidColorBrush(_projectsPageServices.GetStatusColor(project.StatusID)),
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 5, 0, 2)
                };

                stackPanel.Children.Add(projectName);
                stackPanel.Children.Add(clientName);
                stackPanel.Children.Add(budget);
                stackPanel.Children.Add(startDate);
                stackPanel.Children.Add(endDate);
                stackPanel.Children.Add(status);

                if (!string.IsNullOrWhiteSpace(project.Description))
                {
                    var description = new TextBlock
                    {
                        Text = $"Комментарий: {project.Description}",
                        TextWrapping = TextWrapping.Wrap,
                        Margin = new Thickness(0, 5, 0, 0),
                        Foreground = new SolidColorBrush(Color.FromRgb(88, 96, 105))
                    };
                    stackPanel.Children.Add(description);
                }

                border.Child = stackPanel;
                ProjectsStackPanel.Children.Add(border);
            }

            var addButton = new Button
            {
                Content = "+ Добавить проект",
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(0, 15, 0, 0),
                Background = new SolidColorBrush(Color.FromRgb(246, 248, 250)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(225, 228, 232)),
                BorderThickness = new Thickness(1),
                Foreground = new SolidColorBrush(Color.FromRgb(36, 41, 46)),
                Padding = new Thickness(10, 5, 10, 5)
            };

            addButton.Click += AddButton_Click;

            ProjectsStackPanel.Children.Add(addButton);
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            _frame.Content = new AddProjectPage(_employee);
        }


    }
}
