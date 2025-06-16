namespace SimpleMVVM.Example;

[Singleton][ViewModel]
public partial class MainViewModel
{
    [Command]
    public void ShowMessage() => System.Windows.MessageBox.Show("Hello, World!");

    [Command]
    public void ShowElements()
    {
        var elements = App.Get<Elements>().All;
        System.Windows.MessageBox.Show(string.Join("\n", elements.Select(e => $"{e.Name}: {e.Description}")));
    }
}
