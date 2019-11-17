using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
public class BoatAgent : Agent
{
    public GameObject boat;
    Rigidbody Rbody;
    Vector3 StartPos;
    Engine LeftEngine;
    Engine RightEngine;
    public bool Done;
    public float Reward;

    void Start()
    {
        StartPos = boat.transform.position;
        Rbody = boat.GetComponent<Rigidbody>();
        LeftEngine = boat.GetComponentsInChildren<Engine>()[0];
        RightEngine = boat.GetComponentsInChildren<Engine>()[1];
        Done = false;
        Reward = 0;

    }
    public override void CollectObservations()
    {
        AddVectorObs(gameObject.transform.rotation.y);
             

    }
    public override void AgentReset()
    {
        gameObject.transform.position = StartPos;
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        Rbody.velocity = Vector3.zero;

   }
    public override void AgentAction(float[] vectorAction, string textAction)
    {

        LeftEngine.Control = vectorAction[0];
        RightEngine.Control = vectorAction[1];
        LeftEngine.Step();
        RightEngine.Step();
        Debug.Log(vectorAction[0]);

        if (Done)
        {
            Reward = 0.1f; 
        }

        // if collide bouy then reward = -0.1f  
        //need to specify cost of time and cost of 

    }



}
