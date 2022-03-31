using System;

namespace UnityAware.Components
{
    /// <summary>
    /// mostly for attachment of Unity components to entities  
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public struct Ref<T> where T : class
    {
        public T value;
    }
}