using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PolySerializer.Tests
{
    public class Deserialization
    {
        [Test]
        public void FloatTest()
        {
            var input = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
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
            
            var targetOutput = new SupportClass<float>(
                1f,
                2f,
                new[] {4f, 5f, 6f},
                new[] {7f, 8f, 9f, 10f, .5f},
                new List<float> {11f, 12f, 13f, -1f, -2f},
                new List<float> {-3f, -4f, -5f, -6f, -7f, -0.5f, 0.25f}
            );

            var deserializer = new Deserializer();

            var deserializedObject = (SupportClass<float>) deserializer.DeserializeFromString(input);
            
            Assert.True(deserializedObject.Equal(targetOutput));
        }
    }
}
