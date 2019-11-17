using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorder : MonoBehaviour
{
    public bool Recording;
    public string SavingPath="/data";

    public LidarSensor lidar;
    public Camera[] Cameras;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {   if (!Recording)
            {
                Recording = true;
                Debug.Log("Recording started");
            }
           if (Recording)
            {
                Recording = false;
                

            }
           
        }

    }
}
