﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    // Start is called before the first frame update

    public float MaxPower = 10;
    public float Control=0;
    Rigidbody rigidbody;
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }
    public void Step()
    {
        rigidbody.AddRelativeForce(new Vector3(0, 0, -Control * MaxPower));
    }
  
    
   
}
