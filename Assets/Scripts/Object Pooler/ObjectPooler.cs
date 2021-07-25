using System;
using System.Collections.Generic;
using Interfaces;
using JetBrains.Annotations;
using Structs;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<Pool> pools;
    private Dictionary<Tag, Queue<GameObject>> _poolDictionary;

    private void OnEnable()
    {
        _poolDictionary = new Dictionary<Tag, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            var objectPool = new Queue<GameObject>();

            for (var i = 0; i < pool.size; i++)
            {
                var instantiatedObject = Instantiate(pool.prefab, pool.parent);
                instantiatedObject.SetActive(false);
                objectPool.Enqueue(instantiatedObject);
            }

            _poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(Tag objectTag, Vector3 position, Quaternion rotation)
    {
        var objectToSpawn = _poolDictionary[objectTag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;


        var pooledObject = objectToSpawn.GetComponent<IPooledObject>();

        pooledObject?.OnObjectSpawn();
        _poolDictionary[objectTag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}