using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private float smoothSpeed = 0.02f;
    
    private void LateUpdate()
    {
        Vector3 smoothedPos = Vector3.Lerp(transform.position, player.position, smoothSpeed);
        smoothedPos.z = -10;
        transform.position = smoothedPos;

        //transform.position = new Vector3(player.position.x, player.position.y, -10);
    }
}
