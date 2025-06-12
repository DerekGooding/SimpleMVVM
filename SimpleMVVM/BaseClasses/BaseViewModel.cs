using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SimpleMVVM.BaseClasses;

/// <summary>
/// Provides a base implementation of <see cref="INotifyPropertyChanged"/> for view models in a WPF MVVM application.
/// Includes property change notification, design mode detection, and a common <c>IsInUse</c> property.
/// </summary>
public class BaseViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Indicates whether the view model is running in live (non-design) mode.
    /// </summary>
    protected readonly bool _isLive = !DesignerProperties.GetIsInDesignMode(new DependencyObject());

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Sets the property and raises the <see cref="PropertyChanged"/> event if the value changes.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="reference">A reference to the backing field.</param>
    /// <param name="value">The new value.</param>
    /// <param name="propertyName">The name of the property. This is optional and is automatically provided by the compiler.</param>
    protected void SetProperty<T>(ref T reference, T value, [CallerMemberName] string propertyName = "")
    {
        if (Equals(reference, value)) return;
        reference = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event for the specified property.
    /// </summary>
    /// <param name="propertyName">The name of the property. This is optional and is automatically provided by the compiler.</param>
    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
