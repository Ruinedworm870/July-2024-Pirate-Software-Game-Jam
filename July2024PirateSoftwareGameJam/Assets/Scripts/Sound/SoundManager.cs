using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    private void Awake()
    {
        Instance = this;
    }

    //This is for one shot sounds like lasers, or maybe an initial sound when missiles are shot, looping sounds will be controlled on the actual object, and have lower priorities
    //Lower importance = higher priority
    public void PlayOneShotSound(AudioSource source, AudioClip clip, Vector3 pos, int importance, float volume)
    {
        Vector3 camPos = Camera.main.transform.position;
        camPos.z = 0;
        
        float distance = Vector3.Distance(pos, camPos);

        source.pitch = Random.Range(0.95f, 1f);
        source.priority = Mathf.Clamp((int)distance + importance, 0, 256);
        source.volume = (volume + volume / (source.priority + 1)) * source.pitch;
        
        source.PlayOneShot(clip);
    }
}
