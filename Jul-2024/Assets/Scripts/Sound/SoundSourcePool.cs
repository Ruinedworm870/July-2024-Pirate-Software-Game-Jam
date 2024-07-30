using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSourcePool : MonoBehaviour
{
    public static SoundSourcePool Instance;
    
    public GameObject prefab;
    public GameObject holder;
    
    private int total = 64;
    
    private List<GameObject> pool = new List<GameObject>();
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < total; i++)
        {
            pool.Add(CreateNewAudioSource());
        }
    }

    private GameObject CreateNewAudioSource()
    {
        GameObject created = Instantiate(prefab, holder.transform);
        created.SetActive(true);
        return created;
    }
    
    public AudioSource GetAudioSource()
    {
        if(pool.Count > 0)
        {
            GameObject source = pool[0];
            pool.RemoveAt(0);
            return source.GetComponent<AudioSource>();
        }
        else
        {
            return CreateNewAudioSource().GetComponent<AudioSource>();
        }
    }
}
