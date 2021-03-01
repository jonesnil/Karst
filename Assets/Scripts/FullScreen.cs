using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreen: MonoBehaviour
{
    [SerializeField] Sprite cropOut;
    [SerializeField] Sprite cropIn;
    Image buttonImage;

    public void Start()
    {
        buttonImage = this.GetComponent<Image>();

    }

    private void Update()
    {
        if (Screen.fullScreen)
        {
            buttonImage.sprite = cropIn;
        }
        else
        {
            buttonImage.sprite = cropOut;
        }
    }

    public void ChangeScreenSize()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }


}
