using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace PolySerializer
{
/// <summary>
/// Class which allows to serialize an object to an xml-format text
/// </summary>
public class Serializer
{
    /// <summary>
    /// The settings which define the syntax of the serialized text
    /// </summary>
    private XmlWriterSettings m_Settings;

    /// <summary>
    /// Class which allows to serialize an object to an xml-format text
    /// </summary>
    /// <param name="settings">The settings which define the syntax of the serialized text</param>
    public Serializer(XmlWriterSettings settings)
    {
        m_Settings = settings;
    }

    /// <summary>
    /// Serialize a specific object to a string
    /// </summary>
    /// <param name="objectToSerialize">The object to serialize</param>
    /// <returns>The serialized object as string</returns>
    public string SerializeToString(object objectToSerialize)
    {
        StringBuilder m_Builder = new StringBuilder();

        var writer = XmlWriter.Create(m_Builder);

        writer.WriteStartDocument();
        writer.WriteStartElement("SerializedObject");
        writer.WriteStartAttribute("type");
        writer.WriteString(objectToSerialize.GetType().FullName);
        writer.WriteEndAttribute();
        SerializeObject(ref writer, objectToSerialize);
        writer.WriteEndElement();
        writer.WriteEndDocument();

        writer.Dispose();

        return m_Builder.ToString();
    }

    /// <summary>
    /// Serialize a specific object to a file
    /// </summary>
    /// <param name="filePath">The path of the file where to save the serialized object</param>
    /// <param name="objectToSerialize">The object to serialize</param>
    public void SerializeToFile(string filePath, object objectToSerialize)
    {
        var serializedObject = SerializeToString(objectToSerialize);
        File.WriteAllText(filePath, serializedObject);
    }

    /// <summary>
    /// Private method used for recursively serialize an object
    /// </summary>
    /// <param name="writer">The xml writer that is serializing the object</param>
    /// <param name="obj">The object that has to be serialized</param>
    /// <param name="directValue">Define is the object value is to get in reflection by the obj parameter</param>
    /// <param name="checkSerialized">Define if has to filter for Xml Serializable</param>
    private void SerializeObject(ref XmlWriter writer, object obj, bool directValue = false, bool checkSerialized = true)
    {
        var type = obj.GetType();
        var fields = checkSerialized ? type.GetFields().Where(x => x.IsDefined(typeof(XmlSerializedAttribute), true)) : type.GetFields();

        foreach (var serializedField in fields)
        {
            //Serialize Field Name
            writer.WriteStartElement(serializedField.Name);
            //Serialize Field Type
            writer.WriteStartAttribute("type");
            writer.WriteString(serializedField.GetValue(obj).GetType().FullName);
            writer.WriteEndAttribute();

            //Serialize Field Value
            if (serializedField.FieldType == typeof(char))
            {
                char value;
                if (directValue)
                    value = (char) obj;
                else
                    value = (char) serializedField.GetValue(obj);
                writer.WriteString(value.ToString());
            }
            else if (serializedField.FieldType == typeof(string))
            {
                string value;
                if (directValue)
                    value = (string) obj;
                else
                    value = (string) serializedField.GetValue(obj);
                writer.WriteString(value);
            }
            else if (serializedField.FieldType == typeof(bool))
            {
                bool value;
                if (directValue)
                    value = (bool) obj;
                else
                    value = (bool) serializedField.GetValue(obj);
                writer.WriteString(value ? "1" : "0");
            }
            else if (serializedField.FieldType == typeof(sbyte))
            {
                sbyte value;
                if (directValue)
                    value = (sbyte) obj;
                else
                    value = (sbyte) serializedField.GetValue(obj);
                writer.WriteString((value).ToString());
            }
            else if (serializedField.FieldType == typeof(byte))
            {
                byte value;
                if (directValue)
                    value = (byte) obj;
                else
                    value = (byte) serializedField.GetValue(obj);
                writer.WriteString((value).ToString());
            }
            else if (serializedField.FieldType == typeof(short))
            {
                short value;
                if (directValue)
                    value = (short) obj;
                else
                    value = (short) serializedField.GetValue(obj);
                writer.WriteString((value).ToString());
            }
            else if (serializedField.FieldType == typeof(ushort))
            {
                ushort value;
                if (directValue)
                    value = (ushort) obj;
                else
                    value = (ushort) serializedField.GetValue(obj);
                writer.WriteString((value).ToString());
            }
            else if (serializedField.FieldType == typeof(int))
            {
                int value;
                if (directValue)
                    value = (int) obj;
                else
                    value = (int) serializedField.GetValue(obj);
                writer.WriteString((value).ToString());
            }
            else if (serializedField.FieldType == typeof(uint))
            {
                uint value;
                if (directValue)
                    value = (uint) obj;
                else
                    value = (uint) serializedField.GetValue(obj);
                writer.WriteString((value).ToString());
            }
            else if (serializedField.FieldType == typeof(long))
            {
                long value;
                if (directValue)
                    value = (long) obj;
                else
                    value = (long) serializedField.GetValue(obj);
                writer.WriteString((value).ToString());
            }
            else if (serializedField.FieldType == typeof(double))
            {
                double value;
                if (directValue)
                    value = (double) obj;
                else
                    value = (double) serializedField.GetValue(obj);
                writer.WriteString((value).ToString());
            }
            else if (serializedField.FieldType == typeof(double))
            {
                double value;
                if (directValue)
                    value = (double) obj;
                else
                    value = (double) serializedField.GetValue(obj);
                writer.WriteString((value).ToString(CultureInfo.InvariantCulture));
            }
            else if (serializedField.FieldType == typeof(decimal))
            {
                decimal value;
                if (directValue)
                    value = (decimal) obj;
                else
                    value = (decimal) serializedField.GetValue(obj);
                writer.WriteString((value).ToString(CultureInfo.InvariantCulture));
            }
            else if (serializedField.FieldType.IsEnum)
            {
                int value;
                if (directValue)
                    value = (int) obj;
                else
                    value = (int) serializedField.GetValue(obj);
                writer.WriteString((value).ToString());
            }
            else if (serializedField.FieldType.IsGenericType && (serializedField.FieldType.GetGenericTypeDefinition() == typeof(List<>)))
            {
                var list = serializedField.GetValue(obj) as ICollection;
                foreach (var listObj in list)
                {
                    SerializeObject(ref writer, listObj, true, false);
                }
            }
            else if (serializedField.FieldType.IsArray)
            {
                object[] array = (object[]) serializedField.GetValue(obj);
                foreach (var arrayObj in array)
                {
                    SerializeObject(ref writer, arrayObj, true, false);
                }
            }
            else
            {
                SerializeObject(ref writer, serializedField.GetValue(obj));
            }

            //Close Serialized Node
            writer.WriteEndElement();
        }
    }
}
}