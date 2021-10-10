using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarButton : MonoBehaviour
{
    public GameObject panel;
    public Text nameText;
    public Text statusText;

    public void AvatarTapped()
    {
        panel.SetActive(true);

        string char_name = gameObject.GetComponent<CharacterAvatar>().character.char_name;
        string char_status = GetStatus(gameObject.GetComponent<CharacterAvatar>().character);
        nameText.text = char_name;
        statusText.text = char_status;
    }

    private string GetStatus(Character character)
    {
        int stat = character.GetStatus();
        switch (stat)
        {
            case -2: return "the worst";
            case -1: return "bad";
            case 0: return "netural";
            case 1: return "ok";
            case 2: return "good";
            default:return "none";
        }
    }
}
