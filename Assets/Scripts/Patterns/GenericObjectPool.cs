using System.Collections.Generic;
using UnityEngine;

public class GenericObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;
    
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        // Başlangıçta havuzu doldur
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    private GameObject CreateNewObject()
    {
        if (prefab == null) return null;

        GameObject obj = Instantiate(prefab, transform); 
        obj.SetActive(false); 
        pool.Enqueue(obj);
        return obj;
    }

    public GameObject GetFromPool(Vector3 position, Quaternion rotation)
    {
        GameObject obj = null;

        while (pool.Count > 0)
        {
            obj = pool.Dequeue();

            if (obj != null)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.transform.SetParent(null); // Havuzun altından çıkar ki bağımsız hareket etsin
                obj.SetActive(true);
                return obj;
            }
        }

        Debug.LogWarning("Havuz boşaldı veya objeler yok edildi, yeni obje yaratılıyor.");
        obj = Instantiate(prefab, position, rotation);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        if (obj == null) return;

        obj.SetActive(false);
        obj.transform.SetParent(transform); 
        pool.Enqueue(obj);
    }
}