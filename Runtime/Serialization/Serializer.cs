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
    private void SerializeObject(ref XmlWriter writer, object obj)
    {
        var type = obj.GetType();
        var fields = type.GetFields().Where(x => x.IsDefined(typeof(XmlSerializedAttribute), true));

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
                writer.WriteString(((char) serializedField.GetValue(obj)).ToString());
            }
            else if (serializedField.FieldType == typeof(string))
            {
                writer.WriteString((string) serializedField.GetValue(obj));
            }
            else if (serializedField.FieldType == typeof(bool))
            {
                writer.WriteString((bool) serializedField.GetValue(obj) ? "1" : "0");
            }
            else if (serializedField.FieldType == typeof(sbyte))
            {
                writer.WriteString(((sbyte) serializedField.GetValue(obj)).ToString());
            }
            else if (serializedField.FieldType == typeof(byte))
            {
                writer.WriteString(((byte) serializedField.GetValue(obj)).ToString());
            }
            else if (serializedField.FieldType == typeof(short))
            {
                writer.WriteString(((short) serializedField.GetValue(obj)).ToString());
            }
            else if (serializedField.FieldType == typeof(ushort))
            {
                writer.WriteString(((ushort) serializedField.GetValue(obj)).ToString());
            }
            else if (serializedField.FieldType == typeof(int))
            {
                writer.WriteString(((int) serializedField.GetValue(obj)).ToString());
            }
            else if (serializedField.FieldType == typeof(uint))
            {
                writer.WriteString(((uint) serializedField.GetValue(obj)).ToString());
            }
            else if (serializedField.FieldType == typeof(long))
            {
                writer.WriteString(((long) serializedField.GetValue(obj)).ToString());
            }
            else if (serializedField.FieldType == typeof(ulong))
            {
                writer.WriteString(((ulong) serializedField.GetValue(obj)).ToString());
            }
            else if (serializedField.FieldType == typeof(double))
            {
                writer.WriteString(((double) serializedField.GetValue(obj)).ToString(CultureInfo.InvariantCulture));
            }
            else if (serializedField.FieldType == typeof(decimal))
            {
                writer.WriteString(((decimal) serializedField.GetValue(obj)).ToString(CultureInfo.InvariantCulture));
            }
            else if (serializedField.FieldType.IsEnum)
            {
                writer.WriteString(((int) serializedField.GetValue(obj)).ToString());
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