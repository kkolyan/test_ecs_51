using System;
using Leopotam.EcsLite;

public static class CommonExtensions
{
    public static T FailIfNull<T>(this T t, object context = null)
    {
        if (t == null)
        {
            throw new NullReferenceException(context?.ToString());
        }

        return t;
    }

    public static ref T GetOrAdd<T>(this EcsPool<T> pool, int entity) where T : struct
    {
        if (pool.Has(entity))
        {
            return ref pool.Get(entity);
        }

        return ref pool.Add(entity);
    }
}