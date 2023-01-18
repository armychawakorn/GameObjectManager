using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript
{
    public GameObject Character, _Slider;
    public AudioSource Click_Sound;
    public void Zoom()
    {
        float size = _Slider.GetComponent<Slider>().value;
        Character.transform.localScale = new Vector3
        {
            x = size,
            y = size,
            z = size
        };
    }
}
