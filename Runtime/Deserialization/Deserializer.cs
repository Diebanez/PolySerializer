using System;
using System.Collections.Generic;
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
    // private object DeserializeToObject(XmlNode node)
    // {
    //     object newObject = Activator.CreateInstance(Type.GetType(node.Attributes["type"].Value));
    //
    //     var objectType = newObject.GetType();
    //
    //     foreach (XmlElement childNode in node.ChildNodes)
    //     {
    //         var field = objectType.GetField(childNode.Name);
    //         if (field != null)
    //         {
    //             var childType = Type.GetType(childNode.GetAttribute("type"));
    //             if (childType.IsArray)
    //             {
    //                 List<object> newArray = new List<object>();
    //                 foreach (XmlElement arrayChild in childNode.ChildNodes)
    //                 {
    //                     var newElement = DeserializeToObject(arrayChild);
    //                     if (newElement != null)
    //                     {
    //                         newArray.Add(newElement);
    //                     }
    //                 }
    //
    //                 field.SetValue(newObject, newArray.ToArray());
    //             }
    //             else if (childType.IsGenericType && (childType.GetGenericTypeDefinition() == typeof(List<>)))
    //             {
    //             }
    //             else if (childType == typeof(char))
    //             {
    //                 var fieldValue = char.Parse(childNode.FirstChild.Value);
    //                 field.SetValue(newObject, fieldValue);
    //             }
    //             else if (childType == typeof(string))
    //             {
    //                 var fieldValue = childNode.FirstChild.Value;
    //                 field.SetValue(newObject, fieldValue);
    //             }
    //             else if (childType == typeof(bool))
    //             {
    //                 var fieldValue = childNode.FirstChild.Value != "0";
    //                 field.SetValue(newObject, fieldValue);
    //             }
    //             else if (childType == typeof(sbyte))
    //             {
    //                 var fieldValue = sbyte.Parse(childNode.FirstChild.Value);
    //                 field.SetValue(newObject, fieldValue);
    //             }
    //             else if (childType == typeof(byte))
    //             {
    //                 var fieldValue = byte.Parse(childNode.FirstChild.Value);
    //                 field.SetValue(newObject, fieldValue);
    //             }
    //             else if (childType == typeof(short))
    //             {
    //                 var fieldValue = short.Parse(childNode.FirstChild.Value);
    //                 field.SetValue(newObject, fieldValue);
    //             }
    //             else if (childType == typeof(ushort))
    //             {
    //                 var fieldValue = ushort.Parse(childNode.FirstChild.Value);
    //                 field.SetValue(newObject, fieldValue);
    //             }
    //             else if (childType == typeof(int))
    //             {
    //                 var fieldValue = int.Parse(childNode.FirstChild.Value);
    //                 field.SetValue(newObject, fieldValue);
    //             }
    //             else if (childType == typeof(uint))
    //             {
    //                 var fieldValue = uint.Parse(childNode.FirstChild.Value);
    //                 field.SetValue(newObject, fieldValue);
    //             }
    //             else if (childType == typeof(long))
    //             {
    //                 var fieldValue = long.Parse(childNode.FirstChild.Value);
    //                 field.SetValue(newObject, fieldValue);
    //             }
    //             else if (childType == typeof(ulong))
    //             {
    //                 var fieldValue = ulong.Parse(childNode.FirstChild.Value);
    //                 field.SetValue(newObject, fieldValue);
    //             }
    //             else if (childType == typeof(double))
    //             {
    //                 var fieldValue = double.Parse(childNode.FirstChild.Value);
    //                 field.SetValue(newObject, fieldValue);
    //             }
    //             else if (childType == typeof(decimal))
    //             {
    //                 var fieldValue = decimal.Parse(childNode.FirstChild.Value);
    //                 field.SetValue(newObject, fieldValue);
    //             }
    //             else if (childType.IsEnum)
    //             {
    //                 var fieldValue = int.Parse(childNode.FirstChild.Value);
    //                 field.SetValue(newObject, fieldValue);
    //             }
    //             else
    //             {
    //                 field.SetValue(newObject, DeserializeToObject(childNode));
    //             }
    //         }
    //     }
    //
    //     return newObject;
    // }
    private object DeserializeToObject(XmlNode node)
    {
        var objectType = Type.GetType(node.Attributes["type"].Value);

        if (objectType.IsArray)
        {
            List<object> newArray = new List<object>();
            foreach (XmlElement arrayChild in node.ChildNodes)
            {
                var newElement = DeserializeToObject(arrayChild);
                if (newElement != null)
                {
                    newArray.Add(newElement);
                }
            }

            var generatedArray = Array.CreateInstance(objectType.GetElementType(), newArray.Count);
            for (int i = 0; i < newArray.Count; i++)
            {
                generatedArray.SetValue(newArray[i], i);
            }

            return generatedArray;
        }
        else if (objectType.IsGenericType && (objectType.GetGenericTypeDefinition() == typeof(List<>)))
        {
        }
        else if (objectType == typeof(char))
        {
            var fieldValue = char.Parse(node.FirstChild.Value);
            return fieldValue;
        }
        else if (objectType == typeof(string))
        {
            var fieldValue = node.FirstChild.Value;
            return fieldValue;
        }
        else if (objectType == typeof(bool))
        {
            var fieldValue = node.FirstChild.Value != "0";
            return fieldValue;
        }
        else if (objectType == typeof(sbyte))
        {
            var fieldValue = sbyte.Parse(node.FirstChild.Value);
            return fieldValue;
        }
        else if (objectType == typeof(byte))
        {
            var fieldValue = byte.Parse(node.FirstChild.Value);
            return fieldValue;
        }
        else if (objectType == typeof(short))
        {
            var fieldValue = short.Parse(node.FirstChild.Value);
            return fieldValue;
        }
        else if (objectType == typeof(ushort))
        {
            var fieldValue = ushort.Parse(node.FirstChild.Value);
            return fieldValue;
        }
        else if (objectType == typeof(int))
        {
            var fieldValue = int.Parse(node.FirstChild.Value);
            return fieldValue;
        }
        else if (objectType == typeof(uint))
        {
            var fieldValue = uint.Parse(node.FirstChild.Value);
            return fieldValue;
        }
        else if (objectType == typeof(long))
        {
            var fieldValue = long.Parse(node.FirstChild.Value);
            return fieldValue;
        }
        else if (objectType == typeof(ulong))
        {
            var fieldValue = ulong.Parse(node.FirstChild.Value);
            return fieldValue;
        }
        else if (objectType == typeof(double))
        {
            var fieldValue = double.Parse(node.FirstChild.Value);
            return fieldValue;
        }
        else if (objectType == typeof(decimal))
        {
            var fieldValue = decimal.Parse(node.FirstChild.Value);
            return fieldValue;
        }
        else if (objectType.IsEnum)
        {
            var fieldValue = int.Parse(node.FirstChild.Value);
            return fieldValue;
        }
        else
        {
            object newObject = Activator.CreateInstance(Type.GetType(node.Attributes["type"].Value));
            var newObjectType = newObject.GetType();
            foreach (XmlElement childNode in node.ChildNodes)
            {
                var field = newObjectType.GetField(childNode.Name);
                if (field != null)
                {
                    field.SetValue(newObject, DeserializeToObject(childNode));
                }
            }

            return newObject;
        }

        return null;
    }
}
}