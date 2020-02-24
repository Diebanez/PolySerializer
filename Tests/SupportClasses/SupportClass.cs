using System;
using System.Collections.Generic;

namespace PolySerializer.Tests
{
    public class SupportClass<T>
    {
        [XmlSerialized] private T m_PrivateVariable;
        [XmlSerialized] public T PublicVariable;
        [XmlSerialized] private T[] m_PrivateArray;
        [XmlSerialized] public T[] PublicArray;
        [XmlSerialized] private List<T> m_PrivateList;
        [XmlSerialized] public List<T> PublicList;
        
        public SupportClass()
        {
            m_PrivateVariable = default(T);
            PublicVariable = default(T);
            m_PrivateArray = new T[0];
            PublicArray = new T[0];
            m_PrivateList = new List<T>();
            PublicList = new List<T>();
        }
        
        public SupportClass(T privateVariable, T publicVariable, T[] privateArray, T[] publicArray, List<T> privateList, List<T> publicList)
        {
            m_PrivateVariable = privateVariable;
            PublicVariable = publicVariable;
            m_PrivateArray = privateArray;
            PublicArray = publicArray;
            m_PrivateList = privateList;
            PublicList = publicList;
        }

        public bool Equal(SupportClass<T> other)
        {
            if (!other.m_PrivateVariable.Equals(m_PrivateVariable))
                return false;

            if (!other.PublicVariable.Equals(PublicVariable))
                return false;

            if (m_PrivateArray == null || other.m_PrivateArray == null || m_PrivateArray.Length != other.m_PrivateArray.Length)
                return false;

            for (int i = 0; i < m_PrivateArray.Length; i++)
            {
                if (!m_PrivateArray[i].Equals(other.m_PrivateArray[i]))
                    return false;
            }
            
            if (PublicArray == null || other.PublicArray == null || PublicArray.Length != other.PublicArray.Length)
                return false;

            for (int i = 0; i < PublicArray.Length; i++)
            {
                if (!PublicArray[i].Equals(other.PublicArray[i]))
                    return false;
            }
            
            if (m_PrivateList == null || other.m_PrivateList == null || m_PrivateList.Count != other.m_PrivateList.Count)
                return false;

            for (int i = 0; i < m_PrivateList.Count; i++)
            {
                if (!m_PrivateList[i].Equals(other.m_PrivateList[i]))
                    return false;
            }
            
            if (PublicList == null || other.PublicList == null || PublicList.Count != other.PublicList.Count)
                return false;

            for (int i = 0; i < PublicList.Count; i++)
            {
                if (!PublicList[i].Equals(other.PublicList[i]))
                    return false;
            }

            return true;
        }
    }
}