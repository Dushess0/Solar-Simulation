using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceSensor : MonoBehaviour
{
    public float Magnitude = 4f;
    public Transform Origin;
    Ray ray;
    void Awake()
    {
        ray = new Ray(Origin.position, Vector3.forward);

    }
    public void UpdateRay()
    {
        ray.origin = Origin.position;
        ray.direction = Vector3.forward;
        ray.direction = Quaternion.AngleAxis(-90+this.transform.rotation.eulerAngles.y, Vector3.up) * ray.direction;
        
       

    }
    public float GetDistance()
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Magnitude)) //4 is layer mask, 4 is used for water
        {
           


            Debug.DrawLine(ray.origin, ray.origin + ray.direction * Magnitude, Color.red);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * Magnitude, Color.green);
        
        }
        return (hit.point - this.Origin.position).magnitude;

    }

}
