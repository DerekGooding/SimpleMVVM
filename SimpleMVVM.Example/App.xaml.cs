using System.Windows;

namespace SimpleMVVM.Example;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static readonly Host _host = Host.Initialize();
    public static T Get<T>() where T : class => _host.Get<T>();

    private void Application_Startup(object sender, StartupEventArgs e) => _host.Get<MainWindow>().Show();
}