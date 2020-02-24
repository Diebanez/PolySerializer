using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using UnityEditor;
using UnityEngine;

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
            SerializeObject(ref writer, objectToSerialize, false);
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
        private void SerializeObject(ref XmlWriter writer, object obj, bool directValue = false,
            bool checkSerialized = true)
        {
            BindingFlags flags = BindingFlags.Public | 
                                 BindingFlags.NonPublic | 
                                 BindingFlags.Static | 
                                 BindingFlags.Instance | 
                                 BindingFlags.DeclaredOnly;
            
            var type = obj.GetType();
            var fields = checkSerialized
                ? type.GetFields(flags).Where(x => x.IsDefined(typeof(XmlSerializedAttribute), true))
                : type.GetFields(flags);

            if (directValue)
            {
                if (obj != null)
                {
                    var objectType = obj.GetType();

                    //Serialize Field Name
                    writer.WriteStartElement("Empty");
                    //Serialize Field Type
                    writer.WriteStartAttribute("type");
                    writer.WriteString(objectType.FullName);
                    writer.WriteEndAttribute();

                    //Serialize Field Value
                    if (objectType == typeof(float))
                    {
                        float value;
                        value = (float) obj;
                        writer.WriteString(value.ToString(CultureInfo.InvariantCulture));
                    }
                    else if (objectType == typeof(char))
                    {
                        char value;
                        value = (char) obj;
                        writer.WriteString(value.ToString());
                    }
                    else if (objectType == typeof(string))
                    {
                        string value;
                        value = (string) obj;
                        writer.WriteString(value == "" ? "." : value);
                    }
                    else if (objectType == typeof(bool))
                    {
                        bool value;
                        value = (bool) obj;
                        writer.WriteString(value ? "1" : "0");
                    }
                    else if (objectType == typeof(sbyte))
                    {
                        sbyte value;
                        value = (sbyte) obj;
                        writer.WriteString((value).ToString());
                    }
                    else if (objectType == typeof(byte))
                    {
                        byte value;
                        value = (byte) obj;
                        writer.WriteString((value).ToString());
                    }
                    else if (objectType == typeof(short))
                    {
                        short value;
                        value = (short) obj;
                        writer.WriteString((value).ToString());
                    }
                    else if (objectType == typeof(ushort))
                    {
                        ushort value;
                        value = (ushort) obj;
                        writer.WriteString((value).ToString());
                    }
                    else if (objectType == typeof(int))
                    {
                        int value;
                        value = (int) obj;
                        writer.WriteString((value).ToString());
                    }
                    else if (objectType == typeof(uint))
                    {
                        uint value;
                        value = (uint) obj;
                        writer.WriteString((value).ToString());
                    }
                    else if (objectType == typeof(long))
                    {
                        long value;
                        value = (long) obj;
                        writer.WriteString((value).ToString());
                    }
                    else if (objectType == typeof(double))
                    {
                        double value;
                        value = (double) obj;
                        writer.WriteString((value).ToString());
                    }
                    else if (objectType == typeof(double))
                    {
                        double value;
                        value = (double) obj;
                        writer.WriteString((value).ToString(CultureInfo.InvariantCulture));
                    }
                    else if (objectType == typeof(decimal))
                    {
                        decimal value;
                        value = (decimal) obj;
                        writer.WriteString((value).ToString(CultureInfo.InvariantCulture));
                    }
                    else if (objectType == typeof(Vector2))
                    {
                        Vector2 value = (Vector2) obj;
                        writer.WriteString(value.x + ";" + value.y);
                    }
                    else if (objectType == typeof(Vector3))
                    {
                        Vector3 value = (Vector3) obj;
                        writer.WriteString(value.x + ";" + value.y + ";" + value.z);
                    }
                    else if (objectType == typeof(Vector4))
                    {
                        Vector4 value = (Vector4) obj;
                        writer.WriteString(value.x + ";" + value.y + ";" + value.z + ";" + value.w);
                    }
                    else if (objectType == typeof(Color))
                    {
                        Color value = (Color) obj;
                        writer.WriteString(value.r + ";" + value.g + ";" + value.g + ";" + value.a);
                    }
                    else if (objectType == typeof(Sprite))
                    {
#if UNITY_EDITOR
                        writer.WriteString(
                            Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(((Sprite) obj).texture)));
#endif
                    }
                    else if (objectType.IsEnum)
                    {
                        int value;
                        value = (int) obj;
                        writer.WriteString((value).ToString());
                    }
                    else if (objectType.IsGenericType && (objectType.GetGenericTypeDefinition() == typeof(List<>)))
                    {
                        var list = obj as ICollection;
                        foreach (var listObj in list)
                        {
                            SerializeObject(ref writer, listObj, true, false);
                        }
                    }
                    else if (objectType.IsArray)
                    {
                        foreach (var arrayObj in (Array)obj)
                        {
                            SerializeObject(ref writer, arrayObj, true, false);
                        }
                    }
                    else
                    {
                        SerializeObject(ref writer, obj);
                    }

                    //Close Serialized Node
                    writer.WriteEndElement();
                }
            }
            else
            {
                foreach (var serializedField in fields)
                {
                    object valueObject = serializedField.GetValue(obj);
                    if (valueObject != null)
                    {
                        //Serialize Field Name
                        writer.WriteStartElement(serializedField.Name);
                        //Serialize Field Type
                        writer.WriteStartAttribute("type");


                        writer.WriteString(valueObject.GetType().FullName);
                        writer.WriteEndAttribute();

                        //Serialize Field Value
                        if (serializedField.FieldType == typeof(float))
                        {
                            float value;
                            value = (float) serializedField.GetValue(obj);
                            writer.WriteString(value.ToString(CultureInfo.InvariantCulture));
                        }
                        else if (serializedField.FieldType == typeof(char))
                        {
                            char value;
                            value = (char) serializedField.GetValue(obj);
                            writer.WriteString(value.ToString());
                        }
                        else if (serializedField.FieldType == typeof(string))
                        {
                            string value;
                            value = (string) serializedField.GetValue(obj);
                            writer.WriteString(value == "" ? "." : value);
                        }
                        else if (serializedField.FieldType == typeof(bool))
                        {
                            bool value;
                            value = (bool) serializedField.GetValue(obj);
                            writer.WriteString(value ? "1" : "0");
                        }
                        else if (serializedField.FieldType == typeof(sbyte))
                        {
                            sbyte value;
                            value = (sbyte) serializedField.GetValue(obj);
                            writer.WriteString((value).ToString());
                        }
                        else if (serializedField.FieldType == typeof(byte))
                        {
                            byte value;
                            value = (byte) serializedField.GetValue(obj);
                            writer.WriteString((value).ToString());
                        }
                        else if (serializedField.FieldType == typeof(short))
                        {
                            short value;
                            value = (short) serializedField.GetValue(obj);
                            writer.WriteString((value).ToString());
                        }
                        else if (serializedField.FieldType == typeof(ushort))
                        {
                            ushort value;
                            value = (ushort) serializedField.GetValue(obj);
                            writer.WriteString((value).ToString());
                        }
                        else if (serializedField.FieldType == typeof(int))
                        {
                            int value;
                            value = (int) serializedField.GetValue(obj);
                            writer.WriteString((value).ToString());
                        }
                        else if (serializedField.FieldType == typeof(uint))
                        {
                            uint value;
                            value = (uint) serializedField.GetValue(obj);
                            writer.WriteString((value).ToString());
                        }
                        else if (serializedField.FieldType == typeof(long))
                        {
                            long value;
                            value = (long) serializedField.GetValue(obj);
                            writer.WriteString((value).ToString());
                        }
                        else if (serializedField.FieldType == typeof(double))
                        {
                            double value;
                            value = (double) serializedField.GetValue(obj);
                            writer.WriteString((value).ToString());
                        }
                        else if (serializedField.FieldType == typeof(double))
                        {
                            double value;
                            value = (double) serializedField.GetValue(obj);
                            writer.WriteString((value).ToString(CultureInfo.InvariantCulture));
                        }
                        else if (serializedField.FieldType == typeof(decimal))
                        {
                            decimal value;
                            value = (decimal) serializedField.GetValue(obj);
                            writer.WriteString((value).ToString(CultureInfo.InvariantCulture));
                        }
                        else if (serializedField.FieldType == typeof(Vector2))
                        {
                            Vector2 value = (Vector2) serializedField.GetValue(obj);
                            writer.WriteString(value.x + ";" + value.y);
                        }
                        else if (serializedField.FieldType == typeof(Vector3))
                        {
                            Vector3 value = (Vector3) serializedField.GetValue(obj);
                            writer.WriteString(value.x + ";" + value.y + ";" + value.z);
                        }
                        else if (serializedField.FieldType == typeof(Vector4))
                        {
                            Vector4 value = (Vector4) serializedField.GetValue(obj);
                            writer.WriteString(value.x + ";" + value.y + ";" + value.z + ";" + value.w);
                        }
                        else if (serializedField.FieldType == typeof(Color))
                        {
                            Color value = (Color) serializedField.GetValue(obj);
                            writer.WriteString(value.r + ";" + value.g + ";" + value.g + ";" + value.a);
                        }
                        else if (serializedField.FieldType == typeof(Sprite))
                        {
#if UNITY_EDITOR
                            writer.WriteString(Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(((Sprite) serializedField.GetValue(obj)).texture)));
#endif
                        }
                        else if (serializedField.FieldType.IsEnum)
                        {
                            int value;
                            value = (int) serializedField.GetValue(obj);
                            writer.WriteString((value).ToString());
                        }
                        else if (serializedField.FieldType.IsGenericType &&
                                 (serializedField.FieldType.GetGenericTypeDefinition() == typeof(List<>)))
                        {
                            var list = serializedField.GetValue(obj) as ICollection;
                            foreach (var listObj in list)
                            {
                                SerializeObject(ref writer, listObj, true, false);
                            }
                        }
                        else if (serializedField.FieldType.IsArray)
                        {
                            var value = serializedField.GetValue(obj);
                            foreach (var arrayObj in (Array)value)
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
    }
}