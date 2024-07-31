using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrameCounter : MonoBehaviour
{
    public TextMeshProUGUI text;

    float t = 0;
    int counter = 0;
    private void Update()
    {
        t += Time.deltaTime;
        counter += 1;

        if(t >= 1f)
        {
            t -= 1f;
            text.text = counter.ToString();
            counter = 0;
        }
    }
}
