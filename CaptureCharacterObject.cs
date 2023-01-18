using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureCharacterObject : MonoBehaviour
{
    public void TakeScreenShot()
    {
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string path = "awfawf" + timeStamp;
        NativeToolkit.SaveScreenshot(Application.dataPath, "gawg", "jpeg");
    }
}
