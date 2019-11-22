using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System.IO;
using Google.Protobuf.Collections;
using Google.Protobuf;

public class CameraSensor : MonoBehaviour
{
    
    public int captureWidth = 720;
    public int captureHeight = 720;  
    Camera cam;
   

    void Awake()
    {  
        cam = GetComponent<Camera>();
    }
    public ByteString Capture()
    {
       Texture2D current_image= Agent.ObservationToTexture(cam, captureWidth, captureHeight);
       return ByteString.CopyFrom(current_image.EncodeToPNG());

    }
   
 

 
  
 

}
