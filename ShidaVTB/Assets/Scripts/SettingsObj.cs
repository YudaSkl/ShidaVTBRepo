using System;

[Serializable]
public class SettingsObj
{
    public bool isVolumeOn;
    public bool isHintsEnable;
    public string language;

    public SettingsObj(bool isVolumeOn, bool isHintsEnable, string language)
    {
        this.isVolumeOn = isVolumeOn;
        this.isHintsEnable = isHintsEnable;
        this.language = language;
    }
}
