using System.Collections.Generic;
using System.Xml;
using NUnit.Framework;
using UnityEngine;

namespace PolySerializer.Tests
{
    public class SerializationTests
    {
        [Test]
        public void FloatTest()
        {
            var targetOutput = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                               "<SerializedObject type=\"PolySerializer.Tests.SupportClass`1[[System.Single, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]\">" +
                               "<m_PrivateVariable type=\"System.Single\">1</m_PrivateVariable>" +
                               "<PublicVariable type=\"System.Single\">2</PublicVariable>" +
                               "<m_PrivateArray type=\"System.Single[]\">" +
                               "<Empty type=\"System.Single\">4</Empty>" +
                               "<Empty type=\"System.Single\">5</Empty>" +
                               "<Empty type=\"System.Single\">6</Empty>" +
                               "</m_PrivateArray>" +
                               "<PublicArray type=\"System.Single[]\">" +
                               "<Empty type=\"System.Single\">7</Empty>" +
                               "<Empty type=\"System.Single\">8</Empty>" +
                               "<Empty type=\"System.Single\">9</Empty>" +
                               "<Empty type=\"System.Single\">10</Empty>" +
                               "<Empty type=\"System.Single\">0.5</Empty>" +
                               "</PublicArray>" +
                               "<m_PrivateList type=\"System.Collections.Generic.List`1[[System.Single, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]\">" +
                               "<Empty type=\"System.Single\">11</Empty>" +
                               "<Empty type=\"System.Single\">12</Empty>" +
                               "<Empty type=\"System.Single\">13</Empty>" +
                               "<Empty type=\"System.Single\">-1</Empty>" +
                               "<Empty type=\"System.Single\">-2</Empty>" +
                               "</m_PrivateList>" +
                               "<PublicList type=\"System.Collections.Generic.List`1[[System.Single, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]\">" +
                               "<Empty type=\"System.Single\">-3</Empty>" +
                               "<Empty type=\"System.Single\">-4</Empty>" +
                               "<Empty type=\"System.Single\">-5</Empty>" +
                               "<Empty type=\"System.Single\">-6</Empty>" +
                               "<Empty type=\"System.Single\">-7</Empty>" +
                               "<Empty type=\"System.Single\">-0.5</Empty>" +
                               "<Empty type=\"System.Single\">0.25</Empty>" +
                               "</PublicList>" +
                               "</SerializedObject>";
            
            var supportClass = new SupportClass<float>(
                1f,
                2f,
                new[] {4f, 5f, 6f},
                new[] {7f, 8f, 9f, 10f, .5f},
                new List<float> {11f, 12f, 13f, -1f, -2f},
                new List<float> {-3f, -4f, -5f, -6f, -7f, -0.5f, 0.25f}
            );

            var serializer = new Serializer(new XmlWriterSettings());

            var serializedText = serializer.SerializeToString(supportClass);
            
            Assert.True(serializedText == targetOutput);
        }
        
        [Test]
        public void CharTest()
        {
            var targetOutput = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                               "<SerializedObject type=\"PolySerializer.Tests.SupportClass`1[[System.Char, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]\">" +
                               "<m_PrivateVariable type=\"System.Char\">a</m_PrivateVariable>" +
                               "<PublicVariable type=\"System.Char\">b</PublicVariable>" +
                               "<m_PrivateArray type=\"System.Char[]\">" +
                               "<Empty type=\"System.Char\">c</Empty>" +
                               "<Empty type=\"System.Char\">d</Empty>" +
                               "<Empty type=\"System.Char\">e</Empty>" +
                               "</m_PrivateArray>" +
                               "<PublicArray type=\"System.Char[]\">" +
                               "<Empty type=\"System.Char\">f</Empty>" +
                               "<Empty type=\"System.Char\">g</Empty>" +
                               "<Empty type=\"System.Char\">h</Empty>" +
                               "<Empty type=\"System.Char\">i</Empty>" +
                               "<Empty type=\"System.Char\">j</Empty>" +
                               "</PublicArray>" +
                               "<m_PrivateList type=\"System.Collections.Generic.List`1[[System.Char, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]\">" +
                               "<Empty type=\"System.Char\">k</Empty>" +
                               "<Empty type=\"System.Char\">l</Empty>" +
                               "<Empty type=\"System.Char\">m</Empty>" +
                               "<Empty type=\"System.Char\">n</Empty>" +
                               "<Empty type=\"System.Char\">o</Empty>" +
                               "</m_PrivateList>" +
                               "<PublicList type=\"System.Collections.Generic.List`1[[System.Char, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]\">" +
                               "<Empty type=\"System.Char\">p</Empty>" +
                               "<Empty type=\"System.Char\">q</Empty>" +
                               "<Empty type=\"System.Char\">r</Empty>" +
                               "<Empty type=\"System.Char\">s</Empty>" +
                               "<Empty type=\"System.Char\">t</Empty>" +
                               "<Empty type=\"System.Char\">u</Empty>" +
                               "<Empty type=\"System.Char\">v</Empty>" +
                               "</PublicList>" +
                               "</SerializedObject>";
            
            var supportClass = new SupportClass<char>(
                'a',
                'b',
                new[] {'c', 'd', 'e'},
                new[] {'f', 'g', 'h', 'i', 'j'},
                new List<char> {'k', 'l', 'm', 'n', 'o'},
                new List<char> {'p', 'q', 'r', 's', 't', 'u', 'v'}
            );

            var serializer = new Serializer(new XmlWriterSettings());

            var serializedText = serializer.SerializeToString(supportClass);
            
            Assert.True(serializedText == targetOutput);
        }
        
        [Test]
        public void StringTest()
        {
            var targetOutput = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                               "<SerializedObject type=\"PolySerializer.Tests.SupportClass`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]\">" +
                               "<m_PrivateVariable type=\"System.String\">aA</m_PrivateVariable>" +
                               "<PublicVariable type=\"System.String\">bB</PublicVariable>" +
                               "<m_PrivateArray type=\"System.String[]\">" +
                               "<Empty type=\"System.String\">cC</Empty>" +
                               "<Empty type=\"System.String\">dD</Empty>" +
                               "<Empty type=\"System.String\">eE</Empty>" +
                               "</m_PrivateArray>" +
                               "<PublicArray type=\"System.String[]\">" +
                               "<Empty type=\"System.String\">fF</Empty>" +
                               "<Empty type=\"System.String\">gG</Empty>" +
                               "<Empty type=\"System.String\">hH</Empty>" +
                               "<Empty type=\"System.String\">iI</Empty>" +
                               "<Empty type=\"System.String\">jJ</Empty>" +
                               "</PublicArray>" +
                               "<m_PrivateList type=\"System.Collections.Generic.List`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]\">" +
                               "<Empty type=\"System.String\">kK</Empty>" +
                               "<Empty type=\"System.String\">lL</Empty>" +
                               "<Empty type=\"System.String\">mM</Empty>" +
                               "<Empty type=\"System.String\">nN</Empty>" +
                               "<Empty type=\"System.String\">oO</Empty>" +
                               "</m_PrivateList>" +
                               "<PublicList type=\"System.Collections.Generic.List`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]\">" +
                               "<Empty type=\"System.String\">pO</Empty>" +
                               "<Empty type=\"System.String\">qQ</Empty>" +
                               "<Empty type=\"System.String\">rR</Empty>" +
                               "<Empty type=\"System.String\">sS</Empty>" +
                               "<Empty type=\"System.String\">tT</Empty>" +
                               "<Empty type=\"System.String\">uU</Empty>" +
                               "<Empty type=\"System.String\">vV</Empty>" +
                               "</PublicList>" +
                               "</SerializedObject>";
            
            var supportClass = new SupportClass<string>(
                "aA",
                "bB",
                new[] {"cC", "dD", "eE"},
                new[] {"fF", "gG", "hH", "iI", "jJ"},
                new List<string> {"kK", "lL", "mM", "nN", "oO"},
                new List<string> {"pO", "qQ", "rR", "sS", "tT", "uU", "vV"}
            );

            var serializer = new Serializer(new XmlWriterSettings());

            var serializedText = serializer.SerializeToString(supportClass);
            
            Debug.Log(serializedText);
            
            Assert.True(serializedText == targetOutput);
        }
    }
}