using System.Windows;

namespace SimpleMVVM.Example;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
[Singleton]
public partial class MainWindow : Window
{
    private readonly MainViewModel _mainViewModel;
    public MainWindow(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        DataContext = _mainViewModel;
        InitializeComponent();
    }
}