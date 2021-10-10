using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    public Sprite volumeOn;
    public Sprite volumeOff;
    Image image;
    bool volEnabled;

    void Start()
    {
        volEnabled = Settings.isVolumeOn;
        SetSprite(volEnabled);
        image = GetComponent<Image>();
    }

    void SetAudio(bool enabled)
    {
        if (enabled)
        {
            Settings.SetVolume(true);
        }
        else
        {
            Settings.SetVolume(false);
        }
    }

    void SetSprite(bool enabled)
    {
        if (enabled)
        {
            image.sprite = volumeOn;
        }
        else
        {
            image.sprite = volumeOff;
        }
    }

    public void SwitchAudio()
    {
        volEnabled = !volEnabled;
        SetAudio(volEnabled);
        SetSprite(volEnabled);
    }
}