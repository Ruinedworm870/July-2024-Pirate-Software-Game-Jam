using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnapPos : MonoBehaviour
{
    private int ppu = 64;

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Floor(transform.position.x * ppu + 0.5f) / ppu, Mathf.Floor(transform.position.y * ppu + 0.5f) / ppu, 0);
    }
}
