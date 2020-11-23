using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{

    AudioSource introSong;
    bool buttonPressed;

    private void Start()
    {
        introSong = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (buttonPressed) 
        {
            float volume = introSong.volume;
            introSong.volume = Mathf.Lerp(volume, 0, .005f);
        }   
    }

    public void StartPressed() 
    {
        buttonPressed = true;
        Invoke("StartGame", .5f);
    }

    public void StartGame() 
    {
        SceneManager.LoadScene("Level1");
    }
}
