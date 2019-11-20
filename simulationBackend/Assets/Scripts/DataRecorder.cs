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
                Debug.Log(lidar.CollectedData);
                Writer writer = new Writer();
                List<byte[]> CameraData = new List<byte[]>();
                CameraData.Add(new byte[4]);
                CameraData.Add(new byte[12]);
                CameraData.Add(new byte[8]);

                foreach (var item in lidar.CollectedData)
                {
                    writer.addRecord(CameraData, item, new List<Vector2>(), new float[] { 0,0});
                }
                writer.write();



                lidar.ClearCollectedData();





            }
           
        }

    }
}
