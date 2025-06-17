namespace SimpleViewModel;

/// <summary>
/// Marks a field for automatic observable property generation with <see cref="System.ComponentModel.INotifyPropertyChanged"/> support.
/// The source generator creates a public property that uses <see cref="BaseClasses.BaseViewModel.SetProperty{T}(ref T, T, string)"/>
/// to handle change notifications.
/// </summary>
/// <remarks>
/// The field name is converted to PascalCase for the generated property name.
/// Underscore prefixes are removed during conversion (e.g., "_title" becomes "Title").
/// </remarks>
/// <example>
/// <code>
/// [ViewModel]
/// public partial class MainViewModel
/// {
///     [Bind]
///     private string _title = "";
///
///     [Bind(OnChangeMethodName = nameof(OnNameChanged))]
///     private string _name = "";
///
///     private void OnNameChanged()
///     {
///         // Custom logic when name changes
///     }
/// }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Field)]
public sealed class BindAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the name of the method to call when the property value changes.
    /// The method must be parameterless and accessible from the view model class.
    /// </summary>
    /// <value>
    /// The name of the method to invoke after the property value is set, or <c>null</c> if no callback is needed.
    /// </value>
    /// <example>
    /// <code>
    /// [Bind(OnChangeMethodName = nameof(OnTitleChanged))]
    /// private string _title = "";
    ///
    /// private void OnTitleChanged()
    /// {
    ///     // React to title changes
    /// }
    /// </code>
    /// </example>
    public string? OnChangeMethodName { get; init; }
}
