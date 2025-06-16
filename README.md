# SimpleMVVM

A lightweight WPF MVVM framework with automatic source generation that eliminates boilerplate code while maintaining full control over your view models.

## Features

- **Zero Boilerplate Commands**: Transform methods into ICommand properties with a simple `[Command]` attribute
- **Automatic Property Binding**: Generate observable properties from fields using `[Bind]` attribute
- **Source Generation**: All code generation happens at compile time with no runtime reflection
- **Lightweight**: Minimal dependencies and overhead
- **Type Safe**: Full IntelliSense support and compile-time validation
- **WPF Optimized**: Built specifically for WPF applications with proper CommandManager integration

## Quick Start

### Installation

```xml
<PackageReference Include="SimpleMVVM" Version="0.9.0" />
```

### Basic Usage

1. **Create a ViewModel**:

```csharp
using SimpleMVVM;
using SimpleMVVM.BaseClasses;

[ViewModel]
public partial class MainViewModel : BaseViewModel
{
    [Command]
    public void SaveData()
    {
        // Your save logic here
        MessageBox.Show("Data saved!");
    }

    [Command] 
    public void LoadData()
    {
        // Your load logic here
        MessageBox.Show("Data loaded!");
    }
}
```

2. **Bind to XAML**:

```xml
<Window x:Class="MyApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <StackPanel>
        <Button Content="Save" Command="{Binding SaveDataCommand}" />
        <Button Content="Load" Command="{Binding LoadDataCommand}" />
    </StackPanel>
</Window>
```

3. **Set DataContext**:

```csharp
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
```

That's it! The source generator automatically creates `SaveDataCommand` and `LoadDataCommand` properties for you.

## Advanced Features

### Observable Properties

Use the `[Bind]` attribute to automatically generate observable properties:

```csharp
[ViewModel]
public partial class UserViewModel : BaseViewModel
{
    [Bind]
    private string _firstName = "";
    
    [Bind]
    private string _lastName = "";
    
    [Bind]
    private int _age;
}
```

Generated code includes proper `INotifyPropertyChanged` implementation:

```csharp
public string FirstName
{
    get => _firstName;
    set => SetProperty(ref _firstName, value);
}
```

### Command with Parameters

Commands automatically support parameters:

```csharp
[ViewModel]
public partial class DocumentViewModel : BaseViewModel
{
    [Command]
    public void DeleteItem(object parameter)
    {
        if (parameter is string itemId)
        {
            // Delete logic here
        }
    }
}
```

### Integration with Dependency Injection

SimpleMVVM works seamlessly with dependency injection containers:

```csharp
// Using SimpleInjection (companion package)
[Singleton][ViewModel]
public partial class MainViewModel : BaseViewModel
{
    private readonly IDataService _dataService;
    
    public MainViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }
    
    [Command]
    public async void LoadData()
    {
        var data = await _dataService.GetDataAsync();
        // Handle data
    }
}
```

## How It Works

SimpleMVVM uses Roslyn source generators to analyze your code at compile time and automatically generate:

1. **Command Classes**: Each `[Command]` method gets a corresponding `ICommand` implementation
2. **Command Properties**: Properties that expose the commands for data binding
3. **Observable Properties**: Properties with `INotifyPropertyChanged` support for `[Bind]` fields

All generated code is available in IntelliSense and can be debugged normally.

## Generated Code Example

**Your Code:**
```csharp
[ViewModel]
public partial class MyViewModel : BaseViewModel
{
    [Command]
    public void DoSomething() => Console.WriteLine("Done!");
}
```

**Generated Code:**
```csharp
public partial class MyViewModel
{
    private DoSomethingCommand? _doSomethingCommand;
    public DoSomethingCommand DoSomethingCommand => _doSomethingCommand ??= new DoSomethingCommand(this);
}

public sealed class DoSomethingCommand : BaseCommand
{
    private readonly MyViewModel _viewModel;
    
    public DoSomethingCommand(MyViewModel viewModel)
    {
        _viewModel = viewModel;
    }
    
    public override void Execute(object? parameter)
    {
        _viewModel.DoSomething();
    }
}
```

## Best Practices

1. **Inherit from BaseViewModel**: Always inherit from `BaseViewModel` for proper `INotifyPropertyChanged` support
2. **Use Partial Classes**: Mark your view models as `partial` to allow source generation
3. **Async Commands**: For async operations, use `async void` in command methods
4. **Parameter Validation**: Always validate parameters in command methods
5. **Dependency Injection**: Use constructor injection for services and dependencies

## Troubleshooting

### Generator Not Running

If the source generator isn't creating commands:

1. Ensure you have the `[ViewModel]` attribute on your class
2. Make sure the class is marked as `partial`
3. Verify you're inheriting from `BaseViewModel`
4. Check that methods have the `[Command]` attribute
5. Clean and rebuild your solution

### Missing Commands in XAML

If commands aren't appearing in XAML IntelliSense:

1. Rebuild the project to trigger source generation
2. Check that the generated files are created (enable `<EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>` to see them)
3. Ensure proper namespace imports in XAML

## Requirements

- .NET 8.0 or .NET 9.0
- Windows (WPF applications only)
- C# 10.0 or later

## License

MIT License - see LICENSE file for details.

## Contributing

Contributions are welcome! Please feel free to submit issues and pull requests.

## Related Packages

- **SimpleInjection**: Companion dependency injection container with automatic service discovery