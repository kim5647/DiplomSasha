using Core.Core.ModelsSasha;
using HablonProject.ViewSasha;
using HablonProject.ServicesSasha;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Input;


namespace HablonProject.ViewsSasha
{
    public partial class AuthView : Window
    {
        // Строка подключения к базе данных
        private readonly AuthViewServices _authViewServices;

        public AuthView()
        {
            InitializeComponent();
            EmailTextBox.Focus();
            _authViewServices = new AuthViewServices();
        }
        private void LoginButton_Click_1(object sender, RoutedEventArgs e)
        {
            string login = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password;

            Users users = new Users(login, password);

            try
            {
                Debug.WriteLine(_authViewServices.AuthorizationUser(users));
                if (_authViewServices.AuthorizationUser(users))
                {
                    users = _authViewServices.GetUsers(users);

                    Debug.WriteLine(users.UserID.ToString() + " " + users.Login + " " + users.Password);
                    // Авторизация успешна, открываем главное окно
                    MainWindow mainWindow = new MainWindow(users);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Введите коректные данные");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при авторизации: {ex.Message}");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
