using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSVWriter;
using System.Linq;
public class DataRecorder : MonoBehaviour
{
    public bool Recording;
    public string SavingPath="/data";

    public LidarSensor lidar;
    Camera[] Cameras;
  
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Recording = !Recording;
            if (Recording)
            {
                lidar.Recording = true;
                Debug.Log("Recording started");
            }
           if (!Recording)
            {
                lidar.Recording = false;
                
                Debug.Log("Recording ended");
                Recording = false;
                Debug.Log(lidar.Data);
                Writer writer = new Writer();
                foreach (var item in lidar.Data)
                {
                    writer.addRecord(new byte[4], new byte[8], new byte[12], item, new List<Vector2>(), new float[] { 0,0});
                }
                writer.write();



                lidar.ClearData();





            }
           
        }

    }
}
