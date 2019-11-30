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


    float StepConstant=0.05f;
  

   public  void Rotate() // rotate base and update position of lasers
    {
        
        this.transform.Rotate(0, StepConstant * RotationSpeed*360, 0,Space.Self);
        UpdateRays();

       
    }
    

    void Awake()
    {
       
        LaserRays = new Ray[LasersCount];
        
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

    //get data about collisions from lasers
    public List<Vector3> CastLasers() 
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
