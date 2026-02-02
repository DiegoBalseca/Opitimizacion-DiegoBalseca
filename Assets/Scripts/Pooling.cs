using UnityEngine;
using System.Collections.Generic;

public class Pooling : MonoBehaviour
{

    public static Pooling Instance;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize;
    [SerializeField] private List<GameObject> _pooledObjects;
    [SerializeField] private string _parentName;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject obj;
        GameObject parent = new GameObject(_parentName);

        for (int i = 0; i < _poolSize; i++)
        {
            obj = Instantiate(_prefab);
            obj.transform.SetParent(parent.transform);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < _poolSize; i++)
        {
            if(!_pooledObjects[i].activeInHierarchy)
            {
                GameObject objectToSpawn;
                objectToSpawn = _pooledObjects[i];
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;
                return objectToSpawn;
            }
        }

        Debug.Log("No hay balas disponibles para disparar");
        return null;
    }
}
