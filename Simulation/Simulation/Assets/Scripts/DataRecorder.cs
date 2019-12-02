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
    public List<float> currentUltraSound;
    [HideInInspector]
    public Vector3[] currentLidar;
   
    [HideInInspector]
    public int rows;

    
    // Start is called before the first frame update
    void Awake()
    {
        rows = 0;
        
        UltraSoundSensors = GetComponentsInChildren<DistanceSensor>();
        Debug.Log(UltraSoundSensors.Length);

        LidarData = new List<List<Vector3>>();
        UltraSoundData = new List<List<float>>();
        CamerasData = new List<Texture2D[]>();
    }

    // Update is called once per frame
    public void Record()
    {
        lidar.Rotate();
        LidarData.Add(lidar.CastLasers());



        Texture2D[] cameras_images = new Texture2D[Cameras.Length];
        for (int i = 0; i < Cameras.Length; i++)
        {
            cameras_images[i] = Cameras[i].Capture();
        }
        CamerasData.Add(cameras_images);

        List<float> distancies = new List<float>();
        for (int i = 0; i < UltraSoundSensors.Length; i++)
        {
            UltraSoundSensors[i].UpdateRay();
            distancies.Add(UltraSoundSensors[i].GetDistance());
        }
        UltraSoundData.Add(distancies);
        currentLidar = LidarData[rows].ToArray<Vector3>();
        currentUltraSound = distancies;

        rows++;
       

    
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
        this.SaveRecordings();
    }
}
