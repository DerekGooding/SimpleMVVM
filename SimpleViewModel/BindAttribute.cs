namespace SimpleViewModel;

[AttributeUsage(AttributeTargets.Field)]
public sealed class BindAttribute : Attribute
{
    public string? OnChangeMethodName { get; init; }
}
