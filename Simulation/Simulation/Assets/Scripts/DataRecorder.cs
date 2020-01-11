using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSVWriter;
using System.Linq;
using Google.Protobuf;
public class DataRecorder : MonoBehaviour
{

    public LidarSensor lidar;
    public CameraSensor[] Cameras;
    [HideInInspector]
    public DistanceSensor[] UltraSoundSensors;
    
    List<List<Vector3>> LidarData;

    List<Texture2D[]> CamerasData;
    
    List<List<float>> UltraSoundData;

   
   
    [HideInInspector]
    public int rows;
    public float WriteFrequency = 0.02f;
    private  float timer = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        rows = 0;
        timer = 0;
        UltraSoundSensors = GetComponentsInChildren<DistanceSensor>();
        

        LidarData = new List<List<Vector3>>();
        UltraSoundData = new List<List<float>>();
        CamerasData = new List<Texture2D[]>();
    }

    // Update is called once per frame

   
   
  
    public void Record()
    {

        //timer += Time.fixedDeltaTime;
        //if (!lidar.Debugging)
        //{
        //    Debug.Log("wtf");
        //    if (timer >= WriteFrequency)
        //    {
        //        this.LidarData.Add(currentLidar());
        //        this.UltraSoundData.Add(currentUltrasound());
        //        this.CamerasData.Add(currentImages());

        //        rows++;
        //        timer = 0;
        //    }               
        //}
       

    }
    public Texture2D[] currentImages()
    {
        Texture2D[] cameras_images = new Texture2D[Cameras.Length];
        for (int i = 0; i < Cameras.Length; i++)
        {
            cameras_images[i] = Cameras[i].Capture();
        }
        return cameras_images;
    }
    public List<float> currentUltrasound()
    {
        List<float> distancies = new List<float>();
        for (int i = 0; i < UltraSoundSensors.Length; i++)
        {
            UltraSoundSensors[i].UpdateRay();
            distancies.Add(UltraSoundSensors[i].GetDistance());
        }
        return distancies;
    }
    public List<Vector3> currentLidar()
    {
        lidar.Rotate();
        return lidar.CastLasers();
    }



    public void ClearRecordings()
    {
        LidarData.Clear();
        CamerasData.Clear();
        UltraSoundData.Clear();
        rows = 0;

    }
    
    void SaveRecordings()
    {
        Writer writer = new Writer();
        for (int i = 0; i < rows; i++)
        {
            writer.addRecord(CamerasData[i], LidarData[i], UltraSoundData[i]);
        }
        writer.write();
        
        
    } 
    void OnDestroy()
    {
        if (!lidar.Debugging)
        this.SaveRecordings();
    }
}
