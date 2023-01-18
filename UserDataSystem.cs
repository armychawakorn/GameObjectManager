using Newtonsoft.Json.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UserDataSystem
{
    public string GameVersion, Object_Tag_To_Save, datapath;
    public void SaveData()
    {
        UserData user = new UserData();
        GameObject[] obj = GameObject.FindGameObjectsWithTag(Object_Tag_To_Save);
        foreach (GameObject newobj in obj)
        {
            CharacterObject newu = new CharacterObject()
            {
                _name = newobj.name,
                _localPosition = newobj.GetComponent<RectTransform>().localPosition,
                _localScale = newobj.GetComponent<RectTransform>().localScale,
                _sizeDelta = newobj.GetComponent<RectTransform>().sizeDelta,
                _localRotation = newobj.GetComponent<RectTransform>().localRotation,
                _sourceImage = newobj.GetComponent<Image>().sprite
            };
            user._CharacterObject.Add(newu);
        }
        user.GameVersion = GameVersion;
        string stroutput = JsonUtility.ToJson(user, true);
        if (Application.isMobilePlatform)
        {
            File.WriteAllText(Path.Combine(Application.persistentDataPath, datapath), stroutput, Encoding.UTF8);
        }
        else
        {
            File.WriteAllText(Path.Combine(Application.dataPath, datapath), stroutput, Encoding.UTF8);
        }
        Debug.Log("Save");
    }
    public void LoadCharacter()
    {
        UserData user = read_data();
        foreach (GameObject findobj in GameObject.FindGameObjectsWithTag("Character_Element"))
        {
            foreach(CharacterObject udata in user._CharacterObject)
            {
                if (findobj.name == udata._name)
                {
                    findobj.GetComponent<RectTransform>().localPosition = udata._localPosition;
                    findobj.GetComponent<RectTransform>().localScale = udata._localScale;
                    findobj.GetComponent<RectTransform>().sizeDelta = udata._sizeDelta;
                    findobj.GetComponent<RectTransform>().localRotation = udata._localRotation;
                    findobj.GetComponent<Image>().sprite = udata._sourceImage;
                }
            }
        }
        Debug.Log("Loaded");
    }
    public UserData read_data()
    {
        UserData result;
        try
        {
            result = JsonUtility.FromJson<UserData>(File.ReadAllText(Path.Combine(Application.dataPath, datapath)));
        }
        catch
        {
            return null;
        }
        return result;
    }
}
[Serializable]
public class UserData
{
    public string GameVersion;
    public List<CharacterObject> _CharacterObject = new List<CharacterObject>();
}
[Serializable]
public class CharacterObject
{
    public string _name;
    public Vector3 _localPosition;
    public Vector3 _localScale;
    public Vector2 _sizeDelta;
    public Quaternion _localRotation;
    public Sprite _sourceImage;
}