using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool : SingletonMonoBehaviour<ObjectPool>
{
   private readonly ComponentPool _componentPool = new();

   public void ClearAllPools()
   {
       _componentPool.ClearAllPool();
   }

    public void AddPooled<T>(T originalReference, int count = 1) where T : Component
    {
        _componentPool.AddToPool(originalReference, count);
    }

    public T Spawn<T>(T originalReference, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion()) where T : Component
    {
//        Debug.Log(originalReference);
        return _componentPool.GetAvailableObject(originalReference, position, rotation);
    }

    public void Recycle<T>(T cloneReference) where T : Component
    {
        _componentPool.ReturnCloneToPool(cloneReference);
    }

    public void Recycle<T>(T cloneReference, float delay) where T : Component
    {
        StartCoroutine(delayedRecycle());

        IEnumerator delayedRecycle()
        {
            yield return new WaitForSeconds(delay);
            _componentPool.ReturnCloneToPool(cloneReference);
        }
    }
}

public class ComponentPool
{
    //the queue of pooled components by their type and asset reference
    private readonly Dictionary<GameObject, Queue<Component>> _pooledComponentsByType = new();

    //dictionaries of instantied objects and their original object
    private Dictionary<GameObject, GameObject> _originalsByClones = new Dictionary<GameObject, GameObject>();

    public void ClearAllPool()
    {
        _pooledComponentsByType.Clear();
        _originalsByClones.Clear();
    }

    /// <summary>
    /// Add new objects to the pool.
    /// </summary>
    /// <param name="originalReference">Referenced object</param>
    /// <param name="count">Number of objects</param>
    /// <typeparam name="T">Type reference of the object</typeparam>
    /// <returns></returns>
    public Queue<Component> AddToPool<T>(T originalReference, int count = 1) where T : Component
    {
        Queue<Component> components;

        if (!_pooledComponentsByType.TryGetValue(originalReference.gameObject, out components))
        {
            _pooledComponentsByType.Add(originalReference.gameObject, components = new Queue<Component>());
        }

        if (count < 0)
        {
            Debug.LogError("Count cannot be negative");
            return null;
        }

        //Create the type of component x times
        for (int i = 0; i < count; i++)
        {
            //Instantiate new component and UPDATE the List of components
            Component clone = Object.Instantiate(originalReference);
            _originalsByClones.Add(clone.gameObject, originalReference.gameObject);
            //De-activate each one until when needed
            clone.gameObject.SetActive(false);
            components.Enqueue(clone);
        }

        return components;
    }


    //Get available component in the ComponentPool
    public T GetAvailableObject<T>(T originalReference, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion()) where T : Component
    {
        //Get all component with the requested type from  the Dictionary
        if (_pooledComponentsByType.TryGetValue(originalReference.gameObject, out Queue<Component> components))
        {
            if (components.Count > 0)
            {
                var component = components.Dequeue();
                component.gameObject.SetActive(true);
                component.gameObject.SetActive(true);
                component.transform.position = position;
                component.transform.rotation = rotation;
                return (T)component;
            }
        }

        //No available object in the pool. Expand list
        //Create new component, activate the GameObject and return it
        Component clone = AddToPool(originalReference).Dequeue();
        clone.gameObject.SetActive(true);
        clone.transform.position = position;
        clone.transform.rotation = rotation;
        return (T)clone;
    }

    public void ReturnCloneToPool<T>(T cloneReference) where T : Component
    {
        Queue<Component> components;

        GameObject clone = cloneReference.gameObject;
        clone.transform.position = Vector3.zero;
        clone.transform.rotation = Quaternion.identity;
        clone.SetActive(false);

        GameObject original = GetOriginal(clone);

        if (!_pooledComponentsByType.TryGetValue(original, out components))
        {
            _pooledComponentsByType.Add(original, components = new Queue<Component>());
        }

        components.Enqueue(cloneReference);
    }

    private GameObject GetOriginal(GameObject clone)
    {
        if (_originalsByClones.TryGetValue(clone, out var original))
            return original;

        return SetOriginal(clone, clone);
    }

    private GameObject SetOriginal(GameObject clone, GameObject original)
    {
        if (!_originalsByClones.ContainsKey(clone))
        {
            _originalsByClones.Add(clone, original);
        }

        return original;
    }
}