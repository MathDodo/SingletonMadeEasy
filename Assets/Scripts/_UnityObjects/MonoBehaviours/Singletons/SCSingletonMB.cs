﻿using UnityEngine;

/// <summary>
/// Base class for self creating monobehaviour singletons, which other classes can derive from to become singletons.
/// These self creating singletons will have the same gameobject.
/// You should mark your singleton (the derived class) as sealed so you can't derive from it.
/// </summary>
/// <typeparam name="T">This generic type, needs to be the type of the derived class</typeparam>
public abstract class SCSingletonMB<T> : SelfCreateSingletonBaseMB, ISingleton where T : SCSingletonMB<T>
{
    //The singleton instance field
    private static T _instance;

    //Object to achieve a lock from
    private static readonly object _lockObject = new object();

    //Whether if the singleton has been destroyed should only happen when the game closes
    protected static bool _destroyed = false;

    /// <summary>
    /// Get accesor for the singleton Instance
    /// </summary>
    public static T Instance
    {
        get
        {
            //Locking for thread safe
            lock (_lockObject)
            {
                if (_destroyed)
                {
                    return null;
                }

                if (_instance == null)
                {
                    //Creating a gameobject with the name of the class type
                    if (_gameObject == null)
                    {
                        _gameObject = new GameObject("SCSingletons");
                        DontDestroyOnLoad(_gameObject);
                    }

                    _instance = _gameObject.AddComponent<T>();
                    _instance.OnInstantiated();
                }

                return _instance;
            }
        }
    }

    /// <summary>
    /// Implementation of the ISingleton called by the Instance get right after Awake
    /// </summary>
    public abstract void OnInstantiated();

    /// <summary>
    /// The ondestroy call made by unity you can override this but remember to base for the _destroyed bool to be set
    /// </summary>
    protected virtual void OnDestroy()
    {
        _destroyed = true;
    }

#if UNITY_EDITOR

    private void Reset()
    {
        DestructionWindow.OpenWindow("A self creating singleton can't be added to a gameobject on edit time, it will now be destroyed on: ", this);
    }

#endif
}