using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintsButton : MonoBehaviour
{
    public Sprite hintsOn;
    public Sprite hintsOff;
    Image image;
    bool hintsEnabled;

    void Start()
    {
        hintsEnabled = Settings.isHintsEnable;
        SetSprite(hintsEnabled);
        image = GetComponent<Image>();
    }

    void SetHints(bool enabled)
    {
        if (enabled)
        {
            Settings.SetHints(true);
        }
        else
        {
            Settings.SetHints(false);
        }
    }

    void SetSprite(bool enabled)
    {
        if (enabled)
        {
            image.sprite = hintsOn;
        }
        else
        {
            image.sprite = hintsOff;
        }
    }
    public void SwitchHints()
    {
        hintsEnabled = !hintsEnabled;
        SetHints(hintsEnabled);
        SetSprite(hintsEnabled);
    }
}
