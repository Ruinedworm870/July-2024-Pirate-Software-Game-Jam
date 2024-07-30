using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerupPool : MonoBehaviour
{
    public static ShieldPowerupPool Instance;
    
    public GameObject prefab;
    public Transform holder;

    private int total = 16;
    private Queue<GameObject> objects = new Queue<GameObject>();
    private List<GameObject> shields = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for(int i = 0; i < total; i++)
        {
            GameObject o = CreateObject();
            objects.Enqueue(o);
            shields.Add(o);
        }
    }
    
    private GameObject CreateObject()
    {
        GameObject created = Instantiate(prefab, holder);
        created.SetActive(false);
        return created;
    }

    public void SpawnShield(Vector3 pos)
    {
        GameObject shield;
        
        if(objects.Count > 0)
        {
            shield = objects.Dequeue();
        }
        else
        {
            shield = CreateObject();
        }

        shield.transform.position = pos;
        shield.SetActive(true);
    }

    public void ReturnShield(GameObject shield)
    {
        shield.SetActive(false);
        objects.Enqueue(shield);
    }

    public void ResetPositions()
    {
        foreach(var i in shields)
        {
            Vector3 pos = i.transform.position - PlayerPosition.pos;
            i.transform.position = pos;
        }
    }
}
