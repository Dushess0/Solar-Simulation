using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;


public class CameraSensor : MonoBehaviour
{
    public List<byte[]>[] Data;
    public int captureWidth = 720;
    public int captureHeight = 720;
    Texture2D screenCap;
    public  RenderTexture renderTexture;
    void Awake()
    {
        
        screenCap = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        

    }

   void FixedUpdate()
    {
        Record();
    }
    void Record()
    {
        screenCap.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        screenCap.Apply();

        
    }

    void SaveImages()
    {

    }
}
