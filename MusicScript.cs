using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MusicScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject Button;
    [SerializeField]
    private Sprite Unmute, Mute;
    [SerializeField]
    private AudioSource Audio;

    private int Session = 0;
    public void excute()
    {
        if (Session == 0)
        {
            Session = 1;
            Button.GetComponent<Image>().sprite = Mute;
            Audio.Pause();
        }
        else
        {
            Session = 0;
            Button.GetComponent<Image>().sprite = Unmute;
            Audio.Play();
        }
    }
}
