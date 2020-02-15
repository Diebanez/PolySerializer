using System;

namespace PolySerializer
{
/// <summary>
/// Attribute used to mark a field to be serialized by a <see cref="PolySerializer.Serializer"/>
/// </summary>
public class XmlSerializedAttribute : Attribute
{
    /// <summary>
    /// Attribute used to mark a field to be serialized by a <see cref="PolySerializer.Serializer"/>
    /// </summary>
    public XmlSerializedAttribute(){}
}
}