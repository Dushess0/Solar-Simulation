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
    public Camera cam;
   

    void Awake()
    {  
       // cam = GetComponent<Camera>();
    }
    public Texture2D Capture()
    {
   
        return Agent.ObservationToTexture(cam, captureWidth, captureHeight);

    }
   
 

 
  
 

}
