using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    // Start is called before the first frame update

    public float MaxPower = 10;
    public float Control=0;
    Rigidbody rBody;
    void Start()
    {
        rBody = this.GetComponent<Rigidbody>();
    }
    public void Step()
    {
        if (Mathf.Abs(Control) > 1) Control = 1;
        rBody.AddRelativeForce(new Vector3(0, 0, -Control * MaxPower));
    }
  
    
   
}
