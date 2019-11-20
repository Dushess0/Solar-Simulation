using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSVWriter;



public class LidarSensor: MonoBehaviour
{
    
    public int LasersCount;
    [Tooltip("Interval between each laser measured in cm")]
    public float LaserInterval;

    public float Degree;
    [Tooltip("0 means all lasers will have same degree as the first one, else every next laser will have degree = degree of first + n*delta")]
    public float DegreeDelta;
    
  
    [Tooltip("Measured in Hz")]
    public float RotationSpeed; //Hz
    [Tooltip("Starting position of lasers")]
    public Transform StartPosition;

    Ray[] LaserRays;
    float[] LaserAngles;
   

    


    [Tooltip("Enable rays and hits in editor but disable collecting data !!!! ")]
    public bool Debugging;
    public float  Magnitude;


    List<Vector3> actualdata;
    List<List<Vector3>> Data;

    public List<Vector3> ActualData { get=> actualdata; }
    public List<List<Vector3>> CollectedData { get => Data; }


    public bool Recording = false;

    void FixedUpdate()
    {
        
        this.transform.Rotate(0, Time.fixedDeltaTime * RotationSpeed*360, 0,Space.Self);
        UpdateRays();
      
        if (Recording)
            RecordCollisions();


    }
    public void ClearCollectedData()
    {
        Data.Clear();
    }

    void Awake()
    {
        Data = new List<List<Vector3>>();      
        LaserRays = new Ray[LasersCount];
        actualdata = new List<Vector3>();
        LaserAngles = new float[LasersCount];
        for (int i = 0; i < LasersCount; i++)
        {
            LaserAngles[i] = Degree + i * DegreeDelta;
            LaserRays[i]=new Ray();

        }


    }
    void UpdateRays()
    {

        for (int i = 0; i < LasersCount; i++)
        {

            LaserRays[i].origin = StartPosition.position + new Vector3(0, -LaserInterval * i / 100, 0);
            LaserRays[i].direction= Vector3.up;

            LaserRays[i].direction = Quaternion.AngleAxis(-LaserAngles[i] - 90, Vector3.right) * LaserRays[i].direction;
            LaserRays[i].direction = Quaternion.AngleAxis(this.transform.rotation.eulerAngles.y, Vector3.up) * LaserRays[i].direction;


        }
        
       
    }
    void RecordCollisions()
    {

        List<Vector3> row = new List<Vector3>();


        for (int i = 0; i < LasersCount; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(LaserRays[i], out hit, Magnitude)) //4 is layer mask, 4 is used for water
            {
                row.Add(LaserRays[i].direction * Magnitude);


                Debug.DrawLine(LaserRays[i].origin, LaserRays[i].origin + LaserRays[i].direction * Magnitude, Color.red);
            }
            else
            {
                Debug.DrawLine(LaserRays[i].origin, LaserRays[i].origin + LaserRays[i].direction * Magnitude, Color.green);
                row.Add(Vector3.zero);
            }

        }
        Data.Add(row);


    }
   

    public static string VectorsToString(List<Vector3> data)
    {
        string row="[";
        foreach (var item in data)
        {
            row += "[" + item.x + "," + item.y + "," + item.z + "],";
        }
        row += "]";
        return row;
    }
 

    void OnDrawGizmosSelected()
    {
        if (Debugging)
        {
            Awake();
            UpdateRays();
            
            Gizmos.color = Color.red;


            for (int i = 0; i < LasersCount; i++)
            {

                
                Gizmos.DrawWireSphere(LaserRays[i].origin, 0.01f);
                Gizmos.DrawLine(LaserRays[i].origin, LaserRays[i].origin + LaserRays[i].direction * Magnitude);


            }
        }
    }

    
}
