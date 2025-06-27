namespace SimpleViewModel;

/// <summary>
/// Marks a method for automatic <see cref="System.Windows.Input.ICommand"/> generation.
/// The source generator creates a command class that inherits from <see cref="BaseClasses.BaseCommand"/>
/// and exposes the method as an executable command for WPF data binding.
/// </summary>
/// <remarks>
/// The generated command class is named "Command_{MethodName}" and is exposed as a property
/// named "{MethodName}Command" on the view model. Commands are lazily initialized when first accessed.
/// The target method must be public and parameterless by default, or can accept a single parameter if AcceptParameter is true.
/// </remarks>
/// <example>
/// <code>
/// [ViewModel]
/// public partial class MainViewModel
/// {
///     [Command]
///     public void Save()
///     {
///         // Save implementation
///     }
///
///     [Command(CanExecuteMethodName = nameof(CanDelete))]
///     public void Delete()
///     {
///         // Delete implementation
///     }
///
///     [Command(AcceptParameter = true)]
///     public void DeleteWithParam(object? param)
///     {
///         // Use param
///     }
///
///     public bool CanDelete()
///     {
///         return _selectedItem != null;
///     }
/// }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Method)]
public sealed class CommandAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the name of the method that determines whether the command can execute.
    /// The method must be public, parameterless, and return a <see cref="bool"/>.
    /// </summary>
    /// <value>
    /// The name of the method that returns <c>true</c> if the command can execute; otherwise, <c>false</c>.
    /// If <c>null</c>, the command can always execute.
    /// </value>
    public string? CanExecuteMethodName { get; init; }

    /// <summary>
    /// Gets or sets whether the command should pass the XAML parameter to the method.
    /// If true, the method must accept a single parameter of type object? (or compatible type).
    /// Default is false (parameterless method).
    /// </summary>
    public bool AcceptParameter { get; init; } = false;
}