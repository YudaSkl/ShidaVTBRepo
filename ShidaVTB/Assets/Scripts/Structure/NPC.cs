using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NPC : Character
{
    public bool isMeet;
    Dictionary<string, int> relationships;
    short maxRel = 2;
    short minRel = -2;

    public NPC(string char_name, string key, string avatarName) : base (char_name, key, avatarName)
    {
        relationships = new Dictionary<string, int>();
        relationships.Add(Global.currPlayerKey, 0);
        isMeet = false;
    }

    public void AddRelationship(string playerKey, int valueToAdd)
    {
        if (!relationships.ContainsKey(playerKey))
        {
            relationships.Add(playerKey, 0);
        }

        relationships[playerKey] += valueToAdd;

        if (relationships[playerKey] > maxRel)
        {
            relationships[playerKey] = maxRel;
        }
        else if (relationships[playerKey] < minRel)
        {
            relationships[playerKey] = minRel;
        }

        if (relationships[playerKey] < -2)
        {
            //You Loose
        }
    }

    override public int GetStatus()
    {
        if (relationships.ContainsKey("Player")){
            return relationships["Player"];
        }
        else
        {
            return -3;
        }
    }
}
