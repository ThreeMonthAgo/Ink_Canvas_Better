namespace Ink_Canvas_Better.Utilities.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ComponentAttribute(Type viewType, string guid) : Attribute
{
    public Type ViewType { get; } = viewType;
    public string Guid { get; } = guid;
}
