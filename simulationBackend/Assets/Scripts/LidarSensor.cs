using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LidarSensor : MonoBehaviour
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
    
    
 
    void FixedUpdate()
    {
        
        this.transform.Rotate(0, Time.fixedDeltaTime * RotationSpeed*360, 0);


    }
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        float magnitude = 3;
        for (int i = 0; i < LasersCount; i++)
        {
            
            Vector3 LaserPosition = StartPosition.position + new Vector3(0, -LaserInterval*i/100, 0);
            float currentangle= (Degree + i * DegreeDelta)*Mathf.Deg2Rad;

            

            
            Vector3 PointingAt = LaserPosition + new Vector3(0, -Mathf.Sin(currentangle) * magnitude, Mathf.Cos(currentangle) * magnitude);
            

            Gizmos.DrawWireSphere(LaserPosition, 0.01f);
            Gizmos.DrawLine(LaserPosition,  PointingAt);
           
        }

    }

    
}
