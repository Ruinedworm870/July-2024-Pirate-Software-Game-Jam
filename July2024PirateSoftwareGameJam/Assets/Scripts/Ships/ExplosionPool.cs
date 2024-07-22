using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    public static ExplosionPool Instance;

    public Transform holder;
    public GameObject prefab;

    private int total = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for(int i = 0; i < total; i++)
        {
            pool.Enqueue(CreateObject());
        }
    } 
    
    private GameObject CreateObject()
    {
        GameObject created = Instantiate(prefab, holder);
        created.SetActive(false);
        return created;
    }

    public void PlayExplosion(Vector3 pos)
    {
        GameObject e = pool.Dequeue();
        
        e.transform.position = pos;
        e.SetActive(true);
        e.GetComponent<ParticleSystem>().Play();

        pool.Enqueue(e);
    }
}