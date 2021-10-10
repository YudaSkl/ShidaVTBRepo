using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Global
{
    static public Dictionary<string, Character> characters;
    static public Story story;
    static public string currPlayerKey;
    static public string charactersFilename = "/characters.yuda";
    static public string storyFilename = "/story.yuda";
    static public string settingsFilename = "/settings.yuda";

    static public void NewGame()
    {
        Settings.SetDefaultSettings();
        Settings.ApplySettings();
        SetCharacters();
        SetStory();
    }

    static public void LoadGame()
    {
        LoadAll();
    }

    static public void LoadAll()
    {
        Settings.LoadSettings();
        Settings.ApplySettings();
        LoadCharacters();
        LoadStory();
    }

    static public void SetPlayer(CharacterKey key) { currPlayerKey = GetCharacterKey(key); }

    static public bool isEventDone(int id)
    {
        if (story != null && story.events.Count > 0)
            return story.events[id].done;
        return false;
    }
    static public Sprite GetSprite(ResourceFolder folder, string spriteName)
    {
        string path = Enum.GetName(typeof(ResourceFolder), folder) + "/" + spriteName;
        Sprite sprite = Resources.Load<Sprite>(path);
        if (sprite == null)
        {
            path = Enum.GetName(typeof(ResourceFolder), folder) + "/default";
            sprite = Resources.Load<Sprite>(path);
        }
        return sprite;
    }
    static public string GetCharacterKey(CharacterKey chKey) { return Enum.GetName(typeof(CharacterKey), chKey); }
    static public Character GetCharacter(CharacterKey chKey) { return characters[GetCharacterKey(chKey)]; }
    static public Character GetCharacter(string chKey) { return characters[chKey]; }

    static public void SetCharacters()
    {
        characters = new Dictionary<string, Character>();
        characters.Add(GetCharacterKey(CharacterKey.Player), new Player("Player", GetCharacterKey(CharacterKey.Player), "avatarPlayer"));
        SetPlayer(CharacterKey.Player);

        characters.Add(GetCharacterKey(CharacterKey.NewsMaker), new NPC("Newsmaker", GetCharacterKey(CharacterKey.NewsMaker), "avatarNewsmaker"));
        characters.Add(GetCharacterKey(CharacterKey.Airflot), new NPC("Airflot", GetCharacterKey(CharacterKey.Airflot), "avatarAirflot"));
        characters.Add(GetCharacterKey(CharacterKey.Apple), new NPC("Apple", GetCharacterKey(CharacterKey.Apple), "avatarApple"));
        characters.Add(GetCharacterKey(CharacterKey.VTB), new NPC("VTB", GetCharacterKey(CharacterKey.VTB), "avatarVTB"));

    }

    static public void SetStory()
    {
        story = new Story();
        List<Page> pages;
        Dictionary<string, int> statsToAdd;
        Dictionary<string, int> relationsToAdd;
        List<Route> routes;

        #region event 0
        //Pages
        pages = new List<Page>();
        pages.Add(new Page("Большой дядя решил инвестировать в меня как в успешного инвестора. Спасибо ему!", GetCharacterKey(CharacterKey.Player)));

        //Reoutes
        routes = new List<Route>();

        //Route 1
        statsToAdd = new Dictionary<string, int>();
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.money), 3);

        routes.Add(new Route("Взял деньги", "Принять сумму", 1, null, statsToAdd, null));

        //Event 0 Add
        Event ev0 = new Event(0, "Начало", pages, routes);
        story.events.Add(ev0);
        #endregion

        #region event 1
        //Pages
        pages = new List<Page>();
        pages.Add(new Page("Через неделю пройдет презентация Apple, обещают много нововведений. Аэрофлот байкотирует это событие", GetCharacterKey(CharacterKey.NewsMaker)));

        //Reoutes
        routes = new List<Route>();

        //Route 1
        statsToAdd = new Dictionary<string, int>();
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.money), 4);

        relationsToAdd = new Dictionary<string, int>();
        relationsToAdd.Add(GetCharacterKey(CharacterKey.Apple), 2);
        relationsToAdd.Add(GetCharacterKey(CharacterKey.Airflot), -2);

        routes.Add(new Route("Вложиля в Apple", "Купить акции Apple", 2, null, statsToAdd, relationsToAdd));

        //Route 2
        statsToAdd = new Dictionary<string, int>();
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.money), -2);

        relationsToAdd = new Dictionary<string, int>();
        relationsToAdd.Add(GetCharacterKey(CharacterKey.Apple), 2);

        routes.Add(new Route("Поддержал Аэрофлот в байкоте", "Игнорировать", 2, null, statsToAdd, relationsToAdd));

        //Event 0 Add
        Event ev1 = new Event(1, "Непонятки между у компаний", pages, routes);
        story.events.Add(ev1);
        #endregion

        #region event 2
        //Pages
        pages = new List<Page>();
        pages.Add(new Page("По инсайдерской информации компания не предоставит инноваций. Аэрофлот получает, заказ на поставку крупной партии гаджетов в Вашу страну", GetCharacterKey(CharacterKey.NewsMaker)));

        //Reoutes
        routes = new List<Route>();

        //Route 1
        statsToAdd = new Dictionary<string, int>();
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.money), 4);

        relationsToAdd = new Dictionary<string, int>();
        relationsToAdd.Add(GetCharacterKey(CharacterKey.Apple), 2);
        relationsToAdd.Add(GetCharacterKey(CharacterKey.Airflot), -2);

        routes.Add(new Route("Вложился в Аэрофлот", "Купить акции Аэрофлота", 3, null, statsToAdd, relationsToAdd));

        //Route 2
        statsToAdd = new Dictionary<string, int>();
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.money), -2);

        relationsToAdd = new Dictionary<string, int>();
        relationsToAdd.Add(GetCharacterKey(CharacterKey.Apple), -2);

        routes.Add(new Route("Поддержать Аэрофлот", "Игнорировать", 3, null, statsToAdd, relationsToAdd));

        //Event 2 Add
        Event ev2 = new Event(2, "Презентация Apple", pages, routes);
        story.events.Add(ev2);
        #endregion

        #region event 3
        //Pages
        pages = new List<Page>();
        pages.Add(new Page("Аэрофлот заключает контракт с ВТБ, в рамках которого ВТБ размещает рекламу на самолетах фирмы Аэрофлот", GetCharacterKey(CharacterKey.NewsMaker)));

        //Reoutes
        routes = new List<Route>();

        //Route 1
        statsToAdd = new Dictionary<string, int>();

        relationsToAdd = new Dictionary<string, int>();
        relationsToAdd.Add(GetCharacterKey(CharacterKey.Apple), -2);
        relationsToAdd.Add(GetCharacterKey(CharacterKey.VTB), 2);

        routes.Add(new Route("Вложиля в ВТБ", "Купить акции ВТБ", 4, null, statsToAdd, relationsToAdd));

        //Route 2
        statsToAdd = new Dictionary<string, int>();
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.money), 2);

        relationsToAdd = new Dictionary<string, int>();
        relationsToAdd.Add(GetCharacterKey(CharacterKey.VTB), -2);

        routes.Add(new Route("Поддержал Apple", "Игнорировать", 4, null, statsToAdd, relationsToAdd));

        //Event 2 Add
        Event ev3 = new Event(3, "Сделка ВТБ", pages, routes);
        story.events.Add(ev3);
        #endregion

        #region event 4
        //Pages
        pages = new List<Page>();
        pages.Add(new Page("Самолет с крупной партией товара компании Apple потерпел крушение", GetCharacterKey(CharacterKey.NewsMaker)));

        //Reoutes
        routes = new List<Route>();

        //Route 1
        statsToAdd = new Dictionary<string, int>();
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.money), -4);

        relationsToAdd = new Dictionary<string, int>();
        relationsToAdd.Add(GetCharacterKey(CharacterKey.Airflot), 2);

        routes.Add(new Route("Вложиля в Apple", "Купить акции Apple", 5, null, statsToAdd, relationsToAdd));

        //Route 2
        statsToAdd = new Dictionary<string, int>();
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.money), 2);

        relationsToAdd = new Dictionary<string, int>();
        relationsToAdd.Add(GetCharacterKey(CharacterKey.Airflot), -2);

        routes.Add(new Route("Поддержать Аэрофлот", "Игнорировать", 5, null, statsToAdd, relationsToAdd));

        //Event 4 Add
        Event ev4 = new Event(4, "Авария в Аэрофлоте", pages, routes);
        story.events.Add(ev4);
        #endregion

        #region event 5
        //Pages
        pages = new List<Page>();
        pages.Add(new Page("ВТБ заключает контракт с Apple, в рамках которого Apple добавит бесконтактную оплату для карт банка ВТБ", GetCharacterKey(CharacterKey.NewsMaker)));

        //Reoutes
        routes = new List<Route>();

        //Route 1
        statsToAdd = new Dictionary<string, int>();

        relationsToAdd = new Dictionary<string, int>();
        relationsToAdd.Add(GetCharacterKey(CharacterKey.VTB), 2);
        relationsToAdd.Add(GetCharacterKey(CharacterKey.Airflot), -2);
        relationsToAdd.Add(GetCharacterKey(CharacterKey.Gazprom), -2);
        relationsToAdd.Add(GetCharacterKey(CharacterKey.Apple), -2);

        routes.Add(new Route("Вложиля в ВТБ", "Купить акции ВТБ", 6, null, statsToAdd, relationsToAdd));

        //Route 2
        statsToAdd = new Dictionary<string, int>();
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.money), 2);

        relationsToAdd = new Dictionary<string, int>();

        routes.Add(new Route("Поддержать Apple", "Игнорировать", 6, null, statsToAdd, relationsToAdd));

        //Event 5 Add
        Event ev5 = new Event(5, "Сделка ВТБ", pages, routes);
        story.events.Add(ev5);
        #endregion

        #region event 6
        //Pages
        pages = new List<Page>();
        pages.Add(new Page("Облачное хранилище Apple подверглось хакерской атаке, произошла большая утечка данных миллионов пользователей", GetCharacterKey(CharacterKey.NewsMaker)));

        //Reoutes
        routes = new List<Route>();

        //Route 1
        statsToAdd = new Dictionary<string, int>();
        statsToAdd.Add(Enum.GetName(typeof(Stat), Stat.money), -4);

        relationsToAdd = new Dictionary<string, int>();
        relationsToAdd.Add(GetCharacterKey(CharacterKey.Apple), 2);
        relationsToAdd.Add(GetCharacterKey(CharacterKey.Airflot), -2);
        relationsToAdd.Add(GetCharacterKey(CharacterKey.Gazprom), -2);
        relationsToAdd.Add(GetCharacterKey(CharacterKey.VTB), -2);

        routes.Add(new Route("Вложиля в Apple", "Купить акции Apple", 7, null, statsToAdd, relationsToAdd));

        //Route 2
        statsToAdd = new Dictionary<string, int>();

        relationsToAdd = new Dictionary<string, int>();

        routes.Add(new Route("Поддержал ВТБ", "Игнорировать", 7, null, statsToAdd, relationsToAdd));

        //Event 6 Add
        Event ev6 = new Event(6, "Защита Apple", pages, routes);
        story.events.Add(ev6);
        #endregion
    }


    static public void SaveAll()
    {
        SaveCharacters();
        SaveStory();
    }

    public static void SaveCharacters()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + charactersFilename;
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, characters);
        stream.Close();
    }

    public static void SaveStory()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + storyFilename;
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, story);
        stream.Close();
    }

    public static void LoadCharacters()
    {
        string path = Application.persistentDataPath + charactersFilename;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            characters = formatter.Deserialize(stream) as Dictionary<string, Character>;
            stream.Close();
        }
        else
        {
            Debug.LogError("Can't find characters file by " + path);
            return;
        }
    }

    public static void LoadStory()
    {
        string path = Application.persistentDataPath + storyFilename;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            story = formatter.Deserialize(stream) as Story;
            stream.Close();
        }
        else
        {
            Debug.LogError("Can't find story file by " + path);
            return;
        }
    }
}