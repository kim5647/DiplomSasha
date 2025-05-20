using Core.Core.ModelsSasha;
using HablonProject.ServicesSasha;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;


namespace HablonProject.ViewSasha
{
    public partial class MainWindow : Window
    {
        private readonly Employee _employee;
        private readonly MainWindowServices _mainWindowServices;
        public MainWindow(Users users)
        {
            InitializeComponent();

            _mainWindowServices = new MainWindowServices(MainContentFrame);

            Debug.WriteLine(users.UserID.ToString() + " " + users.Login + " " + users.Password);

            _employee = _mainWindowServices.GetEmployee(users);

            // Загружаем страницу профиля при запуске
            MainContentFrame.Content = new ProfilePage(_employee);
            HighlightSelectedButton(btnProfile);
        }
        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            // Сбрасываем стили всех кнопок
            ResetButtonStyles();

            // Устанавливаем стиль для выбранной кнопки
            HighlightSelectedButton(button);

            // Загружаем соответствующую страницу
            if (button == btnProfile)
                MainContentFrame.Content = new ProfilePage(_employee);
            else if (button == btnProjects)
                MainContentFrame.Content = new ProjectsPage(_employee);
            else if (button == btnBoard)
                MainContentFrame.Content = new BoardPage(_employee);
        }

        private void ResetButtonStyles()
        {
            btnProfile.Style = (Style)FindResource("NavButtonStyle");
            btnProjects.Style = (Style)FindResource("NavButtonStyle");
            btnBoard.Style = (Style)FindResource("NavButtonStyle");
        }

        private void HighlightSelectedButton(Button button)
        {
            button.Style = (Style)FindResource("SelectedNavButtonStyle");
        }

        private void Button_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
