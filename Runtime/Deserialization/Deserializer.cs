using System;
using System.IO;
using System.Xml;

namespace PolySerializer
{
/// <summary>
/// Class which allows to deserialize an object from an xml-format text
/// </summary>
public class Deserializer
{
    /// <summary>
    /// Deserialize an object from a string
    /// </summary>
    /// <param name="sourceString">The string containing the xml-format serialized object</param>
    /// <returns>The deserialized object</returns>
    public object DeserializeFromString(string sourceString)
    {
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(sourceString);

        return DeserializeToObject(xmlDoc.LastChild);
    }

    /// <summary>
    /// Deserialize an object from a file
    /// </summary>
    /// <param name="filePath">The path of the file containing the xml-format serialized object</param>
    /// <returns>The deserialized object</returns>
    public object DeserializeFromFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException();

        var text = File.ReadAllText(filePath);
        return DeserializeFromString(text);
    }

    /// <summary>
    /// Private recursive method, used to deserialized an object
    /// </summary>
    /// <param name="node">The <see cref="XmlNode"/> actually being deserialized</param>
    /// <returns>The object deserialized based on the node value</returns>
    private object DeserializeToObject(XmlNode node)
    {
        var newObject = Activator.CreateInstance(Type.GetType(node.Attributes["type"].Value));

        var objectType = newObject.GetType();
        
        foreach (XmlElement childNode in node.ChildNodes)
        {
            var field = objectType.GetField(childNode.Name);
            if (field != null)
            {
                var childType = Type.GetType(childNode.GetAttribute("type"));
                if (childType == typeof(char))
                {
                    var fieldValue = char.Parse(childNode.FirstChild.Value);
                    field.SetValue(newObject, fieldValue);
                }
                else if (childType == typeof(string))
                {
                    var fieldValue = childNode.FirstChild.Value;
                    field.SetValue(newObject, fieldValue);
                }
                else if (childType == typeof(bool))
                {
                    var fieldValue = childNode.FirstChild.Value != "0";
                    field.SetValue(newObject, fieldValue);
                }
                else if (childType == typeof(sbyte))
                {
                    var fieldValue = sbyte.Parse(childNode.FirstChild.Value);
                    field.SetValue(newObject, fieldValue);
                }
                else if (childType == typeof(byte))
                {
                    var fieldValue = byte.Parse(childNode.FirstChild.Value);
                    field.SetValue(newObject, fieldValue);
                }
                else if (childType == typeof(short))
                {
                    var fieldValue = short.Parse(childNode.FirstChild.Value);
                    field.SetValue(newObject, fieldValue);
                }
                else if (childType == typeof(ushort))
                {
                    var fieldValue = ushort.Parse(childNode.FirstChild.Value);
                    field.SetValue(newObject, fieldValue);
                }
                else if (childType == typeof(int))
                {
                    var fieldValue = int.Parse(childNode.FirstChild.Value);
                    field.SetValue(newObject, fieldValue);
                }
                else if (childType == typeof(uint))
                {
                    var fieldValue = uint.Parse(childNode.FirstChild.Value);
                    field.SetValue(newObject, fieldValue);
                }
                else if (childType == typeof(long))
                {
                    var fieldValue = long.Parse(childNode.FirstChild.Value);
                    field.SetValue(newObject, fieldValue);
                }
                else if (childType == typeof(ulong))
                {
                    var fieldValue = ulong.Parse(childNode.FirstChild.Value);
                    field.SetValue(newObject, fieldValue);
                }
                else if (childType == typeof(double))
                {
                    var fieldValue = double.Parse(childNode.FirstChild.Value);
                    field.SetValue(newObject, fieldValue);
                }
                else if (childType == typeof(decimal))
                {
                    var fieldValue = decimal.Parse(childNode.FirstChild.Value);
                    field.SetValue(newObject, fieldValue);
                }
                else if (childType.IsEnum)
                {
                    var fieldValue = int.Parse(childNode.FirstChild.Value);
                    field.SetValue(newObject, fieldValue);
                }
                else
                {
                    field.SetValue(newObject, DeserializeToObject(childNode));
                }
            }
        }

        return newObject;
    }
}
}