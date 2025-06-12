using System.Windows.Input;

namespace SimpleMVVM;

/// <summary>
/// Provides a base implementation of the <see cref="ICommand"/> interface for WPF MVVM scenarios.
/// Handles the <see cref="CanExecuteChanged"/> event using <see cref="CommandManager.RequerySuggested"/>.
/// Override <see cref="CanExecute"/> and <see cref="Execute"/> in derived classes to implement command logic.
/// </summary>
public class BaseCommand : ICommand
{
    /// <inheritdoc/>
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <summary>
    /// Determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">Data used by the command. If the command does not require data, this object can be set to null.</param>
    /// <returns>true if this command can be executed; otherwise, false.</returns>
    public virtual bool CanExecute(object? parameter) => true;

    /// <summary>
    /// Defines the method to be called when the command is invoked.
    /// </summary>
    /// <param name="parameter">Data used by the command. If the command does not require data, this object can be set to null.</param>
    /// <exception cref="NotImplementedException">Thrown if not overridden in a derived class.</exception>
    public virtual void Execute(object? parameter) => throw new NotImplementedException();
}
