using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForSecret : MonoBehaviour
{
    public AudioClip normalBGM;
    public AudioClip secretBGM;
    
    // Start is called before the first frame update
    void Start()
    {
        Secret secret = FindObjectOfType<Secret>();
        
        if (secret && secret.unlocked)
            GetComponent<AudioSource>().clip = secretBGM;
        else
            GetComponent<AudioSource>().clip = normalBGM;

        GetComponent<AudioSource>().Play();
    }
}
