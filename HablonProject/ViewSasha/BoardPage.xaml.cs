using Core.Core.ModelsSasha;
using HablonProject.ServicesSasha;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HablonProject.ViewSasha
{
    public partial class BoardPage : Page
    {
        private List<Tasks> TasksList = new(); // Здесь должны быть твои задачи (можно получить из БД)
        private readonly BoardPageServises _boardPageServises;
        private readonly Employee _employee;

        public BoardPage(Employee employee)
        {
            InitializeComponent();
            _employee = employee;
            _boardPageServises = new BoardPageServises();
            TasksList = _boardPageServises.GetTasks(_boardPageServises.GetTaskAssignment(employee));
            GenerateBoard(_boardPageServises.GetStatuse());
        }

        private void GenerateBoard(List<Statuse> statuse)
        {
            BoardStackPanel.Children.Clear();

            var groupedTasks = new Dictionary<string, List<Tasks>>();
            int i = 1;
            foreach (var status in statuse)
            {
                groupedTasks.Add(status.StatusName, TasksList.FindAll(t => t.StatusID == i));
                i++;
            }

            foreach (var group in groupedTasks)
            {
                BoardStackPanel.Children.Add(CreateSection(group.Key, group.Value));
            }
        }

        private UIElement CreateSection(string sectionTitle, List<Tasks> tasks)
        {
            var border = new Border
            {
                Background = GetSectionBackground(sectionTitle),
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(10),
                Margin = new Thickness(0, 0, 0, 15),
                //BorderBrush = GetSectionBorderBrush(sectionTitle),
                BorderThickness = new Thickness(1)
            };

            var stack = new StackPanel();

            var header = new StackPanel { Orientation = Orientation.Horizontal };
            header.Children.Add(new TextBlock
            {
                Text = sectionTitle,
                FontSize = 16,
                FontWeight = FontWeights.SemiBold
            });
            header.Children.Add(new TextBlock
            {
                Text = $"{tasks.Count} задач",
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#586069")),
                Margin = new Thickness(10, 0, 0, 0)
            });

            stack.Children.Add(header);

            foreach (var task in tasks)
            {
                stack.Children.Add(CreateTaskCard(task));
            }

            border.Child = stack;
            return border;
        }

        private UIElement CreateTaskCard(Tasks task)
        {
            var border = new Border
            {
                Background = Brushes.White,
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(10),
                Margin = new Thickness(0, 10, 0, 0),
                BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e1e4e8")),
                BorderThickness = new Thickness(1)
            };

            var stack = new StackPanel();

            var titleText = new TextBlock
            {
                Text = $"{task.TaskName} ({task.Priority})",
                FontWeight = task.Priority == "High" ? FontWeights.SemiBold : FontWeights.Normal,
                Foreground = task.Priority == "High"
                    ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d32f2f"))
                    : Brushes.Black
            };

            var descriptionText = new TextBlock
            {
                Text = string.IsNullOrEmpty(task.Description) ? "Нет описания" : task.Description,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#586069")),
                FontSize = 12,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 5, 0, 0)
            };

            stack.Children.Add(titleText);
            stack.Children.Add(descriptionText);

            border.Child = stack;
            return border;
        }


        private Brush GetSectionBackground(string section)
        {
            return section switch
            {
                "Не начато" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fff1f1")),
                "В работе" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fff8e1")),
                "На удерживании" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e3f2fd")),
                "Завершенный" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e8f5e9")),
                "Отмененный" => new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f8d7da")),
                _ => Brushes.White
            };
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем страницу/окно добавления новой задачи
            MainWindowServices.MainContentFrame.Content = new AddTaskPage(_employee); // если у тебя есть навигация через _frame
        }
    }
}
