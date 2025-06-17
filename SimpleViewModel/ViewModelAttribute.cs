namespace SimpleViewModel;

/// <summary>
/// Marks a class for view model code generation. When applied to a class, the source generator
/// creates a partial class that inherits from <see cref="BaseClasses.BaseViewModel"/> and includes
/// auto-generated properties and commands based on <see cref="BindAttribute"/> and <see cref="CommandAttribute"/>.
/// </summary>
/// <remarks>
/// The target class must be declared as partial to allow the generator to extend it.
/// Generated code includes observable properties from fields marked with <see cref="BindAttribute"/>
/// and ICommand implementations from methods marked with <see cref="CommandAttribute"/>.
/// </remarks>
/// <example>
/// <code>
/// [ViewModel]
/// public partial class MainViewModel
/// {
///     [Bind]
///     private string _title = "";
///
///     [Command]
///     public void Save() { /* implementation */ }
/// }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class)]
public sealed class ViewModelAttribute : Attribute;
