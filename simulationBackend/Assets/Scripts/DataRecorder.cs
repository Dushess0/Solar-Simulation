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
                Debug.Log("Recording ended");
                Recording = false;
                Writer writer = new Writer();
                var a = Enumerable.Repeat((byte)0x20, 5).ToArray();
                var b = new List<Vector3>();
                var d = new List<Vector2>();
                var c = new float[] { 5,5};
                
                writer.addRecord(a, a, a,b , d,c );
                writer.write();

               
                

            }
           
        }

    }
}
