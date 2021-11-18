using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceRepository
{
    #region Singleton Setup
    
    private static InstanceRepository instance;
    
    public static InstanceRepository Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning(
                    "The InstanceRepository was not instantiated during runtime initialization. It was instantiated on this first access.");
                instance = new InstanceRepository();
            }

            return instance;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void RuntimeInitialize()
    {
        instance = new InstanceRepository();
    }
    
    #endregion

    private readonly Dictionary<Type, List<object>> repository;

    private InstanceRepository()
    {
        repository = new Dictionary<Type, List<object>>();
    }

    public void Add<T>(T obj)
    {
        Type type = typeof(T);
        
        if (!repository.ContainsKey(type))
            repository.Add(type, new List<object>());

        repository[type].Add(obj);
    }
    
    public void AddOnce<T>(T obj)
    {
        Type type = typeof(T);
        
        if (!repository.ContainsKey(type))
            repository.Add(type, new List<object>());

        if (!repository[type].Contains(obj))
            repository[type].Add(obj);
    }

    public T Get<T>()
    {
        Type type = typeof(T);

        if (!repository.ContainsKey(type) || repository[type].Count == 0)
            return default;

        return (T)repository[type][0];
    }

    public List<T> GetAll<T>()
    {
        Type type = typeof(T);

        if (!repository.ContainsKey(type) || repository[type].Count == 0)
            return default;

        return repository[type] as List<T>;
    }

    public void Remove(object obj)
    {
        Type type = obj.GetType();

        if (!repository.ContainsKey(type))
            return;

        repository[type].Remove(obj);
    }
}
