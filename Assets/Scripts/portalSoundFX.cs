using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalSoundFX : MonoBehaviour
{

    AudioSource portalSound;

    void Awake()
    {
        DontDestroyOnLoad(this);
        portalSound = this.GetComponent<AudioSource>();
        portalSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!portalSound.isPlaying) 
        {
            Destroy(this.gameObject);
        }
    }
}
