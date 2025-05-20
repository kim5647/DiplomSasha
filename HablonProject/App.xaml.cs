using System.Windows;

public partial class App : Application
{
    //private IHost _host;
    //private async void Application_Startup(object sender, StartupEventArgs e)
    //{
    //    _host = Host.CreateDefaultBuilder()
    //        .ConfigureServices((context, services) =>
    //        {
    //            // Регистрация DbContext (если используете EF Core)
    //            services.AddDbContext<DBContext>();  // Замените YourDbContext

    //            // Регистрация репозиториев
    //            services.AddScoped<IUserRepository, UserRepository>();
    //            services.AddScoped<IProjectRepository, ProjectRepository>();
    //            // ... другие репозитории

    //            // Регистрация сервисов
    //            services.AddTransient<AuthViewServices>();
    //            services.AddTransient<MainWindowServices>();
    //            services.AddTransient<ProfilePageServices>();

    //            // Регистрация окон и страниц
    //            services.AddTransient<AuthView>();
    //            services.AddTransient<MainWindow>();
    //            services.AddTransient<ProfilePage>();
    //        })
    //        .Build();

    //    await _host.StartAsync();

    //    // Запуск AuthView (стартовая страница)
    //    var authView = _host.Services.GetRequiredService<AuthView>();
    //    authView.Show();
    //}

    //protected override async void OnExit(ExitEventArgs e)
    //{
    //    await _host.StopAsync();
    //    _host.Dispose();
    //    base.OnExit(e);
    //}
}