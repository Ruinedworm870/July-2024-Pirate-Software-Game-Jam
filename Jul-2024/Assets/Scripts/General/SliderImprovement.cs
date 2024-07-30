using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderImprovement : MonoBehaviour
{
    private Slider slider;
    private GameObject fillArea;
    
    private void Start()
    {
        slider = GetComponent<Slider>();
        fillArea = transform.Find("Fill Area").gameObject;

        fillArea.SetActive(slider.value != 0);
    }
    
    private void Update()
    {
        fillArea.SetActive(slider.value != 0);
    }
}
