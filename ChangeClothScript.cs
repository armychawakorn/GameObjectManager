using System;
using System.Collections.Generic;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class ChangeClothScript
{
    public Sprite[] Sprites;
    public AudioSource ClickSound;
    public ESelectElement SelectElementToCreate;
    public GameObject ReferenceButton, ParentButton;
    float newHeightParent;
    List<GameObject> ListNewPrefab = new();
    public void SetActivate(bool value)
    {
        if (value){ ShowThis(); } else{ DestroyThis(); }
    }
    void ShowThis()
    {
        if (ListNewPrefab.Count != Sprites.Length)
        {
            for (int i = 0; i < Sprites.Length; i++)
            {
                GameObject newPrefab = GameObject.Instantiate(ReferenceButton);
                ListNewPrefab.Add(newPrefab);
                try
                {
                    ButtonElement refbtn = new()
                    {
                        ButtonPrefab = newPrefab,
                        ParentButton = ParentButton,
                        CharacterElement = GameObject.Find("Character_"+SelectElementToCreate),
                        Sprite = Sprites[i],
                        Select = i + 1,
                        SelectElementToCreate = SelectElementToCreate,
                        ClickSound = ClickSound
                    };
                    refbtn.GenerateElement<GameObject>();
                    newHeightParent += newPrefab.GetComponent<RectTransform>().rect.height;
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.ToString());
                }
            }
            ParentButton.GetComponent<RectTransform>().sizeDelta = new Vector2 { y = newHeightParent  - ((88f / 100f) * newHeightParent), x = 91.844f };
            ParentButton.GetComponent<RectTransform>().localPosition = new Vector2 { y = -5000f, x = 0 };
        }
    }
    void DestroyThis()
    {
        foreach(GameObject prefab in ListNewPrefab)
        {
            GameObject.Destroy(prefab);
        }
        ListNewPrefab.Clear();
        newHeightParent = 0;
    }
}
public class ButtonElement
{
    public GameObject CharacterElement, ButtonPrefab, ParentButton;
    public ESelectElement SelectElementToCreate;
    public Sprite Sprite;
    public AudioSource ClickSound;
    public int Select;
    public void GenerateElement<T>()
    {
        Transform Image_Of_Button = this.ButtonPrefab.transform.Find("Image");
        //this.ButtonPrefab.transform.Find("Text").GetComponent<Text>().text = "แบบที่ " + Select.ToString();
        this.ButtonPrefab.name = SelectElementToCreate.ToString() +" " + Select.ToString();
        this.ButtonPrefab.transform.SetParent(ParentButton.transform, false);
        Image_Of_Button.GetComponent<RectTransform>().sizeDelta = Sprite.rect.size;
        Image_Of_Button.GetComponent<Image>().sprite = Sprite;
        this.ButtonPrefab.GetComponent<Button>().onClick.AddListener(ButtonExecuter);
        switch (SelectElementToCreate)
        {
            case ESelectElement.Hair:
                Image_Of_Button.transform.localScale = new Vector3 { x = 0.015f, y = 0.025f, z = 0.015f };
                break;
            case ESelectElement.Eyes:
                Image_Of_Button.transform.localScale = new Vector3 { x = 0.035f, y = 0.05f, z = 0.05f };
                break;
        }
    }
    private void ButtonExecuter()
    {
        Image Layer = CharacterElement.GetComponent<Image>();
        Layer.sprite = Sprite;
        CharacterElement.GetComponent<RectTransform>().sizeDelta = Sprite.rect.size;

        switch (SelectElementToCreate)
        {
            case ESelectElement.Hair:
                //default position
                CharacterElement.GetComponent<RectTransform>().localPosition = new Vector3 { x = 4.5f, y = 845f, z = 0f };

                //assign position
                if (Sprite.name == "8" || Sprite.name == "9" || Sprite.name == "10")
                {
                    CharacterElement.GetComponent<RectTransform>().localPosition = new Vector3 { x = 4.5f, y = 706f, z = 0f };
                }
                if (Sprite.name == "7")
                {
                    CharacterElement.GetComponent<RectTransform>().localPosition = new Vector3 { x = 3f, y = 718f, z = 0f };
                }
                break;
            case ESelectElement.Eyes:
                CharacterElement.GetComponent<RectTransform>().sizeDelta = new Vector2 { x = 648f, y = 168f };
                break;

        }
        ClickSound.Play();
    }
    
}
public enum ESelectElement
{
    None,
    Hair,
    Head,
    Eyes,
    EyeBrow,
    Mouth,
    Body
}