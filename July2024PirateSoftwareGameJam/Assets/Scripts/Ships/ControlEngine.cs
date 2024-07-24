using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEngine : MonoBehaviour
{
    public ParticleSystem part;
    
    private Vector3 lastPos;
    
    private void Start()
    {
        lastPos = transform.position;
    }
    
    private void FixedUpdate()
    {
        var forceOverLifetime = part.forceOverLifetime;
        
        Vector3 direction = transform.InverseTransformDirection((lastPos - transform.position).normalized);
        forceOverLifetime.x = direction.x * 2f;
        forceOverLifetime.z = -direction.y;
        
        lastPos = transform.position;
    }
}
