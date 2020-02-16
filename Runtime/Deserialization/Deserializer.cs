using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using UnityEngine;

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
        Type objectType = null;

        foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
        {
            objectType = a.GetType(node.Attributes["type"].Value);
            if(objectType != null)
                break;
        }

        if (objectType == null)
        {
            throw new Exception("Type Not Found");
        }

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
            var newList = Activator.CreateInstance(objectType);

            var collection = newList as IList;

            foreach (XmlElement arrayChild in node.ChildNodes)
            {
                var newElement = DeserializeToObject(arrayChild);
                if (newElement != null)
                {
                    collection.Add(newElement);
                }
            }

            return newList;
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
        else if (objectType == typeof(Vector2))
        {
            var fieldValue = new Vector2(float.Parse(node.FirstChild.Value.Split(';')[0]), float.Parse(node.FirstChild.Value.Split(';')[1]));
            return fieldValue;
        }
        else if (objectType == typeof(Vector3))
        {
            var fieldValue = new Vector3(float.Parse(node.FirstChild.Value.Split(';')[0]), float.Parse(node.FirstChild.Value.Split(';')[1]), float.Parse(node.FirstChild.Value.Split(';')[2]));
            return fieldValue;
        }
        else if (objectType == typeof(Vector4))
        {
            var fieldValue = new Vector4(float.Parse(node.FirstChild.Value.Split(';')[0]), float.Parse(node.FirstChild.Value.Split(';')[1]), float.Parse(node.FirstChild.Value.Split(';')[2]),
                float.Parse(node.FirstChild.Value.Split(';')[3]));
            return fieldValue;
        }
        else if (objectType == typeof(Color))
        {
            var fieldValue = new Color(float.Parse(node.FirstChild.Value.Split(';')[0]), float.Parse(node.FirstChild.Value.Split(';')[1]), float.Parse(node.FirstChild.Value.Split(';')[2]),
                float.Parse(node.FirstChild.Value.Split(';')[3]));
            return fieldValue;
        }
        else if (objectType == typeof(Sprite))
        {
            var texture = Resources.Load<Texture2D>(node.FirstChild.Value);
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
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