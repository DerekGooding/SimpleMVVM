namespace SimpleMVVM.Example;

public record struct Element(string Name, string Description) : INamed;
[Singleton]
public partial class Elements : IContent<Element>
{
    public Element[] All { get; } =
        [
            new Element("Element 1", "Description of Element 1"),
            new Element("Element 2", "Description of Element 2"),
            new Element("Element 3", "Description of Element 3"),
            new Element("Element 4", "Description of Element 4"),
            new Element("Element 5", "Description of Element 5")
        ];
}
