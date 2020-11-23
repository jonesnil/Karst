using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Immortal : MonoBehaviour
{

    static Immortal instance;
    AudioSource bGM;

    void Start()
    {
        bGM = this.GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (bGM.volume != 1) 
        {
            float volume = bGM.volume;
            bGM.volume = Mathf.Lerp(volume, .4f, .005f);
        }

        if (SceneManager.GetActiveScene().name == "StartMenu") 
        {
            Destroy(this.gameObject);
        } 
    }
}
