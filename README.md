# SimpleViewModel

A lightweight WPF ViewModel framework with automatic source generation that eliminates boilerplate code while maintaining full control over your view models.

[![NuGet Version](https://img.shields.io/nuget/v/SimpleViewModel)](https://www.nuget.org/packages/SimpleViewModel/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/SimpleViewModel)](https://www.nuget.org/packages/SimpleViewModel/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Features

- **Zero Boilerplate Commands**: Transform methods into ICommand properties with a simple `[Command]` attribute
- **Automatic Property Binding**: Generate observable properties from fields using `[Bind]` attribute
- **Source Generation**: All code generation happens at compile time with no runtime reflection
- **Lightweight**: Minimal dependencies and overhead
- **Type Safe**: Full IntelliSense support and compile-time validation
- **WPF Optimized**: Built specifically for WPF applications with proper CommandManager integration

## Table of Contents

- [Quick Start](#quick-start)
- [Advanced Features](#advanced-features)
- [How It Works](#how-it-works)
- [Generated Code Example](#generated-code-example)
- [Best Practices](#best-practices)
- [Troubleshooting](#troubleshooting)
- [Requirements](#requirements)
- [Contributing](#contributing)

## Quick Start

### Installation

```xml
<PackageReference Include="SimpleViewModel" Version="0.9.7" />
```

### Basic Usage

1. **Create a ViewModel**:

```csharp
using SimpleViewModel;
using SimpleViewModel.BaseClasses;

[ViewModel]
public partial class MainViewModel
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
public partial class UserViewModel
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

### Property Change Callbacks

Add custom logic when properties change:

```csharp
[ViewModel]
public partial class UserViewModel
{
    [Bind(OnChangeMethodName = nameof(OnNameChanged))]
    private string _name = "";
    
    private void OnNameChanged()
    {
        // Custom logic when name changes
        Console.WriteLine($"Name changed to: {_name}");
    }
}
```

### Command with CanExecute

Commands can include conditional execution logic:

```csharp
[ViewModel]
public partial class DocumentViewModel
{
    [Bind]
    private string _selectedItem = "";

    [Command(CanExecuteMethodName = nameof(CanDelete))]
    public void Delete()
    {
        // Delete logic here
        Console.WriteLine($"Deleting {_selectedItem}");
    }
    
    public bool CanDelete()
    {
        return !string.IsNullOrEmpty(_selectedItem);
    }
}
```

### Command with Parameters

Commands automatically support parameters through the Execute method:

```csharp
[ViewModel]
public partial class DocumentViewModel
{
    [Command]
    public void DeleteItem()
    {
        // Note: Parameter handling is done in the generated command class
        // The method itself doesn't need to accept parameters
    }
}
```

## How It Works

SimpleViewModel uses Roslyn source generators to analyze your code at compile time and automatically generate:

1. **Partial ViewModel Class**: Extends your class to inherit from `BaseViewModel`
2. **Command Classes**: Each `[Command]` method gets a corresponding command class that inherits from `BaseCommand`
3. **Command Properties**: Properties that expose the commands for data binding (lazily initialized)
4. **Observable Properties**: Properties with `INotifyPropertyChanged` support for `[Bind]` fields

All generated code is available in IntelliSense and can be debugged normally.

## Generated Code Example

**Your Code:**
```csharp
[ViewModel]
public partial class MyViewModel
{
    [Command]
    public void DoSomething() => Console.WriteLine("Done!");
}
```

**Generated ViewModel Extension:**
```csharp
public partial class MyViewModel : BaseViewModel
{
    private Command_DoSomething? _DoSomethingCommand { get; set; }
    public Command_DoSomething DoSomethingCommand => _DoSomethingCommand ??= new(this);
}
```

**Generated Command Class:**
```csharp
public sealed class Command_DoSomething : BaseCommand
{
    private readonly MyViewModel vm;
    
    public Command_DoSomething(MyViewModel vm)
    {
        this.vm = vm;
    }
    
    public override void Execute(object? parameter)
    {
        vm.DoSomething();
    }
}
```

## Best Practices

1. **Use Partial Classes**: Always mark your view models as `partial` to allow source generation
2. **Apply ViewModel Attribute**: Use `[ViewModel]` on your class - the generator automatically makes it inherit from `BaseViewModel`
3. **Field Naming**: Use underscore prefixes for fields marked with `[Bind]` (e.g., `_title` becomes `Title` property)
4. **Async Commands**: For async operations, use `async void` in command methods
5. **Parameter Validation**: Commands receive parameters through the `Execute(object? parameter)` method in the generated class
6. **Dependency Injection**: Use constructor injection for services and dependencies

## Troubleshooting

### Generator Not Running

If the source generator isn't creating commands:

1. Ensure you have the `[ViewModel]` attribute on your class
2. Make sure the class is marked as `partial`
3. Check that methods have the `[Command]` attribute and are public
4. Verify fields have the `[Bind]` attribute for observable properties
5. Clean and rebuild your solution

### Missing Commands in XAML

If commands aren't appearing in XAML IntelliSense:

1. Rebuild the project to trigger source generation
2. Check that the generated files are created (enable `<EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>` to see them)
3. Ensure proper namespace imports in XAML

### Common Issues

- **Fields not generating properties**: Make sure fields are marked with `[Bind]` and are private
- **Commands not working**: Ensure methods are public and marked with `[Command]`
- **CanExecute not working**: Verify the method name in `CanExecuteMethodName` exists and is public with bool return type

## Requirements

- .NET 8.0 or .NET 9.0 (Windows targets only)
- Windows (WPF applications only)
- C# 10.0 or later

## License

MIT License - see LICENSE file for details.

## Contributing

Contributions are welcome! Please feel free to submit issues and pull requests.

## Repository

- **🏠 GitHub**: [https://github.com/DerekGooding/SimpleViewModel](https://github.com/DerekGooding/SimpleViewModel)
- **📦 NuGet**: [https://www.nuget.org/packages/SimpleViewModel/](https://www.nuget.org/packages/SimpleViewModel/)
- **🐛 Issues**: [https://github.com/DerekGooding/SimpleViewModel/issues](https://github.com/DerekGooding/SimpleViewModel/issues)

---

*Built with ❤️ for the WPF community*