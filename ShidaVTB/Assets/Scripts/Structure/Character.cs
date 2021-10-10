using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character
{
    public string avatarName;
    public string char_name;
    public string key;

    public Character(string char_name, string key, string avatarName) {
        this.char_name = char_name;
        this.key = key;
        this.avatarName = avatarName;
    }
    public virtual int GetStatus()
    {
        return 0;
    }
}
