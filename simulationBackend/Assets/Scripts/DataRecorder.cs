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

    List<ByteString[]> CamerasData;
    
    List<float[]> UltraSoundData;

    public float[] currentUltraSound;
    public Vector3[] currentLidar;
   

    public int rows;

    
    // Start is called before the first frame update
    void Awake()
    {
        rows = 0;
        
        UltraSoundSensors = GetComponentsInChildren<DistanceSensor>();
        Debug.Log(UltraSoundSensors.Length);

        LidarData = new List<List<Vector3>>();
        UltraSoundData = new List<float[]>();
        CamerasData = new List<ByteString[]>();
    }

    // Update is called once per frame
    public void Record()
    {
        lidar.Step();
        LidarData.Add(lidar.CastLasers());

      

        ByteString[] cameras_images = new ByteString[Cameras.Length];
        for (int i = 0; i < Cameras.Length; i++)
        {
            cameras_images[i] = Cameras[i].Capture();
        }
        CamerasData.Add(cameras_images);

        float[] distancies = new float[UltraSoundSensors.Length];
        for (int i = 0; i < UltraSoundSensors.Length; i++)
        {
            UltraSoundSensors[i].UpdateRay();
            distancies[i] = UltraSoundSensors[i].GetDistance();
        }

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

    }
}
