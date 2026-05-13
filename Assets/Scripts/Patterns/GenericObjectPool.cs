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
GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity); 
obj.transform.SetParent(null); // Herhangi bir objenin altına girmeyecek böylece
        }
    }

    public GameObject GetFromPool(Vector3 position, Quaternion rotation)
    {
if (pool.Count > 0)
    {
        GameObject obj = pool.Dequeue();
        
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        
        obj.SetActive(true);
        return obj;
    }
        else
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}