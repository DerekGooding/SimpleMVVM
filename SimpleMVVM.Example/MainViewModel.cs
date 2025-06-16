namespace SimpleMVVM.Example;

[Singleton, ViewModel]
public partial class MainViewModel
{
    [Command]
    public void ShowMessage() => System.Windows.MessageBox.Show("Hello, World!");
}
