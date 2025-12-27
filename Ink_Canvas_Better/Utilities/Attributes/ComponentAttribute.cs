using System;
using System.Collections.Generic;
using System.Text;

namespace Ink_Canvas_Better.Utilities.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ComponentAttribute(Type viewType, string guid) : Attribute
{
    public Type ViewType { get; } = viewType;
    public string Guid { get; } = guid;
}
