using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSliders : MonoBehaviour
{
    public static EnemyHealthSliders Instance;

    public GameObject sliderPrefab;
    public Transform sliderCanvas;

    private int totalSliders = 32;

    private Queue<GameObject> sliders = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;

        for(int i = 0; i < totalSliders; i++)
        {
            sliders.Enqueue(CreateObject());
        }
    }

    private GameObject CreateObject()
    {
        GameObject created = Instantiate(sliderPrefab, sliderCanvas);
        created.SetActive(false);
        return created;
    }    

    public GameObject GetSliderObject()
    {
        if(sliders.Count > 0)
        {
            return sliders.Dequeue();
        }   
        else
        {
            return CreateObject();
        }
    }

    public void ReturnSliderObject(GameObject o)
    {
        o.SetActive(false);
        o.transform.GetChild(0).GetComponent<Slider>().value = 1;
        o.transform.GetChild(1).GetComponent<Slider>().value = 1;
        sliders.Enqueue(o);
    }
}
