namespace SimpleViewModel;

[AttributeUsage(AttributeTargets.Method)]
public sealed class CommandAttribute : Attribute
{
    public string? CanExecuteMethodName { get; init; }
}