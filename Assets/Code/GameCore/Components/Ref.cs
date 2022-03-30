using System;

namespace GameCore.Components
{
    [Serializable]
    public struct Ref<T> where T : class
    {
        public T value;
    }
}